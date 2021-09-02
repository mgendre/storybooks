namespace StoryBooks.Infra.Controllers.Dto
{
    public class CurrentUserDto
    {
        public CurrentUserDto(string issuer, string email, string lastName, string firstName, string subjectId)
        {
            Issuer = issuer;
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            SubjectId = subjectId;
        }

        public string Issuer { get; }
        public string SubjectId { get; }
        public string Email { get; }
        public string LastName { get; }
        public string FirstName { get; }
    }
}
