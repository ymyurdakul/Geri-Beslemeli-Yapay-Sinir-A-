using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GeriBeslemeliYapaySinirAg
{
    
    public partial class Form1 : Form
    {
        public static List<double[]> beklenenDegerler;
        int[] girişMatris;
        public Form1()
        {
             girişMatris=new int[35];
            InitializeComponent();
        }

        bool çizgiDegistir = true;
        private void btnÇizgiKaldır_Click(object sender, EventArgs e)
        {
            if (çizgiDegistir)
            {
                for (int i = 1; i < panelGirişler.Controls.Count + 1; i++)
                {
                    PictureBox pb = (PictureBox)panelGirişler.Controls["pictureBox" + i];
                    pb.BorderStyle = BorderStyle.None;
                }
            }
                
            else
            {
                for (int i = 1; i < panelGirişler.Controls.Count + 1; i++)
                {
                    PictureBox pb = (PictureBox)panelGirişler.Controls["pictureBox" + i];
                    pb.BorderStyle = BorderStyle.Fixed3D;
                }
            }
            çizgiDegistir = !çizgiDegistir;

        }
        //tÜM GİRİŞLERİN CLİCK EVENTİ BURAYA BAĞLI
        private void pictureBox35_Click(object sender, EventArgs e)
        {
            //Tersleme yapıyorum
            PictureBox pb = (PictureBox)sender;
            if (pb.BackColor == Color.White)
                pb.BackColor = Color.Black;
            else
                pb.BackColor = Color.White;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < panelGirişler.Controls.Count + 1; i++)
            {
                PictureBox pb = (PictureBox)panelGirişler.Controls["pictureBox" + i];
                pb.BackColor = Color.White;
            }
        }

        private void btnRastgeleDoldur_Click(object sender, EventArgs e)
        {
            btnTemizle_Click(sender,e);
            //Minimum 10tane kare boyansın ki bişeye benzesin
            Random rnd = new Random();
            int rasgeleElemanSayısı = rnd.Next(10,35);
            for (int i = 0; i < rasgeleElemanSayısı; i++)
            {
                int x = rnd.Next(1,35);
                PictureBox pb = (PictureBox)panelGirişler.Controls["pictureBox" + x];
                pb.BackColor = Color.Black;
            }

        }

        private void btnTanımla_Click(object sender, EventArgs e)
        {
            matrisSıfırla(girişMatris);
            for (int i = 1; i < panelGirişler.Controls.Count+1; i++)
            {
                PictureBox pb=(PictureBox)panelGirişler.Controls["pictureBox"+i];
                if (pb.BackColor == Color.Black)
                    girişMatris[i-1] = 1;
                
            }

       


          
        }
        public void matrisSıfırla(int []matris)
        {
            for (int i = 0; i < matris.Length; i++)
            {
                matris[i] = 0;
            }
        }
        public String matrisToString(int []matris)
        {
             
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < matris.Length; i++)
            {
                builder.Append(matris[i]+",");
            }
            return builder.ToString();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            yükle();
        }
        public void yükle()
        {
            
            beklenenDegerler = new List<double[]>();
 

            String[] beklenenSatırlar = File.ReadAllLines("beklenen.txt");
            for (int i = 0; i < beklenenSatırlar.Length; i++)
            {
                String[] parcalanmis = beklenenSatırlar[i].Split(',');
                double[] temp = new double[5];
                for (int j = 0; j < 5; j++)
                {
                    temp[j] = Double.Parse(parcalanmis[j]);
                }
                beklenenDegerler.Add(temp);
            }

        }

        private void btnEgit_Click(object sender, EventArgs e)
        {
            int epoch=0;
            epoch=(int)nudEpoch.Value;
            double lr = (double)nudOgrenmeKatsayı.Value;
            SinirAgı ag = new SinirAgı(lr,epoch);
            ag.eğit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
