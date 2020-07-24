using OficinaPAM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficinaPAM.Servicos.Atendimentos
{
    public class AtendimentoFotoService : IAtendimentoFotoService
    {
        private readonly IRequest _request;
        private const string ApiUrlBase = "http://oficinamarcelo.somee.com/api/AtendimentoFotos";
        //xyz --> Site da sua API

        public AtendimentoFotoService()
        {
            _request = new Request();
        }
        public async Task<ObservableCollection<AtendimentoFoto>> GetAtendimentoFotosAsync()
        {
            ObservableCollection<Models.AtendimentoFoto> atendimentoFotos = await
                _request.GetAsync<ObservableCollection<Models.AtendimentoFoto>>(ApiUrlBase);
            return atendimentoFotos;
        }
        public async Task<AtendimentoFoto> PostAtendimentoFotoAsync(AtendimentoFoto f)
        {
            return await _request.PostAsync(ApiUrlBase, f);
        }
        public async Task<AtendimentoFoto> PutAtendimentoFotoAsync(AtendimentoFoto f)
        {
            var result = await _request.PutAsync(ApiUrlBase, f);
            return result;
        }
        public async Task<AtendimentoFoto> DeleteAtendimentoFotoAsync(int atendimentoFotoId)
        {
            string urlComplementar = string.Format("/{0}", atendimentoFotoId);
            await _request.DeleteAsync(ApiUrlBase + urlComplementar);
            return new AtendimentoFoto() { AtendimentoFotoID = atendimentoFotoId };
        }

        public async Task<ObservableCollection<AtendimentoFoto>> GetAtendimentoFotosAsync(int atendimentoId)
        {
            ObservableCollection<Models.AtendimentoFoto> atendimentoFotos = await _request.GetAsync<ObservableCollection<Models.AtendimentoFoto>>(ApiUrlBase);

            var fotosFiltradasPorAtendimento = atendimentoFotos.Where(a => a.AtendimentoFotoID == atendimentoId);

            return new ObservableCollection<AtendimentoFoto>(fotosFiltradasPorAtendimento);
        }

    }
}
