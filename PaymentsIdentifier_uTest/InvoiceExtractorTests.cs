/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using NUnit.Framework;
using PaymentsIdentifier.Model;
using System.Collections.Generic;

namespace PaymentsIdentifier_uTest
{
    [TestFixture]
    internal class InvoiceExtractorTests
    {
        [Test]
        public void InvoiceExtractor_LongInputThreeOutputs_ExtractOnlyDistinct()
        {
            // arrange
            string input = "2885622352326 TRANSFER VALEO VISION BELGIQUE 6232057170 6232057297 6232057453TRANSFER Valeo Vision Belgique 6232057170 6232057297 6232057453";
            
            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6232057170", "6232057297", "6232057453" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_WhiteSpaceInputSingleOutput_ExtractAtEndOfString()
        {
            // arrange
            string input = "O/O VODAFONE PROCUREMENT COMPANY SA/RFB/01-LU07820001637              6252091784";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6252091784" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_WhiteSpaceInputSingleOutput_ExtractAtBeginningOfString()
        {
            // arrange
            string input = "6256187658          3902           V?RT KUNDNUMMER HOS ER: GC00704L";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6256187658" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_VeryLongInputNoWhiteSpaces_ExtractFifteenMatches()
        {
            // arrange
            string input = "B/O IBM DANMARK APS 0019/IBM DANMARK APS   EW3892480001 LENOVO DANMARK APS E2EID/NOTPROVIDED 62341378796234137878623413785962341378586234138172623413817162341386234137879                         6234137878                         6234137859                         6234137858                         6234138172                         6234138171                         6234138145                         6234138146                         6234138294                         6234138284                         6234138275                         6234138295                         6234138274                         6234144402                         6234144413";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6234137879", "6234137878", "6234137859", "6234137858", "6234138172", "6234138171", "6234138623", "6234138145", "6234138146", "6234138294", "6234138284", "6234138275", "6234138295", "6234138274", "6234144402", "6234144413" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_SingleWordInput_ExtractSingleValue()
        {
            // arrange
            string input = "6256179399";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6256179399" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_ThreeInputValuesCombinedTwoStandalone_ExtractThreeMatches()
        {
            // arrange
            string input = "B/O IBM DANMARK APS 0019/IBM DANMARK APS   EW3823640001 LENOVO DANMARK APS E2EID/NOTPROVIDED 6234136631623413659862341375716234136631                         6234136598                         6234137571";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6234136631", "6234136598", "6234137571" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_QuestionMarksCombinedInput_ExtractSingleValue()
        {
            // arrange
            string input = "710?00CREDIT?32Tieto-Tapiola Oy?20Transfer other bank?20.00?204500000832?20DABAFIHH?20LENOVO TECHNOLOGY B.V.00000?2020150330362980117130";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6298011713" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_ConnectedAndSeparatedInput_ExtractTwoDistinctValues()
        {
            // arrange
            string input = "B/O SCHNEIDER ELECTRIC DANMARK A/S 0019/SCHNEIDER ELECTRIC D     E2EID/NOTPROVIDED FAKTURA: 62341382586234138257Faktura: 6234138258                6234138257";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>() { "6234138258", "6234138257" };
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_InvalidInputNineDigitEnd_ReturnEmptyCollection()
        {
            // arrange
            string input = "B/O SCHNEIDER ELECTRIC DANMARK A/S 0019/SCHNEIDER ELECTRIC D     E2EID/NOTPROVIDED FAKTURA: 2341382582341623825796";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>();
            CollectionAssert.AreEqual(output, result);
        }

        [Test]
        public void InvoiceExtractor_InvalidInputNoInvoice_ReturnEmptyCollection()
        {
            // arrange
            string input = "B/O SCHNEIDER ELECTRIC DANMARK A/S 0019/SCHNEIDER ELECTRIC D     E2EID/NOTPROVIDED FAKTURA: 234138258234123825796";

            // act
            List<string> result = input.ExtractInvoiceNumbers();

            // assert
            List<string> output = new List<string>();
            CollectionAssert.AreEqual(output, result);
        }
    }
}
