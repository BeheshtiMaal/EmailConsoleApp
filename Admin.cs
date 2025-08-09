namespace EmailConsoleApp
{
    internal class Admin
    {
        private static string Username = "admin";
        private static string Password = "2244";
        public static List<User> allUsers = new List<User>();
        //public static int usersCount = 0;
        public static bool Auth(string username, string password) => Username == username && Password == password;
        

    }
}
