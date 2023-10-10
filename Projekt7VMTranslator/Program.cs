using Projekt7VMTranslator.Interfaces;

namespace Projekt7VMTranslator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IASMCodeHandler handler = new ASMCodeHandler();

            string filepath = "C:\\Users\\Morten\\OneDrive\\Documents\\nand2tetris\\projects\\07\\StackArithmetic\\StackTest\\StackTest.vm";
            string destination = "C:\\Users\\Morten\\OneDrive\\Documents\\nand2tetris\\projects\\07\\StackArithmetic\\StackTest\\Output.txt";

            List<string> finishedFileLines = handler.BuildASMCode(filepath);

            File.WriteAllLines(destination, finishedFileLines);
        }
    }
}