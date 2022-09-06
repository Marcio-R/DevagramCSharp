using DevagramCSharp.Models;

namespace DevagramCSharp.Repository
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioPorId(int v);
        Usuario GetUsuarioPorLoginSenha(string email, string senha);
        public void Salva(Usuario usuario);
        public bool VerificarEmail(string email);

    }
}
