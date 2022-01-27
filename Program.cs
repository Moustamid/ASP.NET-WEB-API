using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

//- ****************************** Add services to the container ******************************


//. Serilog
builder.Host.UseSerilog( (ctx , lc) => lc
    .WriteTo.File(
    path : "C:\\Users\\karim.moustamid\\OneDrive - AgileThought\\Bootcamp\\00-ASP.NET Core\\00-Web API\\HotelListing\\log-.txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: LogEventLevel.Information));


//. Add Controllers
builder.Services.AddControllers();


//. Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//. CORS Policy
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicyAllowAll" , builder => 
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
});


var app = builder.Build();

//- ****************************** Configure the HTTP request pipeline ******************************
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("CorsPolicyAllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();