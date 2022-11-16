
// See https://aka.ms/new-console-template for more information
using AIDAS;

Console.WriteLine("Hello, World!");

var a = new AIDAService();

while (true)
{
   var c = await a.GetTempAsync();
   var b = await a.GetSpeedsAsync();


    Task.Delay(200).Wait();    
}

