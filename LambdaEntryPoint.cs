using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.AspNetCoreServer;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                // No need to manually specify region; AWS SDK will automatically use the Lambda's region
                return new AmazonDynamoDBClient();
            });

            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            // Add ASP.NET Core MVC (Controllers)
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Ensure camelCase is applied in Lambda as well
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                });

            // Swagger isn't needed in Lambda, but you can keep it for local development (in Program.cs)
            // services.AddSwaggerGen(); // Keep this commented out for Lambda
        });

        builder.Configure(app =>
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Maps your API controllers
            });
        });
    }
}
