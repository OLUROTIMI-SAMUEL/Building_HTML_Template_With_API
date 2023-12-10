using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OnePage.SendEmail.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr-FR"),
                new CultureInfo("es-ES"),
            };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseRequestLocalization();
app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseRequestLocalization(DefaultRequestCulture);


app.MapControllers();

app.Run();



//To open the html code and see it full fnctionality open project run it, then come back to the project open terminal
//then type  "dotnet tool install -g dotnet-serve" after locate the htmlpage.html file path e.g mine is
//"C:\Users\SAMUEL OLUROTIMI\source\repos\OnePage.SendEmail\OnePage.SendEmail\Template"
//press enter then type "dotnet-serve" press enterthis should generate a local url apart form the url on swagger
//with this you can test the "html code and implementation"
//it will generate something like this as the url "http://localhost:65457"put this on  the default browser and
//try to navigate to the "htmlpage.html" in side template folder
