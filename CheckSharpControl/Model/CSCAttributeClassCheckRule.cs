
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckSharpControlLibrary.Model
{
    public class CSCAttributeClassCheckRule : CSCRule
    {
        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
        public string AttributeName { get; set; }


        public override void Check(TextWriter output)
        {
            var assembly = Assembly.Load(this.AssemblyName);

            Regex typeRegex = new Regex(this.TypeName);

            var types = assembly.GetExportedTypes().Where(x => typeRegex.IsMatch(x.FullName));

            Regex attributeRegex = new Regex(this.AttributeName);

            var attributes = assembly.GetReferencedAssemblies().SelectMany(x => Assembly.Load(x.FullName).GetTypes().Where(y => attributeRegex.IsMatch(y.FullName))).Concat(assembly.GetExportedTypes().Where(x => attributeRegex.IsMatch(x.FullName)));



            foreach (var type in types)
            {
                foreach (var attribute in attributes)
                {
                    if (AttributeCheck.Contains(type, attribute))
                    {
                        output.WriteLine("[pass] [" + attribute.FullName + "] " + type.FullName + " ");
                    }
                    else
                    {
                        output.WriteLine("[fail] [" + attribute.FullName + "] " + type.FullName + " ");
                    }
                }
            }
        }

        public override void Parse(string line)
        {
            this.AssemblyName = line.Split(new String[] { "].[" }, StringSplitOptions.None)[0].Trim().Trim('[', ']');
            this.TypeName = line.Split(':')[0].Split(new String[] { "].[" }, StringSplitOptions.None)[1].Trim().Trim('[', ']');
            this.AttributeName = line.Split(':')[1].Trim().Trim('[', ']');
        }
    }
}
