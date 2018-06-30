# Sample project showing three Entity Framework Core contexts in the same databse.

To initialize the migrations, run (Powershell commands below):
* `dotnet ef migrations add CreateAnimalsSchema --context AnimalContext -o Data/Migrations/Animals`
* `dotnet ef migrations add CreateDoctorSchema --context DoctorContext -o Data/Migrations/Doctors`
* `dotnet ef migrations add CreateCarsSchema --context CarContext -o Data/Migrations/Cars`

Then, to initialize the databse for three contexts:
* `dotnet ef database update --context AnimalContext`
* `dotnet ef database update --context CarContext`
* `dotnet ef database update --context DoctorContext`


Copyright 2018 KevDever
MIT License.  See License.md