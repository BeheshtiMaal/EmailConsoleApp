namespace EmailConsoleApp
{
    internal class Draft
    {
        public static void viewDrafts(User tmpUser)
        {
            if (tmpUser.drafts.Count() == 0) Console.WriteLine("There is no email");
            else
            {
                string str = "";
                for (int i = 0; i < tmpUser.drafts.Count(); i++)
                {
                    str += (i + 1) + ")\nFROM: " + tmpUser.drafts[i].Sender + "\nTO: " + tmpUser.drafts[i].Receiver + "\nSUBJECT: " + tmpUser.drafts[i].Subject + "\nBODY: " + tmpUser.drafts[i].Body + "\n\n";
                }
                Console.WriteLine(str);

                Console.WriteLine("1- To view an email");
                Console.WriteLine("2- exit");
                int emailId = int.Parse(Console.ReadLine());
                if (emailId == 1)
                {
                    Console.Write("Enter email number: ");
                    emailId = int.Parse(Console.ReadLine());
                    viewDraft(tmpUser, emailId);
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

        public static void viewDraft(User tmpUser, int emailId)
        {
            string str = "\n";
            str += "FROM: " + tmpUser.drafts[emailId - 1].Sender + "\nTO: " + tmpUser.drafts[emailId - 1].Receiver + "\nSubject: " + tmpUser.drafts[emailId - 1].Subject + "\nBody: " + tmpUser.drafts[emailId - 1].Body;
            Console.WriteLine(str);
            Console.WriteLine("\n1- Send");
            Console.WriteLine("2- Delete");
            Console.WriteLine("3- Exit");

            string dd = Console.ReadLine();
            if (dd == "1")
            {
                sendDraft(tmpUser,emailId);
            }
            else if (dd == "2")
            {
                deleteDrafts(tmpUser,emailId);
            }
            else if (dd == "3")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid input!!! Please Try again");
            }
        }



        public static void deleteDrafts(User tmpUser, int emailId)
        {

            tmpUser.trash.Add(tmpUser.drafts[emailId - 1]);
            tmpUser.drafts.RemoveAt(emailId - 1);
            Console.WriteLine("\nEmail deleted and went to trash!\nIt will be completely deleted within 2 hours\n");
        }

        public static void sendDraft(User tmpUser, int emailId)
        {
            tmpUser.drafts[emailId - 1].SentTime = DateTime.Now;    // updating sent time before sending.
            Admin.allEmails.Add(tmpUser.drafts[emailId - 1]);
            Admin.allUsers.Find(l => l.Username == tmpUser.drafts[emailId - 1].Sender).sentMessages.Add(tmpUser.drafts[emailId - 1]);
            Admin.allUsers.Find(l => l.Username == tmpUser.drafts[emailId - 1].Receiver).inbox.Add(tmpUser.drafts[emailId - 1]);
            tmpUser.trash.Add(tmpUser.drafts[emailId - 1]);
            Console.WriteLine("Email sent successfully.\n");

        }
    }
}
