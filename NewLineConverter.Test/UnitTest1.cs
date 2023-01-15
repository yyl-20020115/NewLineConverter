namespace NewLineConverter.Test
{
    [TestClass]
    public class UnitTest1
    {
        public const string TestInput = "\n\r\r\n";
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("", (NewLineConvertReader.ConvertAll("", EOLType.CRLF)));
            Assert.AreEqual("", (NewLineConvertReader.ConvertAll("", EOLType.LF)));
            Assert.AreEqual("", (NewLineConvertReader.ConvertAll("", EOLType.CR)));

            Assert.AreEqual("\r\n", (NewLineConvertReader.ConvertAll("\r\n", EOLType.CRLF)));
            Assert.AreEqual("\n", (NewLineConvertReader.ConvertAll("\r\n", EOLType.LF)));
            Assert.AreEqual("\r", (NewLineConvertReader.ConvertAll("\r\n", EOLType.CR)));

            Assert.AreEqual("\r\n", (NewLineConvertReader.ConvertAll("\n", EOLType.CRLF)));
            Assert.AreEqual("\n", (NewLineConvertReader.ConvertAll("\n", EOLType.LF)));
            Assert.AreEqual("\r", (NewLineConvertReader.ConvertAll("\n", EOLType.CR)));

        }
    }
}