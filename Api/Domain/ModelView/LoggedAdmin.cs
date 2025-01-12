namespace minimal_api.Domain.ModelView
{
    public record LoggedAdmin
    {
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
