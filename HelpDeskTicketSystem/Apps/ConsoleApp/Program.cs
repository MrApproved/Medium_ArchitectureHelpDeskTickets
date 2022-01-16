using HelpDeskTicketSystem.Models;
using HelpDeskTicketSystem.Repositories;
using System;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Feel free to use either the InMemory or the Json repository
            var helpDeskTicketsRepository = new InMemoryHelpDeskTicketsRepository();
            //var helpDeskTicketsRepository = new JsonHelpDeskTicketsRepository();

            Console.WriteLine("--- Help Desk Ticket Syste ---");
            while (true)
            {
                Console.WriteLine("Commands available");
                Console.WriteLine("C: Create help desk tickets");
                Console.WriteLine("R: Read help desk tickets");
                Console.WriteLine("U: Update help desk ticket");
                Console.WriteLine("D: Delete help desk ticket");
                Console.WriteLine("0: Exist application");
                switch (Console.ReadLine())
                {
                    case "C":
                        {
                            var ticket = new HelpDeskTicket()
                            {
                                Created = DateTime.Now,
                                Id = Guid.NewGuid(),
                                Title = ConsoleWriteLineAndReadLine("Title:"),
                                SubmittedBy = ConsoleWriteLineAndReadLine("Submitted By:"),
                                Message = ConsoleWriteLineAndReadLine("Message:")
                            };
                            helpDeskTicketsRepository.Create(ticket);
                            helpDeskTicketsRepository.Save();
                        }
                        break;
                    case "R":
                        {
                            var id = ConsoleWriteLineAndReadLine("Provide an Id for a specific ticket or leave blank for all records");
                            if (string.IsNullOrWhiteSpace(id))
                                Array.ForEach(helpDeskTicketsRepository.GetRecords().ToArray(), x => Console.WriteLine(PrettyPrintHelpDeskTicket(x)));
                            else
                                Console.WriteLine(PrettyPrintHelpDeskTicket(helpDeskTicketsRepository.GetRecord(new Guid(id))));
                        }
                        break;
                    case "U":
                        {
                            var helpDeskTicket = helpDeskTicketsRepository.GetRecord(new Guid(ConsoleWriteLineAndReadLine("Provide an Id for the ticket to update")));
                            if (helpDeskTicket != null)
                            {
                                helpDeskTicket.Answer = ConsoleWriteLineAndReadLine("Answer:");
                                helpDeskTicket.AnweredBy = ConsoleWriteLineAndReadLine("Anwered By:");
                                helpDeskTicket.Closed = DateTime.Now;
                            }
                            helpDeskTicketsRepository.Update(helpDeskTicket);
                            helpDeskTicketsRepository.Save();
                        }
                        break;
                    case "D":
                        {
                            helpDeskTicketsRepository.Delete(new Guid(ConsoleWriteLineAndReadLine("Provide an Id for the ticket to delete")));
                            helpDeskTicketsRepository.Save();
                        }
                        break;
                    case "0": return;
                    default: Console.WriteLine("Provide a valid command"); break;
                }
            }
        }

        static string ConsoleWriteLineAndReadLine(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }

        static string PrettyPrintHelpDeskTicket(HelpDeskTicket ticket)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Id: {ticket.Id}");
            sb.AppendLine($"Title: {ticket.Title}");
            sb.AppendLine($"SubmittedBy: {ticket.SubmittedBy}");
            sb.AppendLine($"Message: {ticket.Message}");
            sb.AppendLine($"Answer: {ticket.Answer}");
            sb.AppendLine($"AnweredBy: {ticket.AnweredBy}");
            sb.AppendLine($"Created: {ticket.Created}");
            sb.AppendLine($"Closed: {ticket.Closed}");
            return sb.ToString();
        }
    }
}
