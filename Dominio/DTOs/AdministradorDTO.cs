namespace minimal_api.Dominio.DTOs
{
    public class AdministradorDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public string Perfil { get; set; } = "Adm";
    }
}
