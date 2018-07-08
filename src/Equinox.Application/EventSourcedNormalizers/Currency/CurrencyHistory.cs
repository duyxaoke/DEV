using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Core.Events;
using Newtonsoft.Json;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class CurrencyHistory
    {
        public static IList<CurrencyHistoryData> HistoryData { get; set; }

        public static IList<CurrencyHistoryData> ToJavaScriptCurrencyHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<CurrencyHistoryData>();
            CurrencyHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<CurrencyHistoryData>();
            var last = new CurrencyHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new CurrencyHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Code = string.IsNullOrWhiteSpace(change.Code) || change.Code == last.Code
                        ? ""
                        : change.Code,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Address = string.IsNullOrWhiteSpace(change.Address) || change.Address == last.Address
                        ? ""
                        : change.Address,
                    Quantity = change.Quantity,
                    IsActive = change.IsActive,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void CurrencyHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new CurrencyHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CurrencyCreatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Code = values["Code"];
                        slot.Name = values["Name"];
                        slot.Address = values["Address"];
                        slot.Quantity = values["Quantity"];
                        slot.IsActive = values["IsActive"];
                        slot.Action = "Created";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;

                    case "CurrencyUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Code = values["Code"];
                        slot.Name = values["Name"];
                        slot.Address = values["Address"];
                        slot.Quantity = values["Quantity"];
                        slot.IsActive = values["IsActive"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;

                    case "CurrencyRemovedEvent":
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