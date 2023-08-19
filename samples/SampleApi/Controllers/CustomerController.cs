using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notificator.Interfaces;
using SampleApi.Command;

namespace SampleApi.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {

        private readonly IMediator _mediator;
        private readonly INotificationContext _notificationContext;

        public CustomerController(
            IMediator mediator,
            INotificationContext notificationContext
        )
        {
            _mediator = mediator;
            _notificationContext = notificationContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddNewCustomerCommand command)
        {
            var response = await _mediator.Send(command);

            if (_notificationContext.HasNotifications())
                return BadRequest(_notificationContext.GetNotifications());

            return Ok(response);
        }
    }
}