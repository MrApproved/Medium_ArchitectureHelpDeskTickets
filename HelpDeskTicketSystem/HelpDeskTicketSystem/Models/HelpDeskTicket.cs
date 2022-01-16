using System;

namespace HelpDeskTicketSystem.Models
{
    public class HelpDeskTicket
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Answer { get; set; }
        public string SubmittedBy { get; set; }
        public string AnweredBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Closed { get; set; }
    }
}
