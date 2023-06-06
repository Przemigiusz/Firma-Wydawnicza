using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma_wydawnicza
{
    class Zamowienie
    {
        protected MaterialyDoDruku material;
        public MaterialyDoDruku Material
        {
            get { return material; }
        }
        protected int naklad;
        public int Naklad
        {
            get { return naklad; }
            set { naklad = value; }
        }
        protected string termin;
        public string Termin
        {
            get { return termin; }
            set { termin = value; }
        }
        protected double cena;
        public double Cena
        {
            get { return cena; }
            set { cena = value; }
        }
        private StanZamowien stan;
        public StanZamowien Stan
        {
            get { return stan; }
            set { stan = value; }
        }
        public Zamowienie(MaterialyDoDruku material, int naklad, string termin, double cena)
        {
            this.material = material;
            this.naklad = naklad;
            this.termin = termin;
            this.cena = cena;
            this.stan = new StanZamowien();
        }

        public Zamowienie(Zamowienie zamowienie)
        {
            this.material = zamowienie.material;
            this.naklad = zamowienie.naklad;
            this.termin = zamowienie.termin;
            this.cena = zamowienie.cena;
            this.stan = new StanZamowien();
        }
        public void anuluj() //anulowanie zamówienia
        {
            stan.anuluj();
        }
        public void kolejny() //zmiana stanu zamówienia na kolejny etap
        {
            stan.kolejny();
        }
        public override string ToString()
        {
            return "Material: " + material + " Naklad: " + naklad + " Termin: " + termin + " Cena: " + cena + " Stan zamowienia: " + stan;
        }
    }
    class MaterialyDoDruku
    {
        protected string autor;
        public string Autor
        {
            get { return autor; }
        }
        protected string tytul;
        public string Tytul
        {
            get { return tytul; }
        }
        protected int liczbaStron;
        public int LiczbaStron
        {
            get { return liczbaStron; }
        }
        protected int rodzajPapieru;
        public int RodzajPapieru
        {
            get { return rodzajPapieru; }
        }

        public MaterialyDoDruku(string autor, string tytul, int liczbaStron, int rodzajPapieru)
        {
            this.autor = autor;
            this.tytul = tytul;
            this.liczbaStron = liczbaStron;
            this.rodzajPapieru = rodzajPapieru;
        }

        public MaterialyDoDruku(MaterialyDoDruku materialy)
        {
            this.autor = materialy.autor;
            this.tytul = materialy.tytul;
            this.liczbaStron = materialy.LiczbaStron;
            this.rodzajPapieru = materialy.rodzajPapieru;
        }
        public override string ToString()
        {
            return tytul + " - " + autor;
        }
    }
    class PozycjeKsiazkowe : MaterialyDoDruku
    {
        private bool kolor;
        public PozycjeKsiazkowe(string autor, string tytul, int liczbaStron, int rodzajPapieru, bool kolor) : base(autor, tytul, liczbaStron, rodzajPapieru)
        {
            this.kolor = kolor;
        }
    }
    class PozycjeCyfrowe : MaterialyDoDruku
    {
        public PozycjeCyfrowe(string autor, string tytul, int liczbaStron, int rodzajPapieru) : base(autor, tytul, liczbaStron, rodzajPapieru) { }
    }
    class Czasopisma : MaterialyDoDruku
    {
        private int typ;
        public Czasopisma(string autor, string tytul, int liczbaStron, int rodzajPapieru, int typ) : base(autor, tytul, liczbaStron, rodzajPapieru)
        {
            this.typ = typ;
        }
    }
    class StanZamowien
    {
        private int numer;

        public StanZamowien()
        {
            this.numer = 0;
        }
                
        public override string ToString()
        {
            switch(numer)
            {
                case 0:
                    return "Przyjęto do realizacji.";
                case 1:
                    return "W trakcie realizacji.";
                case 2:
                    return "Zrealizowano.";
                case 3:
                    return "Anulowano.";
                default:
                    return null;
            }
        }
        public void anuluj()    //ustawienie zamówienia na stan anulowano
        {
            numer = 3;
        }
        public void kolejny()   //ustawienia zamówienia na następny stan
        {
            numer++;
        }
    }
}
