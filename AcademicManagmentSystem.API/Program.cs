using AcademicManagmentSystem.API.Configurations;
using AcademicManagmentSystem.API.Contracts;
using AcademicManagmentSystem.API.Data;
using AcademicManagmentSystem.API.Repository;
using AcademicManagmentSystem.API.Services.Implementation;
using AcademicManagmentSystem.API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Уклоните ову линију, јер може изазвати проблеме
    // c.DocInclusionPredicate((docName, apiDesc) =>
    // {
    //     var assemblyName = apiDesc.ControllerTypeInfo.Assembly.GetName().Name;
    //     return assemblyName.StartsWith("YourProjectNamespace");
    // });

    // Додајте овај ред уместо претходног
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddControllers();

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

builder.Services.AddAutoMapper(typeof(MapperConfig));//ukljucujemo automapper u projekat 

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); //Ovo ce vaziti u duzini jednog HTTP zahteva
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IUploadExamService, UploadExamService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDeloviService, DeloviService>();
builder.Services.AddScoped<IPredmetService, PredmetService>();
builder.Services.AddSingleton<IPendingChangesService, PendingChangesService>();

builder.Services.AddScoped<IPredmetiRepository, PredmetiRepository>();//Ovo ce vaziti u duzini jednog HTTP zahteva
builder.Services.AddScoped<IDeloviRepository,DeloviRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITipRepository, TipRepository>();

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
