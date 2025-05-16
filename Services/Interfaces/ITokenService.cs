using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Ev4.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken();
    }
}