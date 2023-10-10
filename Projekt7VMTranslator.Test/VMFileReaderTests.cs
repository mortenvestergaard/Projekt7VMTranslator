using Projekt7VMTranslator;

namespace Projekt7VMTranslator.Test
{
    public class VMFileReaderTests
    {

        [Theory]
        [InlineData("add //This comment should be removed", "add")]
        [InlineData("push constant 1 //This is a constant push", "push constant 1")]
        public void RemoveComment_ShouldRemoveCodeComments(string input, string expected)
        {
            //Arrange/Act
            var trimmedStringMock = VMFileReader.RemoveCodeCommentsAndSpaces(input);

            //Assert
            Assert.Equal(expected, trimmedStringMock);
        }

        [Theory]
        [InlineData(" sub       ", "sub")]
        [InlineData("         pop that 5                     ", "pop that 5")]
        [InlineData(" eq  ", "eq")]
        public void RemoveSpaces_ShouldRemoveTrimSpaces(string input, string expected)
        {
            //Act
            var trimmedInputMock = VMFileReader.RemoveCodeCommentsAndSpaces(input);

            //Assert
            Assert.Equal(expected, trimmedInputMock);
        }
    }
}