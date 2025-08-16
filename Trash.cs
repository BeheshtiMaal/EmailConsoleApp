namespace EmailConsoleApp
{
    internal class Trash
    {

        public static void viewTrashs(User tmpUser)
        {
            if (tmpUser.trash.Count() == 0) Console.WriteLine("There is no email");
            else
            {
                string str = "";
                for (int i = 0; i < tmpUser.trash.Count(); i++)
                {
                    str += (i + 1) + ")\nFROM: " + tmpUser.trash[i].Sender + "\nTO: " + tmpUser.trash[i].Receiver + "\nSUBJECT: " + tmpUser.trash[i].Subject + "\nBODY: " + tmpUser.trash[i].Body + "\n\n";
                }
                Console.WriteLine(str);

                Console.WriteLine("1- To view an email");
                Console.WriteLine("2- exit");
                int emailId = int.Parse(Console.ReadLine());
                if (emailId == 1)
                {
                    Console.Write("Enter email number: ");
                    emailId = int.Parse(Console.ReadLine());
                    viewTrash(tmpUser, emailId);
                }
                else if (emailId == 2)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input!!! Please Try again");
                }
            }
        }

        public static void viewTrash(User tmpUser, int emailId)
        {
            string str = "\n";
            str += "FROM: " + tmpUser.trash[emailId - 1].Sender + "\nTO: " + tmpUser.trash[emailId - 1].Receiver + "\nSubject: " + tmpUser.trash[emailId - 1].Subject + "\nBody: " + tmpUser.trash[emailId - 1].Body;
            Console.WriteLine(str);
            Console.WriteLine("\n1- Restore");
            Console.WriteLine("2- Delete");
            Console.WriteLine("3- Exit");

            string tt = Console.ReadLine();
            if (tt == "1")
            {
                restoreTrash(tmpUser, emailId);
            }
            else if (tt == "2")
            {
                deleteTrash(tmpUser, emailId);
            }
            else if (tt == "3")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid input!!! Please Try again");
            }
        }



        public static void deleteTrash(User tmpUser, int emailId)
        {
            tmpUser.trash.RemoveAt(emailId - 1);
            //Console.WriteLine("\nEmail deleted completely!\n");
        }


        public static void restoreTrash(User tmpUser, int emailId)
        {
            //tmpUser.drafts[emailId - 1].SentTime = DateTime.Now;    // updating sent time before sending.
            //Admin.allEmails.Add(tmpUser.drafts[emailId - 1]);
            //Admin.allUsers.Find(l => l.Username == tmpUser.drafts[emailId - 1].Sender).sentMessages.Add(tmpUser.drafts[emailId - 1]);
            //Admin.allUsers.Find(l => l.Username == tmpUser.drafts[emailId - 1].Receiver).inbox.Add(tmpUser.drafts[emailId - 1]);
            //tmpUser.trash.Add(tmpUser.drafts[emailId - 1]);
            //Console.WriteLine("Email sent successfully.\n");
            string ss = tmpUser.trash[emailId - 1].Sender;
            string rr = tmpUser.trash[emailId - 1].Receiver;
            bool dd = tmpUser.trash[emailId - 1].isDraft;

            if (rr == tmpUser.Username && !dd)
            {

                Admin.allEmails.Add(tmpUser.trash[emailId - 1]);
                Admin.allUsers.Find(l => l.Username == tmpUser.trash[emailId - 1].Receiver).inbox.Add(tmpUser.trash[emailId - 1]);
                deleteTrash( tmpUser, emailId);
                Console.WriteLine("\nEmail restored to your inbox!\n");

            }
            else if (ss == tmpUser.Username && !dd)
            {
                Admin.allEmails.Add(tmpUser.trash[emailId - 1]);
                Admin.allUsers.Find(l => l.Username == tmpUser.trash[emailId - 1].Receiver).sentMessages.Add(tmpUser.trash[emailId - 1]);
                deleteTrash( tmpUser, emailId);
                Console.WriteLine("\nEmail restored to your sent!\n");

            }
            else if ((ss ==tmpUser.Username  || rr == tmpUser.Username ) && dd)
            {

                Admin.allUsers.Find(l => l.Username == tmpUser.trash[emailId - 1].Sender).drafts.Add(tmpUser.trash[emailId - 1]);
                deleteTrash( tmpUser, emailId);
                Console.WriteLine("\nEmail restored to your draft!\n");
            }
        }
    }
}
