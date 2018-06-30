/*
Copyright 2018 KevDever
MIT License.  See License.md
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp3.Cars
{
    public class ConsumerCarModel
    {
        public ConsumerCarModel(string manufacturerName, string name)
        {
            ManufacturerName = manufacturerName;
            Name = name;
        }

        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public string Name { get; set; }
    }

    public class CarContext : DbContext
    {
        public DbSet<ConsumerCarModel> ConsumerCars { get; set; }

        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {

        }
    }

    public class DesignTimeAnimalContextFactory : IDesignTimeDbContextFactory<CarContext>
    {
        public CarContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CarContext>();
            builder.UseSqlServer(Globals.CONNECTIONSTRING);
            return new CarContext(builder.Options);
        }
    }

    public static class CarHelpers
    {
        public static async Task Seed(CarContext context)
        {
            if (await context.ConsumerCars.AnyAsync())
                return;
            Console.WriteLine("Seeding car data");
            var cars = new List<ConsumerCarModel>
            {
                new ConsumerCarModel("Ford", "Model T"),
                new ConsumerCarModel("Ferrari", "Enzo"),
                new ConsumerCarModel("Renault", "Megane")
            };

            context.ConsumerCars.AddRange(cars);
            await context.SaveChangesAsync();
        }

        public static async Task Read(CarContext context)
        {
            if (await context.ConsumerCars.AnyAsync())
            {
                Console.WriteLine("Cars from AnimalsContext");
                foreach (var car in context.ConsumerCars)
                    Console.WriteLine($"Id: {car.Id} | Genus: {car.ManufacturerName} | Name: {car.Name}");
            }
        }
    }
}
