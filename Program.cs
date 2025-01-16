using MagazinOnline;
using System.Globalization;

Magazin magazin = new Magazin("Lidl");
bool merge=false;
bool user = false;
FunctAdmin functAdmin = new FunctAdmin(magazin);
ActiuniUtilizator actiuniUtilizator = new ActiuniUtilizator(magazin);
FunctAdmin.AdaugaProdInStoc(functAdmin);
functAdmin.AdaugareComenzi();
while(!merge)
{
    Cos cos = new Cos();
    Console.WriteLine();
    Console.WriteLine("~~~~~~~~~ LIDL ADEVARAT ~~~~~~~~~");
    Console.WriteLine("Utilizator -1 ||||| Administrator-2");
    int GradAbsolutDePutere;
    while(true)
    {
        Console.WriteLine("Introduceti un numar (nu altceva) ");
        string citire= Console.ReadLine();
        if(int.TryParse(citire, out GradAbsolutDePutere) )
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid nu ati introdus un numar");
        }
    }
    switch(GradAbsolutDePutere)
    {
        case 1:
            user = true;
            bool iesireU = false;
            while(!iesireU)
            {
                Console.WriteLine("Optiuniile pentru utilizator");
                Console.WriteLine("1 Vizualizeaza lista cu toate produsele");
                Console.WriteLine("2 Inspecteaza un produs");
                Console.WriteLine("3 Cauta produs dupa nume");
                Console.WriteLine("4 Ordoneaza produsele dupa pret");
                Console.WriteLine("5 Adauga un produs in cos");
                Console.WriteLine("6 Efectueaza o comanda");
                Console.WriteLine("0 Revenire la meniu mod utilizare");
                int alegere;
                while(true)
                {
                    Console.WriteLine("Introduceti un numar (nu altceva) ");
                    string input= Console.ReadLine();
                    if(int.TryParse(input,out alegere))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Input gresit");
                    }
                }
                switch (alegere)
                {
                    case 1:
                        actiuniUtilizator.VizualizareProduse();
                        break;
                    case 2:
                        Console.WriteLine("Numele produsului ce urmeaza inspectat ");
                        string prodInspectat = Console.ReadLine();
                        actiuniUtilizator.InspectProdus(prodInspectat);
                        break;
                    case 3:
                        Console.WriteLine("Numele produsul cautat ");
                        string prodCautat = Console.ReadLine();
                        actiuniUtilizator.CautareNume(prodCautat);
                        break;
                    case 4:
                        Console.WriteLine("Ce sortare faceti: crescator-1  descrescator-2");
                        int SortareOpt;
                        while (true)
                        {
                            Console.WriteLine("Introduceti un numar (nu altceva) ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out SortareOpt))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                            }
                        }
                        switch (SortareOpt)
                        {
                            case 1:
                                Console.WriteLine("Produsele sortate crescator");
                                actiuniUtilizator.OrdonareCrescator();
                                actiuniUtilizator.VizualizareProduse();
                                break;
                            case 2:
                                Console.WriteLine("Produsele sortate descrescator");
                                actiuniUtilizator.OrdonareDescrescator();
                                actiuniUtilizator.VizualizareProduse();
                                break;
                            default:
                                Console.WriteLine("Optiune invalida");
                                break;
                        }
                        break;
                    case 5:
                        Console.WriteLine("Produsul adaugat in cos ");
                        string prodAdaugat = Console.ReadLine();
                        int NumarProdu = -1;
                        for (int i = 0; i < magazin.Produse.Count; i++)
                            if (magazin.Produse[i].Nume == prodAdaugat)
                                NumarProdu = i;
                        if (NumarProdu != -1)
                        {
                            cos.AdaugareInCos(magazin.Produse[NumarProdu].Nume);
                            Console.WriteLine("Produs adaugat");
                        }
                        else
                            Console.WriteLine("Produsul nu exista");
                        break;
                    case 6:
                        string Nume, NrTel, Email, AdresaLiv;
                        Console.WriteLine("Nume:");
                        Nume = Console.ReadLine();
                        Console.WriteLine("Nr tel:");
                        NrTel = Console.ReadLine();
                        Console.WriteLine("Email:");
                        Email = Console.ReadLine();
                        Console.WriteLine("Adresa livrare:");
                        AdresaLiv = Console.ReadLine();
                        Console.WriteLine("Plasare in proces");
                        try
                        {
                            Validare.ValidareCos(cos.CosProd);
                            Comanda ComUtilizator = new Comanda(cos, Nume, NrTel, Email, AdresaLiv);
                            functAdmin.AdaugareComandaNoua(ComUtilizator);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Eroare: {ex.ToString()}");
                        }
                        break;
                    case 0:
                        iesireU = true;
                        break;
                    default:
                        Console.WriteLine("Optiunea nu este valida");
                        break;
                }
            }
            break;
        case 2:
            bool iesireA = false;
            while (!iesireA)
            {
                Console.WriteLine("Optiuni Administrator");
                Console.WriteLine("1 Adauga produs in stoc");
                Console.WriteLine("2 Scoate produs din stoc");
                Console.WriteLine("3 Modifica numarul de bucati ale unui produs din stoc");
                Console.WriteLine("4 Vizualizare comenzi");
                Console.WriteLine("5 Procesare o comanda");
                Console.WriteLine("0 Revenire la meniu mod utilizare");
                int optiuneA;
                while(true)
                {
                    Console.WriteLine("Introduceti un numar (nu altceva) ");
                    string citire= Console.ReadLine();
                    if(int.TryParse(citire,out optiuneA))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Input invalid");
                    }
                }
                switch(optiuneA)
                {
                    case 1:
                        string Nume;
                        decimal Pret;
                        int Stoc;
                        Console.WriteLine("Introduceti numele produsului ");
                        Nume = Console.ReadLine();
                        Console.WriteLine("Introduceti pretul produsului ");
                        while (true)
                        {
                            Console.WriteLine("Introduceti un numar (nu altceva) ");
                            string citire = Console.ReadLine();

                            if (decimal.TryParse(citire, out Pret))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Input invalid");
                            }
                        }
                        Console.WriteLine("Introdceti stocul produsului ");
                        while (true)
                        {
                            Console.WriteLine("Introduceti un numar (nu altceva) ");
                            string citire = Console.ReadLine();

                            if (int.TryParse(citire, out Stoc))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Input invalid");
                            }
                        }
                        Console.WriteLine("Produs generic-1, produs perisabil-2, produs electrocasnic-3");
                        int OptProd;
                        while (true)
                        {
                            Console.WriteLine("Introduceti un numar (nu altceva) ");
                            string citire = Console.ReadLine();

                            if (int.TryParse(citire, out OptProd))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Input invalid");
                            }
                        }
                        switch (OptProd)
                        {
                            case 1:
                                ProdusGeneric prodGeneric = new ProdusGeneric(Nume, Pret, Stoc);
                                functAdmin.AddProdusGeneric(prodGeneric);
                                actiuniUtilizator.VizualizareProduse();
                                break;
                            case 2:
                                DateTime Expirare;
                                string ConditiiPastrare;
                                Console.WriteLine("Introduceti data de expirare a produsului ");
                                while (true)
                                {
                                    string citire = Console.ReadLine();
                                    if (DateTime.TryParse(citire, out Expirare))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Data introdusa invalida");
                                    }
                                }

                                Console.WriteLine("Introduceti conditiile de pastrare ");
                                ConditiiPastrare = Console.ReadLine();
                                ProdusPerisabil prodPers = new ProdusPerisabil(Nume, Pret, Stoc, Expirare, ConditiiPastrare);
                                functAdmin.AddProdusPerisabil(prodPers);
                                actiuniUtilizator.VizualizareProduse();
                                break;
                            case 3:
                                string Eficienta;
                                Console.WriteLine("Introduceti clasa de eficienta a produsului");
                                Eficienta = Console.ReadLine();
                                Console.WriteLine("Introduceti puterea maxima ");
                                int PutereMax;
                                while (true)
                                {
                                    Console.WriteLine("Introduceti un numar (nu altceva) ");
                                    string citire = Console.ReadLine();

                                    if (int.TryParse(citire, out PutereMax))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Input invalid");
                                    }
                                }
                                ProdusElectrocasnic ProdElectro = new ProdusElectrocasnic(Nume, Pret, Stoc, Eficienta, PutereMax);
                                functAdmin.AddProdusElectrocasnic(ProdElectro);
                                actiuniUtilizator.VizualizareProduse();
                                break;
                            default:
                                Console.WriteLine("Optiune gresita");
                                break;
                        }
                        break;
                    case 2:
                        string prodSters;
                        Console.WriteLine("Introduceti numele produsului pe care doriti sa il scoateti");
                        prodSters = Console.ReadLine();
                        functAdmin.ScoateProdusStoc(prodSters);
                        actiuniUtilizator.VizualizareProduse();
                        break;
                    case 3:
                        Console.WriteLine("La care produs doriti sa modificati stocul");
                        string prodMod = Console.ReadLine();
                        Console.WriteLine("Cu cat doriti sa modificati stocul");
                        int Modificarestoc;
                        while (true)
                        {
                            Console.WriteLine("Introduceti un numar (nu altceva) ");
                            string citire = Console.ReadLine();
                             if (int.TryParse(citire, out Modificarestoc))
                             {
                                break;
                             }
                             else
                             {
                                Console.WriteLine("Input invalid");
                             }
                        }
                        functAdmin.ASnumarBucati(prodMod, Modificarestoc);
                        actiuniUtilizator.VizualizareProduse();
                        break;
                    case 4:
                        functAdmin.VizualizareComenzi();
                        break;
                    case 5:
                        Console.WriteLine("Ce comanda doriti sa procesati ");
                        int ProcComanda;
                        while (true)
                        {
                            Console.WriteLine("Introduceti un numar (nu altceva) ");
                            string citire = Console.ReadLine();
                            if (int.TryParse(citire, out ProcComanda))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Input invalid");
                            }
                        }
                        bool iesireProces = false;
                        while (!iesireProces)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Optiuni schimbare status-1  schimbare data livrare-2  incheiere procesare-0");
                            int OptProces;
                            while (true)
                            {
                                Console.WriteLine("Introduceti un numar (nu altceva) ");
                                string citire = Console.ReadLine();

                                if (int.TryParse(citire, out OptProces))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid");
                                }
                            }

                            switch (OptProces)
                            {
                                
                                case 1:
                                    bool valid;
                                    while (true)
                                    {
                                        Console.WriteLine("Schimbare statusul in  'in curs de livrare'");
                                        Console.WriteLine("Introduceti true pentru schimbare sau false pentru a nu schimba");
                                        string citire = Console.ReadLine();

                                        if (bool.TryParse(citire, out valid))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Input invalid");
                                        }
                                    }
                                    functAdmin.ModificareStatus(ProcComanda, valid);
                                    break;
                                case 2:
                                    Console.WriteLine("Noua data de livrare format yyyy/mm/dd");
                                    DateTime DataNoua;
                                    while (true)
                                    {
                                        string citire = Console.ReadLine();
                                        if (DateTime.TryParseExact(citire, "yyyy/mm/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DataNoua))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Data introdusa invalida");
                                        }
                                    }
                                    functAdmin.ModificareDataLiv(ProcComanda, DataNoua);
                                    break;
                                case 0:
                                    iesireProces = true;
                                    break;
                                default:
                                    Console.WriteLine("Optiune invalida");
                                    break;
                            }
                        }
                        break;
                    case 0:
                        iesireA=true;
                        if (user = false)
                            cos.Golire();
                        break;
                    default :
                        Console.WriteLine("Optiune invalida");
                        break;
                }

            }
            break;
    }
}