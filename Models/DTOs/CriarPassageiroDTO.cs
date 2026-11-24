namespace ProjetoAutenticaçãoBasica.API.Models.DTOs
{
    public class CriarPassageiroDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
    }

    public class PassageiroInfoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }   
}
