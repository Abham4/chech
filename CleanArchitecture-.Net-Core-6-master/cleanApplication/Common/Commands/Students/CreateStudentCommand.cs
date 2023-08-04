namespace cleanApplication.Common.Commands.Students
{
    public class CreateStudentCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int SexId { get; set; }
    }

    public class CreatesStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IStudentRepository _student;
        private readonly ILogger<CreatesStudentCommandHandler> _logger;

        public CreatesStudentCommandHandler(IStudentRepository studentRepository, ILogger<CreatesStudentCommandHandler> logger)
        {
            _logger = logger;
            _student = studentRepository;
        }

        public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var stu = new Student(request.FirstName, request.LastName, request.Age, request.Email, request.SexId);
            _logger.LogWarning("----Creating Student-----{@stu}----", stu);
            await _student.AddAsync(stu);
            await _student.UnitOfWork.SaveChanges();
            return stu.Id;
        }
    }

    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("{Student Name} is required")
                .NotNull().MaximumLength(200).WithMessage("Length must not exceed 200 character");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("{Student Name} is required")
                .NotNull().MaximumLength(200).WithMessage("Length must not exceed 200 character");

            RuleFor(c => c.Email)
                .EmailAddress()
                .NotEmpty().WithMessage("{Student Email} is required");
            
            RuleFor(c => c.Age)
                .NotEmpty().WithMessage("{Student Age} is required")
                .NotNull().GreaterThanOrEqualTo(18);
        }
    }
}