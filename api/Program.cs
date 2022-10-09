var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8080", "https://drunc.net")
                            .AllowAnyMethod()
                            .AllowAnyMethod()
                            .WithExposedHeaders("content-disposition")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
                      });

});

// Add services to the container.

builder.Services.AddControllers();
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

String result = "";
do
{
    result = DatabaseService.dbInit();
    if (result != "success")
    {
        Console.WriteLine("The Database may not be ready to take connections");
        Console.WriteLine("Let me try again just givma sec");
        var end = DateTimeOffset.UtcNow.Add(TimeSpan.FromSeconds(1));
        while (DateTimeOffset.UtcNow < end)
        {}
    }
} while (result != "success");

//app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();