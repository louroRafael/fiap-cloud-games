using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCG.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository UsuarioRepository { get; }
        IJogoRepository JogoRepository { get; }
        IPromocaoRepository PromocaoRepository { get; }
        Task<bool> Commit();
    }
}