using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using System.Text.RegularExpressions;



// rajouter fonction opacité
// rajouter A B C D E F quand on clique hexadecimal
// boutons radio sur les bases
// pouvoir selectionner la base et alterner
// les randoms 

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        long operande1 = 0;
        long operande2 = 0;
        char operateur1;
        long memoire = 0;
        string baseNum = "dec";
        string temp="";
        System.Random random = new System.Random();
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        Stack pile = new Stack();
        Stack pileOperateur = new Stack();

        bool parenthese = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Afficheur_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btn0_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Afficheur.Text = "";
            Form1.ActiveForm.Width = 290;
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
           
                Afficheur.Text = Afficheur.Text + "1";
           
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "2";
        }

        private void Btn3_Click(object sender, EventArgs e)
        {


            Afficheur.Text = Afficheur.Text + "3";
         
        }

        private void Btn4_Click(object sender, EventArgs e)
        {

            Afficheur.Text = Afficheur.Text + "4";
        

        }

        private void Btn5_Click(object sender, EventArgs e)
        {

            Afficheur.Text = Afficheur.Text + "5";
        

        }

        private void Btn6_Click(object sender, EventArgs e)
        {

            Afficheur.Text = Afficheur.Text + "6";
         
        }

        private void Btn7_Click(object sender, EventArgs e)
        {

            Afficheur.Text = Afficheur.Text + "7";
           
        }

        private void Btn8_Click(object sender, EventArgs e)
        {

            Afficheur.Text = Afficheur.Text + "8";

        }

        private void Btn9_Click(object sender, EventArgs e)
        {

            Afficheur.Text = Afficheur.Text + "9";

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Afficheur.Text = "";
            operande1 = 0;
            operande2 = 0;
            operateur1 = ' ';       
        }

        private void BtnMult_Click(object sender, EventArgs e)
        {
            if (parenthese == true)
            {
                Afficheur.Text = Afficheur.Text + "*";
            }
            else
            {
                operande1 = int.Parse(Afficheur.Text);
                operateur1 = '*';
                Afficheur.Text = "";
            }
        }

        private void BtnDiv_Click(object sender, EventArgs e)
        {
            if (parenthese == true)
            {
                Afficheur.Text = Afficheur.Text + "/";
            }
            else
            {
                operande1 = int.Parse(Afficheur.Text);
                operateur1 = '/';
                Afficheur.Text = "";
            }
        }

        private void BtnPlus_Click(object sender, EventArgs e)
        {
            if (parenthese == true)
            {   
                Afficheur.Text = Afficheur.Text + "+";
            }

            else
            {
                operande1 = int.Parse(Afficheur.Text);
                operateur1 = '+';
                Afficheur.Text = "";
            }

        }

        private void BtnMoins_Click(object sender, EventArgs e)
        {
            if (parenthese == true)
            {
                Afficheur.Text = Afficheur.Text + "-";
            }
            else
            {
                operande1 = int.Parse(Afficheur.Text);
                operateur1 = '-';
                Afficheur.Text = "";
            }
        }

        private void BtnEgal_Click(object sender, EventArgs e)
        {
            if (parenthese == true)
            {
                //mieux de declarer le string dans une form ou pas ? et pour compteur
                string afficheurParenthese;
                int compteurParGauche = 0;
                int compteurParDroite = 0;
                afficheurParenthese = Afficheur.Text;
                for (int i = 0; i < afficheurParenthese.Length; i++)
                {
                    if (afficheurParenthese[i] == '(')
                    {
                        compteurParGauche++;
                    }
                    else if (afficheurParenthese[i] == ')')
                    {
                        compteurParDroite++;
                    }
                }
                if (compteurParDroite != compteurParGauche)
                {

                    Afficheur.Text = "FATAL ERROR";

                }
                else
                {
                    for (int i = 0; i < afficheurParenthese.Length; i++)
                    {
                        if (afficheurParenthese[i] == '1')
                        {
                            temp = temp + "1";
                        }
                        else if (afficheurParenthese[i] == '0')
                        {
                            temp = temp + "0";
                        }
                        else if (afficheurParenthese[i] == '2')
                        {
                            temp = temp + "2";
                        }
                        else if (afficheurParenthese[i] == '3')
                        {
                            temp = temp + "3";
                        }
                        else if (afficheurParenthese[i] == '4')
                        {
                            temp = temp + "4";
                        }
                        else if (afficheurParenthese[i] == '5')
                        {
                            temp = temp + "5";
                        }
                        else if (afficheurParenthese[i] == '6')
                        {
                            temp = temp + "6";
                        }
                        else if (afficheurParenthese[i] == '7')
                        {
                            temp = temp + "7";
                        }
                        else if (afficheurParenthese[i] == '8')
                        {
                            temp = temp + "8";
                        }
                        else if (afficheurParenthese[i] == '9')
                        {
                            temp = temp + "9";
                        }

                        else if (afficheurParenthese[i] == '+')
                        {
                            if (temp != "")
                            {
                                pile.Push(long.Parse(temp));
                                temp = "";
                            }
                            pileOperateur.Push(afficheurParenthese[i]);
                        }
                        else if (afficheurParenthese[i] == '-')
                        {
                            if (temp != "")
                            {
                                pile.Push(long.Parse(temp));
                                temp = "";
                            }
                            pileOperateur.Push(afficheurParenthese[i]);
                        }
                        else if (afficheurParenthese[i] == '/')
                        {
                            if (temp != "")
                            {
                                pile.Push(long.Parse(temp));
                                temp = "";
                            }
                            pileOperateur.Push(afficheurParenthese[i]);
                        }
                        else if (afficheurParenthese[i] == '*')
                        {
                            if (temp != "")
                            {
                                pile.Push(long.Parse(temp));
                                temp = "";
                            }
                            pileOperateur.Push(afficheurParenthese[i]);
                        }
                        else if (afficheurParenthese[i] == ')')
                        {
                            pile.Push(long.Parse(temp));
                            temp = "";
                            operateur1 = (char)pileOperateur.Pop();

                            operande1 = (long)pile.Pop();
                            operande2 = (long)pile.Pop();
                            if (operateur1 == '*')
                            {
                                pile.Push(operande1 * operande2);
                            }

                            else if ((operateur1 == '/') && (operande2 == 0))
                            {
                                Afficheur.Text = "operation impossible";
                            }
                            else if (operateur1 == '/')
                            {
                                pile.Push((float)(operande1 / operande2));
                            }

                            else if (operateur1 == '+')
                            {
                                pile.Push(operande1 + operande2);
                            }
                            else if (operateur1 == '-')
                            {
                                pile.Push(operande1 - operande2);
                            }



                        }

                    }

                }
                Afficheur.Text = pile.Pop().ToString();
            }

            else
            {
                if (operateur1 == 'm') // l'operateur est le modulo, on effectue le traitement
                {
                    
                    int value;
                    if (int.TryParse(Afficheur.Text, out value)) // test de la saisie
                    {
                        operande2 = int.Parse(Afficheur.Text); // on récupère la deuxième opérande au clic sur le bouton égal
                        Afficheur.Text = "";
                        Afficheur.Text = (operande1 % operande2).ToString();
                    }
                    else
                    {
                        Afficheur.Text = "FATAL ERROR : Veuillez saisir un entier";
                    }

                }
                else // si l'operateur n'est pas le modulo on gere comme d'habitude
                {

                    operande2 = int.Parse(Afficheur.Text); // on récupère la deuxième opérande au clic sur le bouton égal
                    Afficheur.Text = "";

                    if (operateur1 == '*')
                    {
                        Afficheur.Text = (operande1 * operande2).ToString();
                    }

                    else if ((operateur1 == '/') && (operande2 == 0))
                    {
                        Afficheur.Text = "operation impossible";
                    }
                    else if (operateur1 == '/')
                    {
                        Afficheur.Text = ((float)operande1 / operande2).ToString();
                    }

                    else if (operateur1 == '+')
                    {
                        Afficheur.Text = (operande1 + operande2).ToString();
                    }
                    else if (operateur1 == '-')
                    {
                        Afficheur.Text = (operande1 - operande2).ToString();
                    }
                    else if (operateur1 == '^')
                    {
                        Afficheur.Text = Math.Pow(operande1, operande2).ToString();
                    }

                }

            }

        }

        private void BtnOption_Click(object sender, EventArgs e)
        {
            BtnOption.Visible = false;
            Form1.ActiveForm.Width = 450;
            BtnReduire.Visible = true;

        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            if ((Afficheur.Text) != null)
            {
                memoire = long.Parse(Afficheur.Text);

            }
            BtnMin.Visible = false;
            BtnMout.Visible = true;

        }

        private void BtnMout_Click(object sender, EventArgs e)
        {
            Afficheur.Text = memoire.ToString();

        }

        private void BtnReduire_Click(object sender, EventArgs e)
        {

            Form1.ActiveForm.Width = 290;
            BtnReduire.Visible = false;
            BtnOption.Visible = true;

        }

        private void ParDroite_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + ")";
        }

        private void ParGauche_Click(object sender, EventArgs e)
        {
            parenthese = true;
            Afficheur.Text = Afficheur.Text + "(";
        }

        private void BtnMclear_Click(object sender, EventArgs e)
        {
            BtnMout.Visible = false;
            BtnMin.Visible = true;
            memoire = 0;
        }

        private void BtnXn_Click(object sender, EventArgs e)
        {
            if (Afficheur.Text == " ")
            {
                operande1 = 0;
            }
            else
            {
                operande1 = int.Parse(Afficheur.Text);
                operateur1 = '^';
                Afficheur.Text = "";
            }
        }

        private void BtnXcube_Click(object sender, EventArgs e)
        {
            if (Afficheur.Text == "")
            {
                operande1 = 0;
            }
            else
            {
                operande1 = int.Parse(Afficheur.Text);
                Afficheur.Text = Math.Pow(operande1, 3).ToString();
            }
        }

        private void BtnXcarre_Click(object sender, EventArgs e)
        {
            if (Afficheur.Text == "")
            {
                operande1 = 0;
            }
            else
            {
                operande1 = int.Parse(Afficheur.Text);
                Afficheur.Text = Math.Pow(operande1, 2).ToString();
            }
        }

        private void BtnRacine_Click(object sender, EventArgs e)
        {
            if (Afficheur.Text == "")
            {
                operande1 = 0;
            }
            else
            {
                operande1 = int.Parse(Afficheur.Text);
                Afficheur.Text = Math.Sqrt(operande1).ToString();
            }
        }

        private void BtnRandInt_Click(object sender, EventArgs e)
        {
            Afficheur.Text = "";
            Afficheur.Text = random.Next().ToString();
        }

        private void BtnRandReel_Click(object sender, System.EventArgs e)
        {

            Afficheur.Text = "";
            Afficheur.Text = (random.Next()).ToString() + "," + (random.Next()).ToString();
        }

        private void BtnPi_Click(object sender, System.EventArgs e)
        {
            Afficheur.Text = "";
            Afficheur.Text = (Math.PI).ToString();
            //Afficheur.Text = "3,14159265358979";
        }

       

        private void opacitéOnToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form1.ActiveForm.Opacity = 0.6;
        }

        private void opacitéOffToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Form1.ActiveForm.Opacity = 1;
        }

        private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
        {
            BtnA.Visible = false;
            BtnB.Visible = false;
            BtnC.Visible = false;
            BtnD.Visible = false;
            BtnE.Visible = false;
            BtnF.Visible = false;
            BtnEgal.Visible = true;
            BtnReduire.Visible = false;
            Btn2.Visible = true;
            Btn3.Visible = true;
            Btn4.Visible = true;
            Btn5.Visible = true;
            Btn6.Visible = true;
            Btn7.Visible = true;
            Btn8.Visible = true;
            Btn9.Visible = true;
            BtnMoins.Visible = true;
            BtnPlus.Visible = true;
            BtnDiv.Visible = true;
            BtnMult.Visible = true;
            BtnXn.Visible = true;
            BtnXcarre.Visible = true;
            BtnXcube.Visible = true;
            BtnRacine.Visible = true;
            ParGauche.Visible = true;
            ParDroite.Visible = true;
            BtnMin.Visible = true;
            BtnMclear.Visible = true;
            BtnMout.Visible = false;
            BtnRandInt.Visible = true;
            BtnRandReel.Visible = true;
            BtnPi.Visible = true;
            BtnOption.Visible = true;

            if ((baseNum == "bin") && (Afficheur.Text != ""))
            {
                string binD = Afficheur.Text;
            Afficheur.Text = Convert.ToInt32(binD, 2).ToString();        
            }

            else if ((baseNum == "octa") && (Afficheur.Text != ""))
            {
                string octD = Afficheur.Text;
                Afficheur.Text = Convert.ToInt32(octD, 8).ToString();
            }

            else if ((baseNum == "hexa") && (Afficheur.Text != ""))
            {
                string hexD = Afficheur.Text;
                Afficheur.Text = Convert.ToInt32(hexD, 16).ToString();
            }


            baseNum = "dec";

        }

        private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
        {
            BtnA.Visible = false;
            BtnB.Visible = false;
            BtnC.Visible = false;
            BtnD.Visible = false;
            BtnE.Visible = false;
            BtnF.Visible = false;
            BtnEgal.Visible = false;
            BtnReduire.Visible = false;
            Btn2.Visible = false;
            Btn3.Visible = false;
            Btn4.Visible = false;
            Btn5.Visible = false;
            Btn6.Visible = false;
            Btn7.Visible = false;
            Btn8.Visible = false;
            Btn9.Visible = false;
            BtnMoins.Visible = false;
            BtnPlus.Visible = false;
            BtnDiv.Visible = false;
            BtnMult.Visible = false;
            BtnXn.Visible = false;
            BtnXcarre.Visible = false;
            BtnXcube.Visible = false;
            BtnRacine.Visible = false;
            ParGauche.Visible = false;
            ParDroite.Visible = false;
            BtnMin.Visible = false;
            BtnMclear.Visible = false;
            BtnMout.Visible = false;
            BtnRandInt.Visible = false;
            BtnRandReel.Visible = false;
            BtnPi.Visible = false;
            BtnOption.Visible = false;
            
            if ((baseNum == "dec")&&(Afficheur.Text != ""))
            {
                int decB = int.Parse(Afficheur.Text);
            Afficheur.Text = Convert.ToString(decB, 2);       
            }

            else if ((baseNum == "octa") && (Afficheur.Text != ""))
            {
                string octB = Afficheur.Text;
                octB = Convert.ToInt32(octB, 8).ToString();
                Afficheur.Text = Convert.ToString(int.Parse(octB), 2);
            }

            else if ((baseNum == "hexa") && (Afficheur.Text != ""))
            {
                string hexB = Afficheur.Text;
                hexB= Convert.ToInt32(hexB, 16).ToString();
                Afficheur.Text = Convert.ToString(int.Parse(hexB), 2);

            }
            
            baseNum = "bin";
        }

        private void radioButton3_CheckedChanged(object sender, System.EventArgs e)
        {

            BtnA.Visible = true;
            BtnB.Visible = true;
            BtnC.Visible = true;
            BtnD.Visible = true;
            BtnE.Visible = true;
            BtnF.Visible = true;
            BtnEgal.Visible = false;
            BtnReduire.Visible = false;
            Btn2.Visible = true;
            Btn3.Visible = true;
            Btn4.Visible = true;
            Btn5.Visible = true;
            Btn6.Visible = true;
            Btn7.Visible = true;
            Btn8.Visible = true;
            Btn9.Visible = true;
            BtnMoins.Visible = false;
            BtnPlus.Visible = false;
            BtnDiv.Visible = false;
            BtnMult.Visible = false;
            BtnXn.Visible = false;
            BtnXcarre.Visible = false;
            BtnXcube.Visible = false;
            BtnRacine.Visible = false;
            ParGauche.Visible = false;
            ParDroite.Visible = false;
            BtnMin.Visible = false;
            BtnMclear.Visible = false;
            BtnMout.Visible = false;
            BtnRandInt.Visible = false;
            BtnRandReel.Visible = false;
            BtnPi.Visible = false;
            BtnOption.Visible = false;

            if ((baseNum == "dec") && (Afficheur.Text != ""))
            {
                string decH = Afficheur.Text;
                Afficheur.Text = int.Parse(decH).ToString("X");

            }

            else if ((baseNum == "bin") && (Afficheur.Text != ""))
            {
                string binH = Afficheur.Text;
                Afficheur.Text = Convert.ToInt32(binH, 2).ToString("X");
            }

            else if ((baseNum == "octa") && (Afficheur.Text != ""))
            {
                string binH = Afficheur.Text;
                Afficheur.Text = Convert.ToInt32(binH, 8).ToString("X");
            }

            baseNum = "hexa";
        }

        private void radioButton4_CheckedChanged(object sender, System.EventArgs e)
        {
            BtnA.Visible = false;
            BtnB.Visible = false;
            BtnC.Visible = false;
            BtnD.Visible = false;
            BtnE.Visible = false;
            BtnF.Visible = false;
            BtnEgal.Visible = false;
            BtnReduire.Visible = false;
            Btn2.Visible = true;
            Btn3.Visible = true;
            Btn4.Visible = true;
            Btn5.Visible = true;
            Btn6.Visible = true;
            Btn7.Visible = true;
            Btn8.Visible = false;
            Btn9.Visible = false;
            BtnMoins.Visible = false;
            BtnPlus.Visible = false;
            BtnDiv.Visible = false;
            BtnMult.Visible = false;
            BtnXn.Visible = false;
            BtnXcarre.Visible = false;
            BtnXcube.Visible = false;
            BtnRacine.Visible = false;
            ParGauche.Visible = false;
            ParDroite.Visible = false;
            BtnMin.Visible = false;
            BtnMclear.Visible = false;
            BtnMout.Visible = false;
            BtnRandInt.Visible = false;
            BtnRandReel.Visible = false;
            BtnPi.Visible = false;
            BtnOption.Visible = false;

            if ((baseNum == "dec") && (Afficheur.Text != ""))
            {
                string decO = Afficheur.Text;
                Afficheur.Text = (Convert.ToString(int.Parse(decO),8));

            }

            else if ((baseNum == "bin") && (Afficheur.Text != ""))
            {
                string binO = Afficheur.Text;
                binO = Convert.ToInt32(binO, 2).ToString();
                Afficheur.Text = Convert.ToString(int.Parse(binO), 8);
            }

            else if ((baseNum == "hexa") && (Afficheur.Text != ""))
            {
                string hexO = Afficheur.Text;
                Afficheur.Text = Convert.ToString(Convert.ToInt32(hexO, 16), 8);
            }


            baseNum = "octa";
        }

        private void BtnA_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "A";
        }

        private void BtnB_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "B";
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "C";
        }

        private void BtnD_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "D";
        }

        private void BtnE_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "E";
        }

        private void BtnF_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + "F";
        }

        private void BtnArrondi_Click(object sender, EventArgs e)
        {
            Arr0.Visible = true;
            Arr2.Visible = true;        

        }

        private void Arr0_Click(object sender, EventArgs e)
        {
            Arr0.Visible = false;
            Arr2.Visible = false;

            double resultatDouble = Convert.ToDouble(Afficheur.Text); // on converti la string en double
            Afficheur.Text = (Math.Round(resultatDouble, 0)).ToString(); // on utilise la fonction qui gere l'arrondi

        }

        private void Arr2_Click(object sender, EventArgs e)
        {
            Arr0.Visible = false;
            Arr2.Visible = false;
            double resultatDouble = Convert.ToDouble(Afficheur.Text); 
            Afficheur.Text = (Math.Round(resultatDouble, 2)).ToString();
        }

        private void BtnFactorielle_Click(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(Afficheur.Text, out value)) //On vérifie d'abord que la string rentrée est bien un int et pas autre chose.
            {
                int facto = int.Parse(Afficheur.Text);
                int resultat = 1;

                if (facto > 1)
                {
                    for (int i = facto; i >= 1; i--) 
                    {
                        resultat = i * resultat;
                    }
                    Afficheur.Text = resultat.ToString();
                }

                else Afficheur.Text = "1"; // si le nombre est 1, afficher 1
            }
                 else{
    Afficheur.Text = "FATAL ERROR : Veuillez saisir un entier"; //Message d'erreur si la saisie n'est pas bonne.
    }
        }

   
        private void BtnModulo_Click(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(Afficheur.Text, out value))
            {
                operande1 = int.Parse(Afficheur.Text);
                Afficheur.Text = "";
                operateur1 = 'm'; // on défini l'opérateur sur modulo et on gerera le traitement avec le bouton égal après la deuxième opérande saisie
            }
            else {
                Afficheur.Text = "FATAL ERROR : Veuillez saisir un entier"; //Message d'erreur si la saisie n'est pas bonne.
            
            }
        }

        private void BtnVirgule_Click(object sender, EventArgs e)
        {
            Afficheur.Text = Afficheur.Text + ",";
        }


    }
}
