namespace MagazinOnline;

public class ActiuniUtilizator : Utilizator
{
    private Magazin _Magazin;
    public ActiuniUtilizator(Magazin magazin)
    {
        _Magazin = magazin;
    }
    public void VizualizareProduse()
    {
        Console.WriteLine("Lista cu produse ");
        foreach(var prod in _Magazin.Produse)
        {
            Console.WriteLine(prod);
        }
        Console.WriteLine();
    }
    public void InspectProdus(string produs)
    {
        bool exista=false; 
        foreach(var prod in _Magazin.Produse)
        {
            if(prod.Nume == produs)
            {
                Console.WriteLine(prod);
                exista = true;
            }
        }
       if (!exista)
            Console.WriteLine("Produsul nu exista");
       Console.WriteLine();
    }
    public void CautareNume(string Nume)
    {
        bool exista = false;
        foreach (var prod in _Magazin.Produse)
        {
            if (prod.Nume == Nume)
            {
                Console.WriteLine("Produsul exista");
                exista = true;
            }
        }
        if (!exista)
            Console.WriteLine("Produsul nu exista");
        Console.WriteLine();
    }
    public void OrdonareCrescator()
    {
        _Magazin.Produse.Sort((p1,p2)=>p1.Pret.CompareTo(p2.Pret));
    }
    public void OrdonareDescrescator()
    {
        _Magazin.Produse.Sort((p1, p2) => p2.Pret.CompareTo(p1.Pret));
    }

}