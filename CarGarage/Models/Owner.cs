﻿namespace CarGarage.Models
{
    public class Owner
    {
        public int  Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
