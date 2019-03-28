#run rabbitMQ inside docker on port 5672
docker run -p 5672:5672 rabbitmq

inside the service folder (common) we create our fluent API which helps quickly to define type of messages.
public static async Task Main(string[] args) starting C#7.0