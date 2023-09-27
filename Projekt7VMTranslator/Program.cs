namespace Projekt7VMTranslator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filepath = "C:\\Users\\Morten\\OneDrive\\Documents\\nand2tetris\\projects\\07\\MemoryAccess\\BasicTest\\BasicTest.vm";
            string destination = "C:\\Users\\Morten\\OneDrive\\Documents\\nand2tetris\\projects\\07\\MemoryAccess\\BasicTest\\Output.txt";
            VMFileReader vmReader = new VMFileReader();
            Parser parser = new Parser();
            string file = vmReader.ReadVMFile(filepath);
            string cleanedFile = vmReader.RemoveCodeCommentsAndSpaces(file);
            List<string> fileLines = parser.SplitLines(cleanedFile);
            List<string> finishedFileLines = new List<string>();
            for (int i = 0; i < fileLines.Count; i++)
            {
                if (i == 7)
                {

                }
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

            File.WriteAllLines(destination, finishedFileLines);
        }
    }
}