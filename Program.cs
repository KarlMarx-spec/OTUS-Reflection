using Reflection;
using System.Diagnostics;
using System.Text.Json;

Console.WriteLine("Сериализуемый класс: class F { int i1, i2, i3, i4, i5; }");
Console.WriteLine("количество замеров: 1000 итераций");
Console.WriteLine("мой рефлекшен:");
Stopwatch timer = new();
var f = new F();
string testStr = "";
int i = 0;

timer.Start();
while (i++ < 1000)
{
    testStr = new ReflectionHelper<F>().Serialize(f.Get());
}
timer.Stop();
Console.WriteLine($"Время на сериализацию ReflectionHelper = {timer.Elapsed}");
timer.Reset();
i = 0;

timer.Start();
while (i++ < 1000)
{
    new ReflectionHelper<F>().Deserialize(testStr);
}
timer.Stop();
Console.WriteLine($"Время на десериализацию ReflectionHelper = {timer.Elapsed}");
timer.Reset();
i = 0;

timer.Start();
while (i++ < 1000)
{
    testStr = JsonSerializer.Serialize(f.Get());
}
timer.Stop();
Console.WriteLine($"Время на сериализацию JsonSerializer = {timer.Elapsed}");
timer.Reset();
i = 0;

timer.Start();
while (i++ < 1000)
{
    JsonSerializer.Deserialize<F>(testStr);
}
timer.Stop();
Console.WriteLine($"Время на десериализацию JsonSerializer = {timer.Elapsed}");
timer.Reset();

//List<F> list = new() { f.Get(), f.Get(), f.Get()};
timer.Start();
var str = CsvFileSerializer<F>.Serialize(f.Get());
timer.Stop();
Console.WriteLine($"Время на сериализацию Csv файла = {timer.Elapsed}");
timer.Reset();
File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "test.csv"), str);


var fileContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "test.csv"));
timer.Start();
CsvFileSerializer<F>.Deserialize(fileContent);
timer.Stop();
Console.WriteLine($"Время на десериализацию Csv файла = {timer.Elapsed}");






