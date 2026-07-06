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
builder.Services.AddScoped<TicketingProcessor>();
builder.Services.AddScoped<ITariffStrategy, MetropolitanoTariffStrategy>();
builder.Services.AddScoped<ITariffStrategy, Linea1TariffStrategy>();

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

builder.Services.AddSingleton<ITransitIssueService, TransitIssueService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazor");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.Run();