using ExcelToJsonExtractor;
using NUnit.Framework;

namespace ExcelToJsonExtractorTest
{
    public class StringExtensionTests
    {

        [Test]
        public void ShouldTruncateTooLongString()
        {
            var testString = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            var length_1 = testString.Length;
            var result_string = testString.TruncateLength(12);

            Assert.That(length_1 != result_string.Length);
        }

        [Test]
        public void ShouldNotTruncateShortString()
        {
            var testString = "ABCD";

            var result_string = testString.TruncateLength(12);

            Assert.That(testString.Equals(result_string));
        }

        
        [Test]
        public void ShouldNotTruncateEqualLengthString()
        {
            var testString = "ABCDE";

            var result_string = testString.TruncateLength(5);

            Assert.That(testString.Equals(result_string));
        }

        [Test]
        public void ShouldAddEllipsisWhenTruncatingString()
        {
            var testString = "ABCDASDASDASDASD";

            var result_string = testString.TruncateLength(7);

            Assert.That(result_string.EndsWith("..."));
        }
        
    }
}
