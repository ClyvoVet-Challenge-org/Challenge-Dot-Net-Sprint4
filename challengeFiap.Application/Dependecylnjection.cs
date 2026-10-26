using challengeFiap.Application.Diagnostics;
using challengeFiap.Application.Interfaces;
using challengeFiap.Application.Service;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace challengeFiap.Infrastruture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<ICarteiraVacinalService, CateiraVacinalService>();
            services.AddScoped<IConsultaService, ConsultaService>();
            services.AddScoped<IClinicaService, ClinicaService>();
            services.AddScoped<IEnderecoAnimalService, EnderecoAnimalService>();
            services.AddScoped<IEnderecoClinicaService, EnderecoClinicaService>();
            services.AddScoped<IEnderecoTutorService, EnderecoTutorService>();
            services.AddScoped<IMedicamentoService, MedicamentoService>();
            services.AddScoped<IPrescricaoService, PrescricaoService>();
            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<IVetClinicaService, VetClinicasService>();
            services.AddScoped<IVeterinarioService, VeterinariosService>();

            services.AddScoped<IAnimalMongoRepository, AnimalRepository>();
            services.AddScoped<ICarteiraVacinalMongoRepository, CarteiraVacinalRepository>();
            services.AddScoped<IConsultaMongoRepository, ConsultaRepository>();
            services.AddScoped<IClinicaMongoRepository, ClinicaRepository>();
            services.AddScoped<IEnderecoAnimalMongoRepository, EnderecoAnimalRepository>();
            services.AddScoped<IEnderecoClinicaMongoRepository, EnderecoClinicaRepository>();
            services.AddScoped<IEnderecoTutorMongoRepository, EnderecoTutorRepository>();
            services.AddScoped<IMedicamentoMongoRepository, MedicamentoRepository>();
            services.AddScoped<IPrescricaoMongoRepository, PrescricaoRepository>();
            services.AddScoped<ITutorMongoRepository, TutorRepository>();
            services.AddScoped<IVetClinicaMongoRepository, VetClinicaRepository>();
            services.AddScoped<IVeterinarioMongoRepository, VeterinarioRepository>();

            services.AddScoped<IAnimalOracleRepository, AnimalOracleRepository>();
            services.AddScoped<ICarteiraVacinalOracleRepository, CarteiraVacinalOracleRepository>();
            services.AddScoped<IConsultaOracleRepository, ConsultaOracleRepository>();
            services.AddScoped<IClinicaOracleRepository, ClinicaOracleRepository>();
            services.AddScoped<IEnderecoAnimalOracleRepository, EnderecoAnimalOracleRepository>();
            services.AddScoped<IEnderecoClinicaOracleRepository, EnderecoClinicaOracleRepository>();
            services.AddScoped<IEnderecoTutorOracleRepository, EnderecoTutorOracleRepository>();
            services.AddScoped<IMedicamentoOracleRepository, MedicamentoOracleRepository>();
            services.AddScoped<IPrescricaoOracleRepository, PrescricaoOracleRepository>();
            services.AddScoped<ITutorOracleRepository, TutorOracleRepository>();
            services.AddScoped<IVetClinicaOracleRepository, VetClinicaOracleRepository>();
            services.AddScoped<IVeterinarioOracleRepository, VeterinarioOracleRepository>();


            var resourceBuilder = ResourceBuilder.CreateDefault()
                .AddService(TelemetryConstants.ServiceName);

            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    tracerProviderBuilder
                        .SetResourceBuilder(resourceBuilder)
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSource(TelemetryConstants.ServiceName)
                        .AddConsoleExporter();
                })
                .WithMetrics(meterProviderBuilder =>
                {
                    meterProviderBuilder
                        .SetResourceBuilder(resourceBuilder)
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddMeter(TelemetryConstants.MeterName)
                        .AddConsoleExporter();
                });

            return services;
        }
    }
}