Ime projekta - # CarGarage
Opis projekta - Aplikacija nam omogucava da pratimo vozila u garazi, kada su usla i kada su izasla. Uz to da obavljaju placanje parkinga i istorija posete.


Struktura modela
## Models for Garage Application
Vehicle je model za osnovne podatke o vozilu, koje imaju njegov registarski broj i podaci o vlasniku
### Vehicle (Vozilo)
```csharp
public class Vehicle
{
    public int Id { get; set; } 
    public string LicencePlate { get; set; }
    public int OwnerId { get; set; }         
    public Owner Owner { get; set; }       
}
```
Garaza ima osnovne podatke o sebi, ime, lokacija, kapacitet i lista vozila koje se trenutno nalaze u njoj
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

Ovaj model je relacija izmedju vozila i garaze, fizicki znaci vozilo u garazi koje ima svoj id i sadrzi svoje kljuceve ka Vehicle i Garage, pored toga ima i vreme ulaska i izlaska, placanje po satu i ukupan racun
,takodje, metoda koja izracunava totalnu sumu za placanje parkinga.
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

Payment je model koji ima svoju sumu, datum placanja i povezan je s vozilom u garazi za koje se obavlja to placanje
### Payment ( Placanje )
```csharp
public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentTime { get; set; }
    public int VehicleInGarageId { get; set; }

    public VehicleInGarage VehicleInGarage { get; set; } = null!;

}
```

Owner je model koji predstavlja vlasnika vozila, sadrzi samo ime i prezime.
### Owner (Vlasnik)
```csharp
public class Owner
{
    public int  Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
```
