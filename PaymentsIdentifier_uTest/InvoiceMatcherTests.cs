/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using NUnit.Framework;
using PaymentsIdentifier.Model;
using System.Collections.Generic;

namespace PaymentsIdentifier_uTest
{
    [TestFixture]
    internal class InvoiceMatcherTests
    {
        [Test]
        public void InvoiceMatcher_SingleNegativeValue_MatchInvoicesWithZeroTolerance()
        {
            // arrange
            List<double> values = new List<double>()
            {
                -639262.0,
                1278524.0
            };
            double target = 639262;
            InvoiceMatcher invoiceMatcher = new InvoiceMatcher(values, target);

            // act
            invoiceMatcher.Match(0);

            // assert
            List<List<double>> output = new List<List<double>>()
            {
                new List<double>
                {
                    -639262.0,
                    1278524.0
                }
            };
            CollectionAssert.AreEqual(output, invoiceMatcher.MatchesFound);
        }

        [Test]
        public void InvoiceMatcher_TwoNegativeFourPositiveValues_MatchInvoicesWithZeroTolerance()
        {
            // arrange
            List<double> values = new List<double>()
            {
                -82.4,
                -8.87,
                519.09,
                3568.29,
                24922.98,
                25993.22
            };
            double target = 28400.00;
            InvoiceMatcher invoiceMatcher = new InvoiceMatcher(values, target);

            // act
            invoiceMatcher.Match(0);

            // assert
            List<List<double>> output = new List<List<double>>()
            {
                new List<double>
                {
                    -82.4,
                    -8.87,
                    3568.29,
                    24922.98
                }
            };
            CollectionAssert.AreEqual(output, invoiceMatcher.MatchesFound);
        }

        //[Test]
        //public void InvoiceMatcher_TooLongListWithRepeatingValues_MatchInvoicesWithZeroTolerance()
        //{
        //    // arrange
        //    double target = 639262;
        //    List<double> values = new List<double>()
        //    {
        //        1470.39,
        //        454.11,
        //        3345.41,
        //        2138.72,
        //        12867.62,
        //        152294.84,
        //        50764.95,
        //        15677.97,
        //        14758.98,
        //        1060.44,
        //        113.62,
        //        1839.2,
        //        162.26,
        //        203059.78,
        //        757.22,
        //        58889.56,
        //        55630.36,
        //        2351.7,
        //        3600.6,
        //        120019.3,
        //        4126.05,
        //        2030.6,
        //        515756.45,
        //        55630.36,
        //        515756.45,
        //        238595.24,
        //        515756.45,
        //        248.72,
        //        2940.78,
        //        9650.72,
        //        927.59,
        //        8579.09,
        //        1001.44,
        //        17158.1,
        //        515756.45,
        //        515756.45,
        //        515756.45,
        //        515756.45,
        //        515756.45,
        //        152294.84
        //    };
        //    InvoiceMatcher invoiceMatcher = new InvoiceMatcher(values, target);

        //    // act
        //    invoiceMatcher.Match(0);

        //    // assert
        //    //CollectionAssert.AreEqual(i.MatchesFound, Enumerable.Empty<List<double>>);
        //}
        
    }
}
