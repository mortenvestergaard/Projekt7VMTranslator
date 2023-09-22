using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt7VMTranslator
{
    public class VMFileReader
    {
        public string ReadVMFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            return File.ReadAllText(filePath);
        }
    }
}
