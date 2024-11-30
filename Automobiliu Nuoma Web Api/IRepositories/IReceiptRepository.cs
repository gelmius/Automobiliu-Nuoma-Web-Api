using System.Threading.Tasks;
using Automobiliu_Nuoma_Web_Api.Models;

public interface IReceiptRepository
{
    Task GenerateReceiptAsync(NuomosUzsakymas uzsakymas, Automobilis automobilis);
}
