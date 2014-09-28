using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckSharpControlLibrary.Model
{
    public abstract class CSCRule
    {

        abstract public void Check(TextWriter output);

        abstract public void Parse(string line);
    }
}
