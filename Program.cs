using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

var builder = WebApplication.CreateBuilder(args);

// Configure AWS DynamoDB Client
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var config = builder.Configuration.GetSection("AWS");
    return new AmazonDynamoDBClient(
        config["AccessKey"],
        config["SecretKey"],
        Amazon.RegionEndpoint.GetBySystemName(config["Region"])
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
