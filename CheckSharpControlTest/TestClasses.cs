using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckSharpControlTest
{
    public class CustomAttribute : Attribute {
    }

    [Custom]
    public class TestClassWithAttributeOnClass
    {
        public void Method1() {}
        public void Method2() {}
    }

   
    public class TestClassWithAttributeOnMethod1
    {
        [Custom]
        public void Method1() {}
        public void Method2() {}
    }

}
