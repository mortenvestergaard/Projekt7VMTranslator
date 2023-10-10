using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt7VMTranslator
{
    //Handles all the converting operations from VM to ASM code
    public class Parser
    {
        //Has all the arithmetic operations
        public Dictionary<string, string> ArithmeticTable { get; set; }
        //Has pop command operations
        public Dictionary<string, string> PopTable { get; set; }
        //Has push command operations
        public Dictionary<string, string> PushTable { get; set; }

        private int labelCounter = 0;

        public Parser()
        {
            ArithmeticTable = new Dictionary<string, string>()
            {
                { "add", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M+D" },
                { "sub", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M-D" },
                { "and", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M&D" },
                { "or",  "@SP\nAM=M-1\nD=M\nA=A-1\nM=M|D" },
                { "gt", "@SP\nAM=M-1\nD=M\nA=A-1\nD=M-D\n@FALSE{count}\nD;JLE\n@SP\nA=M-1\nM=-1\n@CONTINUE{count}\n0;JMP\n(FALSE{count})\n@SP\nA=M-1\nM=0\n(CONTINUE{count})" },
                { "lt", "@SP\nAM=M-1\nD=M\nA=A-1\nD=M-D\n@FALSE{count}\nD;JGE\n@SP\nA=M-1\nM=-1\n@CONTINUE{count}\n0;JMP\n(FALSE{count})\n@SP\nA=M-1\nM=0\n(CONTINUE{count})" },
                { "eq", "@SP\nAM=M-1\nD=M\nA=A-1\nD=M-D\n@FALSE{count}\nD;JNE\n@SP\nA=M-1\nM=-1\n@CONTINUE{count}\n0;JMP\n(FALSE{count})\n@SP\nA=M-1\nM=0\n(CONTINUE{count})" },
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
                { "constant", ""+"\nD=A\n@SP\nA=M\nM=D\n@SP\nM=M+1" },
                { "local", "LCL" },
                { "argument", "ARG" },
                { "this", "THIS" },
                { "that", "THAT" },
                { "temp", "R5" },
            };
        }

        /// <summary>
        /// Handles an arithmetic command
        /// </summary>
        /// <param name="segment">The segmented command as a list</param>
        /// <returns>The final arithmetic string</returns>
        /// /// <exception cref="ArgumentException"></exception>
        public string HandleArithmeticCommand(List<string> segment)
        {
            string segmentValue = ArithmeticTable[segment[0]];
            if (segment[0] == "gt" || segment[0] == "lt" || segment[0] == "eq")
            {
                segmentValue = segmentValue.Replace("{count}", labelCounter.ToString());
                labelCounter++;
            }
            else if (segment[0] == "add" || segment[0] == "sub" || segment[0] == "and" || segment[0] == "or" || segment[0] == "not" || segment[0] == "neg")
            {
                segmentValue = segmentValue.Replace("{count}", "");
            }
            else {
                throw new ArgumentException("Arithmetic command not recognized");
            }
            return segmentValue;
        }

        /// <summary>
        /// Handles a pop command and calls ProcessPopValue with the correct value based on the command
        /// </summary>
        /// <param name="segment">The current command segmented in a list</param>
        /// <returns>The final pop value string</returns>
        /// <exception cref="ArgumentException"></exception>
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
                output = ProcessPopValue(PopTable[segment[1]], (int.Parse(segment[2])+5), false);
            }
            else if (segment[1] == "static")
            {
                output = ProcessPopValue((int.Parse(segment[2])+16).ToString(), int.Parse(segment[2]), true);
            }
            else if (segment[1] == "pointer" && int.Parse(segment[2]) == 0)
            {
                output = ProcessPopValue(PopTable[segment[1]], int.Parse(segment[2]), true);
            }
            else if (segment[1] == "pointer" && int.Parse(segment[2]) == 1)
            {
                output = ProcessPopValue(PopTable[segment[1]], int.Parse(segment[2]), true);
            }
            else
            {
                throw new ArgumentException("Invalid pop command. Command not understood");
            }
            Console.WriteLine(output);
            return output;
        }

        /// <summary>
        /// Handles a push command and calls ProcessPushValue with the correct value based on the command
        /// </summary>
        /// <param name="segment">The current command segmented in a list</param>
        /// <returns>The final push value string</returns>
        /// <exception cref="ArgumentException"></exception>
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
                output = ProcessPushValue(PushTable[segment[1]], int.Parse(segment[2])+5, false);
            }
            else if (segment[1] == "static")
            {
                output = ProcessPushValue((int.Parse(segment[2])+16).ToString(), int.Parse(segment[2]), true);
            }
            else if (segment[1] == "pointer" && int.Parse(segment[2]) == 0)
            {
                output = ProcessPopValue(PushTable[segment[1]], int.Parse(segment[2]), true);
            }
            else if (segment[1] == "pointer" && int.Parse(segment[2]) == 1)
            {
                output = ProcessPopValue(PushTable[segment[1]], int.Parse(segment[2]), true);
            }
            else
            {
                throw new ArgumentException("Invalid push command. Command not understood");
            }
            Console.WriteLine(output);
            return output;
        }

        /// <summary>
        /// Processes a push command
        /// </summary>
        /// <param name="pushTableVal">The value equal to the key in the PushTable dictionary</param>
        /// <param name="number">The index value at the end of the given command</param>
        /// <param name="hasPointer">If the command has a pointer or not</param>
        /// <returns>The push value string</returns>
        private string ProcessPushValue(string pushTableVal, int number, bool hasPointer)
        {
            string pointerCode = hasPointer ? "" : "@" + number + "\nA=D+A\nD=M\n";
            
            return "@" + pushTableVal + "\nD=M\n" + pointerCode + "@SP\nA=M\nM=D\n@SP\nM=M+1";
        }

        /// <summary>
        /// Processes a pop command
        /// </summary>
        /// <param name="popTableVal">The value equal to the key in the PopTable dictionary</param>
        /// <param name="number">The index value at the end of the given command</param>
        /// <param name="hasPointer">If the command has a pointer or not</param>
        /// <returns>The pop value string</returns>
        private string ProcessPopValue(string popTableVal, int number, bool hasPointer)
        {
            string pointerCode = hasPointer ? "D=A\n" : "D=M\n@" + number + "\nD=D+A\n";

            return "@" + popTableVal + "\n" + pointerCode + "@R13\nM=D\n@SP\nAM=M-1\nD=M\n@R13\nA=M\nM=D";
        }

        /// <summary>
        /// Split the cleaned file string into a list to work with
        /// </summary>
        /// <param name="fileText">The text from the inputted file</param>
        /// <returns>A list with all commands in the file</returns>
        public List<string> SplitLines(string fileText)
        {
            return fileText.Split("\n").ToList();
        }

        /// <summary>
        /// Splits each line in the file into segments to be processed
        /// </summary>
        /// <param name="line">Represents a line in the file</param>
        /// <returns>The segmented line in a list</returns>
        public List<string> SegmentLine(string line)
        {
            return line.Split(" ").ToList();
        }
    }
}
