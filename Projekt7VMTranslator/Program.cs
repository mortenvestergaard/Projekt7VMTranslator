namespace Projekt7VMTranslator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "C:\\Users\\mort286f\\Desktop\\nand2tetris\\nand2tetris\\projects\\07\\StackArithmetic\\SimpleAdd\\SimpleAdd.vm";

            VMFileReader vmReader = new VMFileReader();
            Parser parser = new Parser();
            string file = vmReader.ReadVMFile(filepath);
            List<string> fileLines = parser.SplitLines(file);

            for (int i = 0; i < fileLines.Count; i++)
            {
                List<string> segment = parser.SegmentLine(fileLines[i]);

                if (parser.ArithmeticTable.ContainsKey(segment.ElementAt(i)))
                {

                }
                else if (segment.ElementAt(i) == "push")
                {

                }
                else if (segment.ElementAt(i) == "pop")
                {

                }
            }
        }
    }
}