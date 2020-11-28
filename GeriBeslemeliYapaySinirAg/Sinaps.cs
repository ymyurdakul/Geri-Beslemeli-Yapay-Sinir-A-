using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeriBeslemeliYapaySinirAg
{
    //Bağlantı için yazıldı
    class Sinaps
    {

        public Nöron GirdiNöron { get; set; }
        public Nöron ÇıktıNöron { get; set; }
        public double Ağırlık { get; set; }
        public Sinaps(Nöron girdi, Nöron çıktı, double ağırlık)
        {
            this.GirdiNöron = girdi;
            this.ÇıktıNöron = çıktı;
            this.Ağırlık = ağırlık;
            çıktı.girenSinapsEkle(this);
        }
    }
}
