using System.Text.RegularExpressions;

namespace MagazinOnline;

public static class Validare
{
    public static void ValidareNume(string Nume)
    {
        if (string.IsNullOrEmpty(Nume))
            throw new ArgumentException("Numele nu poate fi gol");
        if (!Regex.IsMatch(Nume, @"^[a-zA-Z0-9]+$"))
            throw new ArgumentException("Numele nu poate avea caractere speciale");
    }
    public static void ValidarePret(decimal Pret)
    {
        if (Pret <= 0)
            throw new ArgumentException("Pretul trebe sa fie pozitiv");
        if (Pret > Configurare.PretMax)
            throw new ArgumentException($"De unde atatia bani pretul trebuie sa fie mai mic de {Configurare.PretMax} ");
    }
    public static void ValidareStoc(int Stoc)
    {
        if (Stoc <= 0)
            throw new ArgumentException("Stocul trebuie sa fie pozitiv");
        if (Stoc > Configurare.StocMax)
            throw new ArgumentException($"Stocul nu poate fi mai mare de {Configurare.StocMax}");
    }
    public static void ValidareData(DateTime Data)
    {
        if (Data < DateTime.Now)
            throw new ArgumentException("Produsul a expirat");
        if (Data > DateTime.Now.AddYears(Configurare.AniExpirareMax))
            throw new ArgumentException("Data de expirare prea mare");
    }
    public static void ValidareAdresa(string Adresa)
    {
        if (string.IsNullOrEmpty(Adresa))
            throw new ArgumentException("Adresa nu poate fi goala");
    }
    public static void ValidareEmail(string Email)
    {
        if (string.IsNullOrEmpty(Email))
            throw new ArgumentException("Emailul nu poate fi gol");
    }
    public static void ValidareCos(List<string> Produse)
    {
        if(Produse == null ||  Produse.Count == 0)
            throw new ArgumentException("Cosul e gol adauga produse");
    }
    public static bool ValidareComanda(string Nume, string Email, string Adresa)
    {
        ValidareNume(Nume);
        ValidareEmail(Email);
        ValidareAdresa(Adresa);
        return true;
    }
    public static void ValidareEficientaEn(string Eficienta, int Consum)
    {
        if (string.IsNullOrEmpty(Eficienta))
            throw new ArgumentException("Clasa de eficienta nu poate fi goala");
        if (!Regex.IsMatch(Eficienta, @"^[A-Z]+$"))
            throw new ArgumentException("Trebuie doar litere mari");
        if (Consum < 0 || Consum > Configurare.PutereElectronicMax)
            throw new ArgumentException("Putere maxima invalida ori prea mare ori sub 0");
    }
    public static void ValidareLivrare(DateTime Livrare, DateTime Detalii)
    {
        DateTime prezent=DateTime.Now.Date;
        DateTime max= Detalii.Date.AddMonths(2);
        if (Livrare < prezent)
            throw new ArgumentException("Data livrare imposibila");
        if (Livrare < Detalii.Date)
            throw new ArgumentException("Data nu poate fi mai devreme decat data initiala");
        if (Livrare > max)
            throw new ArgumentException("Data nu poate fii mai tarziu de 2 luni decat data initiala");
    }
    public static void ValidareCantitate(int Cantitae, int Stoc)
    {
        int Scazut=Math.Abs(Cantitae);
        if (Cantitae <= 0 && Scazut >= Stoc)
            throw new ArgumentException("Cantitatea scazuta depaseste stocul");
    }
    public static void ValidareConditii(string Condititii)
    {
        if (string.IsNullOrEmpty(Condititii))
            throw new ArgumentException("Conditiile nu pot fi goale");
        if (!Regex.IsMatch(Condititii, @"^[a-zA-Z9]+$"))
            throw new ArgumentException("Conditiile pot avea doar litere");
    }
}