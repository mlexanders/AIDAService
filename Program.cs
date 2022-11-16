
// See https://aka.ms/new-console-template for more information
using AIDAS;

Console.WriteLine("Hello, World!");

var a = new AIDAService();

while (true)
{
    a.GetData();
    break;
    //Task.Delay(200).Wait();    
}

