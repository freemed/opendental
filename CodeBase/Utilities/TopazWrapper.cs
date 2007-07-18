using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeBase{
    public class TopazWrapper{

        public static Control GetTopaz(){
            return new Topaz.SigPlusNET();
        }

    }
}
