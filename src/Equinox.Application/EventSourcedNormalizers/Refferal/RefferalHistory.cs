using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Core.Events;
using Newtonsoft.Json;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class RefferalHistory
    {
        public static IList<RefferalHistoryData> HistoryData { get; set; }

        public static IList<RefferalHistoryData> ToJavaScriptRefferalHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<RefferalHistoryData>();
            RefferalHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<RefferalHistoryData>();
            var last = new RefferalHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new RefferalHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    UserId = change.UserId == Guid.Empty || change.UserId == last.UserId
                        ? Guid.Empty
                        : change.UserId,
                    UserRefId = change.UserRefId == Guid.Empty || change.UserRefId == last.UserRefId
                        ? Guid.Empty
                        : change.UserRefId,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void RefferalHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new RefferalHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "RefferalCreatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.UserId = values["UserId"];
                        slot.UserRefId = values["UserRefId"];
                        slot.Action = "Created";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;

                    case "RefferalRemovedEvent":
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