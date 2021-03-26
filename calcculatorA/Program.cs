using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calcculatorA
{
    class Program
    {       
        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 100;
            Console.WindowTop = 0;
            Console.WindowLeft = 0;
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Räknaren funkar med double data typ och positiva och negativta talen.]");
            Console.WriteLine("[När räknaren är på mer än ett tecken i följd är oacceptabel.]");
            Console.WriteLine("[Det ska börja med ett tal eller minus tecken.]");
            Console.WriteLine("[Mata in tecken och ett tal.Retur knappen betyder '=']");
            Console.WriteLine("[För att koppla ifrån när räknaren är på tryck på knappen 'n']");

            Console.WriteLine("----------------------------------------------------------------------");
            Console.ResetColor();

            //Körs i gång Räknare.Bara accepterar y/n
            //Den här räknaren funkar med double data typ och positiva och negativa tal
            char onOf = ' ';
            bool answer = false;

            while (answer == false|| (char.ToLower(onOf) != 'y'&& char.ToLower(onOf) != 'n'))
            {
                Console.WriteLine("Kör i gång Räknaren(y/n)");
                answer = char.TryParse(Console.ReadLine(), out onOf);
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Räknaren är på:");
            
            Console.WriteLine("---------------");
            Console.ResetColor();
            //End

            string strInputData = "";
            char currc = ' ';
            
            //Looper då användaren tryckar på knappet 'n'
            while (char.ToLower(onOf) == 'y')
            {
                string strNumber="";
                ConsoleKeyInfo kNum = new ConsoleKeyInfo();
                char LastCharInString=' ';

                //Tar matade in tal eller tecknar på varje knapp stryck på tangenbordet
                do
                {
                    kNum = Console.ReadKey();
                    if (char.IsDigit(kNum.KeyChar))
                        strNumber += kNum.KeyChar;
                    else
                        currc = kNum.KeyChar;
                } while (char.IsDigit(kNum.KeyChar));

                strInputData += strNumber!=""?strNumber:"";
                if (strInputData.Length > 0) { LastCharInString = char.Parse(strInputData.Substring(strInputData.Length - 1, 1)); }
                //End 


                //På grund av Oprations knappar eller Retur knapp och 'n' knapp bästemmer 
                //fortsätter  att mata in eller räkna och visa resultat.
                //Om användaren matar ogiltig knappar exempelvis'++' eller '+\r' visar lämpligt meddelande
                switch (currc)
                {
                    case '\r':
                        if (char.IsDigit(LastCharInString))
                        {
                            Console.WriteLine();

                            Console.WriteLine();
                            Console.WriteLine("Result {0} = {1}", strInputData, DecisionA(strInputData));
                            for (int i = 0; i < (strInputData.Length+17); i++)
                            {
                                Console.Write("-");                               
                            }
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Next>");
                            Console.ResetColor();
                            
                            strInputData = "";
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\rTvå tecken i följed!");
                        }

                        break;
                    case ',':
                    case '.':
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        if(char.IsDigit(LastCharInString))
                            strInputData += (char)kNum.KeyChar;
                        else
                        {
                            if(strInputData.Length==0&& currc=='-')
                                strInputData += (char)kNum.KeyChar;
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("\rTvå tecken i följed  !");
                            }
                            
                        }
                        break;
                    case 'n':
                        Console.WriteLine();
                        Console.WriteLine("programmet stängs ner");
                        Console.ReadKey();
                        onOf = 'n';
                        break;
                    default:
                        Console.WriteLine("Ogiltig");
                        break;
                }
            }
        }
        static double DecisionA(string str)
        {
            //string str = "2+543*456-5+8+2*2/2+9";

            char operators = ' ';
            double quota = -1.0;
            double befor = -1.0;
            double after = -1;
            int operatorsIndex = -1;
            string priority = "*/+-";

            for (int i = 0; i < priority.Length; i++)
            {
                operators = char.Parse(priority.Substring(i, 1));
                operatorsIndex = str.IndexOf(operators);

                if (operatorsIndex == 0)//om första operator var tecken ska avstå
                    operatorsIndex = str.IndexOf(operators,1);

                //Plockar upp talen för och efter tecken och skickar till funktionen för räkning
                while (operatorsIndex != -1)
                {
                    after = getNumAfter(str, operatorsIndex);
                    befor = getNumBefor(str, operatorsIndex);

                    switch (operators)
                    {
                        case '*':
                            quota = Multiply(befor, after);
                            break;
                        case '/':
                            quota = Divid(befor, after);
                            break;
                        case '+':
                            quota = Add(befor, after);
                            break;
                        case '-':
                            quota = Subtract(befor, after);
                            break;
                        default:
                            break;
                    }

                    str = str.Replace('.', ',');
                    str = str.Replace(befor.ToString() + operators + after.ToString(), quota.ToString());

                    operatorsIndex = str.IndexOf(operators);
                    
                    if (operatorsIndex == 0)//om första operator var tecken ska avstå
                        operatorsIndex = str.IndexOf(operators, 1);
                    else
                        operatorsIndex = str.IndexOf(operators);                    
                }
            }
            str = str.Replace('.', ',');

            return double.Parse(str);
        }
        static double Add(double f, double s)
        {
            
            return f + s;
        }
        static double Subtract(double f, double s)
        {

            return f - s;
        }
        static double Divid(double f, double s)
        {

            return f / s;
        }
        static double Multiply(double f, double s)
        {
            return f * s;
        }
        static double getNumAfter(string str, int i)
        {
            //string strBefor = "";
            string strAfterr = "";
            //int quota = -1;
            //int befor = -1;
            double after = -1.0;
            //int operatorsIndex = -1;
            string strNum = "";


            i += 1;
            while (i < str.Length)
            {
                if ((char.IsDigit(char.Parse(str.Substring(i, 1)))))
                {
                    strNum += str.Substring(i, 1);
                    i++;
                }
                else if (char.Parse(str.Substring(i, 1)) == '.' || char.Parse(str.Substring(i, 1)) == ',')
                {
                    strNum += str.Substring(i, 1);
                    i++;
                }
                else
                    break;
            }
            strNum = strNum.Replace('.', ',');
            strAfterr = strNum;
            strNum = "";
            after = double.Parse(strAfterr);
            return after;
        }
        static double getNumBefor(string str, int i)
        {
            string strBefor = "";
            //string strAfterr = "";
            //int quota = -1;
            double befor = -1;
            //int after = -1;
            int operatorsIndex = -1;
            string strNum = "";


            i -= 1;
            while (i > -1)
            {
                if ((char.IsDigit(char.Parse(str.Substring(i, 1)))))
                {
                    strNum += str.Substring(i, 1);
                    i--;
                }
                else if (char.Parse(str.Substring(i, 1)) == '.'|| char.Parse(str.Substring(i, 1)) == ',')
                { 
                    strNum += str.Substring(i, 1);
                    i--;
                }
                else
                {
                    operatorsIndex = str.IndexOf(str.Substring(i, 1));
                    break;
                }
                    
               
            }
            string s = "";
            for (int j = strNum.Length - 1; j > -1; j--)
            {
                s += strNum.Substring(j, 1);
            }
            s = s.Replace('.', ',');
            if(operatorsIndex==0)
                strBefor ="-"+ s;
            else
                strBefor = s;
            
            befor = double.Parse(strBefor);

            return befor;
        }

    }
}
