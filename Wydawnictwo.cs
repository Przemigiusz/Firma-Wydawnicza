using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Firma_wydawnicza
{
    class Wydawnictwo
    {
        private string filePath = @"ścieżka";

        private Application excel = new Application();

        private Workbook Wb;
        private Worksheet Ws_clients;
        private Worksheet Ws_institutions;



        private List<Klient> klienci;
        private int liczba_dzwyklych;
        public int Liczba_dzwyklych
        {
            get { return liczba_dzwyklych;  }
        }
        private int liczba_dkolorowych;
        public int Liczba_dkolorowych
        {
            get { return liczba_dkolorowych; }
        }
        private int liczba_dcyfrowych;
        public int Liczba_dcyfrowych
        {
            get { return liczba_dcyfrowych; }
        }
        public List<Klient> Klienci
        {
            get { return klienci; }
        }
        /*private List<Drukarnia> drukarnie;
        public List<Drukarnia> Drukarnie
        {
            get { return drukarnie; }
        }*/
        private string nazwaWydawnictwa;

        public Wydawnictwo(string NazwaWydawnictwa)
        {
            this.nazwaWydawnictwa = NazwaWydawnictwa;
            klienci = new List<Klient>();
        }

        public void dodajAutora(AutorKsiazki klient)    //dodawanie nowego klienta-autora
        {
            klienci.Add(new AutorKsiazki(klient));
        }
        public void dodajInstytucje(Instytucja klient)  //dodawanie nowego klienta-instytucji
        {
            klienci.Add(new Instytucja(klient));
        }
        public void dodajDrukarnie(Drukarnia drukarnia) //dodawanie nowej drukarni
        {
            //drukarnie.Add(new Drukarnia(drukarnia));
            if (drukarnia is DrukarniaZwykla) liczba_dzwyklych++;
            else if (drukarnia is DrukarniaKolorowa) liczba_dkolorowych++;
            else liczba_dcyfrowych++;
        }
        //obsługa pakietu Microsoft.Office.Interop.Excel
        public void OpeningFile()
        {
            Wb = excel.Workbooks.Open(filePath);
        }

        public void Loading()
        {
            Ws_clients = Wb.Worksheets[1];
            Ws_institutions = Wb.Worksheets[2];
        }

        public void CreateNewSheets()
        {
            Worksheet tempSheet = Wb.Worksheets.Add();
        }

        public void ReadCell_clients()
        {
            OpeningFile();
            Loading();
            int number_of_rows = Ws_clients.UsedRange.Rows.Count;
            for (int row = 1, column = 1; row <= number_of_rows; row++)
            {
                if (Ws_clients.Cells[row, column].Value2 != null && Ws_clients.Cells[row, column + 1].Value2 != null)
                {
                    this.dodajAutora(new AutorKsiazki(Ws_clients.Cells[row, column].Text, Ws_clients.Cells[row, column + 1].Text));
                }
            }
            ClosingAndSaving();
        }

        public void WriteCell_clients(string imie, string nazwisko)
        {
            OpeningFile();
            Loading();
            int number_of_rows = Ws_clients.UsedRange.Rows.Count;
            if (number_of_rows == 1 && Ws_clients.Cells[1, 1].Value2 == null && Ws_clients.Cells[1, 2].Value2 == null)
            {
                Ws_clients.Cells[number_of_rows, 1] = imie;
                Ws_clients.Cells[number_of_rows, 2] = nazwisko;
            }
            else
            {
                number_of_rows++;
                Ws_clients.Cells[number_of_rows, 1] = imie;
                Ws_clients.Cells[number_of_rows, 2] = nazwisko;
            }
            ClosingAndSaving();
        }

        public void ReadCell_institutions()
        {
            OpeningFile();
            Loading();
            int number_of_rows = Ws_institutions.UsedRange.Rows.Count;
            for (int row = 1, column = 1; row <= number_of_rows; row++)
            {
                if (Ws_institutions.Cells[row, column].Value2 != null)
                {
                    this.dodajInstytucje(new Instytucja(Ws_institutions.Cells[row, column].Text));
                }

            }
            ClosingAndSaving();
        }


        public void WriteCell_institutions(string nazwa)
        {
            OpeningFile();
            Loading();
            int number_of_rows = Ws_institutions.UsedRange.Rows.Count;
            if (number_of_rows == 1 && Ws_institutions.Cells[1, 1].Value2 == null && Ws_institutions.Cells[1, 2].Value2 == null)
            {
                Ws_institutions.Cells[number_of_rows, 1] = nazwa;
            }
            else
            {
                number_of_rows++;
                Ws_institutions.Cells[number_of_rows, 1] = nazwa;
            }
            ClosingAndSaving();
        }

        public void ClosingAndSaving()
        {
            excel.DisplayAlerts = false;
            Wb.SaveAs(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
            Wb.Close();
        }
        public override string ToString()
        {
            return nazwaWydawnictwa;
        }
    }
    abstract class Drukarnia
    {
        protected double cenaStrona;
        protected double CenaStrona
        {
            get { return cenaStrona; }
        }
        public Drukarnia(double wartosc)
        {
            cenaStrona = wartosc;
        }
        public Drukarnia(Drukarnia d)
        {
            this.cenaStrona = d.CenaStrona;
        }

        public override string ToString()
        {
            return "Drukarnia";
        }
    }
    class DrukarniaZwykla : Drukarnia
    {
        public DrukarniaZwykla(double wartosc) : base(wartosc) { }

        public override string ToString()
        {
            return "Drukarnia zwykla";
        }

        public double Wartosc
        {
            get {return CenaStrona; }
        }
    }
    class DrukarniaKolorowa : Drukarnia
    {
        public DrukarniaKolorowa(double wartosc) : base(wartosc) { }
        public override string ToString()
        {
            return "Drukarnia kolorowa";
        }
        public double Wartosc
        {
            get { return CenaStrona; }
        }
    }
    class DrukarniaCyfrowa : Drukarnia
    {
        public DrukarniaCyfrowa(double wartosc) : base(wartosc) { }

        public override string ToString()
        {
            return "Drukarnia cyfrowa";
        }

        public double Wartosc
        {
            get { return CenaStrona; }
        }
    }

}