using EnvDTE80;

namespace Templates
{

    public class DbsetDialogResult
    {
        public DbsetDialogResult(DbsetCandidate[] candidates, bool isGenGetbytype)
        {
            _Selection = candidates;
            _IsGenGetbytype = isGenGetbytype;
        }
        private readonly DbsetCandidate[] _Selection;

        public DbsetCandidate[] Selection
        {
            get
            {
                return _Selection;
            }
        }

        private readonly bool _IsGenGetbytype;

        public bool IsGenGetbytype
        {
            get { return _IsGenGetbytype; }

        }
    }
    public class DbsetCandidate {
        public CodeClass2 Class { get; set; }
        public bool IsAlreadyDefined { get; set; }

        public string SetName { get; set; }

        public override string ToString()
        {
            return Class.Name;
        }
    }
}