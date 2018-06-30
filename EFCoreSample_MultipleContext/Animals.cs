/*
Copyright 2018 KevDever
MIT License.  See License.md
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp3.Animals
{
    public class AnimalContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
        {

        }
    }

    public class DesignTimeAnimalContextFactory : IDesignTimeDbContextFactory<AnimalContext>
    {
        public AnimalContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AnimalContext>();
            builder.UseSqlServer(Globals.CONNECTIONSTRING);
            return new AnimalContext(builder.Options);
        }
    }

    public class Animal
    {
        public Animal(string genus, string name)
        {
            Genus = genus;
            Name = name;
        }

        public Animal()
        {

        }

        public int Id { get; set; }
        public string Genus { get; set; }
        public string Name { get; set; }
    }

    public static class AnimalHelpers
    {
        public static async Task Seed(AnimalContext context)
        {
            if (await context.Animals.AnyAsync())
                return;
            Console.WriteLine("Seeding animal data");
            var animals = new List<Animal>
            {
                new Animal("Carnivora", "Housecat"),
                new Animal("Perissodactyla", "Horse"),
                new Animal("Dermoptera", "Lemur")
            };

            context.Animals.AddRange(animals);
            await context.SaveChangesAsync();
        }

        public static async Task Read(AnimalContext context)
        {
            if (await context.Animals.AnyAsync())
            {
                Console.WriteLine("Animals from AnimalsContext");
                foreach (var animal in context.Animals)
                    Console.WriteLine($"Id: {animal.Id} | Genus: {animal.Genus} | Name: {animal.Name}");
            }
        }
    }
}
