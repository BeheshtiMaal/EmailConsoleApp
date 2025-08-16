namespace EmailConsoleApp
{
    internal class Admin
    {
        private static string Username = "admin";
        private static string Password = PasswordHasher.Hash("2244");
        public static List<User> allUsers = new List<User>();
        public static List<Email> allEmails = new List<Email>();
        public static int totalUsersCount = 0;
        public static bool Auth(string username, string password) => Username == username && Password == password;
        

    }
}
