using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    // Override the Init method to configure services for Lambda
    protected override void Init(IWebHostBuilder builder)
    {
        // Add services needed for your Lambda function
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                // No need to manually specify region; AWS SDK will automatically use the Lambda's region
                return new AmazonDynamoDBClient();
            });

            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            // Add ASP.NET Core MVC (Controllers)
            services.AddControllers();

            // If you need to enable Swagger (for local development), keep this, but it won't be used in Lambda
            // services.AddSwaggerGen();
        });

        // Configure the middleware (request handling)
        builder.Configure(app =>
        {
            // Configure routing and controller endpoints
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Maps your API controllers
            });
        });
    }
}
