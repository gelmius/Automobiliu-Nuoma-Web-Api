using System.IO;
using System.Threading.Tasks;
using Automobiliu_Nuoma_Web_Api.Models;

public class ReceiptRepository : IReceiptRepository
{
    private readonly string _receiptsDirectory = Directory.GetCurrentDirectory();

    public ReceiptRepository()
    {
        _receiptsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Receipts");
        if (!Directory.Exists(_receiptsDirectory))
        {
            Directory.CreateDirectory(_receiptsDirectory);
        }
    }

    public async Task GenerateReceiptAsync(NuomosUzsakymas uzsakymas, Automobilis automobilis)
    {
        var fileName = Path.Combine(_receiptsDirectory, $"{uzsakymas.Id}.txt");
        using var writer = new StreamWriter(fileName);

        await writer.WriteLineAsync($"Nuomos Užsakymas ID: {uzsakymas.Id}");
        await writer.WriteLineAsync($"Marke Modelis: {automobilis.Pavadinimas}");
        await writer.WriteLineAsync($"Paros Kaina: {automobilis.NuomosKaina}");
        await writer.WriteLineAsync($"Bendra Kaina: {uzsakymas.Kaina}");
        await writer.WriteLineAsync($"Laikotarpis Nuo: {uzsakymas.PradziosData}");
        await writer.WriteLineAsync($"Laikotarpis Iki: {uzsakymas.PabaigosData}");
    }
}
