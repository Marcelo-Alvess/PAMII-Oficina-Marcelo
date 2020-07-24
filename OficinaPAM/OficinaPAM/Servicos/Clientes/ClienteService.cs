using OficinaPAM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficinaPAM.Servicos.Clientes
{
    public class ClienteService : IClienteService
    {
        private readonly IRequest _request;
        private const string ApiUrlBase = "http://oficinamarcelo.somee.com/api/Clientes";
        //xyz --> Site da sua API

        public ClienteService()
        {
            _request = new Request();
        }

        public async Task<Cliente> UpdateAsync(Cliente c, int? clienteID)
        {
            if (clienteID != null)
            {
                await this.PutClienteAsync(c);
            }
            else
            {
                await this.PostClienteAsync(c);
            }

            return c;
        }

        public async Task<ObservableCollection<Cliente>> GetClientesAsync()
        {

            ObservableCollection<Models.Cliente> clientes = await
                _request.GetAsync<ObservableCollection<Models.Cliente>>(ApiUrlBase);

            return clientes;
        }

        public async Task<Cliente> GetClienteAsync(int clienteId)
        {
            string urlComplementar = string.Format("/{0}", clienteId);

            Cliente cliente = await _request.GetAsync<Cliente>(ApiUrlBase + urlComplementar);

            return cliente;
        }

        public async Task<Cliente> PostClienteAsync(Cliente c)
        {
            if (c.Id == 0)
                return await _request.PostAsync(ApiUrlBase, c);
            else
                return await _request.PutAsync(ApiUrlBase, c);
        }

        public async Task<Cliente> PutClienteAsync(Cliente c)
        {
            var result = await _request.PutAsync(ApiUrlBase, c);
            return result;
        }

        public async Task<Cliente> DeleteClienteAsync(int clienteID)
        {
            string urlComplementar = string.Format("/{0}", clienteID);
            await _request.DeleteAsync(ApiUrlBase + urlComplementar);
            return new Cliente() { Id = clienteID };
        }
        public async Task<ObservableCollection<Cliente>> GetClientesByNomeAsync(string value)
        {
            ObservableCollection<Models.Cliente> clientes =
                await _request.GetAsync<ObservableCollection<Models.Cliente>>(ApiUrlBase);

            var clientesFiltrados = clientes.Where(c => c.Nome.Contains(value));

            return new ObservableCollection<Cliente>(clientesFiltrados);
        }
    }
}
