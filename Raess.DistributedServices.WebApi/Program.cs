using Raess.DomainServices.RepositoryContract;
using Raess.ApplicationServices.ProductServices.Contracts;
using Raess.ApplicationServices.ProductServices.Implementations;
using Raess.InfraestructureServices.Repository.Contracts;
using AutoMapper;
using Raess.CrossCutting;
using Raess.DomainServices.Domain.Contracts;
using Raess.DomainServices.Domain.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDomain, ProductDomain>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});

var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


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
