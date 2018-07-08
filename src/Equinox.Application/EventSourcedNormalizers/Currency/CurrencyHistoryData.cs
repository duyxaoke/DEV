namespace Equinox.Application.EventSourcedNormalizers
{
    public class CurrencyHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal? Quantity { get; set; }
        public bool IsActive { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}