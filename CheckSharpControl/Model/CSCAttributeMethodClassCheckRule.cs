
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
    public class CSCAttributeMethodClassCheckRule : CSCRule
    {
        public string AssemblyName { get; set; }
        public string TypeName { get; set; }
        public string MethodName { get; set; }
        public string AttributeName { get; set; }

        public override void Check(TextWriter output)
        {
            var assembly = Assembly.Load(this.AssemblyName);

            Regex typeRegex = new Regex(this.TypeName);

            var types = assembly.GetExportedTypes().Where(x => typeRegex.IsMatch(x.FullName));

            Regex methodRegex = new Regex(this.MethodName);

            var methods = types.SelectMany(x => x.GetMethods().Where(y => methodRegex.IsMatch(y.Name) && y.DeclaringType.FullName == x.FullName));

            Regex attributeRegex = new Regex(this.AttributeName);

            var attributes = assembly.GetReferencedAssemblies().SelectMany(x => Assembly.Load(x.FullName).GetTypes().Where(y => attributeRegex.IsMatch(y.FullName))).Concat(assembly.GetExportedTypes().Where(x => attributeRegex.IsMatch(x.FullName)));

            foreach (var method in methods)
            {
                foreach (var attribute in attributes)
                {
                    if (AttributeCheck.Contains(method, attribute) || AttributeCheck.Contains(method.ReflectedType, attribute))
                    {
                        output.WriteLine("[pass] [" + attribute.FullName + "] " + method.ReflectedType.FullName + "." + method + " ");
                    }
                    else
                    {
                        output.WriteLine("[fail] [" + attribute.FullName + "] " + method.ReflectedType.FullName + "." + method + " ");
                    }
                }
            }
        }

        public override void Parse(string line)
        {
            this.AssemblyName = line.Split(new String[] { "].[" }, StringSplitOptions.None)[0].Trim().Trim('[', ']');
            this.TypeName = line.Split(new String[] { "].[" }, StringSplitOptions.None)[1].Trim().Trim('[', ']');
            this.MethodName = line.Split(':')[0].Split(new String[] { "].[" }, StringSplitOptions.None)[2].Trim().Trim('[', ']');
            this.AttributeName = line.Split(':')[1].Trim().Trim('[', ']');
        }
    }
}
