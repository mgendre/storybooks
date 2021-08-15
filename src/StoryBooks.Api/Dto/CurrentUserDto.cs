namespace StoryBooks.Api.Dto
{
    public class CurrentUserDto
    {
        public CurrentUserDto(string issuer, string email, string lastName, string firstName, string subject)
        {
            Issuer = issuer;
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            Subject = subject;
        }

        public string Issuer { get; }
        public string Email { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public string Subject { get; }
        public string Id => $"{Issuer}#{Subject}";
    }
}
