using System.Text.Json.Serialization;
using Jojo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataContext, DataContext>();
builder.Services.AddSingleton<ICharacterData, CharacterData>();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

static async Task<IResult> GetAllCharacters(ICharacterData data)
{
    try
    {
        return Results.Ok(await data.GetAll());
    }
    catch(Exception ex)
    {
        return Results.Problem(ex.Message);
    }
}
app.MapGet("/characters", GetAllCharacters);

app.Run();

