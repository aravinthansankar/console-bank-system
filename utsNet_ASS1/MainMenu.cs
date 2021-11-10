using System;
using System.Collections.Generic;
using System.Text;

namespace utsNet_ASS1
{
    class MainMenu
    {

        public void AllMenu()
        {
            int UserInput;
            Account account = new Account();
            LoginMenu loginmenu = new LoginMenu();
            

                do
                {
            // when i strat my do while loop im going to emtpy the screen 
            Console.Clear();

                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|               Welocome to My Bank                 |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t 1. Create a New account ");
                Console.WriteLine("\t\t    |");
                Console.Write("\t\t|\t 2. Search for an account ");
                Console.WriteLine("\t\t    |");
                Console.Write("\t\t|\t 3. Deposit ");
                Console.WriteLine("\t\t                    |");
                Console.Write("\t\t|\t 4. Withdraw ");
                Console.WriteLine("\t\t                    |");
                Console.Write("\t\t|\t 5. A/C statement ");
                Console.WriteLine("\t\t\t    |");
                Console.Write("\t\t|\t 6. Delete account ");
                Console.WriteLine("\t\t\t    |");
                Console.Write("\t\t|\t 7. Exit ");
                Console.WriteLine("\t\t\t\t    |");
                Console.WriteLine("\t\t ===================================================");
                Console.Write("\t\t|\t  Enter your choice (1-7): ");
                int username_Cursor_X = Console.CursorTop;
                int username_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t    |");
                Console.WriteLine("\t\t ===================================================");

            Console.SetCursorPosition(username_Cursor_Y, username_Cursor_X);


                try {
                    UserInput = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException) {
                    UserInput = 0;
                    Console.WriteLine(" input cant be string  ");
                }
  

       
            int erro_Cursor_X = Console.CursorTop;
            int erro_Cursor_Y = Console.CursorLeft;
       
           // checking if the input value out of range
            if (UserInput > 7 && UserInput < 1 )
            {
                Console.SetCursorPosition(erro_Cursor_Y, erro_Cursor_X);
                Console.WriteLine(" input cant be more than 1-7 pls enter the right input ");
             //   Console.ReadKey();
            }
           
            else {
             
                if (UserInput == 1)
                {
                    account.Createaccount();
                    break;
                }
                else if (UserInput == 2)
                {
                        account.SearchAccount();
                        break;


                    }
                    else if (UserInput == 3)
                    {
                        account.Deposit();
                        break;


                    }
                    else if (UserInput == 4)
                    {
                        account.Withdrawal();
                        break;


                    }
                    else if (UserInput == 5)
                    {
                        account.AccountStatement();
                        break;


                    }
                    else if (UserInput == 6)
                    {
                        account.DeleteAccount();
                        break;


                    }
                    else if (UserInput == 7)
                    {
                        loginmenu.MainLogin();
                        break;


                    }

                }

          

           
           

            } while (true);
          
        }
    }
}
