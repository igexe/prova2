
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Models.Entidades;
using Models.Data;

namespace Models.Data
{
    public class UsuarioRepository : AbstractRepository<Usuario>
    {
        private readonly DapperContext _context;

        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        public override void Salvar(Usuario model)
        {
            // TODO: Implementar INSERT específico para a tabela Usuario
            throw new System.NotImplementedException("Implemente o INSERT para Usuario conforme o seu modelo.");
        }

        public override void Atualizar(Usuario model)
        {
            // TODO: Implementar UPDATE específico para a tabela Usuario
            throw new System.NotImplementedException("Implemente o UPDATE para Usuario conforme o seu modelo.");
        }

        public override void Excluir(Usuario model)
        {
            const string sql = "DELETE FROM Usuario WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, model);
            }
        }

        public override Usuario? Buscar(int id)
        {
            const string sql = "SELECT * FROM Usuario WHERE Id = @id;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Usuario>(sql, new { id }).FirstOrDefault();
            }
        }

        public override List<Usuario> BuscarTodos()
        {
            const string sql = "SELECT * FROM Usuario;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Usuario>(sql).ToList();
            }
        }
    }
}
