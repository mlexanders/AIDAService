using AIDAS;

var a = new AIDAService();

while (true)
{
    var c = await a.GetTempAsync();
    var b = await a.GetSpeedsAsync();

    Task.Delay(200).Wait();
}

