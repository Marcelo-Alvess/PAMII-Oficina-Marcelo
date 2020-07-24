using System;
using System.Collections.Generic;
using System.Text;
using OficinaPAM.Models;

namespace OficinaPAM.Servicos
{
    public class DadosParaTeste
    {
        public List<VeiculoTeste> Veiculos;
        public List<ClienteTeste> Clientes;
        public DadosParaTeste()
        {
            Veiculos = new List<VeiculoTeste>() {
                new VeiculoTeste() {Placa = "ABC-1234", Marca="Fiat", Modelo="147"},
                new VeiculoTeste() {Placa = "DEF-5678", Marca="Chevrolet", Modelo="Monza"},
                new VeiculoTeste() {Placa = "GHI-9012", Marca="Volkswagen", Modelo="Brasília"},
                new VeiculoTeste() {Placa = "JKL-3456", Marca="Ford",Modelo="Corcel"},
                new VeiculoTeste() {Placa = "MNO-7890", Marca="Citroen", Modelo="C4"},
                new VeiculoTeste() {Placa = "PQR-1234", Marca="Honda",Modelo="Civic"}
            };

            Clientes = new List<ClienteTeste>()
            {
                new ClienteTeste() { Nome="Gestrubindo", Endereco="Rua Sai debaixo", Telefone="12345-6789" },
                new ClienteTeste() { Nome="Berssindrílio", Endereco="Rua Sobe, mas não desce", Telefone="01234-5678" },
                new ClienteTeste() { Nome="Kestchbuncio", Endereco="Rua do beco sem fim", Telefone="90123-4567" }
            };

            Clientes[0].Veiculos.Add(Veiculos[0]);
            Clientes[0].Veiculos.Add(Veiculos[1]);
            Clientes[0].Veiculos.Add(Veiculos[2]);
            Clientes[1].Veiculos.Add(Veiculos[3]);
            Clientes[1].Veiculos.Add(Veiculos[4]);
            Clientes[2].Veiculos.Add(Veiculos[5]);
        }
    }
}
