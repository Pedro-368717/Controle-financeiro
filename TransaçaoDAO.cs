using MySql.Data.MySqlClient;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.DAO
{
    public class TransacaoDAO : IDAO<Transacao>
    {
        private string connectionString = "Server=localhost;Database=financeiro_db;Uid=root;Pwd=suasenha;";

        public void Inserir(Transacao entidade)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO Usuarios (nome, email, senha, foto_perfil_path) VALUES (@nome, @email, @senha, @foto)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", entidade.Nome);
                cmd.Parameters.AddWithValue("@email", entidade.Email);
                cmd.Parameters.AddWithValue("@senha", entidade.Senha); 
                cmd.Parameters.AddWithValue("@foto", entidade.FotoPerfilPath);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(Transacao entidade)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE Usuarios SET nome = @nome, email = @email, foto_perfil_path = @foto WHERE id_usuario = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", entidade.Nome);
                cmd.Parameters.AddWithValue("@email", entidade.Email);
                cmd.Parameters.AddWithValue("@foto", entidade.FotoPerfilPath);
                cmd.Parameters.AddWithValue("@id", entidade.IdUsuario);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        
        public Transacao Excluir(int id)
        {
            using(var conn = new MySqlConnection(connectionString))
            {
              string sql = "DELETE FROM Transacoes WHERE id_transacao = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id); 

                conn.Open();   
                cmd.ExecuteNonQuery();
            } 
        }

        public Transacao BuscarPorID( int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Transacoes WHERE id_transacao = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Transacao
                        {
                            IdTransacao = Convert.ToInt32[reader("id_transacao")],
                            IdUsuario = Convert.ToInt32[reader("id_usuario")],
                            IdCategoria = Convert.ToInt32[reader("id_categoria")],
                            Valor = Convert.ToDecimal[reader("valor")],
                            DataPagamento = Convert.ToDateTime[reader("data_pagamento")],
                            Descricao = Convert.ToString[reader("descricao")],
                            Status = Convert.ToString[reader("status")]
                        };
                    }
                }
            }
            return null;
        }

        public List<Transacao> ListaTodos()
        {
            var transacoes = new List<Transacao>();
            using (var conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Transacoes ORDER BY data_pagamento DESC";
                var cmd = new MySqlCommand(sql, conn);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new transacao
                        {
                            IdTransacao = Convert.ToInt32(reader["id_transacao"]),
                            IdConta = Convert.ToInt32(reader["id_conta"]),
                            IdCategoria = Convert.ToInt32(reader["id_categoria"]),
                            Valor = Convert.ToDecimal(reader["valor"]),
                            DataPagamento = Convert.ToDateTime(reader["data_pagamento"]),
                            Descricao = reader["descricao"].ToString(),
                            Status = reader["status"].ToString()
                        });
                    }
                }       
           }
           return lista;
        }
    }
}
                