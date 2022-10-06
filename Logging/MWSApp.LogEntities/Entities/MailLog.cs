

namespace MWSApp.LogEntities.Entities
{
    public class MailLog: MainBase
    {
        public string Email { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public string Error { get; set; } = "";
        public DateTime ActionDate { get; set; }
    }
}
