using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.PairsAnalysis
{
    public static class AnalysisExtensions
    {
        public static IEnumerable<Tuple<T, T>> Pairs<T>(this IEnumerable<T> dates)
        {
            var enumerator = dates.GetEnumerator();
            if (!enumerator.MoveNext()) yield break;
            var item1 = enumerator.Current;
            while(enumerator.MoveNext())
            {
                var item2 = enumerator.Current;
                yield return new Tuple<T, T>(item1, item2);                
                item1 = item2;
            }           
            yield break;
        }
    
                          
        public static int MaxIndex<T>(this IEnumerable<T> dates) 
            where T : IComparable
        {
            var enumerator = dates.GetEnumerator();
            if (!enumerator.MoveNext()) throw new ArgumentException();
            var item = enumerator.Current;
            var bestIndex = -1;
            var index = 1;
            while (enumerator.MoveNext())
            {
                if (item.CompareTo(enumerator.Current) < 0)
                {
                    item = enumerator.Current;
                    bestIndex = index;
                }
                index++;
            }
            return bestIndex;
        }
    }


    public static class Analysis
    {
        public static int FindMaxPeriodIndex(params DateTime[] data)
        {            
            return data.Pairs().Select(z => (z.Item2 - z.Item1)).MaxIndex();
        }

        public static double FindAverageRelativeDifference(params double[] data)
        {
            return data.Pairs().Select(z => (z.Item2 - z.Item1)/ z.Item1).Max();
        }
    }
}
