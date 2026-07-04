using TransitSystem.Core.Interfaces;
using TransitSystem.Core.Services;
using TransitSystem.WebApi.Mocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Lógica de Negocio
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<RechargeService>();

// Infraestructura simulada en memoria para la presentación
builder.Services.AddScoped<IAccountRepository, MockAccountRepository>();
builder.Services.AddScoped<ICardRepository, MockCardRepository>();
builder.Services.AddScoped<IRechargeTransactionRepository, MockRechargeRepository>();
builder.Services.AddScoped<IPaymentGateway, MockPaymentGateway>();

// Habilitar CORS para que el Frontend se pueda comunicar sin bloqueos
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddSingleton<TransitSystem.Core.Interfaces.ITransitIssueService, TransitSystem.Core.Services.TransitIssueService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();