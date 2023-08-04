namespace cleanDomain.Enumerations
{
    public class Sex : Enumeration
    {
        public static Sex Male = new Sex(1, nameof(Male).ToLowerInvariant());
        public static Sex Female = new Sex(2, nameof(Female).ToLowerInvariant());
        public static Sex NotSpecified = new Sex(3, nameof(NotSpecified).ToLowerInvariant());
        public static IEnumerable<Sex> List() => new [] { Male, Female, NotSpecified };

        private Sex() {}

        private Sex(int id, string name) : base(id, name)
        {}
    }
}