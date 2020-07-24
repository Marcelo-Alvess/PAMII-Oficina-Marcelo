﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OficinaPAM.Servicos
{
    public interface IRequest
    {
        Task<TResult> GetAsync<TResult>(string uri);

        Task<TResult> PostAsync<TResult>(string uri, TResult data);

        Task<TResult> PutAsync<TResult>(string uri, TResult data);

        Task DeleteAsync(string uri);
    }


    //Task<IEnumerable<TResult>> GetStartsWithByFieldAsync(string field, string value);

}


