namespace Brace.DomainModel.Command.Subjects
{
    public abstract class Subject
    {
        private static Subject _empty;
        public static Subject Nothing => _empty ?? (_empty = new NothingSubject());

        public static bool IsNullOrNothing(Subject subject)
        {
            return subject == null || subject == Nothing;
        }

        public string Id { get; set; }
    }
}