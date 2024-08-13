using Application.Extension;
using Infra.Mongo.Extensions;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigureControllers()
    .AddMongo(builder.Configuration)
    .AddUseCases()
    .AddViaCEP(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
