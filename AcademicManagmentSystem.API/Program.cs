using AcademicManagmentSystem.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionSting = builder.Configuration.GetConnectionString("AcademicManagmentSystemDbConnectionString");
builder.Services.AddDbContext<AcademicManagmentSystemDbContext>(options =>
{
    options.UseSqlServer(connectionSting);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", 
        b => b.AllowAnyHeader() //omogucava sve HTTP zaglavlje
            .AllowAnyOrigin() //dozvoljava pristup resursima sa bilo kog izvora (origin)
            .AllowAnyMethod()); //dozvoljava sve HTTP metode (GET, POST, PUT, DELETE itd.)
}); //Ova konfiguracija omogucava bilo kojoj spoljnoj strani da pristupi resursima
    //na nasem serveru bez ogranicenja, sto moze biti korisno u razvojnom okruzenju,
    //ali moze biti opasno u produkcionom okruzenju


builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration)); //Ovim postavkama omogucavamo da nasa aplikacija koristi Serilog za logovanje, pri cemu logovi mogu biti ispisani na konzoli. Serilog omogucava detaljno logovanje razlicitih dogadjaja u nasoj aplikaciji, sto nam pomaze u pronalazenju i resavanju problema.



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // ovo je middleware koji dolazi uz Serilog i koristi se za automatsko logovanje informacija o HTTP zahtevima i odgovorima

app.UseCors("AllowAll"); //ovde samo pozivamo tu politiku koju smo napravili

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
