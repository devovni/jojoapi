global using  System.Data;
using Jojo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDataContext, DataContext>();
builder.Services.AddSingleton<ICharacterData, CharacterData>();


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

