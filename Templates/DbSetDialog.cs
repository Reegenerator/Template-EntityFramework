using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE80;
using Kodeo.Reegenerator.Wrappers;
using RgenLib.Extensions;
namespace RgenLib.Templates {
    public partial class DbSetDialog : Form
    {
        public delegate void BaseclassSelectionChangedHandler(object sender, BaseclassSelectionChangedArgs args);
        public event BaseclassSelectionChangedHandler BaseclassSelectionChanged;

        private ProjectItem ProjectItem;
        private CodeClass2 TargetClass;
        public DbSetDialog() {
            InitializeComponent();
            baseclassCombo.SelectionChangeCommitted += baseclassCombo_SelectionChangeCommitted;
            
        }

        void baseclassCombo_SelectionChangeCommitted(object sender, EventArgs e) {
            
                var selectedBase =  (CodeClass2) baseclassCombo.SelectedItem;
                var sce = new BaseclassSelectionChangedArgs() {Selected = selectedBase};
                var descendants = new List<CodeClass2>();
                ProjectItem.Project.TraverseCodeElements<CodeClass2>(descendants.Add, x => x!=selectedBase && x.IsDerivedFrom[selectedBase.FullName]);
                classesList.DataSource = descendants.ToArray();
                classesList.DisplayMember = "Name";

    
        }
        private IEnumerable<CodeClass2> GetClassesFromSameNamespace(CodeClass2 cls) {
            var classes = new List<CodeClass2>();
            ProjectItem.Project.TraverseCodeElements<CodeClass2>(classes.Add, x => x.Namespace.FullName == cls.Namespace.FullName && cls != x); ;//cls.Namespace.Children.OfType<CodeClass2>();
            return classes.ToArray();
        }
        public static IEnumerable<CodeClass2> ShowDialog(ProjectItem prjItem, CodeClass2 cls, BaseclassSelectionChangedHandler selectionHandler)
        {
            var dlg = new DbSetDialog {ProjectItem = prjItem, TargetClass = cls};
            var baseClassCandidates = dlg.GetClassesFromSameNamespace(cls);
            dlg.baseclassCombo.DataSource = baseClassCandidates;
            dlg.ShowDialog();
            return dlg.classesList.SelectedItems.Cast<CodeClass2>();
           
        }
    }

}
