using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeriBeslemeliYapaySinirAg
{
    class Nöron
    {
        public double Net { get; set; }
        //Aktivasyon fonksiyonu sonrası için
        public double Çıkış { get; set; }
        public List<Sinaps> GirenSinapslar { get; set; }
        public List<Sinaps> ÇıkanSinapslar { get; set; }
        public double YanlışlıkMiktarı { get; set; }

        //KAre alarak
        public double HataMiktarı { get; set; }
        public Nöron()
        {
            GirenSinapslar = new List<Sinaps>();
            ÇıkanSinapslar = new List<Sinaps>();

        }
        public void netHesapla()
        {
            Net = 0;
            GirenSinapslar.ForEach(sinaps =>
            {
                Net += (sinaps.Ağırlık * sinaps.GirdiNöron.Çıkış);
            });
        }
        public void çıkışHesapla()
        {
            Çıkış = Fonksiyonlar.sigmoid(Net);

        }
        public void girenSinapsEkle(Sinaps sinaps)
        {
            this.GirenSinapslar.Add(sinaps);
        }
        public void çıkanSinapsEkle(Sinaps sinaps)
        {
            this.ÇıkanSinapslar.Add(sinaps);
        }
        public void hataHesapla(double beklenen)
        {
            this.YanlışlıkMiktarı = Çıkış - beklenen;
        }
        public void HataKareHesapla(double beklenen)
        {
            this.HataMiktarı = 0.5 * (Math.Pow(beklenen - Çıkış, 2));
        }
    }
}
