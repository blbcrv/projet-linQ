using System.Xml.Linq;
using carList_LinQ;

namespace CarProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> cars = InitializeCars();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nAffichage de la liste de voitures:");
                Console.WriteLine("1 - Afficher toutes les voitures");
                Console.WriteLine("2 - Afficher les voitures triées par années");
                Console.WriteLine("3 - Rechercher les voitures avant une certaine année");
                Console.WriteLine("4 - Rechercher des voitures en fonction de leur constructeur");
                Console.WriteLine("5 - Rechercher des voitures en fonction de leur modèle");
                Console.WriteLine("6 - Rechercher des voitures électriques (depuis la liste ou depuis un fichier XML)");
                Console.WriteLine("7 - Générer un XML avec la liste de voiture présente");
                Console.WriteLine("10 - Quitter");

                Console.Write("Choisissez une option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayCars(cars);
                        break;
                    case 2:
                        DisplayCars(cars.OrderBy(car => car.Year).ToList());
                        break;
                    case 3:
                        Console.Write("Entrez l'année maximale: ");
                        int year = int.Parse(Console.ReadLine());
                        DisplayCars(cars.Where(car => car.Year < year).ToList());
                        break;
                    case 4:
                        XElement xml = CarsToXml(cars);
                        Console.WriteLine("\nXML Généré:");
                        Console.WriteLine(xml);
                        break;
                    case 5:
                        Console.Write("Entrez le constructeur à afficher: ");
                        string constructor = Console.ReadLine();
                        DisplayCars(cars.Where(car => car.Constructor == constructor).ToList());
                        break;
                    case 6:
                        Console.Write("Entrez le modèle à afficher: ");
                        string model = Console.ReadLine();
                        DisplayCars(cars.Where(car => car.Model == model).ToList());
                        break;
                    case 7:
                        Console.WriteLine("1 - Depuis la liste de base ");
                        Console.WriteLine("2 - Depuis un fichier XML");
                        int sourceChoice = int.Parse(Console.ReadLine());
                        switch (sourceChoice)
                        {
                            case 1:
                                DisplayCars(cars.Where(car => car.IsElectric == true).ToList());
                                break;
                            case 2:
                                DisplayCarsFromXML();
                                break;
                        }
                        break;
                    case 10:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Option non valide, essayez à nouveau.");
                        break;
                }
            }
        }

        static List<Car> InitializeCars()
        {
            return new List<Car>
            {
                new Car(1, "Tesla", "Model S", 2020, true),
                new Car(2, "Ford", "Mustang", 2021, false),
                new Car(3, "Nissan", "Leaf", 2019, true),
                new Car(4, "Chevrolet", "Volt", 2018, true),
                new Car(5, "Toyota", "Camry", 2022, false)
            };
        }

        static void DisplayCars(List<Car> cars)
        {
            if (cars.Count > 0)
            {
                foreach (var car in cars)
                {
                    Console.WriteLine($"ID: {car.Id}, Constructor: {car.Constructor}, Model: {car.Model}, Year: {car.Year}, Electric: {car.IsElectric}");
                }
            }
            else
            {
                Console.WriteLine("Aucune voiture à afficher.");
            }
        }

        static void DisplayCarsFromXML()
        {
            var xml = XDocument.Load("../../../Cars.xml");

            var electricCars = from car in xml.Descendants("Car")
                               where (bool)car.Element("IsElectric")
                               select new
                               {
                                   ID = (int)car.Element("ID"),
                                   Constructor = (string)car.Element("Constructor"),
                                   Model = (string)car.Element("Model"),
                                   Year = (int)car.Element("Year"),
                                   IsElectric = (bool)car.Element("IsElectric")
                               };

            foreach (var car in electricCars)
            {
                Console.WriteLine($"ID: {car.ID}, Constructor: {car.Constructor}, Model: {car.Model}, Year: {car.Year}, Electric: {car.IsElectric}");
            }
        }

        static XElement CarsToXml(List<Car> cars)
        {
            XElement xmlCars = new XElement("Cars",
                cars.Select(car => new XElement("Car",
                    new XElement("ID", car.Id),
                    new XElement("Constructor", car.Constructor),
                    new XElement("Model", car.Model),
                    new XElement("Year", car.Year),
                    new XElement("IsElectric", car.IsElectric)
                ))
            );

            return xmlCars;
        }
    }
}
