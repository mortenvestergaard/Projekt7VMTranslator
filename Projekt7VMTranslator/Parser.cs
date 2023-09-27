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
        public Dictionary<string, string> PopTable { get; set; }
        public Dictionary<string, string> PushTable { get; set; }
        private string Argument1 { get; set; }
        private int Argument2 { get; set; }

        public Parser()
        {
            ArithmeticTable = new Dictionary<string, string>()
            {
                { "add", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M+D" },
                { "sub", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M-D" },
                { "and", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M&D" },
                { "or",  "@SP\nAM=M-1\nD=M\nA=A-1\nM=M|D" },
                { "gt", "@SP\nAM=M-1\nD=M\nA=A-1\nD=M-D\n@FALSE\n" + "D;JLE\n@SP\nA=M-1\nM=-1\n@CONTINUE" + "\n0;JMP\n(FALSE" + ")\n@SP\nA=M-1\nM=0\n(CONTINUE"+")\n" },
                { "lt", "@SP\nAM=M-1\nD=M\nA=A-1\nD=M-D\n@FALSE\n" + "D;LGE\n@SP\nA=M-1\nM=-1\n@CONTINUE" + "\n0;JMP\n(FALSE" + ")\n@SP\nA=M-1\nM=0\n(CONTINUE"+")\n" },
                { "eq", "@SP\nAM=M-1\nD=M\nA=A-1\nD=M-D\n@FALSE\n" + "D;JNE\n@SP\nA=M-1\nM=-1\n@CONTINUE" + "\n0;JMP\n(FALSE" + ")\n@SP\nA=M-1\nM=0\n(CONTINUE"+")\n" },
                { "not", "@SP\nA=M-1\nM=!M\n" },
                { "neg", "D=0\n@SP\nA=M-1\nM=D-M\n" },
            };
            PopTable = new Dictionary<string, string>()
            {
                { "local", "LCL" },
                { "argument", "ARG" },
                { "this", "THIS" },
                { "that", "THAT" },
                { "temp", "R5" },

            };
            PushTable = new Dictionary<string, string>()
            {
                { "constant", ""+"\nD=A\n@SP\nA=M\nM=D\n@SP\nM=M+1\n" },
                { "local", "LCL" },
                { "argument", "ARG" },
                { "this", "THIS" },
                { "that", "THAT" },
                { "temp", "R5" },
            };
        }
        public string HandleArithmeticCommand(List<string> segment)
        {
            string output;
            string seg1 = ArithmeticTable[segment[0]];


        }
        public string HandlePopCommand(List<string> segment)
        {
            string output = "";
            if (segment[1] == "local")
            {
                output = ProcessPopValue(PopTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "argument")
            {
                output = ProcessPopValue(PopTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "this")
            {
                output = ProcessPopValue(PopTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "that")
            {
                output = ProcessPopValue(PopTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "temp")
            {
                output = ProcessPopValue(PopTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "static")
            {
                output = ProcessPopValue(int.Parse(segment[2] + 16).ToString(), int.Parse(segment[2]), false);
            }
            return output;
        }

        public string HandlePushCommand(List<string> segment)
        {
            string output = "";
            if (segment[1] == "constant")
            {
                output = "@" + segment[2] + PushTable[segment[1]];
            }
            else if (segment[1] == "local")
            {
                output = ProcessPushValue(PushTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "argument")
            {
                output = ProcessPushValue(PushTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "this")
            {
                output = ProcessPushValue(PushTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "that")
            {
                output = ProcessPushValue(PushTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "temp")
            {
                output = ProcessPushValue(PushTable[segment[1]], int.Parse(segment[2]), false);
            }
            else if (segment[1] == "static")
            {
                output = ProcessPushValue(int.Parse(segment[2] + 16).ToString(), int.Parse(segment[2]), true);
            }
            return output;
        }


        private string ProcessPushValue(string pushTableVal, int number, bool hasPointer)
        {
            string pointerCode = "";
            if (hasPointer)
                pointerCode = "@" + number + "\nA=D+A\nD=M\n";
            
            return "@" + pushTableVal + "\nD=M\n" + pointerCode + "@SP\nA=M\nM=D\n@SP\nM=M+1\n";
        }

        private string ProcessPopValue(string popTableVal, int number, bool hasPointer)
        {
            string pointerCode = hasPointer ? "D=A\n" : "D=M\n@" + number + "\nD=D+A\n";

            return "@" + popTableVal + "\n" + pointerCode + "@R13\nM=D\n@SP\nAM=M-1\nD=M\n@R13\nA=M\nM=D\n";
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
