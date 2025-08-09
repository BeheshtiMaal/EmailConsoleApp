using Microsoft.VisualBasic;

namespace EmailConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Welcome to Email Console Application *****");
            while (true)
            {
                Console.WriteLine("If you already have an account sign in otherwise create an account\n");
                Console.WriteLine("1- Sign in \n2- Create Account");
                string E1 = Console.ReadLine(); //  Enter1.
                if (E1 == "1")
                {
                    Console.Write("Enter email address: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter password: ");  // ADD hiden password/cursor blinking next time.
                    string password = Console.ReadLine();

                    if (Admin.Auth(username, password))
                    {
                        while (true)
                        {
                            Console.WriteLine("HI ADMIN !\nChoose what you want to do\n");
                            Console.WriteLine("1- add a user");
                            Console.WriteLine("2- remove a user");
                            Console.WriteLine("3- Ban a user");
                            Console.WriteLine("4- Unban a user");
                            Console.WriteLine("5- View all users");
                            Console.WriteLine("6- View a user information");
                            Console.WriteLine("7- View All emails");
                            Console.WriteLine("8- Logout");
                            Console.WriteLine();
                            Console.WriteLine();

                            string wdawd = Console.ReadLine();

                            if (wdawd == "1")
                            {
                                // Add a user.
                                while (true)
                                {
                                    Console.WriteLine("Username must have a form like this example@example1.example3");
                                    Console.Write("Enter username: ");
                                    username = Console.ReadLine();
                                    if (User.regex(username) && !Admin.allUsers.Exists(u => u.Username == username))
                                        break;
                                    else
                                        Console.WriteLine("Wrong username or allready taken username!!!\nPlease try again");
                                }
                                Console.Write("Enter password: ");
                                password = Console.ReadLine();
                                DateTime regtime = DateTime.Now;

                                User tmp = new User(username, password, regtime);
                                tmp.Username = username;
                                tmp.Password = password;
                                tmp.registrationTime = regtime;
                                Admin.allUsers.Add(tmp);
                                Console.WriteLine("User added successfully!");
                            }

                            else if (wdawd == "2")
                            {
                                // Remove a user.
                                Console.Write("Enter username: ");
                                username = Console.ReadLine();
                                if (Admin.allUsers.Exists(u => u.Username == username))
                                {
                                    Admin.allUsers.RemoveAll(u => u.Username == username); // Remove ba RemoveAll dar in context yekie chon, har username yekta hast.
                                    Console.WriteLine("User removed successfully!");
                                }
                                else
                                    Console.WriteLine("No user found with this username!!!");
                            }

                            else if (wdawd == "3")
                            {
                                // Ban a user.
                                Console.Write("Enter username: ");
                                username = Console.ReadLine();
                                if (Admin.allUsers.Exists(u => u.Username == username))
                                {
                                    Admin.allUsers[Admin.allUsers.FindIndex(u => u.Username == username)].isBanned = true;
                                    Console.WriteLine("User baned successfully!");
                                }
                                else
                                    Console.WriteLine("No user found with this username!!!");
                            }
                        }

                        else if (User.Auth(username, password))
                        {

                        }
                        else
                            Console.WriteLine("Invalid username or password!!!\nPlease try again");

                    }
                    else if (E1 == "2")
                    {
                        // Create account 
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!!!\nPlease try again");

                    }


                }
            }
        }
    }
}
