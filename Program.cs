using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
    var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
    var region = Environment.GetEnvironmentVariable("AWS_REGION");

    return new AmazonDynamoDBClient(
        accessKey,
        secretKey,
        Amazon.RegionEndpoint.GetBySystemName(region)
    );
});

// Register DynamoDB Context
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
