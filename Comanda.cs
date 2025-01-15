namespace MagazinOnline;

public class Comanda
{
    public int NrComanda {  get; set; }
    public string Nume { get; private set; }
    public string NrTel { get; private set; }
    public string Email { get; private set; }
    public string AdresaLiv {  get; private set; }
    public string Status {  get; private set; }
    public DateTime DataLivrare { get; private set; }
    public Cos CosProd { get; private set; }
    public static int cout = -1;

    public Comanda(Cos cos,string Nume, string NrTel, string Email, string AdresaLiv)
    {
        cout++;
        this.NrComanda = cout;
        this.Nume = Nume;
        this.NrTel = NrTel;
        this.Email = Email;
        this.CosProd=cos;
        this.AdresaLiv=AdresaLiv;
        DataLivrare=DateTime.Now.AddDays(2);
        if(DataLivrare<DateTime.Now)
        {
            Status = "Livrat";
        }
        else
        {
            Status = "In asteptare";
        }
    }
    public void StatusSet(string status)
    {
        Status = status;
    }
    public void DataSet(DateTime Data)
    {
        this.DataLivrare = Data;
    }
    public override string ToString()
    {
        return $"Comanda {NrComanda} are {CosProd} pentru {Nume}, {NrTel}, {Email},la adresa {AdresaLiv},status {Status}";
    }
}
