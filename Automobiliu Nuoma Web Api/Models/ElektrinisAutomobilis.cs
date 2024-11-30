namespace Automobiliu_Nuoma_Web_Api.Models
{
    public class ElektrinisAutomobilis : Automobilis
    {
        public decimal BaterijosTalpa { get; set; }
        public int MaxNuvaziuojamasAtstumas { get; set; }
        public decimal IkrovimoLaikas { get; set; }
    }

}
