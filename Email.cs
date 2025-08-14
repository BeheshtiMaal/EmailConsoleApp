using System;

namespace EmailConsoleApp
{
    internal class Email
    {
        public string Id { get; set; }          // Guid myGuid = Guid.NewGuid();string guidString = myGuid.ToString();
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public bool Reply = false;      // Direct(0) or Reply(1) message
        public DateTime SentTime { get; set; }      // DateTime currentLocalTime = DateTime.Now; .UtcNow 
        //DateTime openedTime;    // Nullable at sent time. after opening the email, it takes value.
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
