using ClientesApi.Context;
using ClientesApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesApi.Services
{
    public class ClientesService : IClienteService
    {

        private readonly AppDbContext _context;

        public ClientesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            try
            {
                return await _context.Clientes.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Cliente> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id); //Busca na memória, se achar retorna (!= FirstOrDefault) - ganho de desempenho
            return cliente;
        }
   
        public async Task<IEnumerable<Cliente>> GetClienteByNome(string nome)
        {
            IEnumerable<Cliente> clientes;
            if(string.IsNullOrEmpty(nome))
            {
                clientes = await _context.Clientes.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                clientes = await GetClientes();
            }
            return clientes;
        }
        public async Task CreateCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        public async Task DeleteCliente(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
