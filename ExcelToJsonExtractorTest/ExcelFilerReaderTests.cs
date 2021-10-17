using ExcelToJsonExtractor;
using NUnit.Framework;

namespace ExcelToJsonExtractorTest
{
    public class Tests
    {

        private ExcelFileReader _fileReader;

        [SetUp]
        public void Setup()
        {
            _fileReader = new ExcelFileReader();
        }

        [Test]
        public void TradeCollectionShouldBeEmptyIfNoFileIsGiven()
        {
            _fileReader.Run();
            
            Assert.IsEmpty(_fileReader.GetTradeCollection());
        }
    }
}
