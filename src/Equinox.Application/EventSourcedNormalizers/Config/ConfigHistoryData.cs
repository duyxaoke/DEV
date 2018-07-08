namespace Equinox.Application.EventSourcedNormalizers
{
    public class ConfigHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public bool SystemEnable { get; set; }
        public string Currency { get; set; }
        public decimal? ReferalBonus { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}