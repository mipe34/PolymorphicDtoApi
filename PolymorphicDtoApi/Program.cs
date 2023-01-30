using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Code.Xml;
using PolymorphicDtoApi.Models.Warrior;
using PolymorphicDtoApi.Polymorph;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var warriorTypeDiscriminator = new WarriorTypeDiscriminator();
builder.Services.AddSingleton(warriorTypeDiscriminator);
builder.Services.AddControllers()
                .AddPolymorphJsonConverter<BaseWarriorDto>(warriorTypeDiscriminator)
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new XElementJsonConverter()))
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
