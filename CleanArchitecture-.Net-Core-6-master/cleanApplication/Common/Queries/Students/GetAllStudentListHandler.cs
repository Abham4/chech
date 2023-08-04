namespace cleanApplication.Common.Queries.Students
{
    public class GetAllStudentListHandler : IRequestHandler<GetAllStudentListQuery, IEnumerable<StudentVM>>
    {
        private readonly IStudentRepository _student;
        private readonly ILogger<GetAllStudentListHandler> _logger;

        public GetAllStudentListHandler(IStudentRepository studentRepository, ILogger<GetAllStudentListHandler> logger)
        {
            _logger = logger;
            _student = studentRepository;
        }
        
        public async Task<IEnumerable<StudentVM>> Handle(GetAllStudentListQuery request, CancellationToken cancellationToken)
        {
            var students = await _student.GetAllAsync();

            return students.Select(x => new StudentVM
            {
                Id = x.Id,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Age = x.Age,
                Email = x.Email,
                Sex = x.Sex.Name
            });
        }
    }
}