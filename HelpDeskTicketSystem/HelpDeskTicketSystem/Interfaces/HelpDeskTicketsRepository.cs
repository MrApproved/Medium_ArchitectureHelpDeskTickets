using HelpDeskTicketSystem.Models;
using System;
using System.Collections.Generic;

namespace HelpDeskTicketSystem.Interfaces
{
    public interface HelpDeskTicketsRepository
    {
        void Create(HelpDeskTicket record);
        IEnumerable<HelpDeskTicket> GetRecords();
        HelpDeskTicket GetRecord(Guid id);
        void Update(HelpDeskTicket record);
        void Delete(Guid id);
        void Save();
    }
}
