namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelSoftcostEntity
{
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string? PamsPin { get; set; }
        public int SoftcostTypeId { get; set; }
        public string? VendorName { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal CostShare { get; set; }
        public decimal SoftcostTotal { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
}
