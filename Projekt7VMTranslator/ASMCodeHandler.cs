using Projekt7VMTranslator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt7VMTranslator
{
    //Handles the building of the output file
    public class ASMCodeHandler:IASMCodeHandler
    {
        private static Parser Parser = new Parser();

        /// <summary>
        /// Builds a list of lines based on the lines of VM code in the inputted file
        /// </summary>
        /// <param name="filepath">The name of the file</param>
        /// <returns>A list of lines to write to the output file</returns>
        public List<string> BuildASMCode(string filepath)
        {

            string file = VMFileReader.ReadVMFile(filepath);
            string cleanedFile = VMFileReader.RemoveCodeCommentsAndSpaces(file);
            List<string> fileLines = Parser.SplitLines(cleanedFile);


            List<string> finishedFileLines = new List<string>();
            for (int i = 0; i < fileLines.Count; i++)
            {
                List<string> segment = Parser.SegmentLine(fileLines[i]);

                if (Parser.ArithmeticTable.ContainsKey(segment[0]))
                {
                    finishedFileLines.Add(Parser.HandleArithmeticCommand(segment));
                }
                else if (segment[0] == "push")
                {
                    finishedFileLines.Add(Parser.HandlePushCommand(segment));
                }
                else if (segment[0] == "pop")
                {
                    finishedFileLines.Add(Parser.HandlePopCommand(segment));
                }
            }
            return finishedFileLines;
        }
    }
}
