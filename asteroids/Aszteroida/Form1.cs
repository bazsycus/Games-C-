using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aszteroida
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool bMegy = false;

        Rectangle recJatek;
        SolidBrush ecsetJatek = new SolidBrush(Color.Maroon);
        List<Aszteroida> asztlAszteroidaLista;
        Random rnd = new Random();
        private void Ujjatek()
        {
            bMegy = true;
            timer1.Interval = 1;
            timer1.Enabled = true;
            timer2.Interval = 1000;
            timer2.Enabled = true;
            timer3.Interval = 1000;
            timer3.Enabled = true;
            progressBar1.Value = 0;
            recJatek = new Rectangle(200,200, 20,20);
            asztlAszteroidaLista = new List<Aszteroida>();
            asztlAszteroidaLista.Add(new Aszteroida(ClientSize.Width, ClientSize.Height));
            asztlAszteroidaLista.Add(new Aszteroida(ClientSize.Width, ClientSize.Height));
            Cursor.Hide();
        }

        private void Vegeajeteknak(bool bGyozelem)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            bMegy = false;
            Cursor.Show();
            if (bGyozelem)
            {
                MessageBox.Show("Győzelem", "Győzelem", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } else
            {
                MessageBox.Show("Vereség", ":(", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bMegy)
            {
                e.Graphics.FillEllipse(ecsetJatek, recJatek);
                foreach (Aszteroida aktAszt in asztlAszteroidaLista)
                {
                    aktAszt.Kirajzol(e.Graphics);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Space)
            {
                Ujjatek();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {//animációt

            //mozgatás
            recJatek.X = PointToClient(Cursor.Position).X;
            recJatek.Y = PointToClient(Cursor.Position).Y;
            foreach (Aszteroida aktAszt in asztlAszteroidaLista)
            {
                aktAszt.Mozgat();
            }
            //ütközés
            for (int i = asztlAszteroidaLista.Count-1; i >= 0; i--)
            {
                if (asztlAszteroidaLista[i].Utkozott(recJatek))
                {
                    Vegeajeteknak(false);
                }
                if (asztlAszteroidaLista[i].Falatert(ClientSize.Width, ClientSize.Height))
                {
                    asztlAszteroidaLista.RemoveAt(i);
                    asztlAszteroidaLista.Add(new Aszteroida(ClientSize.Width, ClientSize.Height));
                    if (rnd.Next(1,11)>5)
                    {
                        asztlAszteroidaLista.Add(new Aszteroida(ClientSize.Width, ClientSize.Height));
                    }
                }
            }

            this.Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {//timer2 új aszteroidák
            asztlAszteroidaLista.Add(new Aszteroida(ClientSize.Width, ClientSize.Height));
        }

        private void timer3_Tick(object sender, EventArgs e)
        {//10 másodperc
            progressBar1.Value++;
            if (progressBar1.Value==progressBar1.Maximum)
            {
                Vegeajeteknak(true);
            }
        }
    }
}
