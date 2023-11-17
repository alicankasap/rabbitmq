using RabbitMQ.Client;
using System.Text;

// Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://dpestitr:F-AB4X8kO-0guCckGM2GbOTCpUjKeh1I@moose.rmq.cloudamqp.com/dpestitr");

// Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

// Queue'ya Mesaj Gönderme
// RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir. Haliyle mesajları bizim byte türüne dönüştürmemiz gerekecektir.
//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 100; i++)
{
    await Task.Delay(500);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: properties);
}

Console.Read();