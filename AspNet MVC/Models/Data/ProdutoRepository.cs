
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Models.Entidades;
using Models.Data;

namespace Models.Data
{
    public class ProdutoRepository : AbstractRepository<Produto>
    {
        private readonly DapperContext _context;

        public ProdutoRepository(DapperContext context)
        {
            _context = context;
        }

        public override void Salvar(Produto model)
        {
            // TODO: Implementar INSERT específico para a tabela Produto
            throw new System.NotImplementedException("Implemente o INSERT para Produto conforme o seu modelo.");
        }

        public override void Atualizar(Produto model)
        {
            // TODO: Implementar UPDATE específico para a tabela Produto
            throw new System.NotImplementedException("Implemente o UPDATE para Produto conforme o seu modelo.");
        }

        public override void Excluir(Produto model)
        {
            const string sql = "DELETE FROM Produto WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, model);
            }
        }

        public override Produto? Buscar(int id)
        {
            const string sql = "SELECT * FROM Produto WHERE Id = @id;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Produto>(sql, new { id }).FirstOrDefault();
            }
        }

        public override List<Produto> BuscarTodos()
        {
            const string sql = "SELECT * FROM Produto;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Produto>(sql).ToList();
            }
        }
    }
}
