using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionals
{
    public static class Codes
    {
        public static Code Success = new Code(CodesNumbers.Success, "Successful operation");
        public static Code NotFound = new Code(CodesNumbers.NotFound, "Item not found");
    }
}
