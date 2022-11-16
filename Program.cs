using AIDAS;

var a = new AIDAService();

while (true)
{
    await a.GetTempAsync();
    await a.GetSpeedsAsync();
    //Task.Delay(200).Wait();
}

