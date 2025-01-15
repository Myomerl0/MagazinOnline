namespace MagazinOnline;

public interface Administrator
{
    public void AddProdusGeneric(ProdusGeneric prouds);
    public void AddProdusPerisabil(ProdusPerisabil produs);
    public void AddProdusElectrocasnic(ProdusElectrocasnic produs);
    public void ScoateProdusStoc(string NumeProdus);
    public void ASnumarBucati(string NumeProdus, int adaugare);
    public void VizualizareProduse();
    public void ModificareStatus(int NumarulComenzii, bool valid);
    public void ModificareDataLiv(int NumarulComenz, DateTime dataNoua);
    public void AdaugareComandaNoua(Comanda ComandaNoua);
}