using System;

namespace RgenLib {
    public abstract class TaggedCodeRenderer : CodeRendererEx {



        protected static Version _version;
        public Version Version { get { return _version; } }
    }
}
