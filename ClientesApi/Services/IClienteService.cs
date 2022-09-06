using ClientesApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientesApi.Services
{
    public interface IClienteService
    {

        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetCliente(int id);
        Task<IEnumerable<Cliente>> GetClienteByNome(string nome);
        Task CreateCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);
        Task DeleteCliente(Cliente cliente);
    }
}
