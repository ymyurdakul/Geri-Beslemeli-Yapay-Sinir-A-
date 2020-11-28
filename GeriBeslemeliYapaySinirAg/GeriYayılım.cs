using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeriBeslemeliYapaySinirAg
{
    class GeriYayılım
    {

        public static void GizliKatmanSinapsGüncelle(Sinaps sinaps, double hedef, double gelen, double oxÇıkış, double hxÇıkış)
        {
            double x = (gelen - hedef) * Fonksiyonlar.sigmoid_türev(oxÇıkış) * hxÇıkış;
            sinaps.Ağırlık = sinaps.Ağırlık - 0.5 * x;
        }
        public static void girisKatmanSinapsGüncelle(Sinaps sinaps,
                                                    double o1hedef, double o1gelen,
                                                    double o2hedef, double o2gelen,
                                                    double o3hedef, double o3gelen,
                                                    double o4hedef,double o4gelen,
                                                    double o5hedef,double o5gelen,
                                                    Nöron o1, Nöron o2,Nöron o3,Nöron o4,Nöron o5)
        {
            //o1 hata hesaplama
            double eox = 0.0;
            sinaps.ÇıktıNöron.ÇıkanSinapslar.ForEach(sin =>
            {
                if (sin.ÇıktıNöron == o1)
                {
                    eox = sin.Ağırlık;
                    
                }

            });
            double türev = Fonksiyonlar.sigmoid_türev(o1.Çıkış);
            double hata1 = o1.YanlışlıkMiktarı * türev;
            hata1 *= eox;

            //o2 hata

            double eox2 = 0.0;
            sinaps.ÇıktıNöron.ÇıkanSinapslar.ForEach(sin =>
            {
                if (sin.ÇıktıNöron == o2)
                {
                    eox2 = sin.Ağırlık;
                  
                }

            });
            double türev2 = Fonksiyonlar.sigmoid_türev(o2.Çıkış);
            double hata2 = o2.YanlışlıkMiktarı * türev2*eox2;
     


           //o3

            double eox3 = 0.0;
            sinaps.ÇıktıNöron.ÇıkanSinapslar.ForEach(sin =>
            {
                if (sin.ÇıktıNöron == o3)
                {
                    eox3 = sin.Ağırlık;
                     
                }

            });
            double türev3 = Fonksiyonlar.sigmoid_türev(o3.Çıkış);
            double hata3 = o3.YanlışlıkMiktarı * türev3*eox3;
            
          
            //04

            double eox4 = 0.0;
            sinaps.ÇıktıNöron.ÇıkanSinapslar.ForEach(sin =>
            {
                if (sin.ÇıktıNöron == o4)
                {
                    eox4 = sin.Ağırlık;
                    //      MessageBox.Show("Eox" + eox2);
                }

            });
            double türev4 = Fonksiyonlar.sigmoid_türev(o4.Çıkış);
            double hata4 = o4.YanlışlıkMiktarı * türev4*eox4;
             
            
            //o5

            double eox5 = 0.0;
            sinaps.ÇıktıNöron.ÇıkanSinapslar.ForEach(sin =>
            {
                if (sin.ÇıktıNöron == o5)
                {
                    eox5 = sin.Ağırlık;
                    //      MessageBox.Show("Eox" + eox2);
                }

            });
            double türev5 = Fonksiyonlar.sigmoid_türev(o5.Çıkış);
            double hata5 = o5.YanlışlıkMiktarı * türev5*eox5;
          
           

            //devam


            double toplamHata = hata1 + hata2+hata3+hata4+hata5;

            double çıktıtürev = Fonksiyonlar.sigmoid_türev(sinaps.ÇıktıNöron.Çıkış);
            double girdi = sinaps.GirdiNöron.Çıkış;

            double rx = toplamHata * çıktıtürev * girdi;
            sinaps.Ağırlık -=( 0.5 * rx);

        }
    }
}
