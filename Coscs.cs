namespace MagazinOnline;

public class Cos
{
    public List<string> CosProd { get; set; }
    public Cos()
    {
        CosProd = new List<string>();
    }
    public void AdaugareInCos(string prod)
    {
        CosProd.Add(prod);
    }
    public void Golire()
    {
        CosProd.Clear();
    }
    public override string ToString()
    {
        string afisare = "Cos: ";
        foreach (var prod in CosProd)
        {
            afisare = afisare + prod + ' , ';
        }
        return afisare;
    }
}