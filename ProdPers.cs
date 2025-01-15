namespace MagazinOnline;
public class ProdusPerisabil : ProdusGeneric
{ 
    public string Conditii {  get;private set; }
    public DateTime Expirare { get; private set; }
    public ProdusPerisabil(string Nume,decimal Pret,int Stoc,DateTime Expirare,string Conditii):base(Nume,Pret, Stoc)
    {   
        this.Expirare = Expirare;
        this.Conditii = Conditii;
    }
    public override string ToString()
    {
        return $"Produsul electro {Nume}, Euro: {Pret}, {Stoc} ramase in stoc, conditii de tinere {Conditii}, data expirare{Expirare}";
    }
}