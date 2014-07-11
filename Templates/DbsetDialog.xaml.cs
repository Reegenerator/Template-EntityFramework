using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Text.RegularExpressions;
using EnvDTE80;
using RgenLib.Extensions;
using ProjectItem = Kodeo.Reegenerator.Wrappers.ProjectItem;
using Window = System.Windows.Window;

namespace Templates {
    /// <summary>
    /// Interaction logic for DbsetDialog.xaml
    /// </summary>
    public partial class DbsetDialog : Window {
        private ProjectItem ProjectItem;
        private CodeClass2 ContextClass;
        private bool _isGenerated;
        public string[] _existingSets;
        public DbsetDialog() {
            InitializeComponent();
            //BaseclassCombo.SelectionChanged += baseclassCombo_SelectionChangeCommitted;
            
        }



        private DbsetCandidate[] GetCandidateList(CodeClass2 selectedBase) {
            try {
                var descendants = new List<CodeClass2>();
                ProjectItem.Project.TraverseHierarchyForCodeElements<CodeClass2>(descendants.Add,
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
                
                var itemsArray = items.ToArray();
                
                return itemsArray;
            }
            catch (Exception e) {
                Debug.DebugHere(e);
                throw;
            }
        }
        private void UpdateCandidateList() {
            var selectedBase = (CodeClass2)BaseclassCombo.SelectedItem;
            //var res = await Task.Run(() => GetCandidateList(selectedBase));
            CandidateList.ItemsSource = GetCandidateList(selectedBase);
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
            ProjectItem.Project
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
            var baseClassCandidates = GetBaseclassesFromSameNamespace(ContextClass);
            BaseclassCombo.ItemsSource = baseClassCandidates;
        }

        public static DbsetDialogResult ShowDialog(ProjectItem prjItem, CodeClass2 cls, string[] existingSets) {
            var dlg = new DbsetDialog { ProjectItem = prjItem, ContextClass = cls , _existingSets = existingSets};
            dlg.Init();
            dlg.ShowDialog();
            return dlg._isGenerated ? new DbsetDialogResult(dlg.CandidateList.SelectedItems.Cast<DbsetCandidate>().ToArray(),true) : null;
        }

        private void GenerateButton_Click(object sender, System.Windows.RoutedEventArgs e) {
            _isGenerated = true;
            Close();

        }

        private void BaseclassCombo_DropDownClosed(object sender, EventArgs e) {
            UpdateCandidateList();
        }
        void baseclassCombo_SelectionChangeCommitted(object sender, EventArgs e) {

        }
    }
}
