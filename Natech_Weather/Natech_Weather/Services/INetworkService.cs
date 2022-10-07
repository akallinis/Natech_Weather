using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Natech_Weather.Services
{
    public interface INetworkService
    {
        Task<TResult> GetAsync<TResult>(string url, CancellationToken ct);
    }
}
