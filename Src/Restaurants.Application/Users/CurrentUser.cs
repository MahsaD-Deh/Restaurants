namespace Restaurants.Application.Users
{
    public record CurrentUser(string id,
        string email,
        IEnumerable<string> Roles,
        string? Nationality,
        DateOnly? DateOfBirth)
    {
        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
