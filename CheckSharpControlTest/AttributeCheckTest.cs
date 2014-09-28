using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckSharpControlLibrary;


namespace CheckSharpControlLibraryTest
{
    [TestClass]
    public class AttributeCheckTest
    {
        [TestMethod]
        public void CheckClass()
        {
            bool result = AttributeCheck.Contains(typeof(AttributeCheckTest), typeof(TestClassAttribute));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckClassFail()
        {
            bool result = AttributeCheck.Contains(typeof(AttributeCheckTest), typeof(TestMethodAttribute));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckMethod()
        {
            bool result = AttributeCheck.Contains(typeof(AttributeCheckTest).GetMethod("CheckMethod"), typeof(TestMethodAttribute));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckMethodFail()
        {
            bool result = AttributeCheck.Contains(typeof(AttributeCheckTest).GetMethod("CheckMethodFail"), typeof(TestClassAttribute));

            Assert.IsFalse(result);
        }


    }
}
