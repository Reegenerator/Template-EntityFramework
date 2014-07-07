using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE80;

namespace RgenLib.Templates {
    public class BaseclassSelectionChangedArgs : EventArgs
    {
        public CodeClass2 Selected { get; set; }
    }
}
