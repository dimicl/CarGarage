namespace CarGarage.Models
{
    public class Vehicle
    {
        public int Id { get; set; } 
        public string LicencePlate { get; set; }
        public int OwnerId { get; set; }         
        public Owner? Owner { get; set; }
    }
}
