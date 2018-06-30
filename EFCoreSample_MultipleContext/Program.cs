/*
Copyright 2018 KevDever
MIT License.  See License.md
*/

using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ConsoleApp3.Animals;
using ConsoleApp3.Doctors;
using ConsoleApp3.Cars;

namespace ConsoleApp3
{
    public static class Globals
    {
        public static readonly string CONNECTIONSTRING = "Server=(localdb)\\mssqllocaldb;Database=ThreeContextsOneDbDemo;Trusted_Connection=True;MultipleActiveResultSets=true";
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var providers = new ServiceCollection()
                .AddDbContext<AnimalContext>(options => options.UseSqlServer(Globals.CONNECTIONSTRING))
                .AddDbContext<DoctorContext>(options => options.UseSqlServer(Globals.CONNECTIONSTRING))
                .AddDbContext<CarContext>(options => options.UseSqlServer(Globals.CONNECTIONSTRING))
                .BuildServiceProvider();

            var animalContext = providers.GetService<AnimalContext>();
            await AnimalHelpers.Seed(animalContext);
            await AnimalHelpers.Read(animalContext);

            var carContext = providers.GetService<CarContext>();
            await CarHelpers.Seed(carContext);
            await CarHelpers.Read(carContext);

            var doctorContext = providers.GetService<DoctorContext>();
            await DoctorHelpers.Seed(doctorContext);
            await DoctorHelpers.Read(doctorContext);

            Console.ReadKey();

        }
    }
}
