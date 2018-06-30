/*
Copyright 2018 KevDever
MIT License.  See License.md
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp3.Doctors
{
    public class DoctorModel
    {
        public DoctorModel(string name, bool isMedical, bool hasTardis)
        {
            IsMedical = isMedical;
            HasTardis = hasTardis;
            Name = name;
        }

        public int Id { get; set; }
        public bool IsMedical { get; set; }
        public bool HasTardis { get; set; }
        public string Name { get; set; }
    }

    public class DoctorContext : DbContext
    {
        public DbSet<DoctorModel> Doctors { get; set; }

        public DoctorContext(DbContextOptions<DoctorContext> options) : base(options)
        {

        }
    }

    public class DesignTimeAnimalContextFactory : IDesignTimeDbContextFactory<DoctorContext>
    {
        public DoctorContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DoctorContext>();
            builder.UseSqlServer(Globals.CONNECTIONSTRING);
            return new DoctorContext(builder.Options);
        }
    }

    public static class DoctorHelpers
    {
        public static async Task Seed(DoctorContext context)
        {
            if (await context.Doctors.AnyAsync())
                return;
            Console.WriteLine("Seeding doctor data");
            var cars = new List<DoctorModel>
            {
                new DoctorModel("House", true, false),
                new DoctorModel("Hawking", false, false),
                new DoctorModel("Crusher", true, false),
                new DoctorModel("Who", false, true)
            };

            context.Doctors.AddRange(cars);
            await context.SaveChangesAsync();
        }

        public static async Task Read(DoctorContext context)
        {
            if (await context.Doctors.AnyAsync())
            {
                Console.WriteLine("Doctors from AnimalsContext");
                foreach (var doc in context.Doctors)
                    Console.WriteLine($"Id: {doc.Id} | Name: {doc.Name} | Medical Doctor? {doc.IsMedical} | Tardis? {doc.HasTardis}");
            }
        }
    }
}
