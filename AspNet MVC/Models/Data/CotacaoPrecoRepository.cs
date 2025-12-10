
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Models.Entidades;
using Models.Data;

namespace Models.Data
{
    public class CotacaoPrecoRepository : AbstractRepository<CotacaoPreco>
    {
        private readonly DapperContext _context;

        public CotacaoPrecoRepository(DapperContext context)
        {
            _context = context;
        }

        public override void Salvar(CotacaoPreco model)
        {
            // TODO: Implementar INSERT específico para a tabela CotacaoPreco
            throw new System.NotImplementedException("Implemente o INSERT para CotacaoPreco conforme o seu modelo.");
        }

        public override void Atualizar(CotacaoPreco model)
        {
            // TODO: Implementar UPDATE específico para a tabela CotacaoPreco
            throw new System.NotImplementedException("Implemente o UPDATE para CotacaoPreco conforme o seu modelo.");
        }

        public override void Excluir(CotacaoPreco model)
        {
            const string sql = "DELETE FROM CotacaoPreco WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, model);
            }
        }

        public override CotacaoPreco? Buscar(int id)
        {
            const string sql = "SELECT * FROM CotacaoPreco WHERE Id = @id;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<CotacaoPreco>(sql, new { id }).FirstOrDefault();
            }
        }

        public override List<CotacaoPreco> BuscarTodos()
        {
            const string sql = "SELECT * FROM CotacaoPreco;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<CotacaoPreco>(sql).ToList();
            }
        }
    }
}
