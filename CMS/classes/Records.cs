using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.classes
{
    public class Records:onlogin
    {
        public override string totalrec(string count)
        {
            if (count!=null) 
            {
                return count;
            }
            else
            {
                return null;
            }
        }
    }
}
