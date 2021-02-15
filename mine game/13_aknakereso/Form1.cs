using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _13_aknakereso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //deklaráció
        //integer
        int iSor, iOszlop; //hány sor és oszlop van
        int iKsor, iKoszlop;//klikkel sor, oszlop
        int iMezo, iAkna;//metők száma, aknák száma
        int iZaszlo; //lent levő zászló szám
        int iFelforditott;
        //bool
        bool bStart = false; //elindult-e már a játék
        bool bVege = false;
        //tollak
        Pen tollFeketeV = new Pen(Color.Black, 2);
        Pen tollFekete = new Pen(Color.Black, 1);
        
        //ecset
        SolidBrush ecsetFeher = new SolidBrush(Color.White);
        SolidBrush ecsetAkna = new SolidBrush(Color.Red);
        SolidBrush ecsetBetu = new SolidBrush(Color.DarkGreen);//ezzel írjuk kia betűket
        //Tömbök
        int[,] ia2Akna, ia2Klikk;
        //Random
        Random rnd = new Random();
        //Fonr
        Font fontKiiras = new Font("Comic Sans MS", 10, FontStyle.Bold);
        /* [sor, oszlop]
         * Akna-->  -1->>-1  akna hány szomszédon van akna
         * Klikk 0->nincs felfedve, 1 bal klikkel van felfedve, -1 zászló
         * 
         */
        private void JatekKezdet()
        {
            iFelforditott = 0;
            iZaszlo = 0;
            label5.Text = iZaszlo.ToString();
            bVege = false;
            iSor = int.Parse(textBox2.Text);
            iOszlop = int.Parse(textBox1.Text);
            iSor=Math.Min(20, Math.Max(5, iSor));
            iOszlop = Math.Min(20, Math.Max(5, iOszlop));
            bStart = true;
            ia2Akna = new int[iSor+2, iOszlop+2];
            ia2Klikk = new int[iSor + 2, iOszlop + 2];
            for (int i = 0; i < iSor+2; i++)//tömbjeinket feltöltöttük.
            {
                for (int j = 0; j < iOszlop+2; j++)
                {
                    ia2Klikk[i, j] = 0;
                    ia2Akna[i, j] = 0;
                }
            }
            iMezo = iSor * iOszlop;
            iAkna = iMezo / 10;
            if (radioButton1.Checked)//hány aknánk van(zászlónk is van)
            {
                iAkna = iAkna * 1;
            }
            else if (radioButton2.Checked)
            {
                iAkna = iAkna * 2;
            } else
            {
                iAkna = iAkna * 3;
            }
            label6.Text = iAkna.ToString();
            for (int i = 0; i < iAkna; i++) //akna generálás
            {
                bool bSzabad = false;
                while (!bSzabad)
                {
                    int iX, iY;
                    iX = rnd.Next(1, iOszlop + 1);
                    iY = rnd.Next(1, iSor + 1);
                    if (ia2Akna[iY, iX]>-1)//nem akna
                    {
                        ia2Akna[iY, iX] = -1;
                        bSzabad = true;
                    }
                }

            }
            for (int i = 1; i <= iSor; i++)
            {
                for (int j = 1; j <= iOszlop; j++)
                {
                    if (ia2Akna[i,j]!=-1)
                    {//nem akna
                        
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int l = -1; l <= 1; l++)
                            {
                                if (ia2Akna[i+k,j+l]==-1)//ha akna
                                {
                                    ia2Akna[i, j]++;
                                }
                            }
                        }
                    }
                }

            }
            this.Invalidate(); //formot újra rajzoltatja(meghívja a paint eseményt)
        }
        /*minimum 5, maximum 20
         * bekért számunk: bármi
         *  Max(5, bekért számnak)-->bekért számunk nagyobb egyenlő mint 5
         *Min(20, előző már átalaított számnak)
         *Min(20, Max(5, bekért szám) )//
         *
         *
         */

        private void RacsKirajzol(Graphics rajzlap)
        {
            rajzlap.DrawRectangle(tollFeketeV,200, 200, 4+24*iOszlop,4+24*iSor );
            for (int i = 1; i <= iSor; i++)
            {
                for (int j = 1; j <= iOszlop; j++)
                {
                    Rectangle recRacselem = new Rectangle(
                        204+(j-1)*24,
                        204+(i-1)*24,
                        20,20);

                    rajzlap.FillRectangle(ecsetFeher, recRacselem);
                    rajzlap.DrawRectangle(tollFekete, recRacselem);
                }
            }
        }

     

        private bool HovaKlikk(int iX, int iY)
        {
            bool bValos = false;
            iX = iX - 204;
            if (iX>=0)//nem balra klikkeltünk a rácstöl
            {
                int iTmpOszlop = iX / 24;
                
                iTmpOszlop += 1;
                if (iTmpOszlop<=iOszlop)
                {
                    if (iX%24<=20)
                    {
                        bValos = true;
                        iKoszlop = iTmpOszlop;
                    }
                }
            }
            
            if (bValos)//valós oszlopszám
            {
                bValos = false;
                iY = iY - 204;
                if (iY >= 0)//nem balra klikkeltünk a rácstöl
                {
                    int iTmpSor = iY / 24;

                    iTmpSor += 1;
                    if (iTmpSor <= iSor)
                    {
                        if (iY % 24 <= 20)
                        {
                            bValos = true;
                            iKsor = iTmpSor;
                        }
                    }
                }
            }

            return bValos;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
           if( HovaKlikk(e.X, e.Y)&&!bVege&&bStart)//leklikk és rácsra klikeltünk-e?
           {
                if (ia2Klikk[iKsor,iKoszlop]!=0)// felvolt fedve
                {
                    if (e.Button==MouseButtons.Right)
                    {
                        if (ia2Klikk[iKsor,iKoszlop]==-1)//zászló??
                        {
                            ia2Klikk[iKsor, iKoszlop] = 0;
                            iZaszlo--;//elvettem egy zászlót
                        }
                    }
                } else
                {
                    if (e.Button==MouseButtons.Right)
                    {
                        if (iZaszlo<iAkna)//van még zászlónk
                        {
                            ia2Klikk[iKsor, iKoszlop] = -1;
                            iZaszlo++;//letettem a zászlót
                        }
                        if (iFelforditott + iZaszlo == iMezo)
                        {//ha minden mező felfordított vagy zászló akkor nyertünk
                            bVege = false;
                            MessageBox.Show("Győzelem, sikerült nyerni", "Győzelem",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    if (e.Button == MouseButtons.Left) //bal klikk
                    {
                        if(ia2Akna[iKsor, iKoszlop] == -1)
                        {
                            bVege = true;
                            MessageBox.Show("Vereség, akánára lépett", "Vereség",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ia2Klikk[iKsor, iKoszlop] = 1;
                        if (!bVege)
                        {
                            iFelforditott++;
                        }
                        if (iFelforditott+iZaszlo==iMezo)
                        {//ha minden mező felfordított vagy zászló akkor nyertünk
                            bVege = false;
                            MessageBox.Show("Győzelem, sikerült nyerni", "Győzelem",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                    }
                    
                }
                this.Invalidate();

           }
        }

        // ([(X-204)/24]+1)%24
        private void button1_Click(object sender, EventArgs e)
        {
            JatekKezdet();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bStart)//ha elindult a jták
            {
                RacsKirajzol(e.Graphics);
                label5.Text = iZaszlo.ToString();
                for (int i = 1; i <= iSor; i++)
                {
                    for (int j = 1; j <= iOszlop; j++)
                    {
                        Rectangle recRacselem = new Rectangle(
                        204 + (j - 1) * 24,
                        204 + (i - 1) * 24,
                        20, 20);
                        if (ia2Klikk[i,j]==1)//felfordított
                        {
                            if (ia2Akna[i,j]==-1)
                            {
                                e.Graphics.FillEllipse(ecsetAkna,recRacselem);
                            } else
                            {
                                e.Graphics.DrawString(
                                    ia2Akna[i,j].ToString(),
                                    fontKiiras,
                                    ecsetBetu,recRacselem);
                            }
                        }
                        else if (ia2Klikk[i,j]==-1)//zászló
                        {
                            Point[] pontazaszlo = new Point[3];
                            pontazaszlo[0] = new Point(recRacselem.X+1,recRacselem.Y+7);
                            pontazaszlo[1]= new Point(recRacselem.X + 17, recRacselem.Y + 3);
                            pontazaszlo[2]= new Point(recRacselem.X + 17, recRacselem.Y + 10);
                            e.Graphics.FillPolygon(ecsetAkna, pontazaszlo);
                            e.Graphics.DrawLine(tollFeketeV, recRacselem.X + 17, recRacselem.Y + 3, recRacselem.X + 17, recRacselem.Y + 18);
                        }
                       
                    }
                }
            }
        }
    }
}
