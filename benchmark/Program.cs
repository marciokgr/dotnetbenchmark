using benchmark;
using BenchmarkDotNet.Running;

Console.WriteLine("### Usando BenchmarkDotNet  ###\n");
Console.WriteLine("Pressione algo para iniciar\n");
Console.ReadLine();

var resultado = BenchmarkRunner.Run<BenchmarkString>();
var resultadoReflection = BenchmarkRunner.Run<BenchmarkReflection>();
var resultadoSort = BenchmarkRunner.Run<BenchmarkSort>(); 
Console.ReadLine();
