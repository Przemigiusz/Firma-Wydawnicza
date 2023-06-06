using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma_wydawnicza
{
    abstract class Klient
    {
        protected static int ID = 1;
        protected int my_id;
        public int My_id
        {
            get { return my_id; }
        }
        protected List<Zamowienie> listaZamowien;
        public List<Zamowienie> ListaZamowien
        {
            get { return listaZamowien; }
        }
        protected bool staly;
        public void dodajZamowienie(Zamowienie z)   //dodawanie zamówienia
        {
            listaZamowien.Add(new Zamowienie(z));
        }
        public void anulujZamowienie(Zamowienie z)  //anulowanie zamówienia
        {
            z.anuluj();
        }
    }
    class AutorKsiazki : Klient
    {
        private string imie, nazwisko;
        public string Imie
        {
            get { return imie; }
        }
        public string Nazwisko
        {
            get { return nazwisko; }
        }
        public AutorKsiazki(string imie, string nazwisko)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            staly = false;
            listaZamowien = new List<Zamowienie>();
        }
        public AutorKsiazki(AutorKsiazki ak)
        {
            this.imie = ak.Imie;
            this.nazwisko = ak.Nazwisko;
            staly = false;
            my_id = ID;
            ID++;
            listaZamowien = new List<Zamowienie>();
        }
        public override string ToString()
        {
            return $"Imie: {imie} Nazwisko: {nazwisko} ID: {my_id}";
        }
    }
    class Instytucja : Klient
    {
        private string nazwaInstytucji;
        public string NazwaInstytucji
        {
            get { return nazwaInstytucji; }
        }
        public Instytucja(string nazwa)
        {
            this.nazwaInstytucji = nazwa;
            staly = false;
            listaZamowien = new List<Zamowienie>();
        }
        public Instytucja(Instytucja i)
        {
            this.nazwaInstytucji = i.NazwaInstytucji;
            staly = false;
            my_id = ID;
            ID++;
            listaZamowien = new List<Zamowienie>();
        }
        public override string ToString()
        {
            return $"Nazwa instytucji: {NazwaInstytucji} ID: {my_id}";
        }
    }
}
