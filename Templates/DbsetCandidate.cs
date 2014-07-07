using EnvDTE80;

namespace RgenLib.Templates
{
    public class DbsetCandidate {
        public CodeClass2 Class { get; set; }
        public bool IsAlreadyDefined { get; set; }

        public string SetName { get; set; }
    }
}