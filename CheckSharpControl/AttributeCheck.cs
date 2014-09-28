using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CheckSharpControlLibrary
{
    public class AttributeCheck
    {

        public static bool Contains(Type type, Type attribute)
        {  
            return type.GetCustomAttributes(attribute, true).Length > 0;
        }

        public static bool Contains(MethodInfo method, Type attribute)
        {
            return method.GetCustomAttributes(attribute, true).Length > 0;
        }

    }
}
