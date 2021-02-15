using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _14_connect_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //deklaráció
        //int
        int iOszlop;
        int iSoronErtek;
        // bool
        bool bStart;
        bool bGep = false;
        
        //ecsetek
        SolidBrush ecsetTabla = new SolidBrush(Color.DarkBlue);
        SolidBrush ecsetUresKorong = new SolidBrush(Color.Gray);
        SolidBrush ecsetSarga = new SolidBrush(Color.Yellow);
        SolidBrush ecsetPiros = new SolidBrush(Color.Red);
        //tollka
        //Random
        Random rnd = new Random();
        //tömb
        int[,] ia2Tabla; //0 semmi, -1 piros, +1 sárga
        private void Geplep()
        {
            if (Kinyert()==0)
            {
                int iMitlepagep = rnd.Next(0,7);
                while (Sorszam(iMitlepagep)==-1)
                {
                    iMitlepagep = rnd.Next(0, 7);
                }
                ia2Tabla[iMitlepagep, Sorszam(iMitlepagep)] = -1;
                iSoronErtek = 1;
            }
        }
        private int Kinyert()
        {
            int iGyoztes = 0;

            //összes sor
            //összes oszlop
            //összes átló
            int iMellett = 0;
            int iMelyik;
            
            
            //a) összes oszlop

            for (int i = 0; i < 7; i++)//végig minden oszlopon
            {
                iMellett = 0;//hány van egymás mellett
                iMelyik = ia2Tabla[i, 5];//az aktuálisoszlop legalsó eleme
                for (int j = 5; j >=0 ; j--)//aluról megyünk felfele
                {//ha az adott elem nem nulla
                    if (ia2Tabla[i,j]!=0)
                    {
                        //adott elem a vizsgált szín-e
                        if (iMelyik==ia2Tabla[i,j])
                        {
                            iMellett++;//egyel több van egymás mellett
                        } else
                        {
                            iMelyik = ia2Tabla[i, j];//legyen a másik szín
                            iMellett = 1;
                        }
                        if (iMellett==4)
                        {
                            iGyoztes = iMelyik;
                        }
                    }

                }
            }

            //b) összes sor

            for (int j = 0; j < 6; j++)//végig minden soron
            {
                iMellett = 0;//hány van egymás mellett
                iMelyik = ia2Tabla[0, j];//az aktuális sor legelső eleme
                for (int i = 0; i < 7; i++)//balról jobbra
                {//ha az adott elem nem nulla
                    if (ia2Tabla[i, j] != 0)
                    {
                        //adott elem a vizsgált szín-e
                        if (iMelyik == ia2Tabla[i, j])
                        {
                            iMellett++;//egyel több van egymás mellett
                        }
                        else
                        {
                            iMelyik = ia2Tabla[i, j];//legyen a másik szín
                            iMellett = 1;
                        }
                        if (iMellett == 4)
                        {
                            iGyoztes = iMelyik;
                        }
                    }

                }
            }
            //c) átló felső eleme balra le megy
            for (int i = 3; i < 7; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bool bNegyazonos = true;
                    if (ia2Tabla[i,j]!=0)//nem-e üres
                    {
                        for (int k = 1; k <= 3; k++)
                        {
                            if (ia2Tabla[i,j]!=ia2Tabla[i-k,j+k])
                            {
                                bNegyazonos = false;
                            }
                        }
                        if (bNegyazonos)
                        {
                            iGyoztes = ia2Tabla[i, j];
                        }
                    }
                }
            }
            //d) átló felső eleme jobbra le megy
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bool bNegyazonos = true;
                    if (ia2Tabla[i, j] != 0)//nem-e üres
                    {
                        for (int k = 1; k <= 3; k++)
                        {
                            if (ia2Tabla[i, j] != ia2Tabla[i + k, j + k])
                            {
                                bNegyazonos = false;
                            }
                        }
                        if (bNegyazonos)
                        {
                            iGyoztes = ia2Tabla[i, j];
                        }
                    }
                }
            }
            return iGyoztes;
        }

        private bool Oszlopszam(int iX)
        {
            bool bSzabalyos = false;
            iX -= 220; //keret, első korong határának levonása
            if (iX>0)//nem a táblától balra
            {
                int iKOszlop = iX / 100;
                if (iKOszlop<7)//7 oszlopunk van, nem-e jobbra klikk
                {
                    if (iX%100<80)
                    {
                        bSzabalyos = true;
                        iOszlop = iKOszlop;
                    }
                }
            }

            return bSzabalyos;
        }

        private int Sorszam(int iOszlopszam)
        {
            int iSorszam = 5;//-1, ha nincs hely, különben hanyas sor
            for (int i = 0; i < 6; i++)
            {
                iSorszam -= Math.Abs(ia2Tabla[iOszlopszam, i]);
            }

            return iSorszam;
        }

        private void Ujjatek()
        {
            bStart = true;
            iOszlop = 0;
            ia2Tabla = new int[7,6];//balról, jobbra, fentről, lefele
            if (radioButton1.Checked)
            {
                bGep = false;
                
            } else
            {
                bGep = true;

            }
            if (rnd.Next(0, 2) == 1)
            {
                iSoronErtek = 1;
            }
            else
            {
                iSoronErtek = -1;//ha gépi, akkor ez a gép
            }
            if (bGep&&iSoronErtek==-1)
            {
                Geplep();
            }

            this.Invalidate();//kpernyő frissítés
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ujjatek();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (bStart)
            {
                if (Oszlopszam(e.X))
                {
                    if (Sorszam(iOszlop)>-1)
                    {
                        //ne a gép sora, 
                        if ((bGep&&iSoronErtek==1)||!bGep)
                        {//ez a gépesítéshez kell nem biztos hogy használjuk
                            ia2Tabla[iOszlop, Sorszam(iOszlop)] = iSoronErtek;
                            iSoronErtek = -iSoronErtek;
                            if (bGep&&iSoronErtek==-1)//megvolt az emberi lépés
                            {//jöhet a égpi lépés
                                Geplep();
                            }

                        }
                        
                        this.Invalidate();
                        if (Kinyert()!=0)
                        {
                            
                            
                            string sUzenet = "Nyert a";
                            if (Kinyert()==1)
                            {
                                sUzenet += "sárga!";
                            }
                            else
                            {
                                sUzenet += "piros!";
                            }
                            MessageBox.Show(sUzenet, "Győzelem", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(ecsetPiros, 0, 0, 30, 30);
            e.Graphics.FillRectangle(ecsetTabla, 200,20, 
                20+7*100,20+6*100);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    e.Graphics.FillEllipse(ecsetUresKorong,
                        200 + 20 + i * 100, 20 + 20 + j * 100, 80, 80);
                }
            }

            if (bStart)
            {
                if (iSoronErtek==1)
                {
                    e.Graphics.FillEllipse(ecsetSarga, 20, 20, 80, 80);
                } else
                {
                    e.Graphics.FillEllipse(ecsetPiros, 20, 20, 80, 80);
                }
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (ia2Tabla[i,j]==1)
                        {
                            e.Graphics.FillEllipse(ecsetSarga,
                            200 + 20 + i * 100, 20 + 20 + j * 100, 80, 80);
                        }
                        else if (ia2Tabla[i,j]==-1)
                        {
                            e.Graphics.FillEllipse(ecsetPiros,
                           200 + 20 + i * 100, 20 + 20 + j * 100, 80, 80);
                        }
                        
                    }
                }
                if (Kinyert()!=0)
                {
                    bStart = false;
                }
            }

        }
    }
}
