using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication11
{
    public class RabbitMqService : IRabbitMqService
    {
		public void SendMessage(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message);
		}

		public void SendMessage(string message)
		{
			var factory = new ConnectionFactory() { Uri = new Uri("amqps://frseewas:xcVFnbux3PtCmsBegoRH3PC36zFPihyg@porpoise.rmq.cloudamqp.com/frseewas") }; //своя очередь

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "MyQueue",
						   durable: false,
						   exclusive: false,
						   autoDelete: false,
						   arguments: null);

				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "",
					   routingKey: "MyQueue",
					   basicProperties: null,
						   body: body);
			}
		}


		public void SendEmail(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			SendEmail(message);
		}

		public void SendEmail(Email email)
		{
			using (MailMessage mm = new MailMessage(email.From, email.To))//"serjo30000@mail.ru" //"to@site.com"
			{
				mm.Subject = email.Subject;//"Hello"
				mm.Body = email.Body;//"Hello world"
				mm.IsBodyHtml = false;
				String str3 = email.Mail;
				int pt = email.Port;
				using (SmtpClient sc = new SmtpClient(str3, pt))//"smtp.mail.ru" //25
				{
					sc.EnableSsl = true;
					sc.DeliveryMethod = SmtpDeliveryMethod.Network;
					sc.UseDefaultCredentials = false;
					String str1 = email.UserName;
					String str2 = email.Password;
					sc.Credentials = new NetworkCredential(str1, str2);//"serjo30000@mail.ru" //"qmehW2VgZwiaxMTp3CN5"
					
					sc.Send(mm);
					var factory = new ConnectionFactory() { Uri = new Uri("amqps://frseewas:xcVFnbux3PtCmsBegoRH3PC36zFPihyg@porpoise.rmq.cloudamqp.com/frseewas") }; //своя очередь

					using (var connection = factory.CreateConnection())
					using (var channel = connection.CreateModel())
					{
						channel.QueueDeclare(queue: "MyQueue",
								   durable: false,
								   exclusive: false,
								   autoDelete: false,
								   arguments: null);

						var body = Encoding.UTF8.GetBytes("To: "+mm.To.ToString()+" From: "+mm.From+" Subject: "+mm.Subject+" UserName: "+str1+" Password: "+str2+" Body: "+mm.Body+" Mail: "+ str3+" Port: "+pt);

						channel.BasicPublish(exchange: "",
							   routingKey: "MyQueue",
							   basicProperties: null,
								   body: body);
					}
				}
			}
		}

	}
}
