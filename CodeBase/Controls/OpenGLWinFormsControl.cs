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
using System.Drawing.Imaging;
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

		protected bool autoMakeCurrent = true,autoSwapBuffers = false,usehardware=true;
		public bool autoFinish=false;

		protected byte accumBits = 0,colorBits = 32,depthBits = 16,stencilBits = 0;
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
		Gdi.PIXELFORMATDESCRIPTOR ChoosePixelFormatEx(System.IntPtr hdc,int p_bpp,int p_depth,int p_dbl,int p_acc,ref int formatRef){
			Logger.openlog.Log(this,"ChoosePixelFormatEx","Beginning",false,Logger.Severity.DEBUG);
			Gdi.PIXELFORMATDESCRIPTOR pfd = new Gdi.PIXELFORMATDESCRIPTOR();
			pfd.nSize = (short)Marshal.SizeOf(pfd);
			pfd.nVersion = 1;
			int num=_DescribePixelFormat(hdc,1,(uint)pfd.nSize,ref pfd);
			Logger.openlog.Log(this,"ChoosePixelFormatEx","There are "+num+" available formats.",false,Logger.Severity.DEBUG);
			num=(formatRef<1?num:Math.Min(num,formatRef));
			Logger.openlog.Log(this,"ChoosePixelFormatEx","Maximum number of formats to choose from is "+num,false,Logger.Severity.INFO);
			long maxqual=-0xEFFFFFFF;
			int maxindex=0;
			bool goodEnough=false;
			if(num>0){
				for(int i=1;i<=num && !goodEnough;i++){
					goodEnough=true;//This format is assumed to be good enough until proven otherwise.
					pfd=new Gdi.PIXELFORMATDESCRIPTOR();
					pfd.nSize=(short)Marshal.SizeOf(pfd);
					pfd.nVersion=1;
					_DescribePixelFormat(hdc,i,(uint)pfd.nSize,ref pfd);
					long bpp=pfd.cColorBits;
					long depth=pfd.cDepthBits;
					bool pal=(pfd.iPixelType==Gdi.PFD_TYPE_COLORINDEX);
					bool icd=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)==0 && 
						(pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)==0;//full hardware accel.
					bool mcd=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)!=0 && 
						(pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)!=0;//general/partial hardware accel.
					bool soft=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)!=0 && 
						(pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)==0;//software only
					bool opengl=(pfd.dwFlags & Gdi.PFD_SUPPORT_OPENGL)!=0;
					bool window=(pfd.dwFlags & Gdi.PFD_DRAW_TO_WINDOW)!=0;
					bool bitmap=(pfd.dwFlags & Gdi.PFD_DRAW_TO_BITMAP)!=0;
					bool dbuff=(pfd.dwFlags & Gdi.PFD_DOUBLEBUFFER)!=0;
					long q=0;
					//Recognize formats which do not meet minimum requirements first and foremost.
					if(!opengl || !window || bpp<8 || depth<8){
						q-=100000000;
						goodEnough=false;
					}
					//Encourage formats where the buffering method is equivalent to the requested buffering method.
					if((p_dbl==0 && !dbuff) || (p_dbl==1 && dbuff)){
						q+=0x2000;
					}else{
						goodEnough=false;
					}
					//We always prefer formats which do not use palettes. Palette technology is depricated
					//with today's computer graphics hardware.
					if(!pal){
						q+=0x0080;
					}else{
						goodEnough=false;
					}
					//Check that color depth meets requested depth or better. Penalty for color-depths which are less than the requested.
					if(bpp>=p_bpp){
						q+=0x0800;
					}else{
						q-=0x0800*(p_bpp-bpp);
						goodEnough=false;
					}
					//Check that z-depth meets requested z-depth or better. Penalty for z-depths which are less than the requested.
					if(depth>=p_depth){
						q+=0x0400;
					}else{
						q-=0x0400*(p_depth-depth);
						goodEnough=false;
					}
					//We are pretty much neutral with bitmapped formats, as long as the format supports windowed rendering.
					//Formats which are bitmapped only are not valid for our uses and lead to blank OpenGL displays. For this
					//reason, there is a slight penalty for bitmapped formats (which really only will make a difference if
					//this format is windowed).
					if(bitmap){
						q-=0x0001;
					}
					//Check that the given pixel format meets the requested hardware acceleration mode.
					if(p_acc==0){//software graphics requested
						if(soft){//This format supports software graphics.
							q+=1000;
						}else{
							q-=1000;
							goodEnough=false;
						}
					}else{//hardware graphics acceleration requested
						if(mcd || icd) {//This format supports some level of hardware rendering.
							if(mcd){
								q+=1000;//encourage partial hardware accleration less than full hardware acceleration.
							}
							if(icd){
								q+=1200;//encourage full hardware acceleration more than partial hardware accleration.
							}
						}else{
							q-=1000;
							goodEnough=false;
						}
					}
					if(q>maxqual || goodEnough){//Take the smallest index (the smallest options) that meet the requirements.
						maxqual=q; 
						maxindex=i;
						autoSwapBuffers=dbuff;
						usehardware=(icd || mcd);
						colorBits=(byte)bpp;
						depthBits=(byte)depth;
					}
					Logger.openlog.Log(this,"ChoosePixelFormatEx",(i==maxindex?"*":"")
						+"Evaluated format with index="+i+"  qval="+q+":",Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","color depth="+bpp,Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","z-depth="+depth,Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","palette="+(pal?"Yes":"No"),Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","mcd="+(mcd?"Yes":"No"),Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","software graphics="+(soft?"Yes":"No"),Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","icd="+(icd?"Yes":"No"),Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","supports OpenGL="+(opengl?"Yes":"No"),Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","windowed="+(window?"Yes":"No"),Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","bitmap="+(bitmap?"Yes":"No"),Logger.Severity.INFO);
					Logger.openlog.Log(this,"ChoosePixelFormatEx","double buffering="+(dbuff?"Yes":"No"),Logger.Severity.INFO);
				}
			}
			pfd=new Gdi.PIXELFORMATDESCRIPTOR();
			pfd.nSize=(short)Marshal.SizeOf(pfd);
			pfd.nVersion=1;
			_DescribePixelFormat(hdc,maxindex,(uint)pfd.nSize,ref pfd);
			formatRef=maxindex;
			Logger.openlog.Log(this,"ChoosePixelFormatEx","Done. Chosen format="+maxindex,Logger.Severity.INFO);
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
			Gdi.PIXELFORMATDESCRIPTOR pixelFormat=ChoosePixelFormatEx(deviceContext,//The window context.
																																colorBits,//Bits-per-pixel of color
																																depthBits,//Z-depth
																																autoSwapBuffers?1:0,//Don't use double buffering.
																																usehardware?1:0,
																																ref selectedFormat);
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
				CreateContexts();

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

		///<summary>Reads the contents of the front OpenGL drawing surface and returns an unaltered, unscaled copy as an image. The idea is that one would first draw using OpenGL, perform a swap (if double buffering), then read the contents of the resulting image to perform an operation on it, then use the final image for rendering.</summary>
		public Bitmap ReadFrontBuffer(){
			byte[] data=new byte[3*this.Width*this.Height];//3 components in each pixel of the width X height image.
			Gl.glReadPixels(0,0,this.Width,this.Height,Gl.GL_RGB,Gl.GL_UNSIGNED_BYTE,data);
			//The red and blue components are swapped in comparison of the returned OpenGL image and a windows bitmap. The returned image data is also inverted on over the x-axis (in the y or vertical direction). Otherwise, this function would be very fast, because we could just basically return the data into the bitmap in just a few lines of code.
			for(int i=0;i<3*this.Width*this.Height;i+=3){
				//Swap the red and blue components of the current pixel.
				byte temp=data[i];
				data[i]=data[i+2];
				data[i+2]=temp;
			}
			IntPtr dataPtr=GCHandle.Alloc(data,GCHandleType.Pinned).AddrOfPinnedObject();
			Bitmap result=new Bitmap(this.Width,this.Height,3*this.Width,PixelFormat.Format24bppRgb,dataPtr);
			result.RotateFlip(RotateFlipType.RotateNoneFlipY);
			return result;
		}

		#endregion

		#region Control Methods

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//Do not paint background
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			Paint(e);
		}

		public void Paint(PaintEventArgs e) {
			//Only draw with OpenGL if rendering is enabled (disabled by default for designing)
			if(renderEnabled) {
				//Initialize the device and rendering contexts if the user hasn't already
				TaoInitializeContexts();

				//Make this the current context
				if(autoMakeCurrent) {
					//Only switch contexts if this is already not the current context
					if(renderContext!=Wgl.wglGetCurrentContext()) {
						MakeCurrentContext();
					}
				}

				//Fire the user-defined TaoRenderScene event
				if(TaoRenderScene!=null) {
					TaoRenderScene(this,null);
				}

				//Automatically finish the scene
				if(autoFinish) {
					Gl.glFinish();
				}

				//Automatically check for errors
				lastErrorCode=Gl.glGetError();

				if(lastErrorCode!=Gl.GL_NO_ERROR) {
					//Fire the error handling event
					if(TaoOpenGLError!=null) {
						TaoOpenGLError(this,new OpenGLErrorEventArgs(lastErrorCode));
					}
				}

				//Swap the OpenGL buffer to the display
				if(autoSwapBuffers) {
					Gdi.SwapBuffersFast(deviceContext);
				}
			} else {
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
