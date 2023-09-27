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

        public string RemoveCodeCommentsAndSpaces(string currentLine)
        {
            var lines = currentLine.Split('\n')
                            .Where(line => !line.TrimStart().StartsWith("//"))  // Remove lines starting with comments
                            .Select(line => line.Contains("//")
                                            ? line.Substring(0, line.IndexOf("//")).Trim() // Remove inline comments and trim spaces 
                                            : line.Trim())
                            .Where(line => !string.IsNullOrWhiteSpace(line));  // Remove empty lines

            return string.Join("\n", lines);
        }
    }
}
