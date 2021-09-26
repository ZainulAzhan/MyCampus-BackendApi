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

namespace MyCampus.Service.Queries.Academics
{
    public class GetCourseQuery : IRequest<CourseOutputDto>
    {
        public int Id { get; set; }
    }

    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseOutputDto>
    {
        private readonly CampusDbContext _context;
        private readonly IMapper _mapper;

        public GetCourseQueryHandler(CampusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CourseOutputDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.Id);
            if (course == null)
            {
                throw new Exception("Record not found");
            }
            return _mapper.Map<Course, CourseOutputDto>(course);
        }
    }
}
