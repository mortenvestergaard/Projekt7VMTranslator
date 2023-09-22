using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt7VMTranslator
{
    public class Parser
    {
        //Has all the arithmetic operations
        public Dictionary<string, string> ArithmeticTable { get; set; }
        //Has all the memory accessible methods/functions like add, neg, sub
        public Dictionary<string, string> MemoryAccessTable { get; set; }

        public Parser()
        {
            ArithmeticTable = new Dictionary<string, string>()
            {
                { "add", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M+D" },
                { "sub", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M-D" },
                { "and", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M&D" },
                { "or",  "@SP\nAM=M-1\nD=M\nA=A-1\nM=M|D" },
                { "gt", "JLE"},
                { "lt", "JGE" },
                { "eq", "JNE" },
                { "not", "@SP\nA=M-1\nM=!M\n" },
                { "neg", "D=0\n@SP\nA=M-1\nM=D-M\n" },
            };
            MemoryAccessTable = new Dictionary<string, string>()
            {

            };
        }
        public void HandleArithmeticCommand()
        {

        }
        public void HandleMemoryAccessCommand()
        {

        }

        public List<string> SplitLines(string fileText)
        {
            return fileText.Split("\r\n").ToList();
        }

        public List<string> SegmentLine(string line)
        {
            return line.Split(" ").ToList();
        }
    }
}
