using System;

namespace EmailConsoleApp
{
    internal class Email
    {
        string id;          // Guid myGuid = Guid.NewGuid();string guidString = myGuid.ToString();
        string sender;
        string receiver;
       // bool wantToReply;      // Direct(0) or Reply(1) message
        DateTime sentTime;      // DateTime currentLocalTime = DateTime.Now; .UtcNow 
        //DateTime openedTime;    // Nullable at sent time. after opening the email, it takes value.
        string subject;
        string body;
        

    }
}
