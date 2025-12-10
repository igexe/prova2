
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Models.Entidades;
using Models.Data;

namespace Models.Data
{
    public class CotacaoParticipanteRepository : AbstractRepository<CotacaoParticipante>
    {
        private readonly DapperContext _context;

        public CotacaoParticipanteRepository(DapperContext context)
        {
            _context = context;
        }

        public override void Salvar(CotacaoParticipante model)
        {
            // TODO: Implementar INSERT específico para a tabela CotacaoParticipante
            throw new System.NotImplementedException("Implemente o INSERT para CotacaoParticipante conforme o seu modelo.");
        }

        public override void Atualizar(CotacaoParticipante model)
        {
            // TODO: Implementar UPDATE específico para a tabela CotacaoParticipante
            throw new System.NotImplementedException("Implemente o UPDATE para CotacaoParticipante conforme o seu modelo.");
        }

        public override void Excluir(CotacaoParticipante model)
        {
            const string sql = "DELETE FROM CotacaoParticipante WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, model);
            }
        }

        public override CotacaoParticipante? Buscar(int id)
        {
            const string sql = "SELECT * FROM CotacaoParticipante WHERE Id = @id;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<CotacaoParticipante>(sql, new { id }).FirstOrDefault();
            }
        }

        public override List<CotacaoParticipante> BuscarTodos()
        {
            const string sql = "SELECT * FROM CotacaoParticipante;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<CotacaoParticipante>(sql).ToList();
            }
        }
    }
}
