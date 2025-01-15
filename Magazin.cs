namespace MagazinOnline;

public class Magazin
{
    public List<ProdusGeneric> Produse { get;private set; }
    public string Nume { get; private set; }
    public Magazin(string name)
    {
        Nume = name;
        Produse = new List<ProdusGeneric>();
    }
}