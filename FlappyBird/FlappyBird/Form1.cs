using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int iFlappySebY = 0;
        int iGravitacio =1 ;

        bool bMegy = false;
        bool bMozgat = false;
        bool bLastFrame = false;
        int iAkadalySzamlalo = 0;

        List<Csovek> csolLista;

        private void Ujjatek()
        {
            timer1.Interval = 16;
            timer1.Enabled = true;
            iFlappySebY = 0;
            flappyBird.Top = 240;
            bMegy = true;
            csolLista = new List<Csovek>();
            csolLista.Add(new Csovek());
            timer2.Interval = 100;
            timer2.Enabled = true;
            bLastFrame = false;
        }


        private void VegJatek()
        {
            timer1.Enabled = false;
            this.Invalidate();
            bLastFrame = true;
            timer2.Enabled = false;

            bMegy = false;

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //mozgatás
            if (bMozgat)
            {
                iFlappySebY += iGravitacio;
                
                bMozgat = false;
            }else
            {
                bMozgat = true;
            }
            flappyBird.Top += iFlappySebY;
            foreach (Csovek aktCsovek in csolLista)
            {
                aktCsovek.Mozgat();
            }

            //ütközésünk
            if (flappyBird.Top<=0)
            {
                VegJatek();
            }
            if (flappyBird.Bottom>=590)
            {
                VegJatek();
            }

            for (int i = csolLista.Count-1; i >=0 ; i--)
            {
                if (csolLista[i].Utkozott(new Rectangle(flappyBird.Left, flappyBird.Top, flappyBird.Width,flappyBird.Height)))
                {
                    VegJatek();
                }
                if (csolLista[i].Kiment())
                {
                    csolLista.RemoveAt(i);
                    
                }
                
            }

            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                Ujjatek();
            }
            if (e.KeyCode==Keys.Space)
            {
                iFlappySebY = -8;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bMegy || bLastFrame)
            {
                bLastFrame = false;
                foreach (Csovek aktCsovek in csolLista)
                {
                    aktCsovek.Kirajzol(e.Graphics);
                }
            }
        }
        Random rnd = new Random();
        private void timer2_Tick(object sender, EventArgs e)
        {
            iAkadalySzamlalo++;
            if (iAkadalySzamlalo>10)
            {
                if (rnd.Next(0, 10) > 5)
                {
                    iAkadalySzamlalo = 0;
                    csolLista.Add(new Csovek());
                }
                
            }
        }
    }
}
