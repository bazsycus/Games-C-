using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlappyBird
{
    class Csovek
    {
        Rectangle[] recCsovek;

        SolidBrush ecsetCso = new SolidBrush(Color.ForestGreen);
        int iSebX = -10;
        static Random rnd = new Random();
        public Csovek()
        {
            recCsovek = new Rectangle[4];
            int iMagassag = rnd.Next(51, 590 - 240);

            recCsovek[0] = new Rectangle(510+10,0,80,iMagassag-50);
            recCsovek[1] = new Rectangle(510 , iMagassag - 50, 100, 50);
            recCsovek[2] = new Rectangle(510, iMagassag + 190, 100, 50);
            recCsovek[3] = new Rectangle(510+10, iMagassag + 190+50, 80, 600-recCsovek[2].Bottom);
        }

        public void Mozgat()
        {
            for (int i = 0; i < 4; i++)
            {
                recCsovek[i].X += iSebX;
            }
        }
        public void Kirajzol(Graphics rajzlap)
        {
            foreach (Rectangle recAkt in recCsovek)
            {
                rajzlap.FillRectangle(ecsetCso, recAkt);
            }
        }

        public bool Kiment()
        {
            if (recCsovek[1].Right<0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        public bool Utkozott(Rectangle recFlappy)
        {
            bool bUtkozott = false;
            for (int i = 0; i < 4; i++)
            {
                if (recCsovek[i].IntersectsWith(recFlappy))
                {
                    bUtkozott = true;
                }
            }
            return bUtkozott;
        }





    }
}
