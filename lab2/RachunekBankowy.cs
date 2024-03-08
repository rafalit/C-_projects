
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace lab2
{
    public class RachunekBankowy : Object
    {
        private string numer;
        private decimal stanRachunku;
        private bool czyDozwolonyDebet;
        List<PosiadaczRachunku> _PosiadaczeRachunku = new List<PosiadaczRachunku>();
        List<Transakcja> _Transakcje = new List<Transakcja>();

        public RachunekBankowy(string n, decimal s, bool czy, List<PosiadaczRachunku> _posiadaczerachunku_)
        {
            numer = n;
            stanRachunku = s;
            czyDozwolonyDebet = czy;

            if (_posiadaczerachunku_.Count == 0)
            {
                throw new Exception("Brak elementów na liście!");
            }
            PosiadaczeRachunku = _posiadaczerachunku_;
        }

        public string Numer
        {
            get => numer;
            set => numer = value;
        }

        public decimal StanRachunku
        {
            get => stanRachunku;
            set => stanRachunku = value;
        }

        public bool CzyDozwolonyDebet
        {
            get => czyDozwolonyDebet;
            set => czyDozwolonyDebet = value;
        }

        public List<PosiadaczRachunku> PosiadaczeRachunku
        {
            get => _PosiadaczeRachunku;
            set => _PosiadaczeRachunku = value;
        }

        public List<Transakcja> Transakcje
        {
            get => _Transakcje;
        }

        public static void DokonajTransakcji(RachunekBankowy rachunekZrodlowy, RachunekBankowy rachunekDocelowy, decimal kwota, string opis)
        {
            if (kwota < 0 || (rachunekZrodlowy == null && rachunekDocelowy == null) || (rachunekZrodlowy.CzyDozwolonyDebet == false && kwota > rachunekZrodlowy.StanRachunku))
            {
                throw new Exception("Nie udało się! Możliwe przyczyny: ujemna kwota, brak podanych rachunków, nie dozwolony debet");
            }
            else
            {

                if (rachunekZrodlowy == null)
                {
                    rachunekDocelowy.StanRachunku += kwota;
                    Transakcja t = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
                    rachunekZrodlowy.DodajTransakcje(t);
                }

                else if (rachunekDocelowy == null)
                {
                    rachunekZrodlowy.StanRachunku -= kwota;
                    Transakcja t = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
                    rachunekDocelowy.DodajTransakcje(t);
                }

                else
                {
                    rachunekZrodlowy.StanRachunku -= kwota;
                    rachunekDocelowy.StanRachunku += kwota;
                    Transakcja t = new Transakcja(rachunekZrodlowy, rachunekDocelowy, kwota, opis);
                    rachunekDocelowy.DodajTransakcje(t);
                    rachunekZrodlowy.DodajTransakcje(t);
                }
            }
        }

        private void DodajTransakcje(Transakcja transakcja)
        {
            _Transakcje.Add(transakcja);
        }

        //zadanie dodatkowe

        public static RachunekBankowy operator+(RachunekBankowy dodaj, PosiadaczRachunku jest)
        {
            if(dodaj.PosiadaczeRachunku.Contains(jest))
            {
                throw new Exception("Podana osoba jest już w bazie!");
            }
            dodaj.PosiadaczeRachunku.Add(jest);
            return dodaj;
        }

        public static RachunekBankowy operator-(RachunekBankowy usun, PosiadaczRachunku jest)
        {
            if(usun.PosiadaczeRachunku.Count == 1)
            {
                throw new Exception("Lista nie może być pusta");
            }
            if(!usun.PosiadaczeRachunku.Contains(jest))
            {
                throw new Exception("Nie ma takiej osoby w bazie!");
            }
            usun.PosiadaczeRachunku.Remove(jest);
            return usun;

        }

        //zadanie dodatkowe
        public override string ToString()
        {
            string debet = czyDozwolonyDebet ? "i możliwym debecie" : "bez możliwości debetu";

            string PosiadaczeString = string.Join(", ", PosiadaczeRachunku.Select(p => p.ToString()));

            string transakcjeString = string.Join(" ", _Transakcje
                .Where(t => t.RachunekZrodlowy?.Numer == numer || t.RachunekDocelowy?.Numer == numer)
                .Select(t => t.ToString()));


            return $"Rachunek Bankowy {numer} o kwocie {StanRachunku} {debet} [{PosiadaczeString}] \n {transakcjeString}\n";
        }
    }
}