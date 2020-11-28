using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GeriBeslemeliYapaySinirAg
{
    class SinirAgı
    {
        Nöron[] girişNöronArr;
        Nöron[] gizliNöronArr;
        Nöron o1, o2, o3, o4, o5;
        Nöron biasGizliNöron, biasÇıkışNöron;
        int GIZLI_NORON = 24;
        Random rnd;
        public List<double[]> eğitimVerisi;
        public  List<double[]> beklenenDegerler;
        public  double öğrenmeOranı;
        int epoch = 0;
        public SinirAgı(double öğrenmeOranı,int epoch)
        {
            this.epoch = epoch;
            this.öğrenmeOranı = öğrenmeOranı;
            rnd = new Random();
            girişNöronArr=new Nöron[35];
            gizliNöronArr=new Nöron[GIZLI_NORON];
            o1 = new Nöron();
            o2 = new Nöron();
            o3 = new Nöron();
            o4 = new Nöron();
            o5 = new Nöron();
            biasÇıkışNöron = new Nöron();
            biasGizliNöron = new Nöron();
            biasÇıkışNöron.Net = 1;
            biasÇıkışNöron.Çıkış = 1;

            biasGizliNöron.Net = 1;
            biasGizliNöron.Çıkış = 1;
            yükle();
        }
        public void yükle()
        {
           eğitimVerisi = new List<double[]>();
           beklenenDegerler = new List<double[]>();

           String[] satırlar=File.ReadAllLines("giris.txt");
           for (int i = 0; i < satırlar.Length; i++)
           {
              String []parcalanmis= satırlar[i].Split(',');
              double []temp=new double[35];
              for (int j = 0; j < 35; j++)
              {
                  temp[j]=Double.Parse(parcalanmis[j]);
              }
              eğitimVerisi.Add(temp);
           }

          String[]beklenenSatırlar=File.ReadAllLines("beklenen.txt");
          for (int i = 0; i < beklenenSatırlar.Length; i++)
          {
              String[] parcalanmis = beklenenSatırlar[i].Split(',');
              double[]temp=new double[5];
              for (int j = 0; j < 5; j++)
              {
                  temp[j] = Double.Parse(parcalanmis[j]);
              }
              beklenenDegerler.Add(temp);
          }

        }
        int index = 0;
        public void eğit()
        {
            
            kur(eğitimVerisi[0]);
           
                for (int i = 0; i < epoch; i++)
                {

                 

                    hesapla(index);
                     ağırlıkGüncelle(index);
                    
                    for (int x = 0; x < gizliNöronArr.Length; x++)
                    {
                        for (int jx = 0; jx < gizliNöronArr[x].ÇıkanSinapslar.Count; jx++)
                        {
                            Sinaps sinaps = gizliNöronArr[x].ÇıkanSinapslar[jx];
                            if (sinaps.ÇıktıNöron == o1)
                            {
                                GeriYayılım.GizliKatmanSinapsGüncelle(sinaps, beklenenDegerler[index][0], o1.Çıkış, o1.Çıkış, sinaps.GirdiNöron.Çıkış);
                            }
                            if (sinaps.ÇıktıNöron == o2)
                            {
                                GeriYayılım.GizliKatmanSinapsGüncelle(sinaps, beklenenDegerler[index][1], o2.Çıkış, o2.Çıkış, sinaps.GirdiNöron.Çıkış);
                            }
                            if (sinaps.ÇıktıNöron == o3)
                            {
                                GeriYayılım.GizliKatmanSinapsGüncelle(sinaps, beklenenDegerler[index][2], o3.Çıkış, o3.Çıkış, sinaps.GirdiNöron.Çıkış);
                            }
                            if (sinaps.ÇıktıNöron == o4)
                            {
                                GeriYayılım.GizliKatmanSinapsGüncelle(sinaps, beklenenDegerler[index][3], o4.Çıkış, o4.Çıkış, sinaps.GirdiNöron.Çıkış);
                            }
                            if (sinaps.ÇıktıNöron == o5)
                            {
                                GeriYayılım.GizliKatmanSinapsGüncelle(sinaps, beklenenDegerler[index][4], o5.Çıkış, o5.Çıkış, sinaps.GirdiNöron.Çıkış);
                            }
                        }
                    } 
                    index++;
                    if (index == 5)
                        index = 0;

               
            }
           
            

            girisMatrisDegiştir(eğitimVerisi[0]);
            hesapla(0);

            MessageBox.Show(o1.Çıkış+"\n"+ o2.Çıkış+"\n"+ o3.Çıkış+"\n"+o4.Çıkış+"\n"+o5.Çıkış);


        }
        private void hesapla(int index)
        {
            for (int j = 0; j < GIZLI_NORON; j++)
            {
                gizliNöronArr[j].netHesapla();
                gizliNöronArr[j].çıkışHesapla();

            }
            o1.netHesapla();
            o1.çıkışHesapla();

            o2.netHesapla();
            o2.çıkışHesapla();

            o3.netHesapla();
            o3.çıkışHesapla();

            o4.netHesapla();
            o4.çıkışHesapla();

            o5.netHesapla();
            o5.çıkışHesapla();

            o1.hataHesapla(beklenenDegerler[index][0]);
            o2.hataHesapla(beklenenDegerler[index][1]);
            o3.hataHesapla(beklenenDegerler[index][2]);
            o4.hataHesapla(beklenenDegerler[index][3]);
            o5.hataHesapla(beklenenDegerler[index][4]);

            o1.HataKareHesapla(beklenenDegerler[index][0]);
            o2.HataKareHesapla(beklenenDegerler[index][1]);
            o3.HataKareHesapla(beklenenDegerler[index][2]);
            o4.HataKareHesapla(beklenenDegerler[index][3]);
            o5.HataKareHesapla(beklenenDegerler[index][4]);
        }
        private void ağırlıkGüncelle(int index)
        {
            for (int i = 0; i < girişNöronArr.Length; i++)
            {
                girişNöronArr[i].ÇıkanSinapslar.ForEach(sinaps => {

                    GeriYayılım.girisKatmanSinapsGüncelle(sinaps,
                                                         beklenenDegerler[index][0],o1.Çıkış,
                                                         beklenenDegerler[index][1],o2.Çıkış,
                                                         beklenenDegerler[index][2],o3.Çıkış,
                                                         beklenenDegerler[index][3],o4.Çıkış,
                                                         beklenenDegerler[index][4],o5.Çıkış,
                                                         o1,o2,o3,o4,o5);
                });
            }
        }
        public void girisMatrisDegiştir(double []matris)
        {
            for (int i = 0; i < 35; i++)
            {
                girişNöronArr[i] = new Nöron();
                girişNöronArr[i].Net = matris[i];
                girişNöronArr[i].Çıkış = matris[i];
                
            }
        }
        //initialize
        public void kur(double []matris)
        {
            
            for (int i = 0; i < GIZLI_NORON; i++)
			{
			    gizliNöronArr[i]=new Nöron();
			}
            for (int i = 0; i < 35; i++)
            {
                girişNöronArr[i] = new Nöron();
                girişNöronArr[i].Net=matris[i];
                girişNöronArr[i].Çıkış=matris[i];
                for (int j = 0; j < GIZLI_NORON; j++)
                {
                    girişNöronArr[i].çıkanSinapsEkle(new Sinaps(girişNöronArr[i],gizliNöronArr[j],rnd.NextDouble()));     
                }
            }
            for (int i = 0; i < GIZLI_NORON; i++)
            {
                biasGizliNöron.çıkanSinapsEkle(new Sinaps(biasGizliNöron,gizliNöronArr[i],rnd.NextDouble()));
            
            }
            for (int i = 0; i < GIZLI_NORON; i++)
            {
                gizliNöronArr[i].çıkanSinapsEkle(new Sinaps(gizliNöronArr[i],o1,rnd.NextDouble()));
                gizliNöronArr[i].çıkanSinapsEkle(new Sinaps(gizliNöronArr[i], o2, rnd.NextDouble()));
                gizliNöronArr[i].çıkanSinapsEkle(new Sinaps(gizliNöronArr[i], o3, rnd.NextDouble()));
                gizliNöronArr[i].çıkanSinapsEkle(new Sinaps(gizliNöronArr[i], o4, rnd.NextDouble()));
                gizliNöronArr[i].çıkanSinapsEkle(new Sinaps(gizliNöronArr[i], o5, rnd.NextDouble()));

            }
            biasÇıkışNöron.çıkanSinapsEkle(new Sinaps(biasÇıkışNöron,o1,rnd.NextDouble()));
            biasÇıkışNöron.çıkanSinapsEkle(new Sinaps(biasÇıkışNöron, o2, rnd.NextDouble()));
            biasÇıkışNöron.çıkanSinapsEkle(new Sinaps(biasÇıkışNöron, o3, rnd.NextDouble()));
            biasÇıkışNöron.çıkanSinapsEkle(new Sinaps(biasÇıkışNöron, o4, rnd.NextDouble()));
            biasÇıkışNöron.çıkanSinapsEkle(new Sinaps(biasÇıkışNöron, o5, rnd.NextDouble()));

        }


    }
}
