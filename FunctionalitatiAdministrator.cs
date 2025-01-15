namespace MagazinOnline;

public class FunctAdmin : Administrator
{
    private Magazin _Magazin;
    private List<Comanda> comanda = new < Comanda > ();
    public FunctAdmin(Mgazin magazin)
    {
        _Magazin = magazin;
    }
    
    static string PathComenzi = "MagazinOnline\\comenzi.txt";
    static string PathProduse = "MagazinOnline\\produse.txt";

    public void AdaugareComenzi()
    {
        if (!File.Exists(PathComenzi))
        {
            Console.WriteLine("Fisierul nu exista");
            return;
        }
        string[] LiniiComenzi = File.ReadAllLines(PathComenzi);
        List<Comanda> ListaComenzi = new List<Comanda>();

        Cos CosCurent = new Cos();
        int NrComanda = 0;
        string Nume = "";
        string Telefon = "";
        string Email = "";
        string Adresa = "";
        string Status = "";
        DateTime DataLivrarii = DateTime.Now;

        foreach (var linie in LiniiComenzi)
        {
            if (linie.StartsWith("Nr comanda:"))
            {
                if (!string.IsNullOrEmpty(Nume))
                {
                    Comanda comanda = new Comanda(CosCurent, Nume, Telefon, Email, Adresa);
                    comanda.StatusComandaSet(Status);
                    comanda.DataLivSet(DataLivrarii);
                    comanda.NumarComanda = NrComanda;
                    ListaComenzi.Add(comanda);
                }
                CosCurent = new Cos(); // cos nou si resetam tot dupa
                int NrComanda = 0;
                string Nume = "";
                string Telefon = "";
                string Email = "";
                string Adresa = "";
                string Status = "";

                string NrComandaT = linie.Replace("Nr comanda:", "").Trim();
                int.TryParse(NrComandaT, out NrComanda);
            }
            else if(linie.StartsWith("Nume:"))
            {
                Nume= linie.Replace("Nume:","").Trim();
            }
            else if(linie.StartsWith("Nr tel:"))
            {
                Telefon = linie.Replace("Nr tel:", "").Trim();
            }
            else if(linie.StartsWith("Email:"))
            {
                Email = linie.Replace("Email:", "").Trim();
            }
            else if(linie.StartsWith("Adresa livrare:"))
            {
                Adresa = linie.Replace("Adresa livrare:", "").Trim();
            }
            else if(linie.StartsWith("Status:"))
            {
                Status = linie.Replace("Status:", "").Trim();
            }    
            else if(linie.StartsWith("Cos:"))
            {
                string InCos = linie.Replace("Cos:", "").Trim();
                string[] NumeProds=InCos.Split(',');
                foreach (var NumeProd in NumeProds)
                {
                    CosCurent.AdaugaInCos(NumeProd);
                }
            }
        }

        if(!string.IsNullOrEmpty(Nume))
        {
            Comanda comanda= new Comanda(CosCurent,Nume,Telefon,Email,Adresa);
            comanda.StatusComandaSet(Status);
            comanda.DataLivSet(DataLivrarii);
            comanda.NumarComanda = NrComanda;
            ListaComenzi.Add(comanda);
        }
        comanda = ListaComenzi;
        Console.WriteLine("Adaugare cu succes :D ");
    }
    
    public static void AdaugaProdInStoc(FunctAdmin functAdmin)
    {
        bool Validare=false;
        try
        {
            string[] linii = File.ReadAllLines(PathProduse);
            foreach (var linie in linii)
            {
                var detalii = linie.Split(',');
                if (detalii.Length >= 3)
                {
                    string Nume = detalii[0].Trim();
                    decimal Pret;
                    int Stoc;
                    if(!decimal.TryParse(detalii[1].Trim(),out Pret) || !int.TryParse(detalii[2].Trim(),out Stoc))
                    {
                        Validare = true; continue;
                    }
                    if(detalii)
                }
            }
        }
    }
}
