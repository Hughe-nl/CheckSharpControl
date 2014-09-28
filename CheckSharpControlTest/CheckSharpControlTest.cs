using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using CheckSharpControlLibrary;

namespace CheckSharpControlLibraryTest
{
    [TestClass]
    public class CheckSharpControlTest
    {
        [TestMethod]
        public void Test()
        {
            string input = @"
[AttributeClassCheck]
    [CheckSharpControlTest].[.*] : [TestClassAttribute]
    [CheckSharpControl].[.*] : [Attribute]

[AttributeMethodCheck]
    [CheckSharpControlTest].[.*].[.*] : [TestMethodAttribute]
    [CheckSharpControl].[.*].[.*] : [^Attribute$]
";

            MemoryStream m = new MemoryStream();

            CheckSharpControl.Check(new StringReader(input),new StreamWriter(m));
            m.Seek(0, 0);

            Debug.Write(new StreamReader(m).ReadToEnd());
        }
    }
}
