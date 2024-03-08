
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2
{
    public class Transakcja : Object
    {
        private RachunekBankowy? rachunekZrodlowy;
        private RachunekBankowy? rachunekDocelowy;
        private decimal kwota;
        private string opis;

        public RachunekBankowy? RachunekZrodlowy
        {
            get => rachunekZrodlowy;
            set => rachunekZrodlowy = value;
        }

        public RachunekBankowy? RachunekDocelowy
        {
            get=> rachunekDocelowy;
            set => rachunekDocelowy = value;
        }

        public decimal Kwota
        {
            get => kwota;
            set 
            {
                if(kwota<=0)
                {
                    throw new Exception("Kwota musi być dodatnia");
                }
                kwota=value;
            }
        }

        public string Opis
        {
            get => opis;
            set => opis=value;
        }

        public Transakcja(RachunekBankowy? z, RachunekBankowy? d, decimal k, string o) 
        {
            rachunekZrodlowy = z;
            rachunekDocelowy = d;
            kwota = k;
            opis = o;

            if (z == null || d == null)
            {
                throw new Exception("Rachunki muszą zawierać wartośći!");
            }
            if(kwota <= 0)
            {
                throw new Exception("Transakcja musi zawierać dodatnią kwotę!");
            }
        }
        //zadanie dodatkowe
        public override string ToString()
        {
            return $"Transakcja {rachunekZrodlowy?.Numer} -> {rachunekDocelowy?.Numer} kwota: {kwota}, opis: {opis}\n";
        }
    }
}