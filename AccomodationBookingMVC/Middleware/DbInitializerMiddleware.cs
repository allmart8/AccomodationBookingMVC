using AccomodationBookingMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AccomodationBookingMVC.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;

        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AccommodationContext db)
        {
            if (await db.Owners.AnyAsync())
            {
                await _next(context);
                return;
            }

            await SeedPremiseTypes(db);
            await SeedServices(db);
            await SeedEmployees(db);
            await SeedTenants(db);
            await SeedOwners(db);
            await SeedPremises(db);
            await SeedRentalAgreements(db);
            await SeedPremiseServices(db);

            await _next(context);
        }

        private async Task SeedPremiseTypes(AccommodationContext db)
        {
            for (int i = 1; i <= 10; i++)
            {
                db.PremiseTypes.Add(new PremiseType
                {
                    TypeName = $"Тип #{i}",
                    TypeDescription = $"Описание типа {i}"
                });
            }
            await db.SaveChangesAsync();
        }

        private async Task SeedServices(AccommodationContext db)
        {
            for (int i = 1; i <= 10; i++)
            {
                db.Services.Add(new Service
                {
                    ServiceName = $"Услуга #{i}",
                    ServiceDescription = $"Описание услуги {i}"
                });
            }
            await db.SaveChangesAsync();
        }

        private async Task SeedEmployees(AccommodationContext db)
        {
            for (int i = 1; i <= 10; i++)
            {
                db.PlatformEmployees.Add(new PlatformEmployee
                {
                    FullName = $"Сотрудник #{i}",
                    Position = "Менеджер",
                    Email = $"emp{i}@mail.com",
                    HireDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-i * 30))
                });
            }
            await db.SaveChangesAsync();
        }

        private async Task SeedTenants(AccommodationContext db)
        {
            for (int i = 1; i <= 10; i++)
            {
                db.Tenants.Add(new Tenant
                {
                    FullName = $"Арендатор #{i}",
                    Gender = i % 2 == 0 ? "M" : "F",
                    BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-25 - i)),
                    PhoneNumber = $"+375 (29) 000-00-{i:00}",
                    PassportData = $"AB{i:0000000}",
                    MaxPrice = 1000 + i * 100,
                    AdditionalWishes = "Нет"
                });
            }
            await db.SaveChangesAsync();
        }

        private async Task SeedOwners(AccommodationContext db)
        {
            for (int i = 1; i <= 10; i++)
            {
                db.Owners.Add(new Owner
                {
                    FullName = $"Владелец #{i}",
                    Gender = i % 2 == 0 ? "M" : "F",
                    BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-30 - i)),
                    PhoneNumber = $"+375 (29) 111-11-{i:00}",
                    PassportData = $"BM{i:0000000}"
                });
            }
            await db.SaveChangesAsync();
        }

        private async Task SeedPremises(AccommodationContext db)
        {
            var owners = await db.Owners.ToListAsync();
            var types = await db.PremiseTypes.ToListAsync();

            for (int i = 1; i <= 10; i++)
            {
                db.Premises.Add(new Premise
                {
                    PremiseName = $"Помещение #{i}",
                    Address = $"Город, Улица {i}, дом {i}",
                    RoomCount = 1 + i % 4,
                    Area = 20 + i * 5,
                    HasRestroom = i % 2 == 0,
                    OwnerId = owners[i % owners.Count].OwnerId,
                    PremiseTypeId = types[i % types.Count].PremiseTypeId
                });
            }

            await db.SaveChangesAsync();
        }

        private async Task SeedRentalAgreements(AccommodationContext db)
        {
            var tenants = await db.Tenants.ToListAsync();
            var premises = await db.Premises.ToListAsync();
            var employees = await db.PlatformEmployees.ToListAsync();

            for (int i = 1; i <= 10; i++)
            {
                db.RentalAgreements.Add(new RentalAgreement
                {
                    TenantId = tenants[i % tenants.Count].TenantId,
                    PremiseId = premises[i % premises.Count].PremiseId,
                    EmployeeId = employees[i % employees.Count].EmployeeId,
                    ConclusionDate = DateOnly.FromDateTime(DateTime.Now),
                    RentalStartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    RentalEndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30)),
                    TotalSum = 1500 + i * 100
                });
            }

            await db.SaveChangesAsync();
        }

        private async Task SeedPremiseServices(AccommodationContext db)
        {
            var premises = await db.Premises.ToListAsync();
            var services = await db.Services.ToListAsync();

            for (int i = 0; i < premises.Count; i++)
            {
                premises[i].Services.Add(services[i % services.Count]);
            }

            await db.SaveChangesAsync();
        }
    }
}
