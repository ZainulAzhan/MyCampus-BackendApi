using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCampus.Service.Handlers.PersonRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCampus.Api.Controllers
{
    public class AccountController : MyCampusControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
