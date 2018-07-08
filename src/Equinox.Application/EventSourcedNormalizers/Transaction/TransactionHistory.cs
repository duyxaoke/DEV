using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Core.Events;
using Newtonsoft.Json;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class TransactionHistory
    {
        public static IList<TransactionHistoryData> HistoryData { get; set; }

        public static IList<TransactionHistoryData> ToJavaScriptTransactionHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<TransactionHistoryData>();
            TransactionHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<TransactionHistoryData>();
            var last = new TransactionHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new TransactionHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    UserId = change.UserId == Guid.Empty || change.UserId == last.UserId
                        ? Guid.Empty
                        : change.UserId,
                    DepWithType = change.DepWithType,
                    Quantity = change.Quantity,
                    IP = string.IsNullOrWhiteSpace(change.IP) ? "" : change.IP,
                    IsActive = change.IsActive,
                    Approve = change.Approve,
                    CreatedDate = change.CreatedDate,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void TransactionHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new TransactionHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "TransactionCreatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.UserId = values["UserId"];
                        slot.DepWithType = values["DepWithType"];
                        slot.Quantity = values["Quantity"];
                        slot.IP = values["IP"];
                        slot.Approve = values["Approve"];
                        slot.CreatedDate = values["CreatedDate"];
                        slot.Action = "Created";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;

                    case "TransactionUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.UserId = values["UserId"];
                        slot.DepWithType = values["DepWithType"];
                        slot.Quantity = values["Quantity"];
                        slot.IP = values["IP"];
                        slot.Approve = values["Approve"];
                        slot.CreatedDate = values["CreatedDate"];
                        slot.IsActive = values["IsActive"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;

                    case "TransactionRemovedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}