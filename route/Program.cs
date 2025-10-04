using route;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace route
{
    internal class Program
    {

        static ServerConnection serverConnection = new ServerConnection("http://127.0.0.1:3000");

        static void Main(string[] args)
        {
            while (true)
            {
                managemenu();
            }
        }
        static void managemenu()
        {
            writemenu();
            int num = getnumber(1, 10);
            switchmenu(num);
            Console.WriteLine("Nyomj egy gombot a folytatashoz");
            Console.ReadKey();
            Console.Clear();
        }
        static void writemenu()
        {
            Console.WriteLine("Valassz a menupontok kozul:");
            Console.WriteLine("1. kocsik listazasa");
            Console.WriteLine("2. tulajdonosok listazasa");
            Console.WriteLine("3. márkák listazasa");
            Console.WriteLine("4. Uj kocsi letrehozasa");
            Console.WriteLine("5. Uj tulajdonos letrehozasa");
            Console.WriteLine("6. Uj márka letrehozasa");
            Console.WriteLine("7. kocsi torlese");
            Console.WriteLine("8. tulajdonos torlese");
            Console.WriteLine("9. márka torlese");
            Console.WriteLine("10. Kilepes");
        }
        static int getnumber(int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write("Add meg a szamot: ");
                string line = Console.ReadLine().Trim(' ', ',', '.');
                int result;

                if (int.TryParse(line, out result))
                {
                    if (result >= min && result <= max)
                        return result;
                    else
                        Console.WriteLine("A szam nincs a megadott hatarerteken belul");
                }
                else
                {
                    Console.WriteLine("Nem szam lett megadva");
                }
            }
        
        }

        static async Task switchmenu(int num)
        {
            switch (num)
            {
                case 1:
                    await funcone();
                    break;
                case 2:
                    await functwo();
                    break;
                case 3:
                    await funchree();
                    break;
                case 4:
                    await funcfour();
                    break;
                case 5:
                    await funcfive();
                    break;
                case 6:
                    await funcsix();
                    break;
                case 7:
                    await funcseven();
                    break;
                case 8:
                    await funceight();
                    break;
                case 9:
                    await funcnine();
                    break;
                case 10:
                    functen();
                    break;
                default:
                    Console.WriteLine("Ismeretlen menupont");
                    break;
            }
        }

        static async Task funcone()
        {
            Console.WriteLine("kocsik listazasa");
            List<cars> cars = await serverConnection.getcars();

            if (cars.Count == 0)
            {
                Console.WriteLine("Nincsenek kocsik az adatbazisban.");
                return;
            }

            foreach (var car in cars)
            {
                Console.WriteLine($"ID: {car.id}, MarkaID: {car.manufacturersid}, Model: {car.model}, Teljesitmeny: {car.power} LE, Evjarat: {car.makeyear}, Kerekmeret: {car.tyresize}\"");
            }
        }

        static async Task functwo()
        {
            Console.WriteLine("Tulajdonosok listazasa");
            List<owners> owners = await serverConnection.getowner();

            if (owners.Count == 0)
            {
                Console.WriteLine("Nincsenek tulajdonosok az adatbazisban.");
                return;
            }

            foreach (var owner in owners)
            {
                Console.WriteLine($"ID: {owner.id}, KocsiID: {owner.carsid}, Nev: {owner.name}, Cim: {owner.address}, Szuletesi ev: {owner.birthyear}");
            }
        }

        static async Task funchree()
        {
            Console.WriteLine("Markak listazasa");
            List<manufacturers> manufacturers = await serverConnection.getmanu();

            if (manufacturers.Count == 0)
            {
                Console.WriteLine("Nincsenek markak az adatbazisban.");
                return;
            }

            foreach (var manu in manufacturers)
            {
                Console.WriteLine($"ID: {manu.id}, Nev: {manu.name}, Orszag: {manu.country}, Alapitas eve: {manu.launchyear}");
            }
        }

        static async Task funcfour()
        {
            Console.WriteLine("Uj kocsi letrehozasa");

            Console.WriteLine("Add meg a marka ID-jet:");
            int manufacturersid = getnumber();

            Console.Write("Add meg a modelt: ");
            string model = Console.ReadLine();

            Console.WriteLine("Add meg a teljesitmenyt (LE):");
            int power = getnumber();

            Console.WriteLine("Add meg a gyartasi evet:");
            int makeyear = getnumber();

            Console.WriteLine("Add meg a kerekmeretet (coll):");
            int tyresize = getnumber();

            if (string.IsNullOrEmpty(model))
            {
                Console.WriteLine("A model nev nem lehet ures!");
                return;
            }

            Message response = await serverConnection.postcars(manufacturersid, model, power, makeyear, tyresize);
            Console.WriteLine("Szerver valasza: " + response.mesage);
        }

        static async Task funcfive()
        {
            Console.WriteLine("Uj tulajdonos letrehozasa");

            Console.WriteLine("Add meg a kocsi ID-jet:");
            int carsid = getnumber();

            Console.Write("Add meg a nevet: ");
            string name = Console.ReadLine();

            Console.Write("Add meg a cimet: ");
            string address = Console.ReadLine();

            Console.WriteLine("Add meg a szuletesi evet:");
            int birthyear = getnumber();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address))
            {
                Console.WriteLine("A nev es a cim nem lehet ures!");
                return;
            }

            Message response = await serverConnection.postowner(carsid, name, address, birthyear);
            Console.WriteLine("Szerver valasza: " + response.mesage);
        }

        static async Task funcsix()
        {
            Console.WriteLine("Uj marka letrehozasa");

            Console.Write("Add meg a nevet: ");
            string name = Console.ReadLine();

            Console.Write("Add meg az orszagot: ");
            string country = Console.ReadLine();
            
            Console.WriteLine("Add meg az alapitas evet:");
            int launchyear = getnumber();
            
            Console.Write("Add meg a gyartasi evet (makeyear): ");
            string makeyear = Console.ReadLine();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(country))
            {
                Console.WriteLine("A nev es az orszag nem lehet ures!");
                return;
            }

            Message response = await serverConnection.postmanufct(name, launchyear, country, makeyear);
            Console.WriteLine("Szerver valasza: " + response.mesage);
        }

        static async Task funcseven()
        {
            Console.WriteLine("Kocsi torlese");
            Console.WriteLine("Add meg a torlendo kocsi ID-jet:");
            int id = getnumber();

            Message response = await serverConnection.deletecar(id);
            Console.WriteLine("Szerver valasza: " + response.mesage);
        }

        static async Task funceight()
        {
            Console.WriteLine("Tulajdonos torlese");
            Console.WriteLine("Add meg a torlendo tulajdonos ID-jet:");
            int id = getnumber();

            Message response = await serverConnection.deleteowner(id);
            Console.WriteLine("Szerver valasza: " + response.mesage);
        }

        static async Task funcnine()
        {
            Console.WriteLine("Marka torlese");
            Console.WriteLine("Add meg a torlendo marka ID-jet:");
            int id = getnumber();

            Message response = await serverConnection.deletemanufact(id);
            Console.WriteLine("Szerver valasza: " + response.mesage);
        }

        static void functen()
        {
            Console.WriteLine("Viszlat!");
            Environment.Exit(0);
        }
    }
}