namespace cleanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentVM>>> GetAllStudent()
        {
            var getStudentsQuery = new GetAllStudentListQuery();
            var students = await _mediator.Send(getStudentsQuery);
            return Ok(students);
        }
        
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateStudent(CreateStudentCommand studentCommand)
        {
            var result = await _mediator.Send(studentCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStudent(UpdateStudentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(DeleteStudentCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}