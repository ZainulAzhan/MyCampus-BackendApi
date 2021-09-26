using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCampus.Service.Dtos.Academics;
using MyCampus.Service.Handlers.Academics;
using MyCampus.Service.Queries.Academics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCampus.Api.Controllers
{
    public class CoursesController : MyCampusControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CourseOutputDto>> Create(CreateCourseCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCourse), new { id = result.Id }, result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CourseOutputDto>> GetCourse(int id)
        {
            try
            {
                var course = await _mediator.Send(new GetCourseQuery { Id = id });
                return course;
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
