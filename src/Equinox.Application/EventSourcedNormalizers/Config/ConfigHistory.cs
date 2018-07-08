using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Core.Events;
using Newtonsoft.Json;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class ConfigHistory
    {
        public static IList<ConfigHistoryData> HistoryData { get; set; }

        public static IList<ConfigHistoryData> ToJavaScriptConfigHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<ConfigHistoryData>();
            ConfigHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<ConfigHistoryData>();
            var last = new ConfigHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new ConfigHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Currency = string.IsNullOrWhiteSpace(change.Currency) || change.Currency == last.Currency
                        ? ""
                        : change.Currency,
                    SystemEnable = change.SystemEnable,
                    ReferalBonus = change.ReferalBonus,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void ConfigHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new ConfigHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "ConfigRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.SystemEnable = values["SystemEnable"];
                        slot.Currency = values["Currency"];
                        slot.ReferalBonus = values["ReferalBonus"];
                        slot.Action = "Created";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;

                    case "ConfigUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.SystemEnable = values["SystemEnable"];
                        slot.Currency = values["Currency"];
                        slot.ReferalBonus = values["ReferalBonus"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;

                    case "ConfigRemovedEvent":
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