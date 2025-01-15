namespace MagazinOnline;

public class ProdusElectrocasnic : ProdusGeneric
{
    public string EficientaEn { get; private set; }
    public int ConsumMax {  get; private set; }
    public ProdusElectrocasnic(string Nume,decimal Pret,int Stoc, string EficientaEn,int ConsumMax):base(Nume,Pret,Stoc)
    {
        this.EficientaEn=EficientaEn;
        this.ConsumMax=ConsumMax;
    }
    public override string ToString()
    {
        return $"Produsul electro {Nume}, Euro: {Pret}, {Stoc} ramase in stoc, eficienta energetica{EficientaEn}, Consum maxim{ConsumMax} W";
    }
}
