using System.Reflection;
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
    public class BenchmarkReflection
    {
        [Benchmark]
        public object MethodInvoke()
        {
            var instance = new TargetByString();
            var method =
                typeof(TargetByString)
                    .GetMethod("One",
                        BindingFlags.Instance |
                        BindingFlags.Public
                    );

            return method.Invoke(instance, parameters: null);
        }

        //Private method
        [Benchmark]
        public object AllPublicMethods()
        {
            var instance = new TargetAllPublic();
            var members =
                typeof(TargetAllPublic)
                    .GetMembers(
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.DeclaredOnly
                    )
                    .Where(x =>
                        !x.Name.StartsWith("get_") &&
                        !x.Name.StartsWith("set_")
                    )
                    .ToList();

            foreach (var member in members)
            {
                //Console.WriteLine(member.Name);
            }

            return members;
        }

        [Benchmark]
        public string PrivateMethodInvoke()
        {
            var instance = new TargetPrivateField();
            var field =
                typeof(TargetPrivateField)
                    .GetField("secret",
                        BindingFlags.Instance |
                        BindingFlags.NonPublic
                    );

            var value =
                (string)field.GetValue(instance);

            return value;
        }
    }

    #region classes auxiliares
    public class TargetByString
    {
        public void One() => Console.WriteLine("One");
    }

    public class TargetAllPublic
    {
        public void Zero() { }
        public string One { get; set; }
        public string Two { get; set; }
    }

    public class TargetPrivateField
    {
        private string secret = "42";
    }

    #endregion
}
