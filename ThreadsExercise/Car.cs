using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadsExercise
{
    public class Car
    {
        public string Name { get; set; }
        public int Speed = 120;
        public int Progress = 0;
        public int Place = 0;
        public Car(string name)
        {
            Name = name;
        }
        public void started(Car car, int racelineLenght, List<Car> finishedcars)
        {
            Console.WriteLine("\t-" + Name + " started driving.");
            do
            {
                //if the raceline is not reached the speed will drive the progress forward
                car.Progress += Speed; Thread.Sleep(50);
                if (car.Progress > racelineLenght)
                {
                    //if the raceline is reached the car is added to finishedcars
                    finishedcars.Add(car);
                    Place = 1 + finishedcars.IndexOf(car);
                    //car will have its place variable set to where it was placed in the race
                    if (Place == 1)
                    {
                        Console.WriteLine("\n\t*\t" + Name + " won first place!\t*");
                    }
                    else
                    {
                        Console.WriteLine("\n\t*\t" + Name + " finished the race, on place " + Place + "!\t*");
                    }
                }
            } while (car.Progress < racelineLenght);
        }
    }
}
