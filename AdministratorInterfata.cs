namespace MagazinOnline;

public interface Administrator
{
    public void AddProdusGeneric(ProdusGeneric produs);
    public void AddProdusPerisabil(ProdusPerisabil produs);
    public void AddProdusElectrocasnic(ProdusElectrocasnic produs);
    public void AdaugareComandaNoua(Comanda ComandaNoua);
    public void ScoateProdusStoc(string NumeProdus);
    public void ASnumarBucati(string NumeProdus, int adaugare);
    public void VizualizareComenzi();
    public void ModificareStatus(int NumarulComenzii, bool valid);
    public void ModificareDataLiv(int NumarulComenz, DateTime dataNoua);
} 