using System.Data.Entity;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using EnvDTE;
using EnvDTE80;
using Kodeo.Reegenerator.Generators;
using RgenLib.Attributes;
using RgenLib.Extensions;
using RgenLib.TaggedSegment;
using Templates;

namespace Templates {
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Kodeo.Reegenerator;

    [CodeSnippet]
    public partial class DbSet
    {
        private static readonly Regex _GetExistingSets_Regex;
        private static readonly string _GetExistingSets_DbsetTypeName;
        private static readonly string _GetExistingSets_ReplacePattern;

        static DbSet() {
            var dbsetTypename = typeof(DbSet<>).FullName;
            _GetExistingSets_DbsetTypeName = dbsetTypename.Substring(0, dbsetTypename.Length - 2); //remove '1 at the end 
            var regexPattern = string.Format(@"(?<type>{0}<(?<param>.*)>)", _GetExistingSets_DbsetTypeName);
            _GetExistingSets_ReplacePattern = "${param}";
             _GetExistingSets_Regex = new Regex(regexPattern);
        }
        public override Kodeo.Reegenerator.Generators.RenderResults Render() {
            var cls = ElementAtCursor.GetClassAtCursor(ProjectItem.DteObject.DTE);
            var contextType = typeof (DbContext);
            if (!cls.IsDerivedFrom[contextType.FullName]) return null;
            var classType = cls.ToType();

            var res = DbsetDialog.ShowDialog(this.ProjectItem, cls, GetExistingSetNames(cls));
            if (res == null) return null;
            WriteDbset(cls, res.Selection);
            if (res.IsGenGetbytype) WriteGetbytypeMethod(cls);

            //we are not writing to an output file
            return null;
        }
        static private CodeProperty2[] GetExistingSets(CodeClass2 cls)
        {

            var setProps = cls.GetProperties()
                .Where(p => p.Type.AsFullName.StartsWith(_GetExistingSets_DbsetTypeName));
               
            return setProps.ToArray();
        }

        /// <summary>
        /// Had to use regex, getting it through CodeClass is too complicated
        /// </summary>
        /// <param name="setClassName"></param>
        /// <returns></returns>
        static private string GetDbsetGenericParameter(CodeTypeRef codeElement)
        {
            
            return _GetExistingSets_Regex.Replace(codeElement.AsFullName, _GetExistingSets_ReplacePattern);
        }
        static private string[] GetExistingSetNames(CodeClass2 cls)
        {
            return GetExistingSets(cls).
                    Select(p => GetDbsetGenericParameter(p.Type)).ToArray();
        }
        private void WriteGetbytypeMethod(CodeClass2 cls)
        {
            var sets = GetExistingSets(cls);
            

            var code = General.GetTemplateOutput(output => GenGetbytype(output, sets));
            var insertPoint = cls.GetStartPoint(vsCMPart.vsCMPartBody);
            var manager = new Manager<DbSet, GeneratorOptionAttribute>(TagFormat.Json);
            var writer = manager.CreateWriter(cls);
            writer.OptionTag.Trigger.Type = TriggerTypes.CodeSnippet;
            writer.InsertStart = insertPoint;
            writer.TargetRange = new TaggedRange() {StartPoint = cls.StartPoint, EndPoint = cls.EndPoint};
            writer.Content = code;
            writer.TagNote = "GetDbsetByType";
            writer.InsertOrReplace(true);

        }

        private void WriteDbset(CodeClass2 targetContext, IEnumerable<DbsetCandidate> selections)
        {
         
                var code =General.GetTemplateOutput(output => GenDbset(output,selections));
                var insertPoint =targetContext.GetStartPoint(vsCMPart.vsCMPartBody);
            insertPoint.InsertAndFormat(code);
        }
    }
}