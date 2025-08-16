using System;

namespace EmailConsoleApp
{
    internal class Email
    {
        public string Id { get; set; }          
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public bool isDraft { get; set; } = false;
        public DateTime SentTime { get; set; }     
        public string Subject { get; set; }
        public string Body { get; set; }
        
        public Email(string id ,string sender, DateTime sentTime)
        {
            Id = id;
            Sender = sender;
            //Receiver = receiver;
            SentTime = sentTime;
            ///Subject = subject;
            //Body = body;
        }

        public static bool isAbleToSend(Email email) 
        {
            return (!string.IsNullOrEmpty(email.Receiver)) && (!string.IsNullOrEmpty(email.Body) || !string.IsNullOrEmpty(email.Subject));
        }

            
        

    }
}
