using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface ISeguidorRepository
    {
        public bool Seguir (Seguidor seguidor);
        public bool DesSeguir(Seguidor seguidor);
        public Seguidor GetSeguidor(int idseguidor, int idseguido);
    }
}
