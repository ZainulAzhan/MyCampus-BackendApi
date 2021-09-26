using AutoMapper;
using MediatR;
using MyCampus.Data.Context;
using MyCampus.Domain.Academics;
using MyCampus.Service.Dtos.Academics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCampus.Service.Handlers.Academics
{

    public class CreateCourseCommand : IRequest<CourseOutputDto>
    {
        public string NameEn { get; set; }
        public string Code { get; set; }
    }

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseOutputDto>
    {
        private readonly CampusDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(CampusDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CourseOutputDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var datetime = DateTime.UtcNow;
            var course = new Course()
            {
                NameEn = request.NameEn,
                Code = request.Code,
                CreatedOn = datetime,
                ModifiedOn = datetime,
                TenantId = 1
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            var output = _mapper.Map<Course, CourseOutputDto>(course);
            return output;
        }
    }
}
