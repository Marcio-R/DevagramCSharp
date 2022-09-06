using DevagramCSharp.Data;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Implementation
{
    public class UsuarioRespositoryImp : IUsuarioRepository
    {
        private readonly DevagramCSharpContext _context;

        public UsuarioRespositoryImp(DevagramCSharpContext context)
        {
            _context = context;
        }

        public Usuario GetUsuarioPorId(int id)
        {
            return _context.Usuario.FirstOrDefault(u => u.Id == id);
        }

        public Usuario GetUsuarioPorLoginSenha(string email, string senha)
        {
            return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public void Salva(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public bool VerificarEmail(string email)
        {
            return _context.Usuario.Any(u => u.Email == email);
        }
    }
}
