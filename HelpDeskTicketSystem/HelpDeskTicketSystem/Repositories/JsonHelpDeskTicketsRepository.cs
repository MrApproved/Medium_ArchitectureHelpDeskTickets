using HelpDeskTicketSystem.Interfaces;
using HelpDeskTicketSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelpDeskTicketSystem.Repositories
{
    public class JsonHelpDeskTicketsRepository : HelpDeskTicketsRepository
    {
        private IDictionary<Guid, HelpDeskTicket> Records { get; set; }
        private const string JsonFileName = "helpdesktickets.json";

        public JsonHelpDeskTicketsRepository()
        {
            LoadTicketsFromFile();

            void LoadTicketsFromFile()
            {
                try
                {
                    Records = JsonConvert.DeserializeObject<IDictionary<Guid, HelpDeskTicket>>(System.IO.File.ReadAllText(JsonFileName));
                }
                catch
                {
                    Records = new Dictionary<Guid, HelpDeskTicket>();
                }
            }
        }

        public void Create(HelpDeskTicket record)
        {
            if (!Records.ContainsKey(record.Id))
                Records.Add(record.Id, record);
        }

        public void Delete(Guid id)
        {
            if (Records.ContainsKey(id))
                Records.Remove(id);
        }

        public HelpDeskTicket GetRecord(Guid id)
            => Records[id] ?? null;

        public IEnumerable<HelpDeskTicket> GetRecords()
            => Records.Select(x => x.Value);

        public void Save()
        {
            System.IO.File.WriteAllText(JsonFileName, JsonConvert.SerializeObject(Records));
        }

        public void Update(HelpDeskTicket record)
        {
            Records[record.Id] = record;
        }
    }
}
