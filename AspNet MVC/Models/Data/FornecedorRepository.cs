
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Models.Entidades;
using Models.Data;

namespace Models.Data
{
    public class FornecedorRepository : AbstractRepository<Fornecedor>
    {
        private readonly DapperContext _context;

        public FornecedorRepository(DapperContext context)
        {
            _context = context;
        }

        public override void Salvar(Fornecedor model)
        {
            // TODO: Implementar INSERT específico para a tabela Fornecedor
            throw new System.NotImplementedException("Implemente o INSERT para Fornecedor conforme o seu modelo.");
        }

        public override void Atualizar(Fornecedor model)
        {
            // TODO: Implementar UPDATE específico para a tabela Fornecedor
            throw new System.NotImplementedException("Implemente o UPDATE para Fornecedor conforme o seu modelo.");
        }

        public override void Excluir(Fornecedor model)
        {
            const string sql = "DELETE FROM Fornecedor WHERE Id = @Id;";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(sql, model);
            }
        }

        public override Fornecedor? Buscar(int id)
        {
            const string sql = "SELECT * FROM Fornecedor WHERE Id = @id;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Fornecedor>(sql, new { id }).FirstOrDefault();
            }
        }

        public override List<Fornecedor> BuscarTodos()
        {
            const string sql = "SELECT * FROM Fornecedor;";
            using (var connection = _context.CreateConnection())
            {
                return connection.Query<Fornecedor>(sql).ToList();
            }
        }
    }
}
