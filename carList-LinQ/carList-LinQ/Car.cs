using System;
namespace carList_LinQ
{
    public class Car
    {
        public int Id { get; set; }
        public string Constructor { get; set; } 
        public string Model { get; set; }
        public int Year { get; set; } 
        public bool IsElectric { get; set; } 

        public Car(int id, string constructor, string model, int year, bool isElectric)
        {
            Id = id;
            Constructor = constructor;
            Model = model;
            Year = year;
            IsElectric = isElectric;
        }
    }

}

