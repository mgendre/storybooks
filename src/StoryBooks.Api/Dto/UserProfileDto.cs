using StoryBooks.Models;

namespace StoryBooks.Api.Dto
{
    public class UserProfileDto
    {
        public UserProfileDto(string id, string issuer, string email, string lastName, string firstName, string subjectId)
        {
            Id = id;
            Issuer = issuer;
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            SubjectId = subjectId;
        }

        public string Id { get; }
        public string Issuer { get; }
        public string SubjectId { get; }
        public string Email { get; }
        public string LastName { get; }
        public string FirstName { get; }

        public static UserProfileDto FromModel(UserProfile up)
        {
            return new UserProfileDto(
                id: up.Id,
                email: up.Email,
                issuer: up.Issuer,
                subjectId: up.SubjectId,
                lastName: up.LastName,
                firstName: up.FirstName
                );
        }
    }
}
