using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpaceCowboy
{
    class Hajo
    {
        //átmérője, szín, listányi téglalap
        int iHajoatmero;
        Color szinHajo;
        List<Rectangle> reclHajolista;
        static Random rnd = new Random();
            
        SolidBrush ecsetHajo;
        //konstruktor
        public Hajo(int iBeatmero, Color szinBemeno, int iSzel, int iMag)
        {
            iHajoatmero = iBeatmero;
            szinHajo = szinBemeno;
            reclHajolista = new List<Rectangle>();
            Ujhajo(iSzel, iMag);
            ecsetHajo = new SolidBrush(szinHajo);
        }

        public void Ujhajo(int iSzel, int iMag)
        {
            int iHajoX, iHajoY;
            iHajoX = rnd.Next(1, iSzel - iHajoatmero);
            iHajoY = rnd.Next(1, iMag * 8 / 10 - iHajoatmero);
            Rectangle recUjhajo =new Rectangle(iHajoX, iHajoY,iHajoatmero, iHajoatmero);


            reclHajolista.Add(recUjhajo);
        }

        public void Hajorajzol(Graphics rajzlap)
        {
            for (int i = 0; i < reclHajolista.Count; i++)
            {
                rajzlap.FillEllipse(ecsetHajo, reclHajolista[i]);
            }
        }

        public bool Kilove(Rectangle Lovedek)
        {
            bool bUtkozott = false;
            foreach (Rectangle recAkt in reclHajolista)
            {
                if (recAkt.IntersectsWith(Lovedek))
                {
                    bUtkozott = true;
                }
            }



            return bUtkozott;
        }

    }
}
