namespace MagazinOnline;
public class ProdusGeneric
{
    public string Nume { get; private set; }
    public decimal Pret {  get; private set; }
    public int Stoc {  get; private set; }
    public ProdusGeneric(string Nume)
    {
        this.Nume = Nume;
        this.Pret = 0;
        this.Stoc = 0;
    }
    public ProdusGeneric(string Nume, decimal Pret, int Stoc)
    {
        this.Nume= Nume;
        this.Pret = Pret;
        this.Stoc = Stoc;
    }
    public void ModificareStoc(int Modificare)
    {
        this.Stoc += Modificare;
    }
    public override string ToString()
    {
        return $"Produsul {Nume}, Euro: {Pret}, {Stoc} ramase in stoc";
    }
}
