using System.Reflection;
using System.Windows.Forms;

namespace OpenDental {
	///<summary>This control prevents flicker in a SplitContainer by setting protected style of child panels.</summary>
	public partial class SplitContainerNoFlicker:SplitContainer {
		public SplitContainerNoFlicker() {
			InitializeComponent();
			MethodInfo mi=typeof(Control).GetMethod("SetStyle",BindingFlags.NonPublic|BindingFlags.Instance);
			object[] args=new object[] {ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer,true};
			mi.Invoke(this.Panel1,args);
			mi.Invoke(this.Panel2,args);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer,true);
		}
	}
}
