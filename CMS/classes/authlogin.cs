using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.classes
{
    public class authlogin:onlogin
    {
        public override string loginauth(string name)
        {
            if (name == "student")
            {
                return name;
            }
            else if((name == "teacher"))
            {
                return name;
            }
            else
            {
                return null;
            }
        }
    }
}
