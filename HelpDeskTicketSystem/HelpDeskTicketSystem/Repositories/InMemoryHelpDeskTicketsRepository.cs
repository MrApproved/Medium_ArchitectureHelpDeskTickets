using HelpDeskTicketSystem.Interfaces;
using HelpDeskTicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpDeskTicketSystem.Repositories
{
public class InMemoryHelpDeskTicketsRepository : HelpDeskTicketsRepository
{
    private Dictionary<Guid, HelpDeskTicket> Records { get; set; }
    private Dictionary<Guid, HelpDeskTicket> UpdatedRecords { get; set; }

    public InMemoryHelpDeskTicketsRepository()
    {
        Records = new Dictionary<Guid, HelpDeskTicket>();
        UpdatedRecords = new Dictionary<Guid, HelpDeskTicket>();
    }

    public void Create(HelpDeskTicket record)
    {
        if (!UpdatedRecords.ContainsKey(record.Id))
            UpdatedRecords.Add(record.Id, record);
    }

    public void Delete(Guid id)
    {
        if (UpdatedRecords.ContainsKey(id))
            UpdatedRecords.Remove(id);
    }

    public HelpDeskTicket GetRecord(Guid id)
        => Records[id] ?? null;

    public IEnumerable<HelpDeskTicket> GetRecords()
        => Records.Select(x => x.Value);

    public void Save()
    {
        Records = UpdatedRecords;
    }

    public void Update(HelpDeskTicket record)
    {
        if (UpdatedRecords.ContainsKey(record.Id))
            UpdatedRecords[record.Id] = record;
    }
}
}
