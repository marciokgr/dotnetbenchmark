using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using System.Text;

namespace benchmark
{    
    [RPlotExporter]
    [RankColumn]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public class BenchmarkString
    {

        int NumeroDeItens = 100;
        [Benchmark(Baseline = true)]
        public string ConcatenandoStringsCom_StringBuilder()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < NumeroDeItens; i++)
            {
                sb.Append("Teste_" + i);
            }
            return sb.ToString();
        }

        [Benchmark]
        public string ConcatStringsUsando_GenericList()
        {
            var list = new List<string>(NumeroDeItens);
            for (int i = 0; i < NumeroDeItens; i++)
            {
                list.Add("Teste_" + i);
            }
            return list.ToString();
        }

    }
}
