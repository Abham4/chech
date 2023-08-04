namespace cleanInfrastructure.EntityConfigurations
{
    internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> typeBuilder)
        {
            typeBuilder.Property<int>(c => c.SexId)
                .UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("SexId").IsRequired();
            typeBuilder.HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}