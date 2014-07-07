using System.Data.Entity;
using System.IO;
using EnvDTE;
using EnvDTE80;
using Kodeo.Reegenerator.Generators;
using RgenLib.Extensions;

namespace RgenLib.Templates {
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Kodeo.Reegenerator;

    [CodeSnippet]
    public partial class DbSet {
        /// <summary>
        /// Method that gets called prior to calling <see cref="Render"/>.
        /// Use this method to initialize the properties to be used by the render process.
        /// You can access the project item attached to this generator by using the <see cref="ProjectItem"/> property.
        /// </summary>
        public override void PreRender() {
            base.PreRender();
            // var projectItem = base.ProjectItem;
        }
        public override Kodeo.Reegenerator.Generators.RenderResults Render() {
            var cls = ElementAtCursor.GetClassAtCursor(ProjectItem.DteObject.DTE);
            var contextType = typeof (DbContext);
            if (!cls.IsDerivedFrom[contextType.FullName]) return null;
            var classType = cls.ToType();


         
            var selections= DbsetDialog.ShowDialog(this.ProjectItem, cls).ToArray();

           if (!selections.Any()) return null;
            GenerateDbset(cls,selections);
            return null;
        }

        private void GenerateDbset(CodeClass2 targetContext, IEnumerable<DbsetCandidate> selections)
        {
         
                var code =General.GetTemplateOutput(GenDbset,selections);
                var insertPoint =targetContext.GetStartPoint(vsCMPart.vsCMPartBody);
            insertPoint.InsertAndFormat(code);
        }
    }
}