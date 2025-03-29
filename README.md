# CarGarage

## Models for Garage Application
### Vehicle (Vozilo)
```csharp
public class Vehicle
{
    public int Id { get; set; } 
    public string LicencePlate { get; set; }
    public string Owner { get; set; }         
}
```

### Garage (Garaza)
```csharp
 public class Garage
 {
     public int Id { get; set; }
     public string Name { get; set; }
     public string Location { get; set; }
     public int Capacity { get; set; }
     public ICollection<VehicleInGarage> VehicleInGarage { get; set; } = new List<VehicleInGarage>();

 }
```

### VehicleInGarage (Vozilo u Garazi)
```csharp

 public class VehicleInGarage
 {
     public int VehicleInGarageId { get; set; }
     public int VehicleId { get; set; }
     public Vehicle Vehicle { get; set; }
     public int GarageId { get; set; }
     public Garage Garage { get; set; }   

     public DateTime EntryTime { get; set; }
     public DateTime? ExitTime { get; set; }
     public decimal HourlyRate { get; set; }

     public decimal? TotalCharge { get; set; }

     public void CalculateTotalCharge()
     {
         if(ExitTime == null) { throw new InvalidDataException("Exit time must be before calculating"); }

         var duration = ExitTime.Value - EntryTime;
         var totalHours = Math.Ceiling(duration.TotalHours);
         TotalCharge = (decimal)totalHours * HourlyRate;

     }

 }
```
