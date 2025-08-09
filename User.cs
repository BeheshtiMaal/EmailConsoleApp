using System.Text.RegularExpressions;

namespace EmailConsoleApp
{
    internal class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime registrationTime { get; set; }

        public bool isBanned = false;
        public List<User> blockedUsers = new List<User>();
       // public int blockedUsersCount=0;
        public List<Email> sentMessages = new List<Email>();
        // public int sentMessagesCount=0;
        public List<Email> inbox = new List<Email>();             // mishe vaqti register kard ye mail khoshaamad gooie goft.
        //public int inboxCount=0;

        public User(string username, string password, DateTime regtime)
        {
            Username = username;
            Password = password;
            registrationTime = regtime;
        }

        public static bool Auth(string username, string password)
        {
            return Admin.allUsers.Exists(u => u.Username == username && u.Password == password);
        }
        public static bool regex(string username) => Regex.IsMatch(username, @"^[\w\.-]+@[\w\.-]+\.[\w\.-]+$"); 

        
    }
}
