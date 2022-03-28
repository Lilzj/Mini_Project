using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onboarding.Core.Services.Implementations;
using Onboarding.Core.Services.Interfaces;
using Onboarding.Data;
using Onboarding.Data.Seed;
using Onboarding.DTOs.Request;
using Onboarding.Models.Models;
using Onboarding.Models.Repositories;
using Onboarding.Models.Repositories.Implementation;
using Onboarding.Utilities.Configuration;
using Onboarding.Utilities.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<Customer, IdentityRole>()
    .AddEntityFrameworkStores<OnboardingDbContext>().AddDefaultTokenProviders();
builder.Services.AddDbContextPool<OnboardingDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IValidator<CustomerRequestDto>, CustomerValidator>();
builder.Services.AddTransient<IValidator<OTPRequestDto>, PhoneNumberValidator>();
builder.Configuration.GetSection(BankConfig.Config);
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOTPService, OTPService>();

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

Seeder.EnsurePopulated(app).Wait();

app.Run();
