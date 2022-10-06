using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Natech_Weather.Services
{
    public interface IMediaPlayerService
    {
        void PlaySuccess();

        void PlayError();
    }
}
