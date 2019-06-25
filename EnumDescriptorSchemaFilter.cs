using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace SwaggerGenEnums
{
    /// <summary>
    /// Add enum description to swagger gen files
    /// </summary>
    public class EnumDescriptorSchemaFilter : ISchemaFilter
    {
        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var typeInfo = context.SystemType.GetTypeInfo();

            if (typeInfo.IsEnum)
            {
                schema.Description = BuildDescription(typeInfo);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static string BuildDescription(TypeInfo typeInfo)
        {
            var docMembers = LoadXmlMembers(typeInfo);

            var stringBuilder = new StringBuilder();

            var docMember = XmlDocHelper.GetTypeMember(docMembers, typeInfo);
            stringBuilder.AppendLine(docMember.Value.Trim());
            stringBuilder.AppendLine();

            BuildMembersDescription(typeInfo, stringBuilder, docMembers);

            return stringBuilder.ToString();
        }

        private static void BuildMembersDescription(TypeInfo typeInfo, StringBuilder stringBuilder, XElement docMembers)
        {
            var enumValues = Enum.GetValues(typeInfo);

            for (int i = 0; i < enumValues.Length; i++)
            {
                var enumValue = enumValues.GetValue(i);
                var member = typeInfo.GetMember(enumValue.ToString()).Single();
                var docMember = XmlDocHelper.GetDocMember(docMembers, member);

                string name = enumValue.ToString();
                string description = docMember.Value.Trim();

                stringBuilder.AppendFormat("* `{0}` - {1}", name, description);
                stringBuilder.AppendLine();
            }
        }

        private static XElement LoadXmlMembers(TypeInfo typeInfo)
        {
            var file = XmlDocHelper.GetXmlDocFile(typeInfo.Assembly);
            var docXml = XDocument.Load(file.FullName);
            var xmlRoot = docXml.Root;

            if (xmlRoot == null) throw new ArgumentNullException(nameof(xmlRoot) + ", for " + typeInfo.FullName);

            return xmlRoot.Element("members");
        }

        #endregion Private Methods
    }
}
