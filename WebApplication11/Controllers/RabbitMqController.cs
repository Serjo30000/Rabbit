using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RabbitMqController : ControllerBase
	{

		private readonly IRabbitMqService _mqService;

		public RabbitMqController(IRabbitMqService mqService)
		{
			_mqService = mqService;
		}
		/// <summary>
		/// Получить Сообщение
		/// </summary>
		/// <response code="200">Успешное выполнение</response>
		/// <response code="404">Нет такого пути</response>
		[Route("[action]/{message}")]
		[HttpGet]
		public IActionResult SendMessage(string message)
		{
			if (message == null)
			{
				return NotFound();
			}
			_mqService.SendMessage(message);
			return Ok("Сообщение отправлено");
		}
		/// <summary>
		/// Создание сообщения на почту
		/// </summary>
		/// <response code="200">Успешное выполнение</response>
		/// <response code="400">Плохой запрос</response>
		[HttpPost]
		public IActionResult SendEmail([FromBody] Email email)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_mqService.SendEmail(email);
			return Ok("Сообщение отправлено");
		}
	}

}
