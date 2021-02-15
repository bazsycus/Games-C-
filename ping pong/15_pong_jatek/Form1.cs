using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _15_pong_jatek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //deklarációs
        //téglalapok
        Rectangle recUto;
        Rectangle recLabda;
        //ecset
        SolidBrush ecsetUto = new SolidBrush(Color.FromArgb(60,120,180));
        SolidBrush ecsetLabda;
        //bool 
        bool bMegy = false;
        //random
        Random rnd = new Random();
        //integer
        int iSebX, iSebY;
        private void Ujjatek()
        {
            //ütő kezdő pozíció
            //120 pixel széles, 20 px magas
            //képernyő közepén (x), Y(40 pixel az alja felett)
            //this.Height, this.Width
            recUto = new Rectangle(this.Width/2-60,this.Height-80-20,120,20);
            //újjáték indult
            bMegy = true;
            //labda létrahozásunk
            //30x30 pixel
            recLabda = new Rectangle(rnd.Next(10,this.Width-60), rnd.Next(30,this.Height/2),30, 30 );
            ecsetLabda = new SolidBrush(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
            iSebX = 5;
            iSebY = 5;
            //animációs beállítás
            timer1.Interval = 1; //1 ms frissítés
            timer1.Enabled = true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bMegy)
            {
                e.Graphics.FillRectangle(ecsetUto, recUto);//ecset, téglalap megadása
                e.Graphics.FillEllipse(ecsetLabda, recLabda);
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
        {
            //mozgatás
            recUto.X = Math.Min(Math.Max(this.PointToClient(MousePosition).X - 60, 0), this.Width - 120);
            recLabda.X += iSebX;
            recLabda.Y += iSebY;
            //ütközés
            //labda fal ütközés
            if (recLabda.Left<=0)
            {
                iSebX = -iSebX;
            }
            if (recLabda.Right>=this.Width)
            {
                iSebX = -iSebX;
            }
            if (recLabda.Top<=0)
            {
                iSebY = -iSebY;
            }
            if (recLabda.Bottom>=this.Height)
            {
                bMegy = false;
                timer1.Enabled = false;
                //game over
            }
            //labda ütő között
            if (recLabda.IntersectsWith(recUto))//ha a két elem közt átfedés true, különben false
            {
                iSebY = -iSebY;
                recLabda.Y = recUto.Y - 30;
                ecsetLabda = new SolidBrush(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
                //számoljátok (label-be) írjátok, hogy hány ütő érintés sikerült
                //minden ütő érintéskor növeljétek a labda sebességét 
                //figyeljetek hogy a sebesség lehet negatív, tehát abszolút érték növekszik
            }
            //képernyő frissítés
            this.Invalidate();
        }
        //this.PointToClient(); kkordinátát a formhoz igazítja, nem a képernyőhöz
        // MousePosition.X  .Y  egér pozíció x, y koordinátáját.
        //képernyő (monitor) bal felső sarkától
    }
}
