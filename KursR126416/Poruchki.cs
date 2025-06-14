using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KursR126416
{
    public class Poruchki
    {
        private static int nomer = 100;

        public int NKasovBon { get; set; }
        public DateTime Data {  get; set; }
        public string NaimenovanieStoka {  get; set; }
        public string TipStoka { get; set; }
        public int Kolichestvo {  get; set; }
        public double EdCena { get; set; }
        public double Suma => Kolichestvo * EdCena;

        public Poruchki(string naimenovanie, string tip, int kolichestvo, double edcena)
        {
            NKasovBon = nomer++;
            Data = DateTime.Now.Date;
            NaimenovanieStoka = naimenovanie;
            TipStoka = tip;
            Kolichestvo = kolichestvo;
            EdCena = edcena;
        }
        public Poruchki(int nBon, DateTime data, string naim, string tip, int kolichestvo, double edcena)
        {
            NKasovBon = nBon;
            Data = data;
            NaimenovanieStoka = naim;
            TipStoka = tip;
            Kolichestvo = kolichestvo;
            EdCena = edcena;
        }

    }
}
