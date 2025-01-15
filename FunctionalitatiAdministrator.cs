namespace MagazinOnline;

public class FunctAdmin : Administrator
{
    private Magazin _Magazin;
    private List<Comanda> comenzi = new List< Comanda > ();
    public FunctAdmin(Magazin magazin)
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

        string Nume = "";
        string Telefon = "";
        string Email = "";
        string Adresa = "";
        string Status = "";
        int NrComanda = 0;
        Cos CosCurent = new Cos();
        DateTime DataLivrarii = DateTime.Now;
        foreach (var linie in LiniiComenzi)
        {
            if (linie.StartsWith("Nr comanda:"))
            {
                if (!string.IsNullOrEmpty(Nume))
                {
                    Comanda comanda = new Comanda(CosCurent, Nume, Telefon, Email, Adresa);
                    comanda.StatusSet(Status);
                    comanda.DataSet(DataLivrarii);
                    comanda.NrComanda = NrComanda;
                    ListaComenzi.Add(comanda);
                }
                CosCurent = new Cos();  // Resetare coș pentru următoarea comandă
                Nume = "";
                Telefon = "";
                Email = "";
                Adresa = "";
                Status = "";
                string NrComandaText = linie.Replace("Nr comanda:", "").Trim();
                int.TryParse(NrComandaText, out NrComanda);
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
                string[] NumeProds = InCos.Split(',');
                foreach (var NumeProd in NumeProds)
                {
                    CosCurent.AdaugareInCos(NumeProd);
                }
            }
        }

        if (!string.IsNullOrEmpty(Nume))
        {
            Comanda comanda = new Comanda(CosCurent, Nume, Telefon, Email, Adresa);
            comanda.StatusSet(Status);
            comanda.DataSet(DataLivrarii);
            comanda.NrComanda = NrComanda;
            ListaComenzi.Add(comanda);
        }
        comenzi = ListaComenzi;
        Console.WriteLine("Adaugare cu succes :D ");
    }

    public static void AdaugaProdInStoc(FunctAdmin functAdmin)
    {
        bool Validare = false;
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
                    if (!decimal.TryParse(detalii[1].Trim(), out Pret) || !int.TryParse(detalii[2].Trim(), out Stoc))
                    {
                        Validare = true; continue;
                    }
                    if (detalii.Length == 3)
                    {
                        ProdusGeneric Produs = new ProdusGeneric(Nume, Pret, Stoc);
                        functAdmin.AddProdusGeneric(Produs);
                    }
                    else if (detalii.Length == 5)
                    {
                        string PtPerisabil = detalii[3].Trim();
                        DateTime Expirare;
                        string PtElectronic = detalii[4].Trim();
                        if (DateTime.TryParse(PtPerisabil, out Expirare))
                        {
                            ProdusPerisabil Produs = new ProdusPerisabil(Nume, Pret, Stoc, Expirare, PtPerisabil);
                            functAdmin.AddProdusPerisabil(Produs);
                        }

                        else
                        {
                            int ConsumMax;
                            if (!int.TryParse(PtElectronic, out ConsumMax))
                            {
                                Validare = true; continue;
                            }
                            string EficientaEn = PtElectronic;
                            ProdusElectrocasnic Produs = new ProdusElectrocasnic(Nume, Pret, Stoc, EficientaEn, ConsumMax);
                            functAdmin.AddProdusElectrocasnic(Produs);
                        }
                    }
                    else
                    {
                        Validare = true;
                    }
                }
                else
                {
                    Validare = true;
                }
            }
            if (Validare)
            {
                Console.WriteLine("Linii incorecte in fisier");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroarea la citire fisier: {ex.ToString()}");
        }
    }
    public void AddProdusGeneric(ProdusGeneric produs)
    {
        try
        {
            Validare.ValidareNume(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            _Magazin.Produse.Add(produs);
            string ProdusInfo = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}";
            File.AppendAllText(PathProduse, ProdusInfo + Environment.NewLine);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare:{ex.ToString()}");
        }
    }
    public void AddProdusPerisabil(ProdusPerisabil produs)
    {
        try
        {
            Validare.ValidareNume(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            Validare.ValidareData(produs.Expirare);
            Validare.ValidareConditii(produs.Conditii);
            _Magazin.Produse.Add(produs);
            string ProdusInfo = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}, {produs.Expirare.ToString("yyyy-mm-dd")}, {produs.Conditii}";
            if (File.Exists(PathProduse))
            {
                string[] linii = File.ReadAllLines(PathProduse);
                foreach (var linie in linii)
                {
                    if (linie.Trim() == ProdusInfo)
                    {
                        Console.WriteLine("Produsul exista deja");
                        return;
                    }
                }
            }
            File.AppendAllText(PathProduse, ProdusInfo + Environment.NewLine);
            Console.WriteLine("Produs perisabil adaugat");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare {ex.ToString()}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Eroare salvare: {ex.ToString()}");
        }
    }
    public void AddProdusElectrocasnic(ProdusElectrocasnic produs)
    {
        try
        {
            Validare.ValidareNume(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            Validare.ValidareEficientaEn(produs.EficientaEn, produs.ConsumMax);
            _Magazin.Produse.Add(produs);
            string ProdusInfo = $"{produs.Nume}, {produs.Pret}, {produs.Stoc},{produs.EficientaEn}, {produs.ConsumMax}";
            if (File.Exists(PathProduse))
            {
                string[] linii = File.ReadAllLines(PathProduse);
                foreach (var linie in linii)
                {
                    if (linie.Trim() == ProdusInfo)
                    {
                        Console.WriteLine("Produsul exista deja");
                        return;
                    }
                }
            }
            File.AppendAllText(PathProduse, ProdusInfo + Environment.NewLine);
            Console.WriteLine("Produs electronic adaugat");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare {ex.ToString()}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Eroare salvare: {ex.ToString()}");
        }
    }
    public void SalveazaComanda(string path, Comanda comanda)
    {
        try
        {
            using (StreamWriter write = new StreamWriter(path, true))
            {
                write.WriteLine($"Nr comanda:{comanda.NrComanda}");
                write.WriteLine($"Data de livrare:{comanda.DataLivrare.ToString("yyyy.mm.dd")}");
                write.WriteLine($"Nr tel:{comanda.NrTel}");
                write.WriteLine($"Email:{comanda.Email}");
                write.WriteLine($"Adresa livrare:{comanda.AdresaLiv}");
                write.WriteLine($"Status:{comanda.Status}");
                if (comanda.CosProd.CosProd != null && comanda.CosProd.CosProd.Count > 0)
                {
                    write.WriteLine($"Cos: {string.Join(",", comanda.CosProd.CosProd)}");
                }
                else
                {
                    write.WriteLine("Cosul este gol");
                }
                write.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare salvare: {ex.ToString()}");
        }
    }
    public void AdaugareComandaNoua(Comanda comanda)
    {
        try
        {
            Validare.ValidareComanda(comanda.Nume, comanda.Email, comanda.AdresaLiv);
            comenzi.Add(comanda);
            SalveazaComanda(PathComenzi, comanda);
            Console.WriteLine("Comanda a fost adaugata");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare:{ex.ToString()}");
        }
    }
    public void ScoateProdusStoc(string NumeProdus)
    {
        int ProdSel = -1;
        for (int i = 0; i < _Magazin.Produse.Count; i++)
        {
            if (_Magazin.Produse[i].Nume == NumeProdus)
            {
                ProdSel = i; break;
            }
        }
        if (ProdSel > -1)
        {
            ProdusGeneric Sters = _Magazin.Produse[ProdSel];
            _Magazin.Produse.RemoveAt(ProdSel);
            Console.WriteLine("Produs eliminat");
            string[] linii = File.ReadAllLines(PathProduse);
            List<string> LiniiRamase = new List<string>();
            string ProdusInfo = $"{Sters.Nume}, {Sters.Pret}, {Sters.Stoc}";
            foreach (var linie in linii)
            {
                if (linie.Trim() != ProdusInfo)
                {
                    LiniiRamase.Add(linie);
                }
            }
            if (LiniiRamase.Count != linii.Length)
            {
                File.WriteAllLines(PathProduse, LiniiRamase);
                Console.WriteLine("Produsul a fost scos");
            }
            else
            {
                Console.WriteLine("Produsul nu exista");
            }
        }
        else
        {
            Console.WriteLine("Produsul nu exista");
        }
    }
    public void ASnumarBucati(string NumeProdus, int adaugare)
    {
        try
        {
            ProdusGeneric produs = _Magazin.Produse.FirstOrDefault(p => p.Nume == NumeProdus);
            if (produs == null)
            {
                Console.WriteLine("Produsul nu exista");
                return;
            }
            Validare.ValidareCantitate(adaugare, produs.Stoc);
            produs.ModificareStoc(adaugare);
            Console.WriteLine("Stoc modificat");
            string[] linii = File.ReadAllLines(PathProduse);
            List<string> actualizare = new List<string>();
            string ModificatInfo = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}";
            foreach (var linie in linii)
            {
                string[] detalii = linie.Split(',');
                if (detalii.Length >= 3 && detalii[0].Trim() == produs.Nume)
                {
                    actualizare.Add(ModificatInfo);
                }
                else
                {
                    actualizare.Add(linie);
                }
            }
            File.WriteAllLines(PathProduse, actualizare);
            Console.WriteLine("Fisier actualizat");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.ToString()}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Eroare la fisier: {ex.ToString()}");
        }
    }
    public void VizualizareComenzi()
    {
        if (!File.Exists(PathComenzi))
        {
            Console.WriteLine("Fisierul nu exista");
            return;
        }
        string[] linii = File.ReadAllLines(PathComenzi);
        if (linii.Length == 0) 
        {
            Console.WriteLine("Nu exista comenzi");
            return;
        }
        Console.WriteLine("Comenziile sunt: ");
        List<string> ComandaActuala = new List<string>();
        foreach (var linie in linii)
        {
            if(string.IsNullOrEmpty(linie))
            {
                AfiseazaComanda(ComandaActuala);
                ComandaActuala.Clear();
            }
            else
            {
                ComandaActuala.Add(linie);
            }
        }
        if(ComandaActuala.Count > 0)
        {
            AfiseazaComanda(ComandaActuala);
        }
    }
    public void AfiseazaComanda(List<string> Comanda)
    {
        foreach (var linie in Comanda)
        {
            Console.WriteLine(linie);
        }
        Console.WriteLine();
    }
    private void ActualizareComenzi(string Fisier)
    {
        try
        {
            using (StreamWriter write = new StreamWriter(Fisier))
            {
                foreach (var comanda in comenzi)
                {
                    write.WriteLine($"Nr comanda:{comanda.NrComanda}");
                    write.WriteLine($"Data de livrare:{comanda.DataLivrare.ToString("yyyy.mm.dd")}");
                    write.WriteLine($"Nume: {comanda.Nume}");
                    write.WriteLine($"Nr tel:{comanda.NrTel}");
                    write.WriteLine($"Email:{comanda.Email}");
                    write.WriteLine($"Adresa livrare:{comanda.AdresaLiv}");
                    write.WriteLine($"Status:{comanda.Status}");
                    if (comanda.CosProd.CosProd != null && comanda.CosProd.CosProd.Count > 0)
                    {
                        write.WriteLine($"Cos: {string.Join(",", comanda.CosProd.CosProd)}");
                    }
                    else
                    {
                        write.WriteLine("Cosul este gol");
                    }
                    write.WriteLine();
                }
            }
            Console.WriteLine("Actualizat cu succes");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare actualizare {ex.ToString()}");
        }
    }
    public void ModificareStatus(int NumarulComenzii, bool valid)
    {
        if(NumarulComenzii <0 || NumarulComenzii >= comenzi.Count)
        {
            Console.WriteLine("Comanda nu exista");
            return;
        }
        if(valid==true)
        {
            comenzi[NumarulComenzii].StatusSet("In curs de livrare");
            Console.WriteLine($"Comanda {NumarulComenzii} are acum statusul 'In curs de livrare'");
            ActualizareComenzi(PathComenzi);
        }
        else
        {
            Console.WriteLine("Nu se mai modifica");
        }
    }
    public void ModificareDataLiv(int NumarulComenz, DateTime dataNoua)
    {
        try
        {
            Comanda comanda = comenzi.FirstOrDefault(p => p.NrComanda == NumarulComenz);
            if (comanda == null)
            {
                Console.WriteLine("Comanda nu exista");
                return;
            }
            Validare.ValidareLivrare(dataNoua, comanda.DataLivrare);
            comanda.DataSet(dataNoua);
            Console.WriteLine("Data livrare modificata");
            ActualizareComenzi(PathComenzi);
        }
        catch(ArgumentException ex)
        {
            Console.WriteLine($"Eroare:{ex.ToString()}");
        }
    }
}
