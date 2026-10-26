using challengeFiap.Domain;
using challengeFiap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace challengeFiap.Infrastruture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<EnderecoClinica> EnderecoClinicas { get; set; }
        public DbSet<VetClinica> VetClinicas { get; set; }
        public DbSet<Tutor> Tutor { get; set; }
        public DbSet<EnderecoTutor> EnderecoTutors { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<EnderecoAnimal> EnderecoAnimals { get; set; }
        public DbSet<CarteiraVacinal> CarteiraVacinals { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Prescricao> Prescricaos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veterinario>().ToTable("T_CLYVO_VET");
            modelBuilder.Entity<Veterinario>().HasKey(g => g.Id_vet);
            modelBuilder.Entity<Veterinario>().Property(g => g.Id_vet).HasColumnName("ID_VET").IsRequired();
            modelBuilder.Entity<Veterinario>().Property(g => g.Nm_vet).HasColumnName("NM_VET").IsRequired();
            modelBuilder.Entity<Veterinario>().Property(g => g.Cpf_vet).HasColumnName("CPF_VET").IsRequired();
            modelBuilder.Entity<Veterinario>().HasIndex(g => g.Cpf_vet).HasDatabaseName("CPF_VET").IsUnique();
            modelBuilder.Entity<Veterinario>().Property(g => g.Crmv_vet).HasColumnName("CRMV_VET").IsRequired();
            modelBuilder.Entity<Veterinario>().HasIndex(g => g.Crmv_vet).HasDatabaseName("CRMV_VET").IsUnique();
            modelBuilder.Entity<Veterinario>().Property(g => g.Email_vet).HasColumnName("EMAIL").IsRequired();
            modelBuilder.Entity<Veterinario>().Property(g => g.Senha_vet).HasColumnName("SENHA").IsRequired();

            modelBuilder.Entity<Clinica>().ToTable("T_CLYVO_CLINICA");
            modelBuilder.Entity<Clinica>().HasKey(g => g.Id_clinica);
            modelBuilder.Entity<Clinica>().Property(g => g.Id_clinica).HasColumnName("ID_CLINICA").IsRequired();
            modelBuilder.Entity<Clinica>().Property(g => g.Cnpj_clinica).HasColumnName("CNPJ_CLINICA").IsRequired();
            modelBuilder.Entity<Clinica>().Property(g => g.Nm_clinica).HasColumnName("NM_CLINICA").IsRequired();

            modelBuilder.Entity<EnderecoClinica>().ToTable("T_CLYVO_ENDERECO_CLINICA");
            modelBuilder.Entity<EnderecoClinica>().HasKey(g => g.Id_endereco_clinica);
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Id_endereco_clinica).HasColumnName("ID_ENDERECO_CLINICA").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Pais).HasColumnName("PAIS").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Estado).HasColumnName("ESTADO").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Cidade).HasColumnName("CIDADE").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Bairro).HasColumnName("BAIRRO").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Logradouro_rua).HasColumnName("LOGRADOURO_RUA").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Nr_rua).HasColumnName("NR_RUA").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Complemento).HasColumnName("COMPLETO").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Cep).HasColumnName("CEP").IsRequired();
            modelBuilder.Entity<EnderecoClinica>().Property(g => g.Id_clinica).HasColumnName("ID_CLINICA");
            modelBuilder.Entity<EnderecoClinica>().HasOne(g => g.Clinica).WithOne().HasForeignKey<EnderecoClinica>(g => g.Id_clinica);

            modelBuilder.Entity<VetClinica>().ToTable("T_CLYVO_VET_CLINICA");
            modelBuilder.Entity<VetClinica>().HasKey(g => g.Id_clinica_vet);
            modelBuilder.Entity<VetClinica>().Property(g => g.Id_clinica_vet).HasColumnName("ID_CLINICA_VET").IsRequired();
            modelBuilder.Entity<VetClinica>().Property(g => g.Id_vet).HasColumnName("ID_VET").IsRequired();
            modelBuilder.Entity<VetClinica>().Property(g => g.Id_clinica).HasColumnName("ID_CLINICA").IsRequired();
            modelBuilder.Entity<VetClinica>().HasOne(g => g.Veterinario).WithMany().HasForeignKey(g => g.Id_vet).IsRequired();
            modelBuilder.Entity<VetClinica>().HasOne(g => g.Clinica).WithMany().HasForeignKey(g => g.Id_clinica).IsRequired();

            modelBuilder.Entity<Tutor>().ToTable("T_CLYVO_TUTOR");
            modelBuilder.Entity<Tutor>().HasKey(g => g.Id_tutor);
            modelBuilder.Entity<Tutor>().Property(g => g.Id_tutor).HasColumnName("ID_TUTOR").IsRequired();
            modelBuilder.Entity<Tutor>().Property(g => g.Cpf_tutor).HasColumnName("CPF_TUTOR").IsRequired();
            modelBuilder.Entity<Tutor>().HasIndex(g => g.Cpf_tutor).HasDatabaseName("CPF_TUTOR").IsUnique();
            modelBuilder.Entity<Tutor>().Property(g => g.Nm_tutor).HasColumnName("NM_TUTOR").IsRequired();
            modelBuilder.Entity<Tutor>().Property(g => g.Nr_telefone_tutor).HasColumnName("NR_TELEFONE_TUTOR").IsRequired();

            modelBuilder.Entity<EnderecoTutor>().ToTable("T_CLYVO_ENDERECO_TUTOR");
            modelBuilder.Entity<EnderecoTutor>().HasKey(g => g.Id_endereco_tutor);
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Id_endereco_tutor).HasColumnName("ID_ENDERECO_TUTOR").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Pais).HasColumnName("PAIS").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Estado).HasColumnName("ESTADO").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Cidade).HasColumnName("CIDADE").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Bairro).HasColumnName("BAIRRO").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Logradouro_rua).HasColumnName("LOGRADOURO_RUA").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Nr_rua).HasColumnName("NR_RUA").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Complemento).HasColumnName("COMPLETO").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Cep).HasColumnName("CEP").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().Property(g => g.Id_tutor).HasColumnName("ID_TUTOR").IsRequired();
            modelBuilder.Entity<EnderecoTutor>().HasOne(g => g.Tutor).WithOne().HasForeignKey<EnderecoTutor>(g => g.Id_tutor).IsRequired();

            modelBuilder.Entity<Animal>().ToTable("T_CLYVO_ANIMAL");
            modelBuilder.Entity<Animal>().HasKey(g => g.Id_animal);
            modelBuilder.Entity<Animal>().Property(g => g.Id_animal).HasColumnName("ID_ANIMAL").IsRequired();
            modelBuilder.Entity<Animal>().Property(g => g.Rg_animal).HasColumnName("RG_ANIMAL");
            modelBuilder.Entity<Animal>().Property(g => g.Nr_microchip_animal).HasColumnName("NR_MICROCHIP_ANIMAL");
            modelBuilder.Entity<Animal>().Property(g => g.Nm_animal).HasColumnName("NM_ANIMAL").IsRequired();
            modelBuilder.Entity<Animal>().Property(g => g.Dt_nascimento_animal).HasColumnName("DT_NASCIMENTO_ANIMAL").IsRequired();
            modelBuilder.Entity<Animal>().Property(g => g.Peso_animal).HasPrecision(5, 2).HasColumnName("PESO_ANIMAL").IsRequired();
            modelBuilder.Entity<Animal>().Property(g => g.Especie_animal).HasColumnName("ESPECIE_ANIMAL").IsRequired();
            modelBuilder.Entity<Animal>().Property(g => g.Raca_animal).HasColumnName("RACA_ANIMAL").IsRequired();
            modelBuilder.Entity<Animal>().Property(g => g.Id_tutor).HasColumnName("ID_TUTOR").IsRequired();
            modelBuilder.Entity<Animal>().HasOne(g => g.Tutor).WithMany().HasForeignKey(g => g.Id_tutor).IsRequired();

            modelBuilder.Entity<EnderecoAnimal>().ToTable("T_CLYVO_ENDERECO_ANIMAL");
            modelBuilder.Entity<EnderecoAnimal>().HasKey(g => g.Id_endereco_animal);
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Id_endereco_animal).HasColumnName("ID_ENDERECO_ANIMAL").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Pais).HasColumnName("PAIS").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Estado).HasColumnName("ESTADO").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Cidade).HasColumnName("CIDADE").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Bairro).HasColumnName("BAIRRO").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Logradouro_rua).HasColumnName("LOGRADOURO_RUA").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Nr_rua).HasColumnName("NR_RUA").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Complemento).HasColumnName("COMPLETO").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Cep).HasColumnName("CEP").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().Property(g => g.Id_animal).HasColumnName("ID_ANIMAL").IsRequired();
            modelBuilder.Entity<EnderecoAnimal>().HasOne(g => g.Animal).WithOne().HasForeignKey<EnderecoAnimal>(g => g.Id_animal).IsRequired();

            modelBuilder.Entity<CarteiraVacinal>().ToTable("T_CLYVO_CARTEIRAVACINAL");
            modelBuilder.Entity<CarteiraVacinal>().HasKey(g => g.Id_carteiraVacinal);
            modelBuilder.Entity<CarteiraVacinal>().Property(g => g.Id_carteiraVacinal).HasColumnName("ID_CARTEIRAVACINAL").IsRequired();
            modelBuilder.Entity<CarteiraVacinal>().Property(g => g.Nm_vacina).HasColumnName("NM_VACINA").IsRequired();
            modelBuilder.Entity<CarteiraVacinal>().Property(g => g.Dt_vacina_prevista).HasColumnName("DT_VACINACAO_PREVISTA");
            modelBuilder.Entity<CarteiraVacinal>().Property(g => g.Dt_vacina_efetuada).HasColumnName("DT_VACINACAO_EFETUADA").IsRequired();
            modelBuilder.Entity<CarteiraVacinal>().Property(g => g.St_vacina).HasColumnName("ST_VACINACAO").HasConversion<string>();
            modelBuilder.Entity<CarteiraVacinal>().Property(g => g.Id_animal).HasColumnName("ID_ANIMAL").IsRequired();
            modelBuilder.Entity<CarteiraVacinal>().HasOne(g => g.Animal).WithMany().HasForeignKey(g => g.Id_animal).IsRequired();

            modelBuilder.Entity<Consulta>().ToTable("T_CLYVO_CONSULTA");
            modelBuilder.Entity<Consulta>().HasKey(g => g.Id_consulta);
            modelBuilder.Entity<Consulta>().Property(g => g.Id_consulta).HasColumnName("ID_CONSULTA").IsRequired();
            modelBuilder.Entity<Consulta>().Property(g => g.Historico_consulta).HasColumnName("HISTORICO_CONSULTA").IsRequired();
            modelBuilder.Entity<Consulta>().Property(g => g.St_consulta).HasColumnName("ST_CONSULTA").HasConversion<string>().IsRequired();
            modelBuilder.Entity<Consulta>().Property(g => g.Dt_consulta).HasColumnName("DT_CONSULTA").IsRequired();
            modelBuilder.Entity<Consulta>().Property(g => g.Id_vet).HasColumnName("ID_VET").IsRequired();
            modelBuilder.Entity<Consulta>().Property(g => g.Id_animal).HasColumnName("ID_ANIMAL").IsRequired();
            modelBuilder.Entity<Consulta>().HasOne(g => g.Veterinario).WithMany().HasForeignKey(g => g.Id_vet).IsRequired();
            modelBuilder.Entity<Consulta>().HasOne(g => g.Animal).WithMany().HasForeignKey(g => g.Id_animal).IsRequired();

            modelBuilder.Entity<Prescricao>().ToTable("T_CLYVO_PRESCRICAO");
            modelBuilder.Entity<Prescricao>().HasKey(g => g.Id_prescricao);
            modelBuilder.Entity<Prescricao>().Property(g => g.Id_prescricao).HasColumnName("ID_PRESCRICAO").IsRequired();
            modelBuilder.Entity<Prescricao>().Property(g => g.Dt_emissao).HasColumnName("DT_EMISSAO").IsRequired();
            modelBuilder.Entity<Prescricao>().Property(g => g.Dt_expiracao).HasColumnName("DT_EXPIRACAO").IsRequired();
            modelBuilder.Entity<Prescricao>().Property(g => g.Id_consulta).HasColumnName("ID_CONSULTA").IsRequired();
            modelBuilder.Entity<Prescricao>().HasOne(g => g.Consulta).WithOne().HasForeignKey<Prescricao>(g => g.Id_consulta).IsRequired();
            modelBuilder.Entity<Prescricao>().Property(g => g.Observacoes_gerais).HasColumnName("OBSERVACOES_GERAIS");

            modelBuilder.Entity<Medicamento>().ToTable("T_CLYVO_MEDICAMENTO");
            modelBuilder.Entity<Medicamento>().HasKey(g => g.Id_medicamento);
            modelBuilder.Entity<Medicamento>().Property(g => g.Id_medicamento).HasColumnName("ID_MEDICAMENTO").IsRequired();
            modelBuilder.Entity<Medicamento>().Property(g => g.Id_prescricao).HasColumnName("ID_PRESCRICAO").IsRequired();
            modelBuilder.Entity<Medicamento>().HasOne(g => g.Prescricao).WithMany().HasForeignKey(g => g.Id_prescricao).IsRequired();
            modelBuilder.Entity<Medicamento>().Property(g => g.Nm_medicamento).HasColumnName("NM_MEDICAMENTO").IsRequired();
            modelBuilder.Entity<Medicamento>().Property(g => g.Dosagem_medicamento).HasColumnName("DOSAGEM_MEDICAMENTO").IsRequired();
            modelBuilder.Entity<Medicamento>().Property(g => g.Frequencia).HasColumnName("FREQUENCIA").IsRequired();
            modelBuilder.Entity<Medicamento>().Property(g => g.Qtd_dias).HasColumnName("QTD_DIAS").IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}