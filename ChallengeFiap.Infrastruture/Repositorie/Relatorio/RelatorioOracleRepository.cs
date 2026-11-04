
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using challengeFiap.Domain.Interfaces.IRelatorio;

namespace ChallengeFiap.Infrastruture.Repositorie.Relatorio
{
    public class RelatorioOracleRepository : IRelatorioRepository
    {
        private readonly IConfiguration _configuration;

        //parte de conecao do oracle com sua procedures, para gerar os relatorios solicitados no desafio

        public RelatorioOracleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<List<string>> ProcurarVetConsultasAsync(string nomeVet)
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.PROCURAR_VET_CONSULTAS(:p_nome); END;",
                cmd => cmd.Parameters.Add(
                    "p_nome", OracleDbType.Varchar2).Value = nomeVet
            );
        }

        public Task<List<string>> ProcurarInformacaoAnimalAsync(string nomeAnimal)
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.PROCURAR_INFORMACAO_ANIMAL(:p_nome); END;",
                cmd => cmd.Parameters.Add(
                    "p_nome", OracleDbType.Varchar2).Value = nomeAnimal
            );
        }

        public Task<List<string>> ResumoConsultasVetAsync()
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.RESUMO_CONSULTAS_VET; END;"
            );
        }

        public Task<List<string>> RelatorioEspecieAsync()
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.RELATORIO_ESPECIE; END;"
            );
        }

        public Task<List<string>> ConsultasClinicasAsync()
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.CONSULTAS_CLINICAS; END;"
            );
        }

        public Task<List<string>> ConsultasRealizadasAsync()
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.CONSULTAS_REALIZADAS; END;"
            );
        }

        public Task<List<string>> VerificarMoradiaAnimalTutorAsync()
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.VERIFICAR_MORADIA_ANIMAL_TUTOR; END;"
            );
        }

        public Task<List<string>> RelatorioOcorrenciasAsync()
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.RELATORIO_OCORRENCIAS; END;"
            );
        }

        public Task<List<string>> RelatorioVetConsultaAsync(int idVet)
        {
            return ExecutarRelatorioAsync(
                "BEGIN PKG_CYLVO_RELATORIO.PRO_RELATORIO_MEDICAMENTO_ANIMAL(:p_id_vet); END;",
                cmd => cmd.Parameters.Add(
                    "p_id_vet", OracleDbType.Int32).Value = idVet
            );
        }

        private async Task<List<string>> ExecutarRelatorioAsync(string comandoSql, Action<OracleCommand>? configurarParametros = null)
        {
            var connectionString = _configuration.GetConnectionString("OracleConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connecao não encontrada.");
            }
                

            var resultado = new List<string>();

            await using var connection = new OracleConnection(connectionString);

            await connection.OpenAsync();

            await using (var enableCommand = connection.CreateCommand())
            {
                enableCommand.CommandText =
                    "BEGIN DBMS_OUTPUT.ENABLE(NULL); END;";

                await enableCommand.ExecuteNonQueryAsync();
            }

            await using (var command = connection.CreateCommand())
            {
                command.BindByName = true;
                command.CommandText = comandoSql;

                configurarParametros?.Invoke(command);

                await command.ExecuteNonQueryAsync();
            }

            await using (var outputCommand = connection.CreateCommand())
            {
                outputCommand.BindByName = true;
                outputCommand.CommandText = "BEGIN DBMS_OUTPUT.GET_LINE(:p_linha, :p_status); END;";

                var linha = new OracleParameter("p_linha", OracleDbType.Varchar2, 32767)
                {
                    Direction = ParameterDirection.Output
                };

                var status = new OracleParameter("p_status", OracleDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };

                outputCommand.Parameters.Add(linha);
                outputCommand.Parameters.Add(status);

                while (true)
                {
                    await outputCommand.ExecuteNonQueryAsync();

                    var codigoStatus = Convert.ToInt32(status.Value.ToString());

                    if (codigoStatus != 0)
                    {
                        break;
                    }

                    resultado.Add(linha.Value?.ToString() ?? "");
                }
            }

            return resultado;
        }
    }
}
