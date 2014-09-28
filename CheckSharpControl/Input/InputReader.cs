
using CheckSharpControlLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CheckSharpControlLibrary.Input
{
    internal class InputReader
    {

        TextReader _input;

        public InputReader(TextReader input)
        {
            _input = input;
        }

        public List<CSCRule> Parse()
        {
            string nextLine;
            Type type = null;

            List<CSCRule> cscRules = new List<CSCRule>();

            while ((nextLine = _input.ReadLine()) != null)
            {

                if (string.IsNullOrWhiteSpace(nextLine))
                {
                    continue;
                }

                Regex r = new Regex("^\\[([^\\[\\]]*)\\]$");

                if (r.IsMatch(nextLine))
                {
                    type = Type.GetType("CheckSharpControlLibrary.Model." + r.Match(nextLine).Groups[1].Value);
                    continue;
                }

                if (type == null) continue;

                CSCRule cscRule = Activator.CreateInstance(type) as CSCRule;

                if (cscRule == null)
                {
                    continue;
                }

                cscRule.Parse(nextLine);
                cscRules.Add(cscRule);

            }

            return cscRules;
        }

    }
}
