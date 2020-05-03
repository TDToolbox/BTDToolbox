using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes
{
    class Guard
    {
        public static bool IsStringValid(string input)
        {
            if (input != null && input != "")
                return true;
            else
                return false;
        }
    }
}
