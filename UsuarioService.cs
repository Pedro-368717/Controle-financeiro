using System;
using ControleFinanceiro.DAO;
namespace ControleFinanceiro.Models;

namespace ControleFinanceiro.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDA<Usuario> _usuarioDAO;
        public UsuarioService(IDA<Usuario> usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
        }
        public void RegistrarUsuario(Usuario usuario, string senhapura)
        {
            if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(senhapura))
                throw new ArgumentException("Email e senha são obrigatórios.");
            if(senhaPura.Length < 6)
                throw new Exception("A senha deve conter pelo menos 6 caracteres.");
            
            var todosUsuarios = _usuarioDAO.ListarTodos();
            foreach (var u in todosUsuarios)
            {
                if (u.Email.Equals(usuario.Email, StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Email já cadastrado.");
            }
            string senhaCriptografada = DCrypt.Net.BCrypt.HashPassword(senhaPura);
            usuario.Senha = senhaCriptografada;
            _usuarioDAO.Inserir(usuario);
        }
        public Usuario FazerLogin(string email, string senhaPura)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senhaPura))
                throw new Exception("Preencha todos os campos.");
            var todosUsuarios = _usuarioDAO.ListarTodos();
            Usuario usuarioEncontrado = null;

            foreach (var u in todosUsuarios)
            {
                if (u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    usuarioEncontrado = _usuarioDAO.BuscarPorId(u.IdUsuario);
                    break;
                }
            }
            if(usuarioEncontrado == null)
                throw new Exception("Email ou senha incorretos.");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senhaPura, usuarioEncontrado.Senha);
            if (!senhaValida)
                throw new Exception("Email ou senha incorretos.");

            usuarioEncontrado.Senha = string.Empty;
            return usuarioEncontrado;
        }
        public void AtualizarPerfil(Usuario usuario)
        {
            if(string.IsNullOrEmpty(usuario.Nome))
                throw new Exception("O nome não pode ficar em branco.");

                _usuarioDAO.Alterar(usuario);
        }
    }
}