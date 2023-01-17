using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace benchmark
{
    [RPlotExporter]
    [RankColumn]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class BenchmarkSort
    {

        private static string[] stringArr = new[] { "José", "Maria", "João", "Abraão", "Jacó", "Adão", "Jesus", "Nazaré" };
        public static readonly Sorter arraySorter = new Sorter(stringArr);

        [Benchmark]
        public void BubbleSort() => arraySorter.BubbleSort();

        [Benchmark]
        public void BuiltInSortWithComparison() => arraySorter.BuiltInSortWithComparison();

        [Benchmark(Baseline = true)]
        public void SortWithLINQ() => arraySorter.SortWithLINQ();
    }


    //Classe auxiliar
    public class Sorter
    {
        private string[] input;
        public Sorter(string[] array)
        {
            input = array;
        }
        public void BubbleSort()
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[i].CompareTo(input[j]) < 0)
                    {
                        (input[j], input[i]) = (input[i], input[j]);
                    }
                }
            }
        }
        public void BuiltInSortWithComparison()
        {
            Array.Sort(input, (a, b) => b.CompareTo(a));
        }
        public void SortWithLINQ()
        {
            input = input.OrderByDescending(s => s).ToArray();
        }
    }
}
