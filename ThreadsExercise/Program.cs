using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

namespace ThreadsExercise
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Creating raceline and cars
            int RaceLineLenght = 100000; //k Meters 
            Car Volvo = new Car("Volvo");
            Car Ferrari = new Car("Ferrari");
            Car Tesla = new Car("Tesla");

            //Creating lists of cars in race and cars who finished the race
            List<Car> carlist = new List<Car>() { (Volvo), (Ferrari), (Tesla) };
            List<Car> finishedcars = new List<Car>();

            //Creating Threads for every car
            Thread car1 = new Thread(() =>
            {
                Volvo.started(Volvo, RaceLineLenght, finishedcars);
            });
            Thread car2 = new Thread(() =>
            {
                Tesla.started(Tesla, RaceLineLenght, finishedcars);
            });
            Thread car3 = new Thread(() =>
            {
                Ferrari.started(Ferrari, RaceLineLenght, finishedcars);
            });

            Console.WriteLine("\n\tTHE RACE HAS STARTED!\n The finish line is " + RaceLineLenght + "meters long!\n");
            car1.Start();
            car2.Start();
            car3.Start();

            //creating timetracker and converting to displayable variables
            Stopwatch TimerTracker = new Stopwatch();
            TimerTracker.Start();
            var TT = TimerTracker.Elapsed;
            string elapsedTime = $"{TT.Minutes}:{TT.Seconds}:{TT.Milliseconds}";
            int quit = 0;
            // loop to display the request for race status

            bool request = true;
            do
            {
                //Problem generator displays and generates problems every 30 seconds
                TT = TimerTracker.Elapsed;
                elapsedTime = $"{TT.Minutes}:{TT.Seconds}:{TT.Milliseconds}";
                if (TT.Seconds == 30 || TT.Seconds == 59)
                {
                    foreach (Car c in carlist)
                    {
                        problem(c);
                    }
                };
                Console.WriteLine("\n\n\tTo display the race - status, press the 'Enter'-key\n");
                char status = Console.ReadKey().KeyChar;
                if (status == '\r')
                {
                    //IF ENTER IS PRESSED THE RACESTATUS SCOOP IS USED
                    bool raceStatus = true;
                    do
                    {
                        TT = TimerTracker.Elapsed;
                        elapsedTime = $"{TT.Minutes}:{TT.Seconds}";
                        Console.Clear();
                        Console.WriteLine("\t\tRunTime: " + elapsedTime);

                        foreach (Car c in carlist)
                        {
                            if (c.Progress > RaceLineLenght) { quit++; if (quit == carlist.Count) { raceStatus = false; }; };

                        };
                        foreach (Car c in carlist)
                        {
                            Console.WriteLine(c.Name + " drove " + c.Progress + "m with " + c.Speed + " km/h");
                        };
                        Thread.Sleep(300);
                    } while (raceStatus == true);
                    //looping the request stops
                    request = false;
                }
            } while (request == true && finishedcars.Count != carlist.Count);
            Console.Clear();
            Console.WriteLine("------------------   Results   -------------------");
            foreach (Car c in carlist) { Console.WriteLine($"\t Place: {c.Place}: {c.Name}  - Speed: {c.Speed} km/h"); };
            Console.WriteLine("-----------------------------------------------------------");
            Console.ReadKey();
        }
        public static void problem(Car car)
        {
            Random prob = new Random(DateTime.Now.Millisecond);
            int problemgenerator = prob.Next(1, 50);
            switch (problemgenerator)
            {
                case 1: gasProb(car); break;//  1/50 
                case 2:
                case 3: puncProb(car); break;//  2/50
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20: birdProb(car); break;//  10/50
                case 21:
                case 22:
                case 23:
                case 24:
                case 25: motorProb(car); break;//  5/50
            }
        }
        public static void gasProb(Car car)
        {
            Console.WriteLine("\n" + car.Name + " ran out of gas and stopped! Refueling takes 30 seconds! ");
            Thread.Sleep(30000);
        }
        public static void puncProb(Car car)
        {
            Console.WriteLine("\n" + car.Name + ":s tire got punctured and stopped! Changeing tires takes 20 seconds! ");
            Thread.Sleep(20000);
        }
        public static void birdProb(Car car)
        {
            Console.WriteLine($"\nA bird hit {car.Name}:s window! Stopping and cleaning the window takes 10 seconds. ");
            Thread.Sleep(10000);
        }
        public static void motorProb(Car car)
        {
            car.Speed -= 1;
            Console.WriteLine($"\nThanks to a motor problem, the speed of {car.Name} is reduced with 1km/h, and is currently{car.Speed}km/h.");
            Thread.Sleep(500);
        }
    }
}