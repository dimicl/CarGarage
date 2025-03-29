namespace CarGarage.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal TotalCharge { get; set; }
        public bool IsPaid { get; set; }

        public DateTime PaymentTime { get; set; }
        public DateTime ExpirationTime { get; set; } //payment time +  15min  ili krece novi obracun
        public int VehicleInGarageId { get; set; }

        public VehicleInGarage VehicleInGarage { get; set; } = null!;

    }
}
