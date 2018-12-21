using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace scaner
{
    class Program
    {
        public static string matchsingle(char s)
        {


            if (s == '+' || s == '-' || s == '*' || s == '/')
            {
                return "ArithmeticOperation";
            }
            else
            {
                if (s == '=')
                {
                    return "Assignmentoperator";

                }
                else
                {
                    if (s == '.')
                    {
                        return "AccessOperator";

                    }
                    else
                    {

                        if (s == '(' || s == ')' || s == '{' || s == '}' || s == '[' || s == ']')
                        {

                            return "Braces";
                        }
                        else
                        {
                            if (s == '"' || s.Equals("'"))
                            {
                                return "Quotation Mark";

                            }
                            else
                            {
                                if (s == '>' || s == '<')
                                {

                                    return "relationaloperators";
                                }
                                else
                                {
                                    if (s == ';')
                                    {
                                        return "semecolone";
                                    }
                                    else
                                    {
                                        if (s == '~')
                                        {
                                            return "Logicoperators";
                                        }

                                    }

                                }
                                


                            }


                        }

                    }


                }

            }

            return "other";
        }

        public static string match(string s, List<string> token)
        {
            foreach (string c in token)
            {
                if(s.Equals(" "))
                {
                    return "space";
                }

                string[] words = c.Split(' ');
                if (words[0].Equals(s))
                {
                    return words[1];
                }
            }
            return "other";
        }
        public static bool IsAllLettersOrDigitsOrUnderscores(string s)
        {
            int fristchar = 0;
            foreach (char c in s)
            {
                if (Char.IsDigit(c) && c != '_' && fristchar == 0)
                {
                    
                    return false;
                }

                if (!Char.IsLetterOrDigit(c) && c != '_')
                    return false;

                fristchar++;
            }
            return true;
        }

        public static bool IsAllDigits(char s)
        {

            if (!Char.IsDigit(s))
                return false;

            return true;
        }



        public static bool IsAllLetters(char s)
        {


            if (!Char.IsLetter(s))
                return false;

            return true;
        }

        static void Main(string[] args)
        {
            // read token from file 
            List<string> token = new List<string>();
            string line;
            //create file that have all token
            string file2 = "project.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(file2);
            while (!file.EndOfStream)
            {
                line = file.ReadLine();

                token.Add(line);

            }
            file.Close();

            // take input from user 
            List<string> input = new List<string>();
            string limted = "f";
            while (limted != "full")
            {
                limted = Console.ReadLine();
                input.Add(limted);

            }
            int check;
            List<string> toparser = new List<string>();
            //get token from one line 
            int countofline = 1,comment=0, comment2 = -1;
            foreach (String oneline in input)
            {
                // string[] words = oneline.Split(' ');
                bool check_for_string, chechData;
                int count_for_input = 0, count_for_stop_point = 0, constant_count = 0, constant_count2 = 0, constant_count3=0, relationaloperators_count = -1, Logicoperators_count = -1, Logicoperators_count2 = 0, data_count = -1, data_count2 = 0,numofindex=0,logic=0,check_equal=0;
                string kind_of_token, constanat, singlevalue = "a7a";
                foreach (char data in oneline)
                {
                    if (data == '-')
                    {
                        int x = count_for_input + 1;
                        if (oneline[x] == '-')
                        {
                            break;
                        }

                    }
                    if (data == '*')
                    {
                        if (comment == 1)
                        {
                            int x = count_for_input + 1;
                            if (oneline[x] == '>')
                            {
                                comment2 = x ;
                                  

                            }

                        }
                        else
                        {
                            if (count_for_input > 0)
                            {
                                int x = count_for_input - 1;
                                if (oneline[x] == '<')
                                {
                                    comment = 1;

                                }
                            }
                        }

                    }
                    
                    if (comment == 0)
                    {
                        if (data == '&')
                        {
                            if (Logicoperators_count2 == 0)
                            {
                                Logicoperators_count = count_for_input;

                                Logicoperators_count2 = 1;
                            }
                            else
                            {
                                if (Logicoperators_count2 == 1)
                                {
                                    Console.WriteLine("line:" + countofline + " && token type: Logicoperators");
                                    Logicoperators_count2 = 0;
                                    logic = 1;

                                }
                            }

                        }


                        if (data == '|')
                        {
                            if (Logicoperators_count2 == 0)
                            {
                                Logicoperators_count = count_for_input;

                                Logicoperators_count2 = 2;
                            }
                            else
                            {
                                if (Logicoperators_count2 == 2)
                                {
                                    Console.WriteLine("line:" + countofline + " || token type: Logicoperators");
                                    Logicoperators_count2 = 0;
                                    logic = 1;

                                }
                            }

                        }
                        if (relationaloperators_count == count_for_input)
                        {
                            if (data == '=')
                            {
                                int x = count_for_input - 1;
                                Console.WriteLine("line:" + countofline + " " + oneline.Substring(x, 2) + " token type:relationaloperators");
                                constant_count = count_for_input + 1;
                                check_equal = 1;

                            }
                            else
                            {
                                int x = count_for_input - 1;

                                Console.WriteLine("line:" + countofline + " " + oneline[x] + " token type:" + singlevalue);
                                constant_count = count_for_input + -1;


                            }

                        }
                        // string temp = match(data, token);
                        if (data_count2 == 0 && IsAllDigits(data) && count_for_input > 0)
                        {
                            if (constant_count2 == 0)
                            {

                                constant_count3 = count_for_input;
                            }
                            constant_count2++;


                        }
                        else
                        {
                            if (constant_count2 > 0)
                            {

                                Console.WriteLine("line:" + countofline + " " + oneline.Substring(constant_count3, constant_count2) + " token type:constant");
                                constant_count2 = 0;

                            }


                        }
                        if (data == ' ' && count_for_input > 0)
                        {
                            data_count2 = 0;
                            int x = count_for_input - count_for_stop_point;

                            string temp = match(oneline.Substring(count_for_stop_point, x), token);
                            if (temp.Equals("space"))
                            {


                            }
                            
                            if (temp.Equals("other"))
                            {
                                bool chck = IsAllLettersOrDigitsOrUnderscores(oneline.Substring(count_for_stop_point, x));
                                if (chck)
                                {
                                    Console.WriteLine("line:" + countofline + " " + oneline.Substring(count_for_stop_point, x) + " token type:idetifier");


                                }

                            }
                            else
                            {
                                if (temp.Equals("space"))
                                {

                                }
                                else
                                {
                                    Console.WriteLine("line:" + countofline + " " + oneline.Substring(count_for_stop_point, x) + " token type:" + temp);
                                }
                            }
                            count_for_stop_point = count_for_input + 1;
                        }
                        else
                        {


                            chechData = IsAllLetters(data);
                            if (chechData == true)
                            {
                                if (data_count2 == 0)
                                {
                                    data_count = count_for_input;
                                }
                                data_count2++;
                            }

                            if (chechData == false)
                            {

                                chechData = IsAllDigits(data);
                                if (chechData)
                                {
                                    if (data_count2 > 0)
                                    {
                                        data_count2++;
                                    }



                                }
                                else
                                {
                                    if (chechData == false)
                                    {
                                        if (data_count2 > 0)
                                        {

                                            string data2 = oneline.Substring(data_count, data_count2);
                                            kind_of_token = match(data2, token);
                                            if (kind_of_token.Equals("other"))
                                            {
                                                data_count2 = 0;

                                                bool chck = IsAllLettersOrDigitsOrUnderscores(data2);
                                                if (chck)
                                                {
                                                    Console.WriteLine("line:" + countofline + " " + data2 + " token type:idetifier");
                                                    data_count2 = 0;

                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("line:" + countofline + " " + oneline.Substring(data_count, data_count2) + " token type:" + kind_of_token);
                                                data_count2 = 0;
                                            }
                                        }
                                        kind_of_token = matchsingle(data);


                                        if (kind_of_token.Equals("ArithmeticOperation"))
                                        {

                                        }
                                        if (kind_of_token.Equals("relationaloperators") || kind_of_token.Equals("Assignmentoperator") || data == '!')
                                        {
                                            if (check_equal == 1)
                                            {
                                                check_equal = 0;

                                            }
                                            else
                                            {
                                                relationaloperators_count = count_for_input + 1;
                                                singlevalue = kind_of_token;
                                            }
                                        }
                                        else
                                        {
                                            if (Logicoperators_count2 == 0 && logic == 0)
                                            {
                                                if (!(data == ' ')) 
                                                Console.WriteLine("line:" + countofline + " " + data + " token type:" + kind_of_token);
                                            }

                                        }
                                        if (logic == 1)
                                        {
                                            logic = 0;
                                        }
                                    }

                                }

                            }
                        }
                       
                    }
                    if (comment2 == count_for_input)
                    {
                        comment = 0;
                    }
                    count_for_input++;
                }
                if (comment == 0)
                {
                    if (count_for_input > 0)
                    {

                        chechData = IsAllLetters(oneline[count_for_input - 1]);

                        if (chechData == true)
                        {

                            chechData = IsAllDigits(oneline[count_for_input - 1]);
                            if (chechData == true)
                            {
                                bool chck = IsAllLettersOrDigitsOrUnderscores(oneline.Substring(data_count, data_count2));
                                if (chck)
                                {
                                    Console.WriteLine("line:" + countofline + " " + oneline.Substring(data_count, data_count2) + " token type:idetifier");


                                }

                            }
                            else
                            {
                                if (chechData == false)
                                {
                                    if (data_count2 > 0)
                                    {
                                        kind_of_token = match(oneline.Substring(data_count, data_count2), token);
                                        if (kind_of_token.Equals("other"))
                                        {
                                            data_count2 = 0;

                                            bool chck = IsAllLettersOrDigitsOrUnderscores(oneline.Substring(data_count, data_count2));
                                            if (chck)
                                            {
                                                string info = oneline.Substring(data_count, data_count2);
                                                Console.WriteLine("line:" + countofline + " " + info + " token type:idetifier");


                                            }

                                        }
                                        else
                                        {
                                            string info = oneline.Substring(data_count, data_count2);

                                            Console.WriteLine("line:" + countofline + " " + info + " token type:" + kind_of_token);
                                            data_count2 = 0;
                                        }
                                    }


                                }
                            }
                        }
                    }
                }
                countofline++;

                Console.ReadLine();

                // spreat project into sub token

            }
        }
    }
}