using System.Text.Json.Serialization;
using Jojo.Data;
using Dapper;

var builder = WebApplication.CreateBuilder(args);
SqlMapper.AddTypeMap(typeof(DateOnly), DbType.DateTime, true);

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
DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// app.MapGet("/GetAllCharacters", CharacterService.GetAllCharacters);
app.MapGet("/GetCharacters", CharacterService.GetCharById);
app.MapPost("/InsertCharacter", CharacterService.InsertCharacter);
app.MapPut("/UpdateCharacter", CharacterService.UpdateCharacter);
app.MapDelete("/DeleteCharacter", CharacterService.DeleteCharacter);




app.Run();

