namespace cleanInfrastructure.Context
{
    public class CleanContextSeed
    {
        public static async Task SeedAsync(CleanContext context, ILogger<CleanContextSeed> logger)
        {
            if(!context.Sexes.Any())
            {
                context.Sexes.AddRange(Sex.List());
            }

            await context.SaveChangesAsync();
        }
    }
}