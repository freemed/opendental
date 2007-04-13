/* File: OpenGLWinFormsControl.cs
 * Description: A Windows Forms control that supports OpenGL rendering using the Tao library.
 * Author: Michael Hansen (hansen.mike@gmail.com)
 * Date Created: 3/2/2006
 * Date Modified: 3/30/2006
 * 
 * Overview
 * ========
 * This control provides a simple Windows Forms control to do OpenGL render using Tao. The control
 * behaves properly in design mode (while being designed and while embedded in another control that's
 * being designed).
 * 
 * Example Usage
 * =============
 * To use this control, add it to your Form or Control using the designer or programmatically.
 * In your initialization routine, you MUST set TaoRenderEnabled to true for the control to use
 * OpenGL rendering (this is initially set to false to allow for designing).
 * 
 * For the most basic usage, add event handlers to the TaoSetupContext and TaoRenderScene events.
 * In TaoSetupContext, you will do your normal OpenGL setup routine.
 * In TaoRenderScene, you will do the actual drawing (by default, glFinish will be called for you after rendering).
 * During your initialization routine, call TaoInitializeContexts; this will create the device
 * and rendering contexts as well as call your TaoSetupContext event handler.
 * During each frame, call TaoDraw to redraw the scene. This will call Invalidate on the control and
 * call your TaoRenderScene event handler.
 * 
 * The step-by-step usage of the control would then be this:
 * 1. Add event handlers to TaoSetupContext (OpenGL initialization) and TaoRenderScene (rendering)
 * 2. Call TaoInitializeContexts
 * 3. Set TaoRenderEnabled to true
 * 4. Call TaoDraw during each frame
 * 
 * Advanced Usage
 * ==============
 * For more advanced usage, you may set the number of bits for the accumulator (TaoAccumBits),
 * color depth (TaoColorBits), depth buffer (TaoDepthBits), and stencil buffer (TaoStencilBits)
 * before calling TaoInitializeContexts.
 * 
 * Add an event handler to TaoControlSizeChanged to be notified whenever the control resizes.
 * The event arguments will give you the control's new width and height.  Adding a handler to this
 * event will disable the default resizing behavior of the control (reset the viewport and redraw).
 * 
 * Add an event handler to TaoOpenGLError to receive notifications of any errors that occur during
 * rendering. The event arguments will give you the error code and a brief description of the
 * error that occurred.
 */

#region Imported Namespaces

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;


using Tao.OpenGl;
using Tao.Platform.Windows;

#endregion

namespace CodeBase {
	/// <summary>
	/// A Windows Forms control that supports OpenGL rendering using the Tao library.
	/// </summary>
	public class OpenGLWinFormsControl:Control {
		#region Protected Fields

		protected IntPtr deviceContext = IntPtr.Zero,renderContext = IntPtr.Zero;
		protected bool renderEnabled = false;
		protected bool autoMakeCurrent = true,autoSwapBuffers = false,autoFinish = false;
		protected int lastErrorCode = Gl.GL_NO_ERROR;

		#endregion

		#region Events

		/// <summary>
		/// A user-defined event that renders the scene (called during each redraw).
		/// </summary>
		public event EventHandler TaoRenderScene;

		/// <summary>
		/// A user-defined event that sets up the OpenGL context (called once during TaoInitializeContexts).
		/// </summary>
		public event EventHandler TaoSetupContext;

		/// <summary>
		/// A user-defined event that's called when the control resizes
		/// (by default, the control resets the viewport and redraws itself).
		/// </summary>
		public event EventHandler<SizeChangedEventArgs> TaoControlSizeChanged;

		/// <summary>
		/// Fired whenever an error occurs during rendering.
		/// </summary>
		public event EventHandler<OpenGLErrorEventArgs> TaoOpenGLError;

		#endregion

		#region Properties

		/// <summary>
		/// Enables / disables rendering. IMPORTANT: This property is initially set to false to allow for smooth designing.
		/// You MUST set this to true before any rendering will take place.
		/// </summary>
		public bool TaoRenderEnabled {
			get {
				return (renderEnabled);
			}
			set {
				renderEnabled = value;
			}
		}

		/// <summary>
		/// True if both the device and rendering contexts have been created
		/// </summary>
		protected bool ContextsReady {
			get {
				return ((deviceContext != IntPtr.Zero) && (renderContext != IntPtr.Zero));
			}
		}

		#endregion

		public OpenGLWinFormsControl() {
			//Setup the control's styles
			SetStyle(	ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint,true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer,false);//Disable C# double buffering.
			this.DoubleBuffered = false;												//so that it does not interfere with OpenGL.

			//Set default size
			this.Size = new Size(100,100);
		}

		#region Protected Methods

		#region int _DescribePixelFormat(System.IntPtr hdc,int iPixelFormat,uint nBytes,ref Gdi.PIXELFORMATDESCRIPTOR ppfd)
		///<summary>Creates an unmanaged reference to DescribePixelFormat(), which is used to choose an appropriate device pixel format for the current OS and video card.</summary>
		[DllImport("gdi32.dll",EntryPoint="DescribePixelFormat",SetLastError=true),SuppressUnmanagedCodeSecurity]
		public static extern int _DescribePixelFormat(System.IntPtr hdc,int iPixelFormat,uint nBytes,ref Gdi.PIXELFORMATDESCRIPTOR ppfd);
		#endregion int _DescribePixelFormat(System.IntPtr hdc,int iPixelFormat,uint nBytes,ref Gdi.PIXELFORMATDESCRIPTOR ppfd)

		///<summary>This function is used to choose an acceptable pixel format for the inheriting OpenGL control.
		///Returns the desireability of the given pixel format pfd. A return value less than zero signifies that the
		///given format is entirely unacceptable. A return value of greater than or equal to one-million signifies that
		///the given format should be used regaurdless of other (possibly better) pixel formats available. After a
		///series of calls to this function with available system pixel formats, the ChoosePixelFormatEx() function 
		///will choose the pixel format which causes this function to return the largest value. However, if all 
		///returned values are negative, then no formats are acceptable, but the system will try to continue with 
		///the first format available in the system (after logging an error).</summary>
		protected virtual int PixelFormatDesire(Gdi.PIXELFORMATDESCRIPTOR pfd){
			long bpp=pfd.cColorBits;
			long depth=pfd.cDepthBits;
			//Uses a palette (older hardware)?
			bool pal=(pfd.iPixelType==Gdi.PFD_TYPE_COLORINDEX);
			//Generic graphics hardware acceleration.
			bool mcd=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)!=0 && (pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)!=0;
			//Full graphics hardware acceleration.
			bool icd=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)==0 && (pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)==0;
			//Software graphics only.
			bool soft=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)!=0 && (pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)==0;
			bool opengl=(pfd.dwFlags & Gdi.PFD_SUPPORT_OPENGL)!=0;
			bool window=(pfd.dwFlags & Gdi.PFD_DRAW_TO_WINDOW)!=0;
			bool bitmap=(pfd.dwFlags & Gdi.PFD_DRAW_TO_BITMAP)!=0;
			bool dbuff=(pfd.dwFlags & Gdi.PFD_DOUBLEBUFFER)!=0;
			Logger.openlog.Log("Evaluating pixel format:",Logger.Severity.INFO);
			Logger.openlog.Log("Color depth bits="+bpp,Logger.Severity.INFO);
			Logger.openlog.Log("Z-depth bits="+depth,Logger.Severity.INFO);
			Logger.openlog.Log("Using palette="+(pal?"Yes":"No"),Logger.Severity.INFO);
			Logger.openlog.Log("Generic graphics hardware acceleration="+(mcd?"Yes":"No"),Logger.Severity.INFO);
			Logger.openlog.Log("Software only graphics="+(soft?"Yes":"No"),Logger.Severity.INFO);
			Logger.openlog.Log("Full graphics hardware acceleration="+(icd?"Yes":"No"),Logger.Severity.INFO);
			Logger.openlog.Log("Supports OpenGL="+(opengl?"Yes":"No"),Logger.Severity.INFO);
			Logger.openlog.Log("Windowed rendering="+(window?"Yes":"No"),Logger.Severity.INFO);
			Logger.openlog.Log("Bitmap rendering="+(bitmap?"Yes":"No"),Logger.Severity.INFO);
			Logger.openlog.Log("Double buffering="+(dbuff?"Yes":"No"),Logger.Severity.INFO);
			//Check for completely unacceptable formats first.
			if(	bpp<8 ||			//Need at least 256 colors.
					depth<8 ||		//Need at least 256 depth values.
					!opengl ||		//MUST support OpenGL.
					!window || 		//MUST support windowed rendering (as opposed to bitmapped rendering, i.e to an image).
					!(mcd || icd || soft)){	//Must support at least software or hardware rendering.
				Logger.openlog.Log("The given format is unacceptable.",Logger.Severity.INFO);
				return -1;
			}
			//Now add values up to encourage certain formats over others.
			int desire=0;
			bool acceptable=true;
			//Prefer software rendering over both partial and full hardware acceleration.
			if(soft){
				desire+=1000;
			}else{
				acceptable=false;
			}
			//We like at least 16-bit color depths.
			if(bpp>=16){
				desire+=1000;
			}else{
				acceptable=false;
			}
			//We like at least 16-bit depth buffering.
			if(depth>=16){
				desire+=500;
			}else{
				acceptable=false;
			}
			//We do not like palettes.
			if(!pal){
				desire+=10;
			}else{
				acceptable=false;
			}
			//We do not want image-based rendering, but instead prefer window-only based rendering.
			if(!bitmap){
				desire+=100;
			}
			//We prefer single buffering, both to save memory and to avoid hardware pbuffer issues.
			if(!dbuff){
				desire+=1000;
			}else{
				acceptable=false;
			}
			//Is this format sufficient for the purposes of this particular OpenGL control?
			if(acceptable){
				desire=2000000;//Chose this exact format not matter what.
				Logger.openlog.Log("The format is acceptable.",Logger.Severity.INFO);
			}else{
				Logger.openlog.Log("Format desireability factor: "+desire,Logger.Severity.INFO);
			}
			return desire;
		}

		///<summary> 
		///hdc: A pointer to the Windows device context to which the setting will apply.
		///bpp: Desired color depth in bits-per-pixel. -1 means "don't care", positive otherwise (8,16,24,32).
		///depth: Desired z-buffer or depth-sorting bits. -1 means "don't care", positive otherwise (8,16,24,32).
		///dbl: Indicates the desire for double-buffering. -1 means "don't care", 0 for none, otherwise 1.
		///acc: Indicates the desire for hardware-acceleration. -1 means "don't care", 0 for none, otherwise 1.
		///formatRef: Specifies the maximum reference number to be returned (to help with loading times). The final format 
		///selection is returned in this reference variable, which corresponds to the returned Gdi.PIXELFORMATDESCRIPTOR. Specify a
		///non-positive number for "don't care".
		///</summary>
		Gdi.PIXELFORMATDESCRIPTOR ChoosePixelFormatEx(System.IntPtr hdc,ref int formatRef){
			Logger.openlog.Log("Choosing OpenGL pixel format...",Logger.Severity.DEBUG);
			Gdi.PIXELFORMATDESCRIPTOR pfd=new Gdi.PIXELFORMATDESCRIPTOR();
			pfd.nSize=(short)Marshal.SizeOf(pfd);
			pfd.nVersion=1;
			int num=_DescribePixelFormat(hdc,1,(uint)pfd.nSize,ref pfd);
			Logger.openlog.Log("There are "+num+" available formats.",Logger.Severity.DEBUG);
			num=(formatRef<1?num:Math.Min(num,formatRef));
			Logger.openlog.Log("Maximum number of formats to choose from is "+num,Logger.Severity.INFO);
			long maxqual=-1;
			int maxindex=1;
			bool goodEnough=false;
			bool oneValidFormat=false;
			if(num>0){
				for(int i=1;i<=num && !goodEnough;i++){
					pfd=new Gdi.PIXELFORMATDESCRIPTOR();
					pfd.nSize=(short)Marshal.SizeOf(pfd);
					pfd.nVersion=1;
					_DescribePixelFormat(hdc,i,(uint)pfd.nSize,ref pfd);
					Logger.openlog.Log("Evaluating format with index# "+i,Logger.Severity.INFO);
					int q=PixelFormatDesire(pfd);
					goodEnough=(q>=1000000);
					if(q>maxqual || goodEnough){//Take the smallest index (the smallest options) that meet the requirements.
						maxqual=q; 
						maxindex=i;
						autoSwapBuffers=((pfd.dwFlags & Gdi.PFD_DOUBLEBUFFER)!=0);
						oneValidFormat=true;
					}
				}
			}else{
				Logger.openlog.Log("FAILED TO CHOOSE A PIXEL FORMAT BECAUSE THERE ARE NONE AVAILABLE!",Logger.Severity.FATAL_ERROR);
			}
			if(!oneValidFormat){
				Logger.openlog.Log("UNABLE TO LOCATE AN ACCEPTABLE PIXEL FORMAT.",Logger.Severity.FATAL_ERROR);
			}
			maxindex=(maxindex>0?maxindex:1);//Ensure that maxindex is positive as required, even in algorithmic failure.
			pfd=new Gdi.PIXELFORMATDESCRIPTOR();
			pfd.nSize=(short)Marshal.SizeOf(pfd);
			pfd.nVersion=1;
			_DescribePixelFormat(hdc,maxindex,(uint)pfd.nSize,ref pfd);
			formatRef=maxindex;
			Logger.openlog.Log("Chosen format number="+maxindex,Logger.Severity.INFO);
			return pfd;
		}
		/// <summary>
		/// Creates the device and rendering contexts using the supplied settings
		/// in accumBits, colorBits, depthBits, and stencilBits.
		/// </summary>
		protected virtual void CreateContexts() {
			//Make sure the handle for this control has been created
			if(this.Handle == IntPtr.Zero) {
				throw new Exception("CreateContexts: The control's window handle has not been created.");
			}

			//Create device context
			deviceContext = User.GetDC(this.Handle);

			if(deviceContext == IntPtr.Zero) {
				throw new Exception("CreateContexts: Unable to create an OpenGL device context");
			}
			int selectedFormat=100;//Don't try more than 100 formats (too time consuming).
			Gdi.PIXELFORMATDESCRIPTOR pixelFormat=ChoosePixelFormatEx(deviceContext,ref selectedFormat);
			//Make sure the requested pixel format is available
			if(selectedFormat == 0) {
				throw new Exception("CreateContexts: Unable to find a suitable pixel format");
			}

			if(!Gdi.SetPixelFormat(deviceContext,selectedFormat,ref pixelFormat)) {
				throw new Exception(string.Format("CreateContexts: Unable to set the requested pixel format ({0})",selectedFormat));
			}

			//Create rendering context
			renderContext = Wgl.wglCreateContext(deviceContext);

			if(renderContext == IntPtr.Zero) {
				throw new Exception("CreateContexts: Unable to create an OpenGL rendering context");
			}

			//Make this the current context
			MakeCurrentContext();
		}

		/// <summary>
		/// Deletes both the device and rendering contexts if they've been created.
		/// </summary>
		protected virtual void DisposeContext() {
			//Dispose of rendering context
			if(renderContext != IntPtr.Zero) {
				Wgl.wglMakeCurrent(deviceContext,renderContext);
				Wgl.wglDeleteContext(renderContext);
				renderContext = IntPtr.Zero;
			}

			//Dispose of device context
			if(deviceContext != IntPtr.Zero) {
				User.ReleaseDC(this.Handle,deviceContext);
				deviceContext = IntPtr.Zero;
			}
		}

		protected override void Dispose(bool disposing) {
			if(disposing) {
				DisposeContext();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Sets this control's OpenGL context as the current context.
		/// </summary>
		public void MakeCurrentContext() {
			if(!Wgl.wglMakeCurrent(deviceContext,renderContext)) {
				throw new Exception("MakeCurrentContext: Unable to active this control's OpenGL rendering context");
			}
		}

		/// <summary>
		/// Draws the design-mode background for the control.
		/// By default, a message is displayed to inform the user that the control is in design mode
		/// and how they can switch to rendering mode.
		/// </summary>
		/// <param name="controlGraphics"></param>
		protected void DrawDesignBackground(Graphics controlGraphics) {
			controlGraphics.Clear(Color.White);

			//Draw heading string
			controlGraphics.DrawString("Tao OpenGL WinForms Control",
					new Font("Arial",14.0f,FontStyle.Bold),Brushes.Black,10.0f,10.0f);

			//Draw information string
			Font infoFont = new Font("Arial",12.0f);

			controlGraphics.DrawString("This control is currently in design mode.",
					infoFont,Brushes.Black,10.0f,35.0f);

			controlGraphics.DrawString("You must set TaoRenderEnabled to true for OpenGL rendering.",
					infoFont,Brushes.Black,10.0f,55.0f);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates device and rendering contexts then fires the user-defined SetupContext event
		/// (if the contexts have not already been created). Call this in your initialization routine.
		/// </summary>
		public void TaoInitializeContexts() {
			if(!ContextsReady) {
				try{
					CreateContexts();
				}catch(Exception e){
					Logger.openlog.Log(this,"TaoInitializeContexts",e.ToString(),Logger.Severity.ERROR);
				}
				//Fire the user-defined TaoSetupContext event
				if(TaoSetupContext != null) {
					TaoSetupContext(this,null);
				}
			}
		}

		/// <summary>
		/// Call this method to redraw the control every frame (internally, this calls Invalidate)
		/// </summary>
		public void TaoDraw() {
			Invalidate();
		}

		#endregion

		#region Control Methods

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//Do not paint background
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);

			//Only draw with OpenGL if rendering is enabled (disabled by default for designing)
			if(renderEnabled) {
				//Initialize the device and rendering contexts if the user hasn't already
				TaoInitializeContexts();

				//Make this the current context
				if(autoMakeCurrent) {
					//Only switch contexts if this is already not the current context
					if(renderContext != Wgl.wglGetCurrentContext()) {
						MakeCurrentContext();
					}
				}

				//Fire the user-defined TaoRenderScene event
				if(TaoRenderScene != null) {
					TaoRenderScene(this,null);
				}

				//Automatically finish the scene
				if(autoFinish) {
					Gl.glFinish();
				}

				//Automatically check for errors
				lastErrorCode = Gl.glGetError();

				if(lastErrorCode != Gl.GL_NO_ERROR) {
					//Fire the error handling event
					if(TaoOpenGLError != null) {
						TaoOpenGLError(this,new OpenGLErrorEventArgs(lastErrorCode));
					}
				}

				//Swap the OpenGL buffer to the display
				if(autoSwapBuffers) {
					Gdi.SwapBuffersFast(deviceContext);
				}
			}
			else {
				//Draw the background for this control when it's in design
				//mode (TaoRenderEnabled = false)
				DrawDesignBackground(e.Graphics);
			}
		}

		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);

			if(ContextsReady && renderEnabled) {
				//Fire the user-defined TaoControlSizeChanged event
				if(TaoControlSizeChanged != null) {
					TaoControlSizeChanged(this,new SizeChangedEventArgs(this.Size));
				}
				else {
					//By default, resize the viewport and request a re-draw
					Gl.glViewport(0,0,this.Width,this.Height);
					Invalidate();
				}
			}
		}

		#endregion
	}

	#region EventArgs Classes

	public class SizeChangedEventArgs:EventArgs {
		private Size newSize = new Size();

		/// <summary>
		/// The new size of the control that has been resized.
		/// </summary>
		public Size NewSize {
			get {
				return (newSize);
			}
		}

		public SizeChangedEventArgs(Size newSize) {
			this.newSize = newSize;
		}
	}

	public class OpenGLErrorEventArgs:EventArgs {
		private int errorCode = Gl.GL_NO_ERROR;
		private string description = "";

		/// <summary>
		/// A brief description of the error.
		/// </summary>
		public string Description {
			get {
				return (description);
			}
		}

		/// <summary>
		/// The OpenGL error code.
		/// </summary>
		public int ErrorCode {
			get {
				return (errorCode);
			}
		}

		public OpenGLErrorEventArgs(int errorCode) {
			this.errorCode = errorCode;

			switch(errorCode) {
				case Gl.GL_INVALID_ENUM:
					description = "GL_INVALID_ENUM - An unacceptable value has been specified for an enumerated argument.  The offending function has been ignored.";
					break;

				case Gl.GL_INVALID_VALUE:
					description = "GL_INVALID_VALUE - A numeric argument is out of range.  The offending function has been ignored.";
					break;

				case Gl.GL_INVALID_OPERATION:
					description = "GL_INVALID_OPERATION - The specified operation is not allowed in the current state.  The offending function has been ignored.";
					break;

				case Gl.GL_STACK_OVERFLOW:
					description = "GL_STACK_OVERFLOW - This function would cause a stack overflow.  The offending function has been ignored.";
					break;

				case Gl.GL_STACK_UNDERFLOW:
					description = "GL_STACK_UNDERFLOW - This function would cause a stack underflow.  The offending function has been ignored.";
					break;

				case Gl.GL_OUT_OF_MEMORY:
					description = "GL_OUT_OF_MEMORY - There is not enough memory left to execute the function.  The state of OpenGL has been left undefined.";
					break;

				default:
					description = "Unknown OpenGL Error.";
					break;
			}
		}
	}

	#endregion
}
