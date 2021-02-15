using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceCowboy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool bMegy = false;
        bool bLovedek = false; //van-e lövedék a pályán
        //ecset
        SolidBrush ecsetAgyu = new SolidBrush(Color.DarkRed);
        SolidBrush ecsetLovedek = new SolidBrush(Color.DarkBlue);

        //Téglalap
        Rectangle recAgyu;
        Rectangle recLovedek;

        //int
        
        double dSebX, dSebY;
        double dX, dY;

        //Hajó osztály
        Hajo hajoSzovetseges;
        Hajo hajoEllenseges;

        private void LovedekReset()
        {
            dX = ClientSize.Width / 2 - 6;
            dY = ClientSize.Height - 30 - 6;
            recLovedek.X = (int)dX;
            recLovedek.Y = (int)dY;
            dSebX = 0;
            dSebY = 0;
            bLovedek = false;
        
            
        }
        private void Ujjatek()
        {
            bMegy = true;
            timer1.Interval = 1; //1ms
            timer1.Enabled = true; //animáció indít
            recAgyu = new Rectangle(ClientSize.Width/2-30, ClientSize.Height-30,60,30);
            bLovedek = false;
            dX = ClientSize.Width / 2 - 6;
            dY = ClientSize.Height - 30 - 6;
            recLovedek = new Rectangle((int)dX, (int)dY, 12, 12);
            dSebX = 0;
            dSebY = 0;
            hajoSzovetseges = new Hajo(20, Color.Green,ClientSize.Width, ClientSize.Height);
            hajoEllenseges = new Hajo(40, Color.Red, ClientSize.Width, ClientSize.Height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bMegy)
            {
                e.Graphics.FillRectangle(ecsetAgyu, recAgyu);
                e.Graphics.FillEllipse(ecsetLovedek, recLovedek);
                hajoSzovetseges.Hajorajzol(e.Graphics);
                hajoEllenseges.Hajorajzol(e.Graphics);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (bMegy)
            {
                if (!bLovedek)
                {
                    if (e.Button==MouseButtons.Left)
                    {
                        double x = e.X - dX;
                        double y = e.Y - dY;
                        double h = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                        double t = h / 15;
                        dSebX = (x / t);
                        dSebY = y / t;
                        bLovedek = true;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Space)
            {
                 Ujjatek();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //mozgatás
            dX += dSebX;
            dY += dSebY;
            recLovedek.X = (int)dX;
            recLovedek.Y = (int)dY;
            //ütközés
            if (recLovedek.Left<=0||recLovedek.Right>=ClientSize.Width)
            {
                dSebX = -dSebX;
            }
            if (recLovedek.Top<=0||recLovedek.Bottom>=ClientSize.Height)
            {
                LovedekReset();

            }
            if (hajoSzovetseges.Kilove(recLovedek))
            {
                this.Invalidate();
                bMegy = false;
                

            }
            if (hajoEllenseges.Kilove(recLovedek))
            {
                LovedekReset();
                hajoEllenseges = new Hajo(40, Color.Red, ClientSize.Width, ClientSize.Height);
                hajoSzovetseges.Ujhajo(ClientSize.Width,ClientSize.Height);
            }
            this.Invalidate();//képfrissítés
        }
    }
}
