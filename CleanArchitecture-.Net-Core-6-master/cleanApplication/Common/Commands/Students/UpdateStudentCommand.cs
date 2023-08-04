namespace cleanApplication.Common.Commands.Students
{
    public class UpdateStudentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int SexId { get; set; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _rep;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _rep = studentRepository;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var stud = await _rep.GetByIdAsync(request.Id);
            stud.Age = request.Age;
            stud.FirstName = request.FirstName;
            stud.LastName = request.LastName;
            stud.Email = request.Email;
            stud.SexId = stud.SetSex(request.SexId);
            await _rep.UpdateAsync(stud);
            await _rep.UnitOfWork.SaveChanges();
            return Unit.Value;
        }
    }
}