using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt7VMTranslator.Test
{
    public class ParserTests
    {
        [Theory]
        [InlineData("add", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M+D")]
        [InlineData("sub", "@SP\nAM=M-1\nD=M\nA=A-1\nM=M-D")]
        [InlineData("not", "@SP\nA=M-1\nM=!M\n")]
        [InlineData("neg", "D=0\n@SP\nA=M-1\nM=D-M\n")]
        public void HandleArithmeticCommand_ReturnsStringBasedOnInput(string input, string expected)
        {
            //Arrange
            var parser = new Parser();
            var inputListStub = new List<string>() { input };


            //Act
            var stringMock = parser.HandleArithmeticCommand(inputListStub);

            //Assert
            Assert.Equal(expected, stringMock);
        }

        [Fact]
        public void HandleArithmeticCommand_ReturnsArgumentException()
        {
            //Arrange
            var parser = new Parser();
            const string keyMock = "NotAKeyValue";
            var listStub = new List<string>() { keyMock };

            //Act
            Action actionMock = () => parser.HandleArithmeticCommand(listStub);

            //Assert
            Assert.Throws<ArgumentException>(actionMock);
        }

        [Fact]
        public void HandlePopCommand_ThrowsArgumentIfWrongDictionaryKey()
        {
            var parser = new Parser();
            const string keyMock = "TestKeyValue";
            var listStub = new List<string>() { keyMock };

            Action actionMock = () => parser.HandlePopCommand(listStub);

            Assert.Throws<ArgumentException>(actionMock);
        }
    }
}
