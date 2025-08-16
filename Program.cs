namespace EmailConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Welcome to Email Console Application *****\n");
            while (true)
            {
                Console.WriteLine("If you already have an account sign in otherwise create an account\n");
                Console.WriteLine("1- Sign in \n2- Create Account");
                string E1 = Console.ReadLine();         
                if (E1 == "1")
                {

                    string firstname, lastname;

                    Console.Write("Enter email address: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter password: ");                                          // ADD hiden password/cursor blinking next time.
                    string password = PasswordHasher.Hash(Console.ReadLine());

                    if (Admin.Auth(username, password))
                    {

                        Console.WriteLine("\n\nHI ADMIN !");
                        while (true)
                        {
                            Console.WriteLine("Choose what you want to do\n");
                            Console.WriteLine("1- add a user");
                            Console.WriteLine("2- remove a user");
                            Console.WriteLine("3- Ban a user");
                            Console.WriteLine("4- Unban a user");
                            Console.WriteLine("5- View all users");
                            Console.WriteLine("6- View all sent emails");
                            Console.WriteLine("7- Logout\n");

                            string wdawd = Console.ReadLine();

                            if (wdawd == "1")
                            {
                                // Add a user.

                                Console.Write("Enter firstname: ");
                                firstname = Console.ReadLine();
                                Console.Write("Enter lastname: ");
                                lastname = Console.ReadLine();
                                while (true)
                                {
                                    Console.WriteLine("Username must have a form like this example@example1.\nexample3 you can use (-) (_) (.) signs.\n");
                                    Console.Write("Enter username: ");
                                    username = Console.ReadLine().ToLower();        // for being case insensitive.

                                    if (User.regex(username) && !Admin.allUsers.Exists(u => u.Username == username))
                                        break;
                                    else
                                        Console.WriteLine("Wrong username or allready taken username!!!\nPlease try again");// try-catch
                                }
                                Console.Write("Enter password: ");
                                password = PasswordHasher.Hash(Console.ReadLine());
                                DateTime regtime = DateTime.Now;

                                User tmp = new User(firstname, lastname, username, password, regtime);
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

                            else if (wdawd == "4")
                            {
                                // Unban a user.
                                Console.Write("Enter username: ");
                                username = Console.ReadLine();
                                if (Admin.allUsers.Exists(u => u.Username == username))
                                {
                                    Admin.allUsers[Admin.allUsers.FindIndex(u => u.Username == username)].isBanned = false;
                                    Console.WriteLine("User baned successfully!");
                                }
                                else
                                    Console.WriteLine("No user found with this username!!!");
                            }

                            else if (wdawd == "5")
                            {
                                // View all users.
                                if (Admin.allUsers.Count() == 0)
                                    Console.WriteLine("There are no users!");
                                else
                                {
                                    string str = "";
                                    for (int i = 0; i < Admin.allUsers.Count(); i++)
                                    {
                                        str += (i + 1) + ": " + Admin.allUsers[i].Username + " - " + Admin.allUsers[i].isBanned + " - " + Admin.allUsers[i].sentMessages.Count() + " - " + Admin.allUsers[i].inbox.Count() + " - " + Admin.allUsers[i].registrationTime + "\n";
                                    }
                                    Console.WriteLine(str);
                                }
                            }

                            else if (wdawd == "6")
                            {
                                // View all emails.
                                if (Admin.allEmails.Count() == 0) Console.WriteLine("There is no email");
                                else
                                {
                                    string str = "";
                                    for (int i = 0; i < Admin.allEmails.Count(); i++)
                                    {
                                        str += (i + 1) + ")\nFROM: " + Admin.allEmails[i].Sender + "\nTO: " + Admin.allEmails[i].Receiver + "\nSUBJECT: " + Admin.allEmails[i].Subject + "\nBODY: " + Admin.allEmails[i].Body + "\n\n";
                                    }
                                    Console.WriteLine(str);
                                }
                            }


                            else if (wdawd == "7")
                            {
                                Console.WriteLine("BYE ADMIN!!!");
                                break;
                            }

                            else
                                Console.WriteLine("\nWrong input!!! Please try again\n");
                        }
                    }

                    else if (User.Auth(username, password))
                    {
                        Console.WriteLine($"WELCOME {Admin.allUsers.Find(l => l.Username == username).firstName} !");
                        // User Panel
                        while (true)
                        {
                            Console.WriteLine("\nChoose what you want to do\n");
                            Console.WriteLine("1- Compose New Email");  // age typed shod dar mail, har vaght biyad biroon mire to draft - delete vali to draft nemire.
                            Console.WriteLine("2- Inbox");              // delete kone mire to trash - view email -> reply
                            Console.WriteLine("3- Sent");               // delete kone mire to trash - view email -> reply
                            Console.WriteLine("4- Drafts");             // delete kone mire to trash - view email - send
                            Console.WriteLine("5- Trash");              // view email - restore to previous folder
                            //Console.WriteLine("6- Block a user");
                            //Console.WriteLine("7- View Blocked users"); // unblock user
                            Console.WriteLine("8- Logout");


                            // while (true)
                            //{
                            string wduwd = Console.ReadLine();
                            if (wduwd == "1")
                            {

                                Guid myGuid = Guid.NewGuid();               // generates new global id.
                                string guidString = myGuid.ToString();
                                DateTime sentTime = DateTime.Now;          // saves initiation time.

                                Email tmp = new Email(guidString, username, sentTime);
                                tmp.Id = guidString;
                                tmp.Sender = username;
                                tmp.SentTime = sentTime;

                                Console.WriteLine("Now you can compose your email, please select what you want to fill");

                                while (true)
                                {
                                    Console.WriteLine("1- Recipient");
                                    Console.WriteLine("2- Subject");
                                    Console.WriteLine("3- Body");
                                    Console.WriteLine("4- Send");
                                    Console.WriteLine("5- Exit");

                                    string composing = Console.ReadLine();
                                    if (composing == "1")
                                    {
                                        Console.Write("Enter Receiver: ");
                                        string receiver = Console.ReadLine();
                                        if (Admin.allUsers.Exists(l => l.Username == receiver))
                                        {
                                            tmp.Receiver = receiver;

                                        }
                                        else
                                        {
                                            Console.WriteLine("Wrong email recipient!!! Please try again\n");
                                        }

                                    }
                                    else if (composing == "2")
                                    {
                                        Console.Write("Enter Subject: ");
                                        string subject = Console.ReadLine();
                                        tmp.Subject = subject;
                                    }
                                    else if (composing == "3")
                                    {
                                        Console.Write("Enter Body: ");
                                        string body = Console.ReadLine();
                                        tmp.Body = body;
                                    }
                                    else if (composing == "4")
                                    {
                                        // if por bood
                                        if (Email.isAbleToSend(tmp))
                                        {
                                            Admin.allEmails.Add(tmp);
                                            Admin.allUsers.Find(l => l.Username == tmp.Sender).sentMessages.Add(tmp);
                                            Admin.allUsers.Find(l => l.Username == tmp.Receiver).inbox.Add(tmp);
                                            Console.WriteLine("Email sent successfully.\n");
                                            break;

                                        }
                                        
                                        else
                                        {
                                            Console.WriteLine("Email has not the requirements to be sent. Please fill subject or body\n");   
                                        }

                                    }
                                    else if (composing == "5")
                                    {
                                        if (Email.isAbleToSend(tmp))
                                        {
                                            tmp.isDraft = true;
                                            Admin.allUsers.Find(l => l.Username == tmp.Sender).drafts.Add(tmp);
                                            Console.WriteLine("\nEmail saved as a Draft!\n");
                                        }
                                        else
                                        {
                                            tmp.Id = null;
                                            tmp.Sender = null;
                                            tmp.Receiver = null;
                                            tmp.Subject = null;
                                            tmp.Body = null;
                                            Console.WriteLine("\nEmail Eliminated\n");
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nWrong input!!! Please try again\n");
                                    }
                                }


                            }
                            else if (wduwd == "2")
                            {
                                // inbox
                                User tmpUser = Admin.allUsers.Find(l => l.Username == username);
                                if (tmpUser.inbox.Count() == 0)
                                {
                                    Console.WriteLine("\nThere is no emails in inbox!\n");
                                }
                                else
                                {
                                    string str = "";
                                    for (int i = 0; i < tmpUser.inbox.Count(); i++)
                                    {
                                        str += (i + 1) + ": " + tmpUser.inbox[i].Id + " - " + tmpUser.inbox[i].Sender + " - " + tmpUser.inbox[i].Subject + " - " + tmpUser.inbox[i].SentTime + "\n";
                                    }
                                    Console.WriteLine(str);

                                    Console.WriteLine("\n1- To view an email");
                                    Console.WriteLine("2- Exit");
                                    string inboxDecision = Console.ReadLine();
                                    if (inboxDecision == "1")
                                    {
                                        while (true)
                                        {
                                            Console.Write("Enter email number: ");
                                            int emailId = int.Parse(Console.ReadLine());
                                            str = "";
                                            str += "FROM: " + tmpUser.inbox[emailId - 1].Sender + "\nTO: " + tmpUser.inbox[emailId - 1].Receiver + "\nSubject: " + tmpUser.inbox[emailId - 1].Subject + "\nBody: " + tmpUser.inbox[emailId - 1].Body;
                                            Console.WriteLine(str);

                                            Console.WriteLine("1- Reply");
                                            Console.WriteLine("2- Delete");
                                            Console.WriteLine("3- Exit");
                                            string emailDecision = Console.ReadLine();

                                            if (emailDecision == "1")
                                            {

                                                Guid myGuid = Guid.NewGuid();
                                                string guidString = myGuid.ToString();
                                                DateTime sentTime = DateTime.Now;
                                                Email tmp = new Email(guidString, tmpUser.inbox[emailId - 1].Receiver, sentTime);
                                                tmp.Id = guidString;
                                                tmp.Sender = username;
                                                tmp.SentTime = sentTime;
                                                tmp.Receiver = tmpUser.inbox[emailId - 1].Sender;

                                                while (true)
                                                {

                                                    Console.WriteLine("1- Subject");
                                                    Console.WriteLine("2- Body");
                                                    Console.WriteLine("3- Send");
                                                    Console.WriteLine("4- Exit");
                                                    string composing = Console.ReadLine();

                                                    if (composing == "1")
                                                    {
                                                        Console.Write("Enter Subject: ");
                                                        string subject = Console.ReadLine();
                                                        tmp.Subject = subject;
                                                    }
                                                    else if (composing == "2")
                                                    {
                                                        Console.Write("Enter Body: ");
                                                        string body = Console.ReadLine();
                                                        tmp.Body = body;
                                                    }
                                                    else if (composing == "3")
                                                    {
                                                        // if por bood
                                                        if (Email.isAbleToSend(tmp))
                                                        {
                                                            Admin.allEmails.Add(tmp);
                                                            Admin.allUsers.Find(l => l.Username == tmp.Sender).sentMessages.Add(tmp);
                                                            Admin.allUsers.Find(l => l.Username == tmp.Receiver).inbox.Add(tmp);
                                                            Console.WriteLine("Reply sent successfully.\n");
                                                            break;


                                                        }
                                                        // if nabood bege por nist va break
                                                        else
                                                        {
                                                            Console.WriteLine("Reply has not the requirements to be sent. Please fill subject or body\n");   // try-catch
                                                        }

                                                    }
                                                    else if (composing == "4")
                                                    {
                                                        if (Email.isAbleToSend(tmp))
                                                        {
                                                            tmp.isDraft = true;
                                                            Admin.allUsers.Find(l => l.Username == tmp.Sender).drafts.Add(tmp);
                                                            Console.WriteLine("\nEmail saved as a Draft!\n");
                                                        }
                                                        else
                                                        {
                                                            tmp.Id = null;
                                                            tmp.Sender = null;
                                                            tmp.Receiver = null;
                                                            tmp.Subject = null;
                                                            tmp.Body = null;
                                                            Console.WriteLine("\nEmail Eliminated\n");
                                                        }

                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nWrong input!!! Please try again\n");
                                                    }
                                                }

                                                break;

                                            }
                                            else if (emailDecision == "2")
                                            {
                                                // delete and refer to trash 

                                                tmpUser.trash.Add(tmpUser.inbox[emailId - 1]);
                                                tmpUser.inbox.RemoveAt(emailId - 1);
                                                Console.WriteLine("\nEmail deleted and went to trash!\nIt will be completely deleted within 2 hours\n");
                                                break;
                                            }
                                            else if (emailDecision == "3")
                                            {
                                                // exit
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Wrong input!!! Please try again");
                                            }
                                        }
                                    }
                                    if (inboxDecision == "2")
                                    {
                                        // exit
                                        break;
                                    }
                                }

                            }
                            else if (wduwd == "3")
                            {
                                // sent
                                User tmpUser = Admin.allUsers.Find(l => l.Username == username);
                                if (tmpUser.sentMessages.Count() == 0)
                                {
                                    Console.WriteLine("\nThere is no emails in sent Box!\n");
                                }
                                else
                                {
                                    string str = "";
                                    for (int i = 0; i < tmpUser.sentMessages.Count(); i++)
                                    {
                                        str += (i + 1) + ": " + tmpUser.sentMessages[i].Id + " - " + tmpUser.sentMessages[i].Receiver + " - " + tmpUser.sentMessages[i].Subject + " - " + tmpUser.sentMessages[i].SentTime + "\n";
                                    }
                                    Console.WriteLine(str);

                                    Console.WriteLine("\n1- To view an email");
                                    Console.WriteLine("2- Exit");
                                    
                                    string inboxDecision = Console.ReadLine();
                                    if (inboxDecision == "1")
                                    {
                                        while (true)
                                        {
                                            Console.Write("Enter email number: ");
                                            int emailId = int.Parse(Console.ReadLine());
                                            str = "";
                                            str += "Sender Email: " + tmpUser.sentMessages[emailId - 1].Sender + "\nReceiver Email: " + tmpUser.sentMessages[emailId - 1].Receiver + "\nSubject: " + tmpUser.sentMessages[emailId - 1].Subject + "\nBody: " + tmpUser.sentMessages[emailId - 1].Body;
                                            Console.WriteLine(str);

                                            Console.WriteLine("1- Reply");
                                            Console.WriteLine("2- Delete");
                                            Console.WriteLine("3- Exit");
                                            int emailDecision = int.Parse(Console.ReadLine());

                                            if (emailDecision == 1)
                                            {

                                                Guid myGuid = Guid.NewGuid();
                                                string guidString = myGuid.ToString();
                                                DateTime sentTime = DateTime.Now;
                                                Email tmp = new Email(guidString, tmpUser.sentMessages[emailId - 1].Sender, sentTime);
                                                tmp.Id = guidString;
                                                tmp.Sender = username;
                                                tmp.SentTime = sentTime;

                                                while (true)
                                                {

                                                    Console.WriteLine("1- Subject");
                                                    Console.WriteLine("2- Body");
                                                    Console.WriteLine("3- Send");
                                                    Console.WriteLine("4- Exit");
                                                    string composing = Console.ReadLine();

                                                    if (composing == "1")
                                                    {
                                                        Console.Write("Enter Subject: ");
                                                        string subject = Console.ReadLine();
                                                        tmp.Subject = subject;
                                                    }
                                                    else if (composing == "2")
                                                    {
                                                        Console.Write("Enter Body: ");
                                                        string body = Console.ReadLine();
                                                        tmp.Body = body;
                                                    }
                                                    else if (composing == "3")
                                                    {
                                                        // if por bood
                                                        if (Email.isAbleToSend(tmp))
                                                        {
                                                            Admin.allEmails.Add(tmp);
                                                            Admin.allUsers.Find(l => l.Username == tmp.Sender).sentMessages.Add(tmp);
                                                            Admin.allUsers.Find(l => l.Username == tmp.Receiver).inbox.Add(tmp);
                                                            Console.WriteLine("Reply sent successfully.\n");



                                                        }
                                                        // if nabood bege por nist va break
                                                        else
                                                        {
                                                            Console.WriteLine("Reply has not the requirements to be sent. Please fill subject or body\n");   // try-catch
                                                        }
                                                        break;
                                                    }
                                                    else if (composing == "4")
                                                    {

                                                        if (Email.isAbleToSend(tmp))
                                                        {
                                                            tmp.isDraft = true;
                                                            Admin.allUsers.Find(l => l.Username == tmp.Sender).drafts.Add(tmp);
                                                            Console.WriteLine("\nEmail saved as a Draft!\n");
                                                        }
                                                        else
                                                        {
                                                            tmp.Id = null;
                                                            tmp.Sender = null;
                                                            tmp.Receiver = null;
                                                            tmp.Subject = null;
                                                            tmp.Body = null;
                                                            Console.WriteLine("\nEmail Eliminated\n");
                                                        }
                                                        break;

                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nWrong input!!! Please try again\n");
                                                    }
                                                }

                                            }
                                            else if (emailDecision == 2)
                                            {
                                                // delete and refer to trash 
                                                while (true)
                                                {
                                                    
                                                    Console.Write("Enter email number: ");
                                                    emailId = int.Parse(Console.ReadLine());
                                                    if (emailId < 1 || emailId > tmpUser.sentMessages.Count)
                                                    {
                                                        Console.WriteLine("\nWrong input!!! Please try again\n");
                                                    }
                                                    else if (emailId >= 1 && emailId < tmpUser.sentMessages.Count)
                                                    {
                                                        tmpUser.trash.Add(tmpUser.sentMessages[emailId - 1]);
                                                        tmpUser.sentMessages.RemoveAt(emailId - 1);
                                                        Console.WriteLine("\nEmail deleted and went to trash!\nIt will be completely deleted within 2 hours\n");
                                                        break;
                                                    }
                                                    
                                                    else
                                                    {
                                                        Console.WriteLine("Wrong input!!! Please try again");
                                                    }
                                                }

                                            }
                                            else if (emailDecision == 3)
                                            {
                                                // exit
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Wrong input!!! Please try again");
                                            }
                                        }
                                    }
                                    if (inboxDecision == "2")
                                    {
                                        // exit
                                        break;
                                    }
                                }
                            }
                            else if (wduwd == "4")
                            {
                                // draft
                                User tmpUser = Admin.allUsers.Find(l => l.Username == username);
                                Draft.viewDrafts(tmpUser);
                                
                            }
                            else if (wduwd == "5")
                            {
                                // trash
                                User tmpUser = Admin.allUsers.Find(l => l.Username == username);
                                Trash.viewTrashs(tmpUser);
                              
                            }
                            else if (wduwd == "6")
                            {
                                // block

                            }
                            else if (wduwd == "7")
                            {
                                // view blocked users
                            }
                            else if (wduwd == "8")
                            {
                                // logout
                                Console.WriteLine($"GOODBYE {Admin.allUsers.Find(l => l.Username == username).firstName} ");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input!!! Please try again");
                            }
                            //  break;
                            //}



                        }

                    }
                    else
                        Console.WriteLine("Invalid username or password!!! Please try again");      // inja bayad try catch bezaam.

                }
                else if (E1 == "2")
                {
                    // Create account 
                    string firstname, lastname, username, password;


                    Console.Write("Enter firstname: ");
                    firstname = Console.ReadLine();
                    Console.Write("Enter lastname: ");
                    lastname = Console.ReadLine();
                    while (true)
                    {
                        Console.WriteLine("Username must have a form like this example@example1.example3\n you can use (-) (_) (.) signs.\n");
                        Console.Write("Enter username: ");
                        username = Console.ReadLine().ToLower();
                        if (User.regex(username) && !Admin.allUsers.Exists(u => u.Username == username))
                            break;
                        else
                            Console.WriteLine("\nWrong username or allready taken username!!!\nPlease try again\n");
                    }
                    Console.Write("Enter password: ");
                    password = PasswordHasher.Hash(Console.ReadLine());
                    DateTime regtime = DateTime.Now;

                    User tmp = new User(firstname, lastname, username, password, regtime);
                    tmp.firstName = firstname;
                    tmp.lastName = lastname;
                    tmp.Username = username;
                    tmp.Password = password;
                    tmp.registrationTime = regtime;
                    Admin.allUsers.Add(tmp);

                    Console.WriteLine("\nAccount created successfuly! Now you can sign in.");
                }
                else
                {
                    Console.WriteLine("\nInvalid input!!! Please try again\n");

                }
            }
        }
    }
}
