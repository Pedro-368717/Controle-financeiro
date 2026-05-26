using ControleFinanceiro.Models;
namespace ControleFinanceiro.Services
{
    public interface IUsuarioService
    {
        void RegistrarUsuario( Usuario usuario, string senhaPura);
        Usuario FazerLogin(string email, string senhaPura);
        void AtualizarPerfeil(Usuario usuario);
    }
}