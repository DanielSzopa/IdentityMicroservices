namespace YourTutor.Application.Models.EmailBase
{
    public abstract class EmailBase
    {
        public EmailBase(string subject, string body, string tag, string to)
        {
            Subject = subject;
            Body = body;
            Tag = tag;
            To = to;
        }

        public string Subject { get; }
        public string Body { get; }
        public string Tag { get; }
        public string To { get; }
    }
}


