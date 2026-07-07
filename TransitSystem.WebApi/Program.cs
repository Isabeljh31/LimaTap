using TransitSystem.Core.Interfaces;
using TransitSystem.Core.Services;
using TransitSystem.WebApi.Mocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// L�gica de Negocio
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<RechargeService>();

builder.Services.AddScoped<ITariffStrategy, MetropolitanoTariffStrategy>();
builder.Services.AddScoped<ITariffStrategy, Linea1TariffStrategy>();

builder.Services.AddScoped<TicketingProcessor>();

// Infraestructura simulada en memoria para la presentación
builder.Services.AddScoped<IAccountRepository, MockAccountRepository>();
builder.Services.AddScoped<ICardRepository, MockCardRepository>();
builder.Services.AddScoped<IRechargeTransactionRepository, MockRechargeRepository>();
builder.Services.AddScoped<IPaymentGateway, MockPaymentGateway>();
builder.Services.AddScoped<IValidationLogRepository, MockValidationLogRepository>();

// Habilitar CORS para que el Frontend se pueda comunicar sin bloqueos
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

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