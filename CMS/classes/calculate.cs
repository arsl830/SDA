using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CMS.classes
{
    public class calculate
    {
        private string cgpa;
        private string salary;
        public string compute(string identity)
        {
            if (identity == "student")
            {
                cgpa = "3.3";
                return cgpa;
            }
            else
            {
                return salary = "70000";
            }
        }
    }
}
