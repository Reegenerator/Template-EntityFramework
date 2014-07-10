using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE80;
using Kodeo.Reegenerator.Wrappers;
using RgenLib.Extensions;

namespace Templates {
    public partial class DbsetDialog : Form {

        private ProjectItem _projectItem;
        private CodeClass2 _contextClass;
        private bool _isGenerated;
        public string[] _existingSets;

        public DbsetDialog() {
            InitializeComponent();
            BaseclassCombo.SelectedIndexChanged += BaseclassCombo_SelectedIndexChanged;
            BaseclassCombo.DisplayMember = "Name";
            GenerateButton.Click += GenerateButton_Click;
        }

      

        private DbsetCandidate[] GetCandidateList(CodeClass2 selectedBase) {
            try {
                var descendants = new List<CodeClass2>();
                _projectItem.Project.TraverseHierarchyForCodeElements<CodeClass2>(descendants.Add,
                   x => x != selectedBase
                       && x.IsDerivedFrom[selectedBase.FullName]);
           
                var pluralizationService = new EnglishPluralizationService();

                var items = from c in descendants
                            select
                                new DbsetCandidate() {
                                    Class = c,
                                    IsAlreadyDefined = _existingSets.Contains(c.FullName),
                                    SetName = pluralizationService.Pluralize(c.Name)
                                };


                return items.ToArray();;
            }
            catch (Exception e) {
                Debug.DebugHere(e);
                throw;
            }
        }
        private void UpdateCandidateList() {
            var selectedBase = (CodeClass2)BaseclassCombo.SelectedItem;
            //var res = await Task.Run(() => GetCandidateList(selectedBase));
            CandidateList.Items.Clear();
            foreach (var c in GetCandidateList(selectedBase).Where(x=> !x.IsAlreadyDefined ))
            {
                CandidateList.Items.Add(c);
            }
        }

        /// <summary>
        /// Returns a list of classes (which have subclasses) within the same namespace of cls
        /// </summary>
        /// <param name="cls"></param>
        /// <remarks> using <see cref="CodeClass2.IsDerivedFrom"/> because <see cref="CodeClass2.DerivedTypes"/> is not implemented in C#
        /// </remarks>
        /// <returns></returns>
        private CodeClass2[] GetBaseclassesFromSameNamespace(CodeClass2 cls) {
            var classes = new List<CodeClass2>();
            _projectItem.Project
                .TraverseHierarchyForCodeElements<CodeClass2>(
                        classes.Add,
                        x => x.Namespace.FullName == cls.Namespace.FullName && cls != x);
            //select classes which have subclasses. 
            var baseClasses = classes.Where(
                                baseC => classes.Any(derived =>
                                    derived.IsDerivedFrom[baseC.FullName]
                                        // skip self, IsDerivedFrom returns true on itself
                                    && baseC != derived));
            return baseClasses.ToArray();
        }


    

        public void Init() {
            var baseClassCandidates = GetBaseclassesFromSameNamespace(_contextClass);
            BaseclassCombo.DataSource = baseClassCandidates;
        }

        public static DbsetDialogResult ShowDialog(ProjectItem prjItem, CodeClass2 cls, string[] existingSets) {
            var dlg = new DbsetDialog { _projectItem = prjItem, _contextClass = cls , _existingSets = existingSets};
            dlg.Init();
            dlg.ShowDialog();
            

            return dlg._isGenerated ? new DbsetDialogResult( dlg.CandidateList.SelectedItems.Cast<DbsetCandidate>().ToArray(), dlg.GenSetByTypeCheck.Checked) : null;
        }

    
        void GenerateButton_Click(object sender, EventArgs e)
        {
            _isGenerated = true;
            Close();
        }


        void BaseclassCombo_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateCandidateList();

        }

    }
}
