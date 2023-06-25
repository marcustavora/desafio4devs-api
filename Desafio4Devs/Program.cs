using Desafio4Devs.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.UseService();
builder.Services.UseRepository(builder.Configuration);
builder.Services.UseAuthentication(builder.Configuration);
builder.Services.UseSwagger();
builder.Services.UseCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("LocalHostPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
