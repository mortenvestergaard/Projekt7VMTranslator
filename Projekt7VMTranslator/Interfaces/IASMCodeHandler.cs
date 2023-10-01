using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt7VMTranslator.Interfaces
{
    internal interface IASMCodeHandler
    {
        //Builds the ASM code for the output file based on the lines in the given file
        public List<string> BuildASMCode(string file);
    }
}
