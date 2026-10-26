using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallengeFiap.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class AjustaMapeamentoOracle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ANIMAL_T_CLYVO_TUTOR_id_tutor",
                table: "T_CLYVO_ANIMAL");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_CARTEIRAVACINAL_T_CLYVO_ANIMAL_id_animal",
                table: "T_CLYVO_CARTEIRAVACINAL");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_ANIMAL_id_animal",
                table: "T_CLYVO_CONSULTA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_VET_id_vet",
                table: "T_CLYVO_CONSULTA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ENDERECO_ANIMAL_T_CLYVO_ANIMAL_id_animal",
                table: "T_CLYVO_ENDERECO_ANIMAL");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ENDERECO_CLINICA_T_CLYVO_CLINICA_id_clinica",
                table: "T_CLYVO_ENDERECO_CLINICA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ENDERECO_TUTOR_T_CLYVO_TUTOR_id_tutor",
                table: "T_CLYVO_ENDERECO_TUTOR");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_MEDICAMENTO_T_CLYVO_PRESCRICAO_id_prescricao",
                table: "T_CLYVO_MEDICAMENTO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_PRESCRICAO_T_CLYVO_CONSULTA_id_consulta",
                table: "T_CLYVO_PRESCRICAO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_CLINICA_id_clinica",
                table: "T_CLYVO_VET_CLINICA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_VET_id_vet",
                table: "T_CLYVO_VET_CLINICA");

            migrationBuilder.RenameColumn(
                name: "id_vet",
                table: "T_CLYVO_VET_CLINICA",
                newName: "ID_VET");

            migrationBuilder.RenameColumn(
                name: "id_clinica",
                table: "T_CLYVO_VET_CLINICA",
                newName: "ID_CLINICA");

            migrationBuilder.RenameColumn(
                name: "id_clinica_vet",
                table: "T_CLYVO_VET_CLINICA",
                newName: "ID_CLINICA_VET");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_VET_CLINICA_id_vet",
                table: "T_CLYVO_VET_CLINICA",
                newName: "IX_T_CLYVO_VET_CLINICA_ID_VET");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_VET_CLINICA_id_clinica",
                table: "T_CLYVO_VET_CLINICA",
                newName: "IX_T_CLYVO_VET_CLINICA_ID_CLINICA");

            migrationBuilder.RenameColumn(
                name: "senha",
                table: "T_CLYVO_VET",
                newName: "SENHA");

            migrationBuilder.RenameColumn(
                name: "nm_vet",
                table: "T_CLYVO_VET",
                newName: "NM_VET");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "T_CLYVO_VET",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "crmv_vet",
                table: "T_CLYVO_VET",
                newName: "CRMV_VET");

            migrationBuilder.RenameColumn(
                name: "cpf_vet",
                table: "T_CLYVO_VET",
                newName: "CPF_VET");

            migrationBuilder.RenameColumn(
                name: "id_vet",
                table: "T_CLYVO_VET",
                newName: "ID_VET");

            migrationBuilder.RenameIndex(
                name: "crmv_vet",
                table: "T_CLYVO_VET",
                newName: "CRMV_VET");

            migrationBuilder.RenameIndex(
                name: "cpf_vet",
                table: "T_CLYVO_VET",
                newName: "CPF_VET");

            migrationBuilder.RenameColumn(
                name: "nr_telefone_tutor",
                table: "T_CLYVO_TUTOR",
                newName: "NR_TELEFONE_TUTOR");

            migrationBuilder.RenameColumn(
                name: "nm_tutor",
                table: "T_CLYVO_TUTOR",
                newName: "NM_TUTOR");

            migrationBuilder.RenameColumn(
                name: "cpf_tutor",
                table: "T_CLYVO_TUTOR",
                newName: "CPF_TUTOR");

            migrationBuilder.RenameColumn(
                name: "id_tutor",
                table: "T_CLYVO_TUTOR",
                newName: "ID_TUTOR");

            migrationBuilder.RenameIndex(
                name: "cpf_tutor",
                table: "T_CLYVO_TUTOR",
                newName: "CPF_TUTOR");

            migrationBuilder.RenameColumn(
                name: "observacoes_gerais",
                table: "T_CLYVO_PRESCRICAO",
                newName: "OBSERVACOES_GERAIS");

            migrationBuilder.RenameColumn(
                name: "id_consulta",
                table: "T_CLYVO_PRESCRICAO",
                newName: "ID_CONSULTA");

            migrationBuilder.RenameColumn(
                name: "dt_expiracao",
                table: "T_CLYVO_PRESCRICAO",
                newName: "DT_EXPIRACAO");

            migrationBuilder.RenameColumn(
                name: "dt_emissao",
                table: "T_CLYVO_PRESCRICAO",
                newName: "DT_EMISSAO");

            migrationBuilder.RenameColumn(
                name: "id_prescricao",
                table: "T_CLYVO_PRESCRICAO",
                newName: "ID_PRESCRICAO");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_PRESCRICAO_id_consulta",
                table: "T_CLYVO_PRESCRICAO",
                newName: "IX_T_CLYVO_PRESCRICAO_ID_CONSULTA");

            migrationBuilder.RenameColumn(
                name: "qtd_dias",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "QTD_DIAS");

            migrationBuilder.RenameColumn(
                name: "nm_medicamento",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "NM_MEDICAMENTO");

            migrationBuilder.RenameColumn(
                name: "id_prescricao",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "ID_PRESCRICAO");

            migrationBuilder.RenameColumn(
                name: "frequencia",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "FREQUENCIA");

            migrationBuilder.RenameColumn(
                name: "dosagem_medicamento",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "DOSAGEM_MEDICAMENTO");

            migrationBuilder.RenameColumn(
                name: "id_medicamento",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "ID_MEDICAMENTO");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_MEDICAMENTO_id_prescricao",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "IX_T_CLYVO_MEDICAMENTO_ID_PRESCRICAO");

            migrationBuilder.RenameColumn(
                name: "pais",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "PAIS");

            migrationBuilder.RenameColumn(
                name: "nr_rua",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "NR_RUA");

            migrationBuilder.RenameColumn(
                name: "logradouro_rua",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "LOGRADOURO_RUA");

            migrationBuilder.RenameColumn(
                name: "id_tutor",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "ID_TUTOR");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "ESTADO");

            migrationBuilder.RenameColumn(
                name: "completo",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "COMPLETO");

            migrationBuilder.RenameColumn(
                name: "cidade",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "CIDADE");

            migrationBuilder.RenameColumn(
                name: "cep",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "bairro",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "BAIRRO");

            migrationBuilder.RenameColumn(
                name: "id_endereco_tutor",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "ID_ENDERECO_TUTOR");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ENDERECO_TUTOR_id_tutor",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "IX_T_CLYVO_ENDERECO_TUTOR_ID_TUTOR");

            migrationBuilder.RenameColumn(
                name: "pais",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "PAIS");

            migrationBuilder.RenameColumn(
                name: "nr_rua",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "NR_RUA");

            migrationBuilder.RenameColumn(
                name: "logradouro_rua",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "LOGRADOURO_RUA");

            migrationBuilder.RenameColumn(
                name: "id_clinica",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "ID_CLINICA");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "ESTADO");

            migrationBuilder.RenameColumn(
                name: "completo",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "COMPLETO");

            migrationBuilder.RenameColumn(
                name: "cidade",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "CIDADE");

            migrationBuilder.RenameColumn(
                name: "cep",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "bairro",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "BAIRRO");

            migrationBuilder.RenameColumn(
                name: "id_endereco_clinica",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "ID_ENDERECO_CLINICA");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ENDERECO_CLINICA_id_clinica",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "IX_T_CLYVO_ENDERECO_CLINICA_ID_CLINICA");

            migrationBuilder.RenameColumn(
                name: "pais",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "PAIS");

            migrationBuilder.RenameColumn(
                name: "nr_rua",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "NR_RUA");

            migrationBuilder.RenameColumn(
                name: "logradouro_rua",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "LOGRADOURO_RUA");

            migrationBuilder.RenameColumn(
                name: "id_animal",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "ID_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "ESTADO");

            migrationBuilder.RenameColumn(
                name: "completo",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "COMPLETO");

            migrationBuilder.RenameColumn(
                name: "cidade",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "CIDADE");

            migrationBuilder.RenameColumn(
                name: "cep",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "bairro",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "BAIRRO");

            migrationBuilder.RenameColumn(
                name: "id_endereco_animal",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "ID_ENDERECO_ANIMAL");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ENDERECO_ANIMAL_id_animal",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "IX_T_CLYVO_ENDERECO_ANIMAL_ID_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "st_consulta",
                table: "T_CLYVO_CONSULTA",
                newName: "ST_CONSULTA");

            migrationBuilder.RenameColumn(
                name: "id_vet",
                table: "T_CLYVO_CONSULTA",
                newName: "ID_VET");

            migrationBuilder.RenameColumn(
                name: "id_animal",
                table: "T_CLYVO_CONSULTA",
                newName: "ID_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "historico_consulta",
                table: "T_CLYVO_CONSULTA",
                newName: "HISTORICO_CONSULTA");

            migrationBuilder.RenameColumn(
                name: "dt_consulta",
                table: "T_CLYVO_CONSULTA",
                newName: "DT_CONSULTA");

            migrationBuilder.RenameColumn(
                name: "id_consulta",
                table: "T_CLYVO_CONSULTA",
                newName: "ID_CONSULTA");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_CONSULTA_id_vet",
                table: "T_CLYVO_CONSULTA",
                newName: "IX_T_CLYVO_CONSULTA_ID_VET");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_CONSULTA_id_animal",
                table: "T_CLYVO_CONSULTA",
                newName: "IX_T_CLYVO_CONSULTA_ID_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "nm_clinica",
                table: "T_CLYVO_CLINICA",
                newName: "NM_CLINICA");

            migrationBuilder.RenameColumn(
                name: "cnpj_clinica",
                table: "T_CLYVO_CLINICA",
                newName: "CNPJ_CLINICA");

            migrationBuilder.RenameColumn(
                name: "id_clinica",
                table: "T_CLYVO_CLINICA",
                newName: "ID_CLINICA");

            migrationBuilder.RenameColumn(
                name: "st_vacinacao",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "ST_VACINACAO");

            migrationBuilder.RenameColumn(
                name: "nm_vacina",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "NM_VACINA");

            migrationBuilder.RenameColumn(
                name: "id_animal",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "ID_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "dt_vacinacao_prevista",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "DT_VACINACAO_PREVISTA");

            migrationBuilder.RenameColumn(
                name: "dt_vacinacao_efetuada",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "DT_VACINACAO_EFETUADA");

            migrationBuilder.RenameColumn(
                name: "id_carteiravacinal",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "ID_CARTEIRAVACINAL");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_CARTEIRAVACINAL_id_animal",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "IX_T_CLYVO_CARTEIRAVACINAL_ID_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "rg_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "RG_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "raca_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "RACA_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "peso_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "PESO_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "nr_microchip_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "NR_MICROCHIP_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "nm_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "NM_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "id_tutor",
                table: "T_CLYVO_ANIMAL",
                newName: "ID_TUTOR");

            migrationBuilder.RenameColumn(
                name: "especie_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "ESPECIE_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "dt_nascimento_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "DT_NASCIMENTO_ANIMAL");

            migrationBuilder.RenameColumn(
                name: "id_animal",
                table: "T_CLYVO_ANIMAL",
                newName: "ID_ANIMAL");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ANIMAL_id_tutor",
                table: "T_CLYVO_ANIMAL",
                newName: "IX_T_CLYVO_ANIMAL_ID_TUTOR");

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ANIMAL_T_CLYVO_TUTOR_ID_TUTOR",
                table: "T_CLYVO_ANIMAL",
                column: "ID_TUTOR",
                principalTable: "T_CLYVO_TUTOR",
                principalColumn: "ID_TUTOR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_CARTEIRAVACINAL_T_CLYVO_ANIMAL_ID_ANIMAL",
                table: "T_CLYVO_CARTEIRAVACINAL",
                column: "ID_ANIMAL",
                principalTable: "T_CLYVO_ANIMAL",
                principalColumn: "ID_ANIMAL",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_ANIMAL_ID_ANIMAL",
                table: "T_CLYVO_CONSULTA",
                column: "ID_ANIMAL",
                principalTable: "T_CLYVO_ANIMAL",
                principalColumn: "ID_ANIMAL",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_VET_ID_VET",
                table: "T_CLYVO_CONSULTA",
                column: "ID_VET",
                principalTable: "T_CLYVO_VET",
                principalColumn: "ID_VET",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ENDERECO_ANIMAL_T_CLYVO_ANIMAL_ID_ANIMAL",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                column: "ID_ANIMAL",
                principalTable: "T_CLYVO_ANIMAL",
                principalColumn: "ID_ANIMAL",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ENDERECO_CLINICA_T_CLYVO_CLINICA_ID_CLINICA",
                table: "T_CLYVO_ENDERECO_CLINICA",
                column: "ID_CLINICA",
                principalTable: "T_CLYVO_CLINICA",
                principalColumn: "ID_CLINICA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ENDERECO_TUTOR_T_CLYVO_TUTOR_ID_TUTOR",
                table: "T_CLYVO_ENDERECO_TUTOR",
                column: "ID_TUTOR",
                principalTable: "T_CLYVO_TUTOR",
                principalColumn: "ID_TUTOR",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_MEDICAMENTO_T_CLYVO_PRESCRICAO_ID_PRESCRICAO",
                table: "T_CLYVO_MEDICAMENTO",
                column: "ID_PRESCRICAO",
                principalTable: "T_CLYVO_PRESCRICAO",
                principalColumn: "ID_PRESCRICAO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_PRESCRICAO_T_CLYVO_CONSULTA_ID_CONSULTA",
                table: "T_CLYVO_PRESCRICAO",
                column: "ID_CONSULTA",
                principalTable: "T_CLYVO_CONSULTA",
                principalColumn: "ID_CONSULTA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_CLINICA_ID_CLINICA",
                table: "T_CLYVO_VET_CLINICA",
                column: "ID_CLINICA",
                principalTable: "T_CLYVO_CLINICA",
                principalColumn: "ID_CLINICA",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_VET_ID_VET",
                table: "T_CLYVO_VET_CLINICA",
                column: "ID_VET",
                principalTable: "T_CLYVO_VET",
                principalColumn: "ID_VET",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ANIMAL_T_CLYVO_TUTOR_ID_TUTOR",
                table: "T_CLYVO_ANIMAL");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_CARTEIRAVACINAL_T_CLYVO_ANIMAL_ID_ANIMAL",
                table: "T_CLYVO_CARTEIRAVACINAL");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_ANIMAL_ID_ANIMAL",
                table: "T_CLYVO_CONSULTA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_VET_ID_VET",
                table: "T_CLYVO_CONSULTA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ENDERECO_ANIMAL_T_CLYVO_ANIMAL_ID_ANIMAL",
                table: "T_CLYVO_ENDERECO_ANIMAL");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ENDERECO_CLINICA_T_CLYVO_CLINICA_ID_CLINICA",
                table: "T_CLYVO_ENDERECO_CLINICA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_ENDERECO_TUTOR_T_CLYVO_TUTOR_ID_TUTOR",
                table: "T_CLYVO_ENDERECO_TUTOR");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_MEDICAMENTO_T_CLYVO_PRESCRICAO_ID_PRESCRICAO",
                table: "T_CLYVO_MEDICAMENTO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_PRESCRICAO_T_CLYVO_CONSULTA_ID_CONSULTA",
                table: "T_CLYVO_PRESCRICAO");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_CLINICA_ID_CLINICA",
                table: "T_CLYVO_VET_CLINICA");

            migrationBuilder.DropForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_VET_ID_VET",
                table: "T_CLYVO_VET_CLINICA");

            migrationBuilder.RenameColumn(
                name: "ID_VET",
                table: "T_CLYVO_VET_CLINICA",
                newName: "id_vet");

            migrationBuilder.RenameColumn(
                name: "ID_CLINICA",
                table: "T_CLYVO_VET_CLINICA",
                newName: "id_clinica");

            migrationBuilder.RenameColumn(
                name: "ID_CLINICA_VET",
                table: "T_CLYVO_VET_CLINICA",
                newName: "id_clinica_vet");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_VET_CLINICA_ID_VET",
                table: "T_CLYVO_VET_CLINICA",
                newName: "IX_T_CLYVO_VET_CLINICA_id_vet");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_VET_CLINICA_ID_CLINICA",
                table: "T_CLYVO_VET_CLINICA",
                newName: "IX_T_CLYVO_VET_CLINICA_id_clinica");

            migrationBuilder.RenameColumn(
                name: "SENHA",
                table: "T_CLYVO_VET",
                newName: "senha");

            migrationBuilder.RenameColumn(
                name: "NM_VET",
                table: "T_CLYVO_VET",
                newName: "nm_vet");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "T_CLYVO_VET",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CRMV_VET",
                table: "T_CLYVO_VET",
                newName: "crmv_vet");

            migrationBuilder.RenameColumn(
                name: "CPF_VET",
                table: "T_CLYVO_VET",
                newName: "cpf_vet");

            migrationBuilder.RenameColumn(
                name: "ID_VET",
                table: "T_CLYVO_VET",
                newName: "id_vet");

            migrationBuilder.RenameIndex(
                name: "CRMV_VET",
                table: "T_CLYVO_VET",
                newName: "crmv_vet");

            migrationBuilder.RenameIndex(
                name: "CPF_VET",
                table: "T_CLYVO_VET",
                newName: "cpf_vet");

            migrationBuilder.RenameColumn(
                name: "NR_TELEFONE_TUTOR",
                table: "T_CLYVO_TUTOR",
                newName: "nr_telefone_tutor");

            migrationBuilder.RenameColumn(
                name: "NM_TUTOR",
                table: "T_CLYVO_TUTOR",
                newName: "nm_tutor");

            migrationBuilder.RenameColumn(
                name: "CPF_TUTOR",
                table: "T_CLYVO_TUTOR",
                newName: "cpf_tutor");

            migrationBuilder.RenameColumn(
                name: "ID_TUTOR",
                table: "T_CLYVO_TUTOR",
                newName: "id_tutor");

            migrationBuilder.RenameIndex(
                name: "CPF_TUTOR",
                table: "T_CLYVO_TUTOR",
                newName: "cpf_tutor");

            migrationBuilder.RenameColumn(
                name: "OBSERVACOES_GERAIS",
                table: "T_CLYVO_PRESCRICAO",
                newName: "observacoes_gerais");

            migrationBuilder.RenameColumn(
                name: "ID_CONSULTA",
                table: "T_CLYVO_PRESCRICAO",
                newName: "id_consulta");

            migrationBuilder.RenameColumn(
                name: "DT_EXPIRACAO",
                table: "T_CLYVO_PRESCRICAO",
                newName: "dt_expiracao");

            migrationBuilder.RenameColumn(
                name: "DT_EMISSAO",
                table: "T_CLYVO_PRESCRICAO",
                newName: "dt_emissao");

            migrationBuilder.RenameColumn(
                name: "ID_PRESCRICAO",
                table: "T_CLYVO_PRESCRICAO",
                newName: "id_prescricao");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_PRESCRICAO_ID_CONSULTA",
                table: "T_CLYVO_PRESCRICAO",
                newName: "IX_T_CLYVO_PRESCRICAO_id_consulta");

            migrationBuilder.RenameColumn(
                name: "QTD_DIAS",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "qtd_dias");

            migrationBuilder.RenameColumn(
                name: "NM_MEDICAMENTO",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "nm_medicamento");

            migrationBuilder.RenameColumn(
                name: "ID_PRESCRICAO",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "id_prescricao");

            migrationBuilder.RenameColumn(
                name: "FREQUENCIA",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "frequencia");

            migrationBuilder.RenameColumn(
                name: "DOSAGEM_MEDICAMENTO",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "dosagem_medicamento");

            migrationBuilder.RenameColumn(
                name: "ID_MEDICAMENTO",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "id_medicamento");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_MEDICAMENTO_ID_PRESCRICAO",
                table: "T_CLYVO_MEDICAMENTO",
                newName: "IX_T_CLYVO_MEDICAMENTO_id_prescricao");

            migrationBuilder.RenameColumn(
                name: "PAIS",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "pais");

            migrationBuilder.RenameColumn(
                name: "NR_RUA",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "nr_rua");

            migrationBuilder.RenameColumn(
                name: "LOGRADOURO_RUA",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "logradouro_rua");

            migrationBuilder.RenameColumn(
                name: "ID_TUTOR",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "id_tutor");

            migrationBuilder.RenameColumn(
                name: "ESTADO",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "COMPLETO",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "completo");

            migrationBuilder.RenameColumn(
                name: "CIDADE",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "cidade");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "cep");

            migrationBuilder.RenameColumn(
                name: "BAIRRO",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "bairro");

            migrationBuilder.RenameColumn(
                name: "ID_ENDERECO_TUTOR",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "id_endereco_tutor");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ENDERECO_TUTOR_ID_TUTOR",
                table: "T_CLYVO_ENDERECO_TUTOR",
                newName: "IX_T_CLYVO_ENDERECO_TUTOR_id_tutor");

            migrationBuilder.RenameColumn(
                name: "PAIS",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "pais");

            migrationBuilder.RenameColumn(
                name: "NR_RUA",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "nr_rua");

            migrationBuilder.RenameColumn(
                name: "LOGRADOURO_RUA",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "logradouro_rua");

            migrationBuilder.RenameColumn(
                name: "ID_CLINICA",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "id_clinica");

            migrationBuilder.RenameColumn(
                name: "ESTADO",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "COMPLETO",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "completo");

            migrationBuilder.RenameColumn(
                name: "CIDADE",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "cidade");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "cep");

            migrationBuilder.RenameColumn(
                name: "BAIRRO",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "bairro");

            migrationBuilder.RenameColumn(
                name: "ID_ENDERECO_CLINICA",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "id_endereco_clinica");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ENDERECO_CLINICA_ID_CLINICA",
                table: "T_CLYVO_ENDERECO_CLINICA",
                newName: "IX_T_CLYVO_ENDERECO_CLINICA_id_clinica");

            migrationBuilder.RenameColumn(
                name: "PAIS",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "pais");

            migrationBuilder.RenameColumn(
                name: "NR_RUA",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "nr_rua");

            migrationBuilder.RenameColumn(
                name: "LOGRADOURO_RUA",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "logradouro_rua");

            migrationBuilder.RenameColumn(
                name: "ID_ANIMAL",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "id_animal");

            migrationBuilder.RenameColumn(
                name: "ESTADO",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "COMPLETO",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "completo");

            migrationBuilder.RenameColumn(
                name: "CIDADE",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "cidade");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "cep");

            migrationBuilder.RenameColumn(
                name: "BAIRRO",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "bairro");

            migrationBuilder.RenameColumn(
                name: "ID_ENDERECO_ANIMAL",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "id_endereco_animal");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ENDERECO_ANIMAL_ID_ANIMAL",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                newName: "IX_T_CLYVO_ENDERECO_ANIMAL_id_animal");

            migrationBuilder.RenameColumn(
                name: "ST_CONSULTA",
                table: "T_CLYVO_CONSULTA",
                newName: "st_consulta");

            migrationBuilder.RenameColumn(
                name: "ID_VET",
                table: "T_CLYVO_CONSULTA",
                newName: "id_vet");

            migrationBuilder.RenameColumn(
                name: "ID_ANIMAL",
                table: "T_CLYVO_CONSULTA",
                newName: "id_animal");

            migrationBuilder.RenameColumn(
                name: "HISTORICO_CONSULTA",
                table: "T_CLYVO_CONSULTA",
                newName: "historico_consulta");

            migrationBuilder.RenameColumn(
                name: "DT_CONSULTA",
                table: "T_CLYVO_CONSULTA",
                newName: "dt_consulta");

            migrationBuilder.RenameColumn(
                name: "ID_CONSULTA",
                table: "T_CLYVO_CONSULTA",
                newName: "id_consulta");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_CONSULTA_ID_VET",
                table: "T_CLYVO_CONSULTA",
                newName: "IX_T_CLYVO_CONSULTA_id_vet");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_CONSULTA_ID_ANIMAL",
                table: "T_CLYVO_CONSULTA",
                newName: "IX_T_CLYVO_CONSULTA_id_animal");

            migrationBuilder.RenameColumn(
                name: "NM_CLINICA",
                table: "T_CLYVO_CLINICA",
                newName: "nm_clinica");

            migrationBuilder.RenameColumn(
                name: "CNPJ_CLINICA",
                table: "T_CLYVO_CLINICA",
                newName: "cnpj_clinica");

            migrationBuilder.RenameColumn(
                name: "ID_CLINICA",
                table: "T_CLYVO_CLINICA",
                newName: "id_clinica");

            migrationBuilder.RenameColumn(
                name: "ST_VACINACAO",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "st_vacinacao");

            migrationBuilder.RenameColumn(
                name: "NM_VACINA",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "nm_vacina");

            migrationBuilder.RenameColumn(
                name: "ID_ANIMAL",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "id_animal");

            migrationBuilder.RenameColumn(
                name: "DT_VACINACAO_PREVISTA",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "dt_vacinacao_prevista");

            migrationBuilder.RenameColumn(
                name: "DT_VACINACAO_EFETUADA",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "dt_vacinacao_efetuada");

            migrationBuilder.RenameColumn(
                name: "ID_CARTEIRAVACINAL",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "id_carteiravacinal");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_CARTEIRAVACINAL_ID_ANIMAL",
                table: "T_CLYVO_CARTEIRAVACINAL",
                newName: "IX_T_CLYVO_CARTEIRAVACINAL_id_animal");

            migrationBuilder.RenameColumn(
                name: "RG_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "rg_animal");

            migrationBuilder.RenameColumn(
                name: "RACA_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "raca_animal");

            migrationBuilder.RenameColumn(
                name: "PESO_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "peso_animal");

            migrationBuilder.RenameColumn(
                name: "NR_MICROCHIP_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "nr_microchip_animal");

            migrationBuilder.RenameColumn(
                name: "NM_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "nm_animal");

            migrationBuilder.RenameColumn(
                name: "ID_TUTOR",
                table: "T_CLYVO_ANIMAL",
                newName: "id_tutor");

            migrationBuilder.RenameColumn(
                name: "ESPECIE_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "especie_animal");

            migrationBuilder.RenameColumn(
                name: "DT_NASCIMENTO_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "dt_nascimento_animal");

            migrationBuilder.RenameColumn(
                name: "ID_ANIMAL",
                table: "T_CLYVO_ANIMAL",
                newName: "id_animal");

            migrationBuilder.RenameIndex(
                name: "IX_T_CLYVO_ANIMAL_ID_TUTOR",
                table: "T_CLYVO_ANIMAL",
                newName: "IX_T_CLYVO_ANIMAL_id_tutor");

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ANIMAL_T_CLYVO_TUTOR_id_tutor",
                table: "T_CLYVO_ANIMAL",
                column: "id_tutor",
                principalTable: "T_CLYVO_TUTOR",
                principalColumn: "id_tutor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_CARTEIRAVACINAL_T_CLYVO_ANIMAL_id_animal",
                table: "T_CLYVO_CARTEIRAVACINAL",
                column: "id_animal",
                principalTable: "T_CLYVO_ANIMAL",
                principalColumn: "id_animal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_ANIMAL_id_animal",
                table: "T_CLYVO_CONSULTA",
                column: "id_animal",
                principalTable: "T_CLYVO_ANIMAL",
                principalColumn: "id_animal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_CONSULTA_T_CLYVO_VET_id_vet",
                table: "T_CLYVO_CONSULTA",
                column: "id_vet",
                principalTable: "T_CLYVO_VET",
                principalColumn: "id_vet",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ENDERECO_ANIMAL_T_CLYVO_ANIMAL_id_animal",
                table: "T_CLYVO_ENDERECO_ANIMAL",
                column: "id_animal",
                principalTable: "T_CLYVO_ANIMAL",
                principalColumn: "id_animal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ENDERECO_CLINICA_T_CLYVO_CLINICA_id_clinica",
                table: "T_CLYVO_ENDERECO_CLINICA",
                column: "id_clinica",
                principalTable: "T_CLYVO_CLINICA",
                principalColumn: "id_clinica",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_ENDERECO_TUTOR_T_CLYVO_TUTOR_id_tutor",
                table: "T_CLYVO_ENDERECO_TUTOR",
                column: "id_tutor",
                principalTable: "T_CLYVO_TUTOR",
                principalColumn: "id_tutor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_MEDICAMENTO_T_CLYVO_PRESCRICAO_id_prescricao",
                table: "T_CLYVO_MEDICAMENTO",
                column: "id_prescricao",
                principalTable: "T_CLYVO_PRESCRICAO",
                principalColumn: "id_prescricao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_PRESCRICAO_T_CLYVO_CONSULTA_id_consulta",
                table: "T_CLYVO_PRESCRICAO",
                column: "id_consulta",
                principalTable: "T_CLYVO_CONSULTA",
                principalColumn: "id_consulta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_CLINICA_id_clinica",
                table: "T_CLYVO_VET_CLINICA",
                column: "id_clinica",
                principalTable: "T_CLYVO_CLINICA",
                principalColumn: "id_clinica",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CLYVO_VET_CLINICA_T_CLYVO_VET_id_vet",
                table: "T_CLYVO_VET_CLINICA",
                column: "id_vet",
                principalTable: "T_CLYVO_VET",
                principalColumn: "id_vet",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
