using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://cpwkrawy:w7VzNYYvjqzpXNt8HJ_Yp2CvB14cdf0u@fish.rmq.cloudamqp.com/cpwkrawy");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange:"direct-exchange-example", type: ExchangeType.Direct);
while (true)
{
    Console.WriteLine("Mesaj: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "direct-exchange-example",
        routingKey: "direct-queue-example",
        body: byteMessage);
}

Console.Read();