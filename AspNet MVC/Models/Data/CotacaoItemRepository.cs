
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Models.Entidades;
using Models.Data;

namespace Models.Data
{
    public class CotacaoItemRepository : AbstractRepository<CotacaoItem>
    {
        private readonly DapperContext _context;

        public CotacaoItemRepository(DapperContext context)
        {
            _context = context;
        }

        public override void Salvar(CotacaoItem model)
        {
            // TODO: Implementar INSERT específico para a tabela CotacaoItem
            throw new System.NotImplementedException("Implemente o INSERT para CotacaoItem conforme o seu modelo.");
        }

        public override void Atualizar(CotacaoItem model)
        {
            // TODO: Implementar UPDATE específico para a tabela CotacaoItem
            throw new System.NotImplementedException("Implemente o UPDATE para CotacaoItem conforme o seu modelo.");
        }

        public override void Excluir(CotacaoItem model)
        {
            const string sql = "DELETE FROM CotacaoItem WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, model);
            }
        }

        public override CotacaoItem? Buscar(int id)
        {
            const string sql = "SELECT * FROM CotacaoItem WHERE Id = @id;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<CotacaoItem>(sql, new { id }).FirstOrDefault();
            }
        }

        public override List<CotacaoItem> BuscarTodos()
        {
            const string sql = "SELECT * FROM CotacaoItem;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<CotacaoItem>(sql).ToList();
            }
        }
    }
}
