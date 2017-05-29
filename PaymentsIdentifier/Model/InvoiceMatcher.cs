/*-----------------------------------------\
| Payments Identifier © 2016 Mário Csaplár |
\-----------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace PaymentsIdentifier.Model
{
    internal class InvoiceMatcher
    {
        private List<double> myValues;
        private double myTarget;
        private double myTolerance;

        public List<List<double>> MatchesFound { get; private set; }

        public InvoiceMatcher(List<double> values, double target)
        {
            myValues = values;
            myTarget = target;
            MatchesFound = new List<List<double>>();
        }

        public void Match(double tolerance)
        {
            myTolerance = tolerance;
            SumUpRecursively(myValues, new List<double>());
        }

        private void SumUpRecursively(List<double> initialList, List<double> partialSum)
        {
            double currentSum = partialSum.Sum();

            if (currentSum >= myTarget - myTolerance && currentSum <= myTarget)
            {
                //Console.WriteLine(string.Format("---> Match found: {0} = {1} with tolerance {2} towards {3}.", string.Join(" + ", partialSum.ToArray()), partialSum.Sum(), myTolerance, myTarget));
                MatchesFound.Add(new List<double>(partialSum));
            }

            if (currentSum >= myTarget)
            {
                return;
            }

            for (int i = 0; i < initialList.Count; i++)
            {
                List<double> remaining = new List<double>();
                double n = initialList[i];

                for (int j = i + 1; j < initialList.Count; j++)
                {
                    remaining.Add(initialList[j]);
                }

                List<double> newPartialSum = new List<double>(partialSum);
                newPartialSum.Add(n);
                SumUpRecursively(remaining, newPartialSum);
            }
        }
    }
}
