using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aszteroida
{
    class Aszteroida
    {

        public Aszteroida(int iSzel, int iMag)
        {
            iAtmero = rnd.Next(10, 20);
            recAszteroida = new Rectangle(rnd.Next(0, iSzel-iAtmero-1), rnd.Next(0, iMag-iAtmero-1),iAtmero,iAtmero);
            iSebX = rnd.Next(-7, 8);
            iSebY = rnd.Next(-7, 8);
        }

        static Random rnd = new Random();
        int iAtmero;
        SolidBrush ecsetAszteroida = new SolidBrush(Color.CornflowerBlue);
        Rectangle recAszteroida;
        int iSebX, iSebY;

        public void Kirajzol(Graphics rajzlap)
        {
            rajzlap.FillEllipse(ecsetAszteroida, recAszteroida);
        }
        public void Mozgat()
        {
            recAszteroida.X += iSebX;
            recAszteroida.Y += iSebY;
        }

        public bool Utkozott(Rectangle recJatek)
        {
            bool bUtkozott = false;
            if (recJatek.IntersectsWith(recAszteroida))
            {
                bUtkozott = true;
            }
            return bUtkozott;
        }
        public bool Falatert(int iSzel, int iMag)
        {
            bool bUtkozott = false;
            if (recAszteroida.Right>=iSzel ||recAszteroida.Left<=0)
            {
                bUtkozott = true;
            }
            if (recAszteroida.Top<=0 ||recAszteroida.Bottom>=iMag)
            {
                bUtkozott = true;
            }


            return bUtkozott;
        }




    }
}
