namespace Projekt7VMTranslator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "C:\\Users\\mort286f\\Desktop\\nand2tetris\\nand2tetris\\projects\\07\\StackArithmetic\\SimpleAdd\\SimpleAdd.vm";
            string destination = "C:\\Users\\mort286f\\Desktop\\nand2tetris\\nand2tetris\\projects\\07\\StackArithmetic\\SimpleAdd\\Output.txt";
            VMFileReader vmReader = new VMFileReader();
            Parser parser = new Parser();
            string file = vmReader.ReadVMFile(filepath);
            List<string> fileLines = parser.SplitLines(file);
            List<string> finishedFileLines = new List<string>();
            for (int i = 0; i < fileLines.Count; i++)
            {
                List<string> segment = parser.SegmentLine(fileLines[i]);

                if (parser.ArithmeticTable.ContainsKey(segment[0]))
                {
                    finishedFileLines.Add(parser.HandleArithmeticCommand(segment));
                }
                else if (segment[0] == "push")
                {
                    finishedFileLines.Add(parser.HandlePushCommand(segment));
                }
                else if (segment[0] == "pop")
                {
                    finishedFileLines.Add(parser.HandlePopCommand(segment));
                }
            }
        }
    }
}