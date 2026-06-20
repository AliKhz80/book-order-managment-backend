using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure.Messaging;

public static class RabbitMqOptionsFactory
{
    public static RabbitMqOptions Create(IConfiguration configuration)
    {
        var section = configuration.GetSection("RabbitMq");

        return new RabbitMqOptions
        {
            HostName = section["HostName"] ?? "localhost",
            Port = int.TryParse(section["Port"], out var port) ? port : 5672,
            UserName = section["UserName"] ?? "guest",
            Password = section["Password"] ?? "guest",
            ExchangeName = section["ExchangeName"] ?? "book-order-management"
        };
    }
}
