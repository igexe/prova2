
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Models.Entidades;
using Models.Data;

namespace Models.Data
{
    public class CotacaoRepository : AbstractRepository<Cotacao>
    {
        private readonly DapperContext _context;

        public CotacaoRepository(DapperContext context)
        {
            _context = context;
        }

        public override void Salvar(Cotacao model)
        {
            // TODO: Implementar INSERT específico para a tabela Cotacao
            throw new System.NotImplementedException("Implemente o INSERT para Cotacao conforme o seu modelo.");
        }

        public override void Atualizar(Cotacao model)
        {
            // TODO: Implementar UPDATE específico para a tabela Cotacao
            throw new System.NotImplementedException("Implemente o UPDATE para Cotacao conforme o seu modelo.");
        }

        public override void Excluir(Cotacao model)
        {
            const string sql = "DELETE FROM Cotacao WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, model);
            }
        }

        public override Cotacao? Buscar(int id)
        {
            const string sql = "SELECT * FROM Cotacao WHERE Id = @id;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Cotacao>(sql, new { id }).FirstOrDefault();
            }
        }

        public override List<Cotacao> BuscarTodos()
        {
            const string sql = "SELECT * FROM Cotacao;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Cotacao>(sql).ToList();
            }
        }
    }
}
