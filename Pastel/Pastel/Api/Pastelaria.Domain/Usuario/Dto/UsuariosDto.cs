using System;


namespace Pastelaria.Domain.Usuario.Dto
{
  public  class UsuariosDto
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ind_Ativo { get; set; }
        public int IdPerfil { get; set; }
        public int IdGestor { get; set; }
        public string Telefone { get; set; }
        public string TipoTelefone { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }

    }
}
