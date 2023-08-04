namespace cleanApplication.Common.Commands.Students
{
    public class DeleteStudentCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        public IStudentRepository _rep;

        public DeleteStudentCommandHandler(IStudentRepository repository)
        {
            _rep = repository;
        }
        
        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var stud = await _rep.GetByIdAsync(request.Id);
            await _rep.DeleteAsync(stud);
            await _rep.UnitOfWork.SaveChanges();
            return Unit.Value;
        }
    }
}