using System.Text.RegularExpressions;

namespace EmailConsoleApp
{
    internal class User
    {
        //public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime registrationTime { get; set; }
        public bool isBanned = false;
        public static List<User> blockedUsers { get; set; } = new List<User>();
        public List<Email> sentMessages { get; set; } = new List<Email>();
        public List<Email> inbox { get; set; } = new List<Email>();             // mishe vaqti register kard ye mail khoshaamad gooie goft.
        public List<Email> drafts { get; set; } = new List<Email>();
        public List<Email> trash { get; set; } = new List<Email>();

        public User(string firstname, string lastname, string username, string password, DateTime regtime)
        {
            firstName = firstname;
            lastName = lastname;
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
