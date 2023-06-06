using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Firma_wydawnicza
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nacisnij dowolny przycisk aby rozpoczac...");
            Console.ReadKey();
            Console.Clear();
            UserInterface MyUI = new UserInterface();
        }
    }

    class UserInterface
    {
        private Wydawnictwo wydawnictwo;
        private int Button;
        private double CenaZwykla;
        private double CenaKolorowa;
        private double CenaCyfrowa;

        public UserInterface()
        {
            //Tworzenie wydawnictwa
            Console.WriteLine("Podaj nazwe wydawnictwa: ");
            var TemporaryName = Convert.ToString(Console.ReadLine());
            if (TemporaryName.Length != 0) {
                this.wydawnictwo = new Wydawnictwo(TemporaryName);
                this.wydawnictwo.ReadCell_clients();
                this.wydawnictwo.ReadCell_institutions();
            }            
            else
            {
                while (true)
                {
                    TemporaryName = Convert.ToString(Console.ReadLine());
                    if (TemporaryName.Length != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Podaj poprawna nazwe!");
                        Console.WriteLine("Podaj nazwe wydawnictwa: ");
                        this.wydawnictwo = new Wydawnictwo(TemporaryName);
                        this.wydawnictwo.ReadCell_clients();
                        this.wydawnictwo.ReadCell_institutions();
                        break;
                    }
                }
            }
            //Tworzenie cennika danych drukarni
            Console.WriteLine("Podaj cennik za strone drukarni zwyklej:");
            while (true)
            {
                string tempCena = Console.ReadLine();
                tempCena = tempCena.Replace(".", ",");
                if (double.Parse(tempCena) > 0)
                {
                    CenaZwykla = double.Parse(tempCena);
                    break;
                }
                Console.WriteLine("Nieprawidlowa cena! Podaj wartosc jeszcze raz.");
            }
            Console.WriteLine("Podaj cennik za strone drukarni kolorowej:");
            while (true)
            {
                string tempCena = Console.ReadLine();
                tempCena = tempCena.Replace(".", ",");
                if (double.Parse(tempCena) > 0)
                {
                    CenaKolorowa = double.Parse(tempCena);
                    break;
                }
                Console.WriteLine("Nieprawidlowa cena! Podaj wartosc jeszcze raz.");
            }
            Console.WriteLine("Podaj cennik za strone drukarni cyfrowa:");
            while (true)
            {
                string tempCena = Console.ReadLine();
                tempCena = tempCena.Replace(".", ",");
                if (double.Parse(tempCena) > 0)
                {
                    CenaCyfrowa = double.Parse(tempCena);
                    break;
                }
                Console.WriteLine("Nieprawidlowa cena! Podaj wartosc jeszcze raz.");
            }
            Menu();
        }

        public void Menu()  //menu główne
        {
            bool wyjscie = false;
            while (!wyjscie)
            {
                Console.Clear();
                Console.SetCursorPosition((Console.WindowWidth - wydawnictwo.ToString().Length) / 2, Console.CursorTop);
                Console.WriteLine(wydawnictwo);
                Console.WriteLine("1.Wyswietl menu drukarni.");
                Console.WriteLine("2.Wyswietl menu klientow.");
                Console.WriteLine("3.Zakoncz program.");
                Console.WriteLine("Prosze podac nr sekcji ktora cie interesuje.");
                Button = Convert.ToInt32(Console.ReadLine());
                switch (Button)
                {
                    case 1:
                        //obsługa drukarni
                        MenuDrukarnia();
                        break;
                    case 2:
                        //obsługa klientów
                        MenuKlienci();
                        break;
                    case 3:
                        //koniec programu
                        wyjscie = true;
                        //Environment.Exit(0);
                        break;
                    default:
                        //Menu();
                        break;
                }
            }
        }

        public void MenuDrukarnia() //menu obsługi drukarni
        { 
            bool wyjscie = false;
            while (!wyjscie)
            {
                Console.Clear();
                Console.SetCursorPosition((Console.WindowWidth - wydawnictwo.ToString().Length) / 2, Console.CursorTop);
                Console.WriteLine(wydawnictwo);
                Console.WriteLine("1.Wyswietl wszystkie dostepne drukarnie.");
                Console.WriteLine("2.Dodaj drukarnie.");
                Console.WriteLine("3.Cofnij sie.");
                Console.WriteLine("Prosze podac nr sekcji ktora cie interesuje:");
                try
                {
                    Button = Convert.ToInt32(Console.ReadLine());
                    switch (Button)
                    {
                        case 1:
                            //wyświetlenie drukarni
                            Console.WriteLine("Liczba drukarni zwyklych: " + wydawnictwo.Liczba_dzwyklych);
                            Console.WriteLine("Liczba drukarni kolorowych: " + wydawnictwo.Liczba_dkolorowych);
                            Console.WriteLine("Liczba drukarni cyfrowych: " + wydawnictwo.Liczba_dcyfrowych);
                            Console.WriteLine("Nacisnij dowolny przycisk aby przejsc dalej.");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 2:
                            //dodawanie drukarni
                            Console.Clear();
                            Console.WriteLine("Jaka drukarnie chcesz wybrac?");
                            Console.WriteLine("1.Drukarnia zwykla");
                            Console.WriteLine("2.Drukarnia kolorowa");
                            Console.WriteLine("3.Drukarnia cyfrowa");
                            Console.WriteLine("Prosze podac nr sekcji ktora cie interesuje:");
                            Button = Convert.ToInt32(Console.ReadLine());
                            switch (Button)
                            {
                                case 1:
                                    wydawnictwo.dodajDrukarnie(new DrukarniaZwykla(CenaZwykla));
                                    break;
                                case 2:
                                    wydawnictwo.dodajDrukarnie(new DrukarniaKolorowa(CenaKolorowa));
                                    break;
                                case 3:
                                    wydawnictwo.dodajDrukarnie(new DrukarniaCyfrowa(CenaCyfrowa));
                                    break;
                                default:
                                    //MenuDrukarnia();
                                    break;
                            }
                            break;
                        case 3:
                            //powrót do menu głównego
                            wyjscie = true;
                            //Menu();
                            break;
                        default:
                            //MenuDrukarnia();
                            break;
                    }
                }
                catch (Exception) { }
            }
        }
        public void MenuKlienci() //menu obsługi klientów
        {
            bool wyjscie = false;
            while (!wyjscie)
            {
                Console.Clear();
                Console.SetCursorPosition((Console.WindowWidth - wydawnictwo.ToString().Length) / 2, Console.CursorTop);
                Console.WriteLine(wydawnictwo);
                Console.WriteLine("1.Wyswietl wszystkich klientow tego wydawnictwa.");
                Console.WriteLine("2.Dodaj klienta.");
                Console.WriteLine("3.Dodaj zamowienie do koszyka klienta.");
                Console.WriteLine("4.Wyswietl koszyk klienta.");
                Console.WriteLine("5.Cofnij sie.");
                Console.WriteLine("Prosze podac nr sekcji ktora cie interesuje:");
                Button = Convert.ToInt32(Console.ReadLine());
                switch (Button)
                {
                    case 1:
                        //wyświetlenie klientów
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - "Lista klientow wydawnictwa: ".Length) / 2, Console.CursorTop);
                        Console.WriteLine("Lista klientow wydawnictwa: ");
                        List<Klient> Clients = wydawnictwo.Klienci;
                        foreach (Klient Client in Clients)
                        {
                            Console.WriteLine(Client);
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        //dodawanie klienta
                        Console.Clear();
                        Console.WriteLine("Jaki rodzaj klienta chcesz dodac?");
                        Console.WriteLine("1.Autor ksiazki");
                        Console.WriteLine("2.Instytucja");
                        Console.WriteLine("3.Cofnij sie");
                        Console.WriteLine("Prosze podac nr sekcji ktora cie interesuje:");
                        Button = Convert.ToInt32(Console.ReadLine());
                        switch (Button)
                        {
                            case 1:
                                //dodawanie autora
                                Console.WriteLine("Podaj imie klienta.");
                                string Imie = Console.ReadLine();
                                Console.WriteLine("Podaj nazwisko klienta.");
                                string Nazwisko = Console.ReadLine();
                                wydawnictwo.dodajAutora(new AutorKsiazki(Imie, Nazwisko));
                                wydawnictwo.WriteCell_clients(Imie, Nazwisko);
                                break;
                            case 2:
                                //dodawanie instytucji
                                Console.WriteLine("Podaj nazwe instytucji.");
                                string NazwaInstytucji = Console.ReadLine();
                                wydawnictwo.dodajInstytucje(new Instytucja(NazwaInstytucji));
                                wydawnictwo.WriteCell_institutions(NazwaInstytucji);
                                break;
                            default:
                                //MenuKlienci();
                                break;
                        }
                        break;
                    case 3:
                        //dodawanie zamówienia do klienta
                        Console.Clear();
                        Console.WriteLine("Wybierz ID klienta dla ktorego chcesz dodac zamowienie do koszyka.");
                        List<Klient> Clients2 = wydawnictwo.Klienci;
                        //wypisywanie klientow
                        foreach (Klient Client in Clients2)
                            Console.WriteLine(Client);
                        Console.Write(">>> ");
                        var IDcase3 = Console.ReadLine();
                        bool exist = false;
                        foreach (Klient Client in Clients2)
                        {
                            Console.Clear();
                            if (Convert.ToInt32(IDcase3) == Client.My_id)
                            {
                                exist = true;
                                Console.WriteLine("1.Pozycje ksiazkowe");
                                Console.WriteLine("2.Pozycje cyfrowe");
                                Console.WriteLine("3.Czasopisma");

                                Console.WriteLine("Na jaki material mamy wytworzyc naklad?");
                                Console.Write(">>> ");
                                var NrPozycji = Console.ReadLine();

                                Console.WriteLine("Jakiego nakladu potrzebuje klient?");
                                Console.Write(">>> ");
                                var ClientNaklad = Console.ReadLine();

                                Console.WriteLine("Prosze podac termin");
                                Console.Write(">>> ");

                                var ClientTermin = Console.ReadLine();

                                //Tu start wyliczania ceny

                                switch (Convert.ToInt32(NrPozycji))
                                {
                                    case 1:
                                        //dodawanie książki
                                        bool poprawny = false;
                                        int Kolor = 0;
                                        while (!poprawny)
                                        {
                                            Console.WriteLine("Czy ksiazka ma byc w kolorze?");
                                            Console.WriteLine("0/1");
                                            Console.Write(">>> ");
                                            string ClientKOLOR = Console.ReadLine();
                                            try
                                            {
                                                Kolor = Convert.ToInt32(ClientKOLOR);
                                                poprawny = true;
                                            }
                                            catch (Exception) { }
                                        }
                                        bool CzyKolor = Kolor == 1;
                                        Console.WriteLine("Prosze podac autora materialu");
                                        Console.Write(">>> ");
                                        string ClientAutor = Console.ReadLine();

                                        Console.WriteLine("Prosze podac tytul materialu");
                                        Console.Write(">>> ");
                                        string ClientTytul = Console.ReadLine();

                                        Console.WriteLine("Prosze podac liczbe stron materialu");
                                        Console.Write(">>> ");
                                        var ClientLiczbaStron = Console.ReadLine();

                                        PozycjeKsiazkowe ClientPozycja = new PozycjeKsiazkowe(ClientAutor, ClientTytul, Convert.ToInt32(ClientLiczbaStron), Kolor, CzyKolor);

                                        double ClientCenaStrona;
                                        double ClientCena;

                                        if (CzyKolor == true)
                                        {
                                            ClientCenaStrona = CenaKolorowa;
                                        }
                                        else
                                        {
                                            ClientCenaStrona = CenaZwykla;
                                        }

                                        ClientCena = Convert.ToInt32(ClientNaklad) * (Convert.ToInt32(ClientLiczbaStron) * ClientCenaStrona);

                                        Zamowienie ClientZamowienie = new Zamowienie(new MaterialyDoDruku(ClientPozycja), Convert.ToInt32(ClientNaklad), Convert.ToString(ClientTermin), ClientCena);

                                        Client.dodajZamowienie(new Zamowienie(ClientZamowienie));
                                        break;
                                    case 2:
                                        //dodawanie pozycji cyfrowej
                                        bool poprawny2 = false;
                                        int Kolor2 = 0;
                                        while (!poprawny2)
                                        {
                                            Console.WriteLine("Czy pozycja ma byc w kolorze?");
                                            Console.WriteLine("0/1");
                                            Console.Write(">>> ");
                                            string ClientKOLOR2 = Console.ReadLine();
                                            try
                                            {
                                                Kolor2 = Convert.ToInt32(ClientKOLOR2);
                                                poprawny2 = true;
                                            }
                                            catch (Exception) { }
                                        }
                                        bool CzyKolor2 = (Kolor2 == 1);
                                        Console.WriteLine("Prosze podac autora materialu");
                                        Console.Write(">>> ");
                                        string ClientAutor2 = Console.ReadLine();

                                        Console.WriteLine("Prosze podac tytul materialu");
                                        Console.Write(">>> ");
                                        string ClientTytul2 = Console.ReadLine();

                                        Console.WriteLine("Prosze podac liczbe stron materialu");
                                        Console.Write(">>> ");
                                        var ClientLiczbaStron2 = Console.ReadLine();

                                        PozycjeCyfrowe ClientPozycja2 = new PozycjeCyfrowe(ClientAutor2, ClientTytul2, Convert.ToInt32(ClientLiczbaStron2), Kolor2);

                                        double ClientCenaStrona2 = CenaCyfrowa;
                                        double ClientCena2;

                                        ClientCena2 = Convert.ToInt32(ClientNaklad) * (Convert.ToInt32(ClientLiczbaStron2) * ClientCenaStrona2);

                                        Zamowienie ClientZamowienie2 = new Zamowienie(new MaterialyDoDruku(ClientPozycja2), Convert.ToInt32(ClientNaklad), Convert.ToString(ClientTermin), ClientCena2);

                                        Client.dodajZamowienie(new Zamowienie(ClientZamowienie2));
                                        break;
                                    case 3:
                                        //dodawanie czasopisma
                                        bool poprawny3 = false;
                                        bool poprawnytyp = false;
                                        int Kolor3 = 0;
                                        int typ3 = 0;
                                        while (!poprawnytyp)
                                        {
                                            Console.WriteLine("Prosze wybrac typ czasopisma");
                                            Console.WriteLine("1.Tygodnik");
                                            Console.WriteLine("2.Miesiecznik");
                                            Console.Write(">>> ");
                                            string Typ3 = Console.ReadLine();
                                            try
                                            {
                                                typ3 = Convert.ToInt32(Typ3);
                                                poprawnytyp = true;
                                            }
                                            catch (Exception) { }
                                        }
                                        while (!poprawny3)
                                        {
                                            Console.WriteLine("Czy pozycja ma byc w kolorze?");
                                            Console.WriteLine("0/1");
                                            Console.Write(">>> ");
                                            string ClientKOLOR3 = Console.ReadLine();
                                            try
                                            {
                                                Kolor3 = Convert.ToInt32(ClientKOLOR3);
                                                poprawny3 = true;
                                            }
                                            catch (Exception) { }
                                        }
                                        bool CzyKolor3 = (Kolor3 == 1);
                                        Console.WriteLine("Prosze podac autora materialu");
                                        Console.Write(">>> ");
                                        string ClientAutor3 = Console.ReadLine();

                                        Console.WriteLine("Prosze podac tytul materialu");
                                        Console.Write(">>> ");
                                        string ClientTytul3 = Console.ReadLine();

                                        Console.WriteLine("Prosze podac liczbe stron materialu");
                                        Console.Write(">>> ");
                                        var ClientLiczbaStron3 = Console.ReadLine();

                                        Czasopisma ClientPozycja3 = new Czasopisma(ClientAutor3, ClientTytul3, Convert.ToInt32(ClientLiczbaStron3), Kolor3, typ3);

                                        double ClientCenaStrona3 = CenaCyfrowa;
                                        double ClientCena3;

                                        ClientCena3 = Convert.ToInt32(ClientNaklad) * (Convert.ToInt32(ClientLiczbaStron3) * ClientCenaStrona3);

                                        Zamowienie ClientZamowienie3 = new Zamowienie(new MaterialyDoDruku(ClientPozycja3), Convert.ToInt32(ClientNaklad), Convert.ToString(ClientTermin), ClientCena3);

                                        Client.dodajZamowienie(new Zamowienie(ClientZamowienie3));
                                        break;
                                    default:
                                        Console.WriteLine("Takiej pozycji nie ma!");
                                        Console.ReadKey();
                                        break;
                                }
                            }
                        }
                        if (!exist)
                        {
                            Console.WriteLine("Nie znaleziono takiego klienta!");
                            Console.ReadKey();
                        }
                        break;
                    case 4:
                        //wyświetlanie zamówień klienta
                        Console.Clear();
                        Console.WriteLine("Wybierz ID klienta ktorego koszyk chcesz wyswietlic.");
                        List<Klient> ClientsForOrder = wydawnictwo.Klienci;
                        //wypisywanie klientow
                        foreach (Klient Client in ClientsForOrder)
                            Console.WriteLine(Client);
                        Console.Write(">>> ");
                        var ID_Order = Console.ReadLine();
                        //wypisywanie zamowien
                        foreach (Klient Client in ClientsForOrder)
                        {
                            if (Convert.ToInt32(ID_Order) == Client.My_id)
                            {
                                Console.Clear();
                                Console.WriteLine($"Zamowienia: {Client}");
                                foreach(Zamowienie zam in Client.ListaZamowien)
                                {
                                    Console.WriteLine(zam);
                                }
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        //powrót do menu głównego
                        wyjscie = true;
                        //Menu();
                        break;
                    default:
                        //MenuKlienci();
                        break;
                }
            }
        }
    }
}

