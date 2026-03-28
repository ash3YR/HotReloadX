var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World  bhaad me  jaao spam hai ab jii correctionse ");

app.Run();