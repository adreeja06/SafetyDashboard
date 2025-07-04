using Microsoft.EntityFrameworkCore;
using _1stModule_PIPremises.Data;
using _1stModule_PIPremises.Models;
using Bogus;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

// Configure EF with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=personinpremises.db"));

var app = builder.Build();

// 🔄 Seed dummy data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    var faker = new Faker();
    var random = new Random();
    var cities = new[] { "Kolkata", "Barauni", "Mumbai", "Delhi", "Chennai", "Bangalore", "Hyderabad", "Guwahati" };

    // ✅ Seed Locations & Users
    if (!db.Locations.Any())
    {
        var flags = new[] { "Employee", "Visitor", "Contractor", "Intern" };

        // Add city-based locations
        var locations = cities.Select(city => new Location
        {
            LocationName = city,
            Users = new List<User>()
        }).ToList();

        db.Locations.AddRange(locations);
        db.SaveChanges();

        // Generate 200 fake users
        var users = new List<User>();
        var swipeLogs = new List<SwipeLog>();

        for (int i = 0; i < 200; i++)
        {
            var flag = faker.PickRandom(flags);
            var user = new User
            {
                EmployeeName = faker.Name.FullName(),
                Flag = flag,
                Designation = faker.Name.JobTitle(),
                Remarks = faker.Lorem.Sentence(),
                Location = faker.PickRandom(locations)
            };
            users.Add(user);
        }

        db.Users.AddRange(users);
        db.SaveChanges();

        // Generate swipe logs
        foreach (var user in db.Users.Include(u => u.Location))
        {
            var swipeIn = faker.Date.Recent(1).AddMinutes(-faker.Random.Int(1, 180));
            DateTime? swipeOut = random.NextDouble() < 0.75
                ? swipeIn.AddMinutes(faker.Random.Int(10, 180))
                : null;

            db.SwipeLogs.Add(new SwipeLog
            {
                UserID = user.UserID,
                LocationID = user.Location.LocationID,
                SwipeIN = swipeIn,
                SwipeOUT = swipeOut,
                Flag = user.Flag,
                User = user,
                Location = user.Location
            });
        }

        db.SaveChanges();
    }

    // ✅ Seed Permits even if Locations already exist
    if (!db.Permits.Any())
    {
        var permitTypes = new[] { "Hot", "Cold", "Height" };
        var permits = new List<Permit>();

        for (int i = 1; i <= 25; i++)
        {
            var type = faker.PickRandom(permitTypes);
            var station = faker.PickRandom(cities);
            var locationCode = $"{faker.Random.Number(1000, 9999)}-{station.Substring(0, 3).ToUpper()}-{faker.Random.AlphaNumeric(2).ToUpper()}-{faker.Random.AlphaNumeric(4).ToUpper()}";

            permits.Add(new Permit
            {
                PermitNumber = faker.Random.AlphaNumeric(10).ToUpper(),
                PermitType = type,
                IssueDateTime = faker.Date.Recent(3),
                FunctionalLocation = locationCode,
                Description = faker.Lorem.Sentence(6),
                StationName = station
            });
        }

        db.Permits.AddRange(permits);
        db.SaveChanges();
    }
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Index}/{id?}");

app.Run();
