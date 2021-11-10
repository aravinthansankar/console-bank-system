using System;
using System.Collections.Generic;
using System.Text;

namespace utsNet_ASS1
{
    class LoginMenu
    {

        public void MainLogin() {
            
            do
            {
                List<string> users = new List<string>();
                List<string> pass = new List<string>();

                String Username;
                String Password;

                
                //geting Login.txt file 
                String[] lines = System.IO.File.ReadAllLines(@"C:\Users\CRACKER\source\UTS_assessment\utsNet_ASS1\utsNet_ASS1\Data\login.txt");


                // when i strat my do while loop im going to emtpy the screen 
                Console.Clear();

                // layout of the loging screen 

                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|               Welocome to My Bank                 |");
                Console.WriteLine("\t\t ===================================================");
                Console.WriteLine("\t\t|\t\t    Login to start \t\t    |");
                Console.WriteLine("\t\t| \t\t\t\t\t\t    |");
                Console.Write("\t\t|\t UserName: ");
                int username_Cursor_X = Console.CursorTop;
                int username_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");
                Console.Write("\t\t|\t Password: ");
                int Password_Cursor_X = Console.CursorTop;
                int Password_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t    |");
                Console.Write("\t\t| ");
                int erro_Cursor_X = Console.CursorTop;
                int erro_Cursor_Y = Console.CursorLeft;
                Console.WriteLine("\t\t\t\t\t\t    | ");
                Console.WriteLine("\t\t ===================================================");

                Console.SetCursorPosition(username_Cursor_Y, username_Cursor_X);
                Username = Console.ReadLine().ToString();
                Console.SetCursorPosition(Password_Cursor_Y, Password_Cursor_X);
                Password = Console.ReadLine().ToString();




                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file and split then  using "|"in to list such as password and users.
                    String[] comp = line.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    //adding to list base on the split
                    users.Add(comp[0]);
                    pass.Add(comp[1]);
                   


                   
                }


                 // get the index of the users and password index base on the input fill
                int IndexUsername = users.IndexOf(Username);
                int Indexpass = pass.IndexOf(Password);

                // checking if the user name and password and we are also double conforming if the username and password have the same index value or not 
                if (users.Contains(Username) && pass.Contains(Password) && IndexUsername == Indexpass)
                {
                    // if all when well move to next page and break the loop 
                    MainMenu MM = new MainMenu();
                    MM.AllMenu();
                    break;
 

                }
                else
                {
                    // keep the loop going just display some informationn to the user about login erro
                    Console.SetCursorPosition(erro_Cursor_Y, erro_Cursor_X);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("\t\t invalid password or username ");
                    Console.ReadKey();
                }

            } while (true);
            Console.ReadKey();
        }

       

    }


}

