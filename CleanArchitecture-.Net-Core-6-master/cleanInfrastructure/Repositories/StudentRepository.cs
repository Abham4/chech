namespace cleanInfrastructure.Repositories
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        private readonly CleanContext _cleanContext;
        public StudentRepository(CleanContext cleanContext) : base(cleanContext)
        {
            _cleanContext = cleanContext;
        }

        public override async Task<IReadOnlyList<Student>> GetAllAsync()
        {
            return await _cleanContext.Students
                .Include(c => c.Sex)
                .AsSingleQuery()
                .ToListAsync();
        }
    }
}