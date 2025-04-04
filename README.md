# CarGarage
## Opis projekta 
Aplikacija nam omogucava da pratimo vozila u garazi, kada su usla i kada su izasla. Uz to da obavljaju placanje parkinga i istorija posete.

## Osnovne funkcionalnosti
-Pravljenje novih garaza 
-Pravljenje novih vozila
-Istorija boravka vozila u garazi
-Ulazak i izlazak iz garaze
-Naplata

## Struktura modela

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
Garaza ima osnovne podatke o sebi, ime, lokacija, kapacitet i lista vozila koje se trenutno nalaze u njoj kao i dostupna mesta za parking koja racunamo na osnovu zauzetih mesta 
### Garage (Garaza)
```csharp
 public class Garage
 {
     public int Id { get; set; }
     public string Name { get; set; }
     public string Location { get; set; }
     public int Capacity { get; set; }
     public int CurrentOccupancy { get; set; }
     public int AvailableSpots { get
         {
             return Capacity - CurrentOccupancy;
         } 
     }

     public ICollection<VehicleInGarage> VehicleInGarage { get; set; } = new List<VehicleInGarage>();

     public bool IsFull { get
         {
             return CurrentOccupancy >= Capacity;
         }
     }

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

     public int? OwnerId { get; set; }
     public Owner? Owner { get; set; }

     public bool IsVehicleStillInGarage { get; set; } = true;


 }
```

Payment je model koji ima svoju sumu, datum placanja i povezan je s vozilom u garazi za koje se obavlja to placanje
### Payment ( Placanje )
```csharp
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
```

Owner je model koji predstavlja vlasnika vozila, sadrzi samo ime i prezime.
### Owner (Vlasnik)
```csharp
public class Owner
{
    public int  Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

}
```
Application je model za pravljenje dodatnih popusta, uz osnovne informacije o korisniku kao i kredit koji se dopunjuje kako bi imali popust
### Application (Aplikacija)
```csharp
public class Application
{
    public int ApplicationId { get; set; }
    public int OwnerId { get; set; }
    public Owner Owner { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public decimal Credit { get; set; }
    public bool HasActiveMembership { get; set; }

}
```
### Instrukcije za instalaciju i pokretanje
1. Klonirajte repozitorijum
   ```bash
   git clone <link>
   ```
2. Instalirajte potrebne pakete
   ```bash
   dotnet restore
   ```
3. Pokrenite migracije za bazu
   ```bash
   dotnet ef database update
   ```
4. Pokrenite aplikaciju
   ```bash
   dotnet run
   ```

### Kako koristiti aplikaciju
- **Dodavanje garaze**: Korisnik moze registrovati novu garazu sa nazivom, lokacijom, kapacitetom i cenom po satu.
- **Dodavanje vozila**: Registracija vozila sa osnovnim informacijama
- **Evidencija ulaska/izlaska**: Zapisivanje vremena ulaska i izlaska
- **Naplata**: Automatsko naplacivanje na osnovnu vremena provedenog u garazi.

## Tehnicki detalji
- **Platforma**: .NET Core 8
- **ORM**: Entity Framework Core
- **Baza podataka**: SQL Server
