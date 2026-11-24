namespace ProjetoAutenticaçãoBasica.API.Models.DTOs
{
    public class CriarPassagemDTO
    {
        public int VooId { get; set; }
        public int PassageiroId { get; set; }
        public int Assento { get; set; }
    }

    public class PassagemInfoDTO
    {
        public int Id { get; set; }
        public int CodigoVoo { get; set; }
        public int Trajeto { get; set; }
        public string NomePassageiro { get; set; }
        public int Assento { get; set; }
    }
}
