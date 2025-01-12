namespace minimal_api.Domain.DTOs
{
    public class EditVeicleDTO
    {
        public string? Name { get; set; } = default!;
        public string? Brand { get; set; } = default!;
        public int? Year { get; set; }
    }
}
