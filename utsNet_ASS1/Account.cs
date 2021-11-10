using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace utsNet_ASS1
{
    class Account
    {

        public void Createaccount()
        {




            do
            {
                String FirstName;
                String LastName;
                String Address;
                String Email;
               


               // clear the console 
                Console.Clear();

                // layout 
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|               Welocome to My Bank                 |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|\t\t    Enter the Details \t\t    |");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t Frist Name: ");
                int FirstName_Cursor_X = Console.CursorTop;
                int FirstName_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");
                Console.Write("\t\t|\t Last Name: ");
                int LastName_Cursor_X = Console.CursorTop;
                int LastName_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");
                Console.Write("\t\t|\t Address: ");
                int address_Cursor_X = Console.CursorTop;
                int address_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");

         
                Console.Write("\t\t|\t Phone: ");
                int Phone_Cursor_X = Console.CursorTop;
                int Phone_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");

                Console.Write("\t\t|\t Email: ");
                int Email_Cursor_X = Console.CursorTop;
                int Email_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");

                Console.Write("\t\t| ");
                int erro_Cursor_X = Console.CursorTop;
                int erro_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t\t\t    | ");
                Console.WriteLine("\t\t ===================================================");

                Console.SetCursorPosition(FirstName_Cursor_Y, FirstName_Cursor_X);
                FirstName = Console.ReadLine().ToString();

                Console.SetCursorPosition(LastName_Cursor_Y, LastName_Cursor_X);
                LastName = Console.ReadLine().ToString();

                Console.SetCursorPosition(address_Cursor_Y, address_Cursor_X);
                Address = Console.ReadLine().ToString();

                Console.SetCursorPosition(Phone_Cursor_Y, Phone_Cursor_X);
                string number = Console.ReadLine();
                int currentNumber;

                //coverting the string to in 
                if (!int.TryParse(number, out currentNumber))
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("\t\t{0} is not an integer", number);

                    Console.ReadLine();

                    Restconsole();


                }





                Console.SetCursorPosition(Email_Cursor_Y, Email_Cursor_X);
                Email = Console.ReadLine().ToString();
                // email validation for gmail
                String EmailCheck1 = "@gmail.com";
                String EmailCheck2 = "@Gmail.com";

                // check if the input value for email contains @gmail.com or not 
                if (!(Email.Contains(EmailCheck1) || Email.Contains(EmailCheck2)))
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("\t\t invalid email and for this system we only except only gmail");
                    Console.ReadLine();

                }
                else {


                    // getting the last file with the path 
                    var directory = new DirectoryInfo("C:/Users/CRACKER/source/UTS_assessment/utsNet_ASS1/utsNet_ASS1/Data/Account/");
                    var myFile = (from f in directory.GetFiles()
                                  orderby f.FullName descending
                                  select f).First().ToString();



                    //using the path and getting the file name
                    string filename = Path.GetFileName(myFile);
                    //filtering  the file name for example 100234.txt by removing .txt so the output will be 100234
                    var charsToRemove = new string[] { ".", "txt", ".", ";", "'" };
                    foreach (var c in charsToRemove)
                    {
                        filename = filename.Replace(c, string.Empty);
                    }


                    //changing the file name  to int and geting a new unique key just adding 1 
                    int x = Int32.Parse(filename);
                    x += 1;

                    Console.WriteLine("\t\t\t\t    ");
                    Console.WriteLine("\t\t\t\t    ");
                    Console.WriteLine("\t\t\t\t    ");
                    do
                    {

                        QuestionHolder("is this information correct (y/n)");
                        ConsoleKeyInfo Mykey = Console.ReadKey();
                     //   Console.WriteLine("my key" + Mykey + "  ");

                        if (Mykey.Key.ToString() == "y" || Mykey.Key.ToString() == "Y")
                        {
                            


                            Console.WriteLine("\t\t\t\t    ");
                            Console.WriteLine("\t\t\t\t    ");
                            Console.WriteLine("\t\t\t\t    ");
                            Console.WriteLine("\t\t Account created details will be provided via email    ");
                            Console.WriteLine("\t\t Account Number is :" + x + "  ");
                           // Console.ReadKey();

                            // here is the path to create a new file 
                            string NewfileName = @"C:\Users\CRACKER\source\UTS_assessment\utsNet_ASS1\utsNet_ASS1\Data\Account\" + x + ".txt";


                            // using the following path here we create the file and fill all the information which given 
                            using (StreamWriter sw = File.CreateText(NewfileName))
                            {

                                sw.WriteLine("First Name|" + FirstName);
                                sw.WriteLine("Last Name|" + LastName);
                                sw.WriteLine("Address|" + Address);
                                sw.WriteLine("Phone|" + currentNumber);
                                sw.WriteLine("Email|" + Email);
                                sw.WriteLine("AccountNo|" + x);
                                sw.WriteLine("Balance|0");


                            }
                            // smtp mail function
                            SmtpClient client = new SmtpClient()
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential()
                                {
                                    UserName = "utsproject97@gmail.com",
                                    Password = "Abc1234@"
                                }

                            };
                            MailAddress FromEmail = new MailAddress("utsproject97@gmail.com", ".Netc#");
                            MailAddress ToEmail = new MailAddress(Email, FirstName);
                            MailMessage message = new MailMessage()
                            {
                                // message body 
                                From = FromEmail,
                                Subject = "AccountInformation",
                                Body = " here is you account information " + FirstName + "" + LastName + "" + x + ""
                            };
                            message.To.Add(ToEmail);
                            // using a try function to check if the email is send or if there is any error
                            try
                            {
                                client.Send(message);
                                Console.WriteLine("\t\t Email have sent to your email successfully");
                                Console.ReadKey();
                                Back();
                                break;

                            }
                            catch (Exception exp)
                            {
                                Console.WriteLine("\t something when wrong" + exp.Message + "error");
                            }
                           // break;

                            //   break;
                            Console.ReadKey();
                            Back();
                            break;

                        }
                        else if (Mykey.Key.ToString() == "n" || Mykey.Key.ToString() == "N")
                        {
                            
                            Restconsole();
                            break;

                        }
                        else
                        {
                            Console.WriteLine("\t\t\t\t    ");
                            Console.WriteLine("\t\t\t\t    ");
                            Console.WriteLine(" pls only enter Y For Yes AND N for No");
                            Console.ReadKey();
                          
                        }

                    } while (true);

                
                }
            







            } while (true);

          
        }

        public void Restconsole()
        {
            // obejct of class account
            Account ac = new Account();
            ac.Createaccount();
        }
        public void Back()
        {
            // create a back function so its easy to navigat back to main page 
            MainMenu main = new MainMenu();
            main.AllMenu();
        }

        public void QuestionHolder(String commen)
        {
           // ask yes or no question 
            Console.Write("\t\t" + commen);
            int question_Cursor_X = Console.CursorTop;
            int question_Cursor_Y = Console.CursorLeft;
            Console.SetCursorPosition(question_Cursor_Y, question_Cursor_X);

        }

        public void SearchAccount()
        {

            do
            {
                List<string> users = new List<string>();
                List<string> pass = new List<string>();

                String UserInput;


               

                // when i strat my do while loop im going to emtpy the screen 
                Console.Clear();

                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|               Search for an account               |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|\t\t    ENTER THE DETAILS \t\t    |");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t Account Number: ");
                int AccountNumber_Cursor_X = Console.CursorTop;
                int AccountNumber_Cursor_Y = Console.CursorLeft;

                Console.WriteLine("\t\t\t    |");
                Console.Write("\t\t| ");
                int erro_Cursor_X = Console.CursorTop;
                int erro_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t\t\t    | ");
                Console.WriteLine("\t\t ===================================================");

                Console.SetCursorPosition(AccountNumber_Cursor_Y, AccountNumber_Cursor_X);
                UserInput = Console.ReadLine();


                String FindAccount;
             

                List<string> temptital = new List<string>();
                List<string> tempvalue = new List<string>();

                List<string> AccountData = new List<string>();
                

               
                int AccountNumber;
                // checking if input account number is int or not 
                if (!Int32.TryParse(UserInput, out AccountNumber))
                {
                    Console.SetCursorPosition(erro_Cursor_Y, erro_Cursor_X);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("pls only use integeras and not more then 10 value");
                    Console.ReadKey();

                }
                else
                {
                    // path of the file 
                    FindAccount = @"C:\Users\CRACKER\source\UTS_assessment\utsNet_ASS1\utsNet_ASS1\Data\Account\" + AccountNumber + ".txt";
                  
                    // check if the file exists or not so it dose not show any erro
                    if (File.Exists(FindAccount))
                    {

                        String[] lines = System.IO.File.ReadAllLines(FindAccount);

                        Console.WriteLine("\t\t\t\t Account Found!");
                        Console.WriteLine("\t\t");
                      
                        Console.WriteLine("\t\t ===================================================");
                        Console.WriteLine("\t\t|                Account Details                    |");
                        Console.WriteLine("\t\t ===================================================");
                       
                        Console.WriteLine("\t\t| \t\t\t\t\t\t    |");

                        // looping and add all the information to a list call AccountData
                        for (int i = 0; i < lines.Count(); i++)
                        {

                            AccountData.Add(lines[i]);

                           


                        }




                        // using account data to split the data using | where the title and the value in a separate list
                        foreach (string line in AccountData)
                        {
                            String[] comp = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            temptital.Add(comp[0]);
                            tempvalue.Add(comp[1]);
                          


                        }

                     
                        //displaying the list here 
                        Console.WriteLine("\t\t|\t " + temptital[0] + ":" + tempvalue[0] + "\t\t\t    |");
                        Console.WriteLine("\t\t|\t " + temptital[1] + ":" + tempvalue[1] + "\t\t\t    |");
                        Console.WriteLine("\t\t|\t " + temptital[2] + ":" + tempvalue[2] + "\t    |");
                        Console.WriteLine("\t\t|\t " + temptital[3] + ":" + tempvalue[3] + "\t\t\t    |");
                        Console.WriteLine("\t\t|\t " + temptital[4] + ":" + tempvalue[4] + "\t    |");
                        Console.WriteLine("\t\t|\t " + temptital[5] + ":" + tempvalue[5] + "\t\t\t    |");
                        Console.WriteLine("\t\t|\t " + temptital[6] + ":" + tempvalue[6] + "\t\t\t            |");

                        Console.WriteLine("\t\t ===================================================");

                        // do while to ask y or n if any other key keep asking 
                        do {
                            QuestionHolder("check another account (y/n)?");
                            ConsoleKeyInfo Mykey = Console.ReadKey();
                         
                            if (Mykey.Key.ToString() == "y" || Mykey.Key.ToString() == "Y")
                            {
                                break;
                            }
                            else if (Mykey.Key.ToString() == "N" || Mykey.Key.ToString() == "n")
                            {
                                Back();
                            }
                            else {
                                Console.WriteLine("\t\t");
                                Console.WriteLine("\t\t pls only enter y for yes or no for n");
                            }
                        } while (true);


                       
                        Console.ReadLine();


                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("\t\t Account does not exist");
                     //   QuestionHolder("check");
                        Console.ReadLine();
                    }




                }









            } while (true);
         
        }

        public void Deposit()
        {
            do
            {
                List<string> users = new List<string>();
                List<string> temptital = new List<string>();
                List<string> tempvalue = new List<string>();

                float Amount;
                string tempAmount;
                String tempAccount;
            

                // clear the console
                Console.Clear();

                // layout
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|                     DEPOSIT                       |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|\t\t    ENTER THE DETAILS \t\t    |");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t Account Number: ");
                int AccountNumber_Cursor_X = Console.CursorTop;
                int AccountNumber_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t    |");
                Console.Write("\t\t|\t Amount:$ ");
                int Amount_Cursor_X = Console.CursorTop;
                int Amount_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");
                Console.Write("\t\t| ");
                int erro_Cursor_X = Console.CursorTop;
                int erro_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t\t\t    | ");
                Console.WriteLine("\t\t ===================================================");

                Console.SetCursorPosition(AccountNumber_Cursor_Y, AccountNumber_Cursor_X);
                tempAccount = Console.ReadLine();
             

                Console.SetCursorPosition(Amount_Cursor_Y, Amount_Cursor_X);
                tempAmount = Console.ReadLine();
           



                String FindAccount;
                
                // getting today data and saving it on a variable
                DateTime today = DateTime.Today;



                int AccountNumber;
                // checking if input account number is or not 
                if (!Int32.TryParse(tempAccount, out AccountNumber))
                {
                    Console.SetCursorPosition(erro_Cursor_Y, erro_Cursor_X);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("\t\t pls only use integeras and not more then 10 value");
                    Console.ReadKey();

                }
                // check the amount is float or not
                else if (!float.TryParse(tempAmount, out Amount))
                {
                    Console.WriteLine("Not a valid float");
                }
                else
                {

                    FindAccount = @"C:\Users\CRACKER\source\UTS_assessment\utsNet_ASS1\utsNet_ASS1\Data\Account\" + AccountNumber + ".txt";
                    
                    // check if the file exists or not 
                    if (File.Exists(FindAccount))
                    {
                        String[] lines = System.IO.File.ReadAllLines(FindAccount);
                        Console.WriteLine("\t\t");
                        Console.WriteLine("\t\t");
                        Console.WriteLine("\t\t");
                        Console.WriteLine();
                        Console.WriteLine("\t\t Account found !");
                       

                        // loop u to 7 and save all the data in users
                        for (int i = 0; i < 7; i++)
                        {

                            users.Add(lines[i]);

                        }

                        // split the list user by | into 2 different list title and value
                        foreach (string line in users)
                        {
                            String[] comp = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            temptital.Add(comp[0]);
                            tempvalue.Add(comp[1]);
                           


                        }
                        
                        // get the value for the list and saving it has a string 
                        string convertamount = tempvalue[6].ToString();
                        //converting the amout to float
                        float Balance = float.Parse(convertamount);

                        //get the balance form the txt file and add with the ented amount
                        Balance += Amount;
                        // replacing the string to the new balance
                        lines[6] = "Balance|" + Balance;
                        Console.WriteLine("\t\t Deposit successful here is your new balance $" + Balance);
                        File.WriteAllLines(FindAccount, lines);

                        using (StreamWriter writer = new StreamWriter(FindAccount, true))
                        {

                            // adding the transition history
                            writer.WriteLine(today.ToString("dd.MM.yyyy") + "|Deposit|" + Amount + "|" + Balance);

                            writer.Close();



                        }

                         //  asking yes or no with do while loop 
                        do
                        {
                            QuestionHolder("Find someother account (y/n)?");
                            ConsoleKeyInfo Mykey = Console.ReadKey();
                       
                            if (Mykey.Key.ToString() == "y" || Mykey.Key.ToString() == "Y")
                            {
                             
                                break;
                            }
                            else if (Mykey.Key.ToString() == "N" || Mykey.Key.ToString() == "n")
                            {
                                Back();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\t\t");
                                Console.WriteLine("\t\t pls only enter y for yes or no for n");
                            }
                        } while (true);


                        Console.ReadLine();


                    }
                    else
                    {
                        Console.WriteLine("\t\t ");
                        Console.WriteLine("\t\t Account Not found !");

                        do
                        {
                            QuestionHolder("Find someother account (y/n)?");
                            ConsoleKeyInfo Mykey = Console.ReadKey();
                            
                            if (Mykey.Key.ToString() == "y" || Mykey.Key.ToString() == "Y")
                            {
                         
                                break;
                            }
                            else if (Mykey.Key.ToString() == "N" || Mykey.Key.ToString() == "n")
                            {
                                Back();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\t\t");
                                Console.WriteLine("\t\t pls only enter y for yes or no for n");
                            }
                        } while (true);

                    }




                }


            } while (true);
           
        }

        public void Withdrawal()
        {
            do
            {
                List<string> users = new List<string>();
                List<string> temptital = new List<string>();
                List<string> tempvalue = new List<string>();

                float Amount;
                string tempAmount;
                String tempAccount;
    


                Console.Clear();

                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|                     Withdrawal                    |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|\t\t    ENTER THE DETAILS \t\t    |");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t Account Number: ");
                int AccountNumber_Cursor_X = Console.CursorTop;
                int AccountNumber_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t    |");
                Console.Write("\t\t|\t Amount:$ ");
                int Amount_Cursor_X = Console.CursorTop;
                int Amount_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");
                Console.Write("\t\t| ");
                int erro_Cursor_X = Console.CursorTop;
                int erro_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t\t\t    | ");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t");

                Console.SetCursorPosition(AccountNumber_Cursor_Y, AccountNumber_Cursor_X);
                tempAccount = Console.ReadLine();
           

                Console.SetCursorPosition(Amount_Cursor_Y, Amount_Cursor_X);
                tempAmount = Console.ReadLine();
            


                String FindAccount;
                //    get the current data
                DateTime today = DateTime.Today;



                int AccountNumber;
                // check if its int or not 
                if (!Int32.TryParse(tempAccount, out AccountNumber))
                {
                    Console.SetCursorPosition(erro_Cursor_Y, erro_Cursor_X);
                    Console.WriteLine("pls only use integeras and not more then 10 value");
                    Console.ReadKey();

                }
                // check if its float or not
                else if (!float.TryParse(tempAmount, out Amount))
                {
                    Console.WriteLine("Not a valid float");
                }
                else
                {

                    FindAccount = @"C:\Users\CRACKER\source\UTS_assessment\utsNet_ASS1\utsNet_ASS1\Data\Account\" + AccountNumber + ".txt";
                 
                    // check if file exists or not 
                    if (File.Exists(FindAccount))
                    {
                        String[] lines = System.IO.File.ReadAllLines(FindAccount);
                     


                        // loop only up to 7 and save the information to list users 
                        for (int i = 0; i < 7; i++)
                        {

                            users.Add(lines[i]);

                      


                        }
                         // split the list user using | and save the data in 2 other list has title and value
                        foreach (string line in users)
                        {
                            String[] comp = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            temptital.Add(comp[0]);
                            tempvalue.Add(comp[1]);
                           

                        }
                     
                        //getting the valu for the list and storing it on a stiring 
                        string convertamount = tempvalue[6].ToString();
                        // converting the string in to float
                        float balance = float.Parse(convertamount);

                        // check if the current balance amount is bigger than the Withdrawal amount
                        if (balance >= Amount)
                        {
                            balance -= Amount;
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("\t\t withdrawal successful here is your remaining balance $" + balance );
                        }
                        else {
                            Console.WriteLine("\t your balance is $ "+ balance + "the amout that you have enter is more the your account ballance");
                        }
                        // updating the balance amount in the list
                        lines[6] = "Balance|" + balance;
                        File.WriteAllLines(FindAccount, lines);

                        using (StreamWriter writer = new StreamWriter(FindAccount, true))
                        {

                            //writing transition history on the txt file
                            writer.WriteLine(today.ToString("dd.MM.yyyy") + "|Withdraw|" + Amount + "|" + balance);

                            writer.Close();



                        }
                          // using do while loop for y and n 
                        do
                        {
                            QuestionHolder("Find someother account (y/n)?");
                            ConsoleKeyInfo Mykey = Console.ReadKey();
                          
                            if (Mykey.Key.ToString() == "y" || Mykey.Key.ToString() == "Y")
                            {
                                //  Restconsole();
                                break;
                            }
                            else if (Mykey.Key.ToString() == "N" || Mykey.Key.ToString() == "n")
                            {
                                Back();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\t\t");
                                Console.WriteLine("\t\t pls only enter y for yes or no for n");
                            }
                        } while (true);


                        Console.ReadLine();


                    }
                    else
                    {
                        Console.WriteLine("\t\t ");
                        Console.WriteLine("\t\t Account Not found !");

                        do
                        {
                            QuestionHolder("Find someother account (y/n)?");
                            ConsoleKeyInfo Mykey = Console.ReadKey();
                            //   Console.WriteLine("my key" + Mykey + "  ");
                            if (Mykey.Key.ToString() == "y" || Mykey.Key.ToString() == "Y")
                            {
                                //  Restconsole();
                                break;
                            }
                            else if (Mykey.Key.ToString() == "N" || Mykey.Key.ToString() == "n")
                            {
                                Back();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\t\t");
                                Console.WriteLine("\t\t pls only enter y for yes or no for n");
                            }
                        } while (true);
                    }




                }


            } while (true);
        
        }

        public void AccountStatement()  {
            do
            {
                List<string> AccountData = new List<string>();
                List<string> AccountUserInfo = new List<string>();

                List<string> temptital = new List<string>();
                List<string> tempvalue = new List<string>();

                String tempAccount;
                String FindAccount;
              
               



                // when i strat my do while loop im going to emtpy the screen 
                Console.Clear();

                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|               AccountStatement                     |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|\t\t    ENTER THE DETAILS  \t\t    |");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t AccountNumber: ");
                int AccountNumber_Cursor_X = Console.CursorTop;
                int AccountNumber_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");
                Console.Write("\t\t| ");
                int erro_Cursor_X = Console.CursorTop;
                int erro_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t\t\t    | ");
                Console.WriteLine("\t\t ===================================================");

                Console.SetCursorPosition(AccountNumber_Cursor_Y, AccountNumber_Cursor_X);
                tempAccount = Console.ReadLine().ToString();

                int AccountNumber;
                // checkking the lenght of the string befor converting to see if the length is more then 10 or not 
                if (tempAccount.Length > 10)
                {
                    Console.WriteLine("account number should not more than 10 characters ");
                    Console.ReadKey();
                }
                else {
                      // converting string to int 
                    if (!Int32.TryParse(tempAccount, out AccountNumber))
                    {
                        Console.SetCursorPosition(erro_Cursor_Y, erro_Cursor_X);
                        Console.WriteLine("pls only use integeras ");
                        Console.ReadKey();

                    }
                    else {

                    
                        

                        FindAccount = @"C:\Users\CRACKER\source\UTS_assessment\utsNet_ASS1\utsNet_ASS1\Data\Account\" + AccountNumber + ".txt";
                       
                        // check if the file exists or not 
                        if (File.Exists(FindAccount))
                        {
                            String[] lines = System.IO.File.ReadAllLines(FindAccount);

                            // loop the txt file and save the data in list 
                            for (int i = 0; i < lines.Count(); i++)
                            {

                                AccountData.Add(lines[i]);



                            }


                            Console.WriteLine("\t\t ===================================================");
                            Console.WriteLine("\t\t|               AccountStatement                     |");
                            Console.WriteLine("\t\t ===================================================");
                            Console.WriteLine("\t\t|\t\t    ENTER THE DETAILS  \t\t    |");
                            Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                        
                          //loop the list and save only the frist 7 item 
                            for (int i = 0; i < 7; i++)
                            {
                                AccountUserInfo.Add(AccountData[i]);
                                
                     

                            }

                            // split the  saved 7 item  using | and store them in 2 different list
                            foreach (string line in AccountUserInfo)
                            {
                                String[] comp = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                temptital.Add(comp[0]);
                                tempvalue.Add(comp[1]);
                           
                            }

                            Console.WriteLine("\t\t|\t " + temptital[0] + ":" + tempvalue[0] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[1] + ":" + tempvalue[1] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[2] + ":" + tempvalue[2] + "\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[3] + ":" + tempvalue[3] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[4] + ":" + tempvalue[4] + "\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[5] + ":" + tempvalue[5] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[6] + ":" + tempvalue[6] + "\t\t\t            |");
                            Console.WriteLine("\t\t ===================================================");
                            Console.WriteLine("\t\t ===================================================");
                            Console.WriteLine("\t\t|                 transactions history              | ");
                            Console.WriteLine("\t\t ===================================================");
                            AccountData.RemoveRange(0, 7);
                            AccountData.ForEach(i => Console.WriteLine("\t\t\t"+i+"\t\t"));

                     
                            string output = string.Join(Environment.NewLine, AccountData.ToArray());

                 
                            Console.WriteLine("\t\t ===================================================");

                            do {
                                // aks for emali if yes send email using the smtp client
                                QuestionHolder("Email statment (y/n)");
                                ConsoleKeyInfo MykeyEmail = Console.ReadKey();
                                if (MykeyEmail.Key.ToString() == "y" || MykeyEmail.Key.ToString() == "Y")
                                {
                                    Console.WriteLine("\t");
                                    Console.WriteLine("\t Email have sent to your email");
                                    //  Console.ReadKey();\
                                    SmtpClient client = new SmtpClient()
                                    {
                                        Host = "smtp.gmail.com",
                                        Port = 587,
                                        EnableSsl = true,
                                        DeliveryMethod = SmtpDeliveryMethod.Network,
                                        UseDefaultCredentials = false,
                                        Credentials = new NetworkCredential()
                                        {
                                            UserName = "utsproject97@gmail.com",
                                            Password = "Abc1234@"
                                        }

                                    };
                                    MailAddress FromEmail = new MailAddress("utsproject97@gmail.com", ".Netc#");
                                    MailAddress ToEmail = new MailAddress(tempvalue[4], tempvalue[1]);
                                    MailMessage message = new MailMessage()
                                    {
                                        From = FromEmail,
                                        Subject = "AccountStatement",
                                        Body = output
                                        //" here is you account Statement for the resent 5 transtaion "+AccountData[0]+"" + AccountData[1] +"" + AccountData[2] +"" + AccountData[3] +"" + AccountData[4] +""
                                    };
                                    message.To.Add(ToEmail);

                                    try
                                    {
                                        client.Send(message);
                                        Console.WriteLine("\t Email have sent to your email successfully");
                                        Console.ReadKey();
                                        Back();
                                        break;

                                    }
                                    catch(Exception exp){  
                                        Console.WriteLine("\t something when wrong"+exp.Message+"error");
                                    }
                                    break;
                                }
                                else if (MykeyEmail.Key.ToString() == "n" || MykeyEmail.Key.ToString() == "N")
                                {
                                    Console.WriteLine("\t");
                                    Console.WriteLine("\t email will not sent ");
                                    //   Console.ReadKey();
                                    Back();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\t");
                                    Console.WriteLine("\t pls only enter y for yes or no for n");

                                    //    Console.ReadKey();
                                }
                            } while (true);




                            Console.ReadKey();


                            
                        }
                        else {
                            Console.WriteLine("no account ");
                            Console.ReadKey();
                        }




                        }

                }
           


            } while (true);
        }

        public void DeleteAccount() {
            do
            {
             

             
                String tempAccount;
                String FindAccount;


                List<string> temptital = new List<string>();
                List<string> tempvalue = new List<string>();

                List<string> AccountData = new List<string>();
              




                // when i strat my do while loop im going to emtpy the screen 
                Console.Clear();

                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|               Delete and Account                  |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|\t\t    ENTER THE DETAILS  \t\t    |");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t AccountNumber: ");
                int AccountNumber_Cursor_X = Console.CursorTop;
                int AccountNumber_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t    |");
                Console.Write("\t\t| ");
                int erro_Cursor_X = Console.CursorTop;
                int erro_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t\t\t    | ");
                Console.WriteLine("\t\t ===================================================");

                Console.SetCursorPosition(AccountNumber_Cursor_Y, AccountNumber_Cursor_X);
                tempAccount = Console.ReadLine().ToString();


                int AccountNumber;
                // check if the input lenght more the 10 or not 
                if (tempAccount.Length > 10)
                {
                    Console.WriteLine("account number should not more than 10 characters ");
                    Console.ReadKey();
                }
                else
                {
                    // converting to int 
                    if (!Int32.TryParse(tempAccount, out AccountNumber))
                    {
                        Console.SetCursorPosition(erro_Cursor_Y, erro_Cursor_X);
                        Console.WriteLine("pls only use integeras ");
                        Console.ReadKey();

                    }
                    else
                    {
                        FindAccount = @"C:\Users\CRACKER\source\UTS_assessment\utsNet_ASS1\utsNet_ASS1\Data\Account\" + AccountNumber + ".txt";
                        // checking if the file exists or not 
                        if (File.Exists(FindAccount))
                        {
                            String[] lines = System.IO.File.ReadAllLines(FindAccount);
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("\t\t Account Found!");
                            Console.WriteLine("\t\t");

                            Console.WriteLine("\t\t ===================================================");
                            Console.WriteLine("\t\t|                Account Details                    |");
                            Console.WriteLine("\t\t ===================================================");

                            Console.WriteLine("\t\t| \t\t\t\t\t\t    |");

                            //loop all the txt data in the txt file and store them in list 
                            for (int i = 0; i < lines.Count(); i++)
                            {

                                AccountData.Add(lines[i]);

                               


                            }




                           // split the list data using | and store them in 2 diffrent list 
                            foreach (string line in AccountData)
                            {
                                String[] comp = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                temptital.Add(comp[0]);
                                tempvalue.Add(comp[1]);



                            }

                             // layout

                            Console.WriteLine("\t\t|\t " + temptital[0] + ":" + tempvalue[0] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[1] + ":" + tempvalue[1] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[2] + ":" + tempvalue[2] + "\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[3] + ":" + tempvalue[3] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[4] + ":" + tempvalue[4] + "\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[5] + ":" + tempvalue[5] + "\t\t\t    |");
                            Console.WriteLine("\t\t|\t " + temptital[6] + ":" + tempvalue[6] + "\t\t\t            |");

                            Console.WriteLine("\t\t ===================================================");

                            do
                            {
                                // asking for confirmation before deletion that text file
                                QuestionHolder(" are you sure you want to delete this account (y/n)?");
                                ConsoleKeyInfo Mykey = Console.ReadKey();
                                //   Console.WriteLine("my key" + Mykey + "  ");
                                if (Mykey.Key.ToString() == "y" || Mykey.Key.ToString() == "Y")
                                {
                                    File.Delete(FindAccount);
                                    Console.WriteLine();
                                    Console.WriteLine("\t\t File deleted.");
                                    Console.ReadKey();
                                    Back();
                                    break;
                                }
                                else if (Mykey.Key.ToString() == "N" || Mykey.Key.ToString() == "n")
                                {
                                    Back();
                                }
                                else
                                {
                                    Console.WriteLine("\t\t");
                                    Console.WriteLine("\t\t pls only enter y for yes or no for n");
                                }
                            } while (true);


                        
                            Console.ReadLine();


                        }
                        else {
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("\t\t File not found!");
                        }

                        Console.ReadKey();
                    }
                }
                    } while (true);
           
        }
    }
}


   