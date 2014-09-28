using CheckSharpControlLibrary.Input;
using CheckSharpControlLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CheckSharpControlLibrary
{
    public class CheckSharpControl
    {

        public static void Check(TextReader input, TextWriter output)
        {
            var inputReader = new InputReader(input);

            var list = inputReader.Parse();

            foreach (var item in list)
            {
                try
                {
                    Check(item, output);
                }
                catch (Exception e)
                {
                    output.WriteLine(e.Message);
                }
            }

            output.Flush();

        }

        private static void Check(CSCRule item, TextWriter output)
        {
            if (item == null) return;

            output.WriteLine("[Processing] " + item.GetType().Name);
            item.Check(output);

        }
    }
}
