using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface IUsuarioRepository
    {
        public void Salva(Usuario usuario);
        public bool VerificarEmail(string email);

    }
}
