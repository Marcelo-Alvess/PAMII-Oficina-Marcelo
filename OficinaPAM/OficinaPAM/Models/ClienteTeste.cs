using System;
using System.Collections.Generic;
using System.Text;

namespace OficinaPAM.Models
{
    public class ClienteTeste
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public IList<VeiculoTeste> Veiculos { get; set; }
        public ClienteTeste()
        {
            Veiculos = new List<VeiculoTeste>();
        }
    }
}
