using Api;
using Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region OpenedService

var groups = new List<OpenApiInfo>();
IEnumerable<IConfigurationSection> openedServiceSections =
    builder.Configuration.GetSection("OpenedService").GetChildren();
foreach (var section in openedServiceSections)
{
    groups.Add(new OpenApiInfo()
    {
        Title = section["title"],
        Version = section["version"],
        Description = section["Description"]
    });
}

string[] SwagggerXMLDocs = builder.Configuration.GetSection("SwagggerXMLDocs").Get<string[]>();

builder.Services.AddSwaggerGen(c =>
{
    groups.ForEach(g => { c.SwaggerDoc(g.Title, g); }); 
    
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Contract.xml");
    if (File.Exists(path))
    {
        c.IncludeXmlComments(path);
    }

    if (SwagggerXMLDocs != null)
    {
        foreach (string docName in SwagggerXMLDocs)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, docName);

            if (File.Exists(fullPath))
            {
                c.IncludeXmlComments(fullPath);
            }
        }
    }
    
    
});

#endregion

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Test>();
builder.Services.AddSugarSqlDB(builder.Configuration,builder.Environment);

builder.Services.AddScoped<Test>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI(c =>
{
                
    groups.ForEach(g =>
    {
        c.SwaggerEndpoint($"{g.Title}/swagger.json", $"{g.Title}");
    });

});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();