using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    class OsobaFizyczna : PosiadaczRachunku
    {
        private string imie;
        private string nazwisko;
        private string drugieImie;
        private string pesel;
        private string numer_Paszportu;

        public OsobaFizyczna(string imie_, string nazwisko_, string drugieImie_, string pesel_, string numer_Paszportu_)
        {
            imie = imie_;
            nazwisko = nazwisko_;
            drugieImie = drugieImie_;
            pesel = pesel_;
            numer_Paszportu = numer_Paszportu_;

            if (pesel == "" && numer_Paszportu == "")
            {
                throw new Exception("PESEL albo numer paszportu musza być nie null");
            }
            //zadanie dodatkowe
            if (pesel == null || pesel.Length != 11)
            {
                throw new Exception("Pesel musi mieć 11 cyfr!");
            }
        }

        public string Imie
        {
            get => imie ?? "";
            set => imie = value;
        }

        public string Nazwisko
        {
            get => nazwisko ?? "";
            set => nazwisko = value;
        }

        public string DrugieImie
        {
            get => drugieImie ?? "";
            set => drugieImie = value;
        }

        public string Pesel
        {
            get => pesel ?? "";
            set 
            {
                //zadanie dodatkowe
                if(value.Length != 11)
                {
                    throw new Exception("Pesel musi mieć długość 11 znaków!");
                }
                pesel=value;
            }

        }

        public string Numer_Paszportu
        {
            get => numer_Paszportu ?? "";
            set => numer_Paszportu = value;
        }

        public override string ToString()
        {
            return imie+" "+nazwisko;
        }
    }
}