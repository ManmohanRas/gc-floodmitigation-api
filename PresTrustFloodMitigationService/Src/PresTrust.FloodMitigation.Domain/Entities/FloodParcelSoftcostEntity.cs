namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelSoftCostEntity
{
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string PamsPin { get; set; }
        public int SoftCostTypeId { get; set; }
        public string? VendorName { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal CostShare { get; set; }
        public decimal SoftCostTotal { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
}
