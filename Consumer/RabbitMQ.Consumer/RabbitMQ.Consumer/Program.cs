using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://dpestitr:F-AB4X8kO-0guCckGM2GbOTCpUjKeh1I@moose.rmq.cloudamqp.com/dpestitr");

// Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

// Queue'dan Mesaj Okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
consumer.Received += (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği yer
    // e.Body: Kuyruktaki mesajın verisini bütünsel olarak getirecektir
    // e.Body.Span veya e.Body.ToArray(): Kuyruktaki mesajın byte verisini getirecektir
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};

Console.Read();