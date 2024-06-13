using Pastelaria.Domain.Telefone.Dto;  // Importação do namespace que contém o DTO de Telefone

namespace Pastelaria.Domain.Telefone
{
    public interface ITelefoneRepository  // Definição da interface ITelefoneRepository
    {
        void Post(TelefoneDto telefone);  // Método da interface para adicionar um telefone (recebe um objeto do tipo TelefoneDto)
    }
}
