using challengeFiap.Application.Diagnostics;
using challengeFiap.Application.Service;
using challengeFiap.Domain.Interfaces;
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
            services.AddScoped<IAnimalRepository, AnimalService>();
            services.AddScoped<ICarteiraVacinalRepository, CateiraVacinalService>();
            services.AddScoped<IConsultaRepository, ConsultaService>();
            services.AddScoped<IClinicaRepository, ClinicaService>();
            services.AddScoped<IenderecoAnimalRepository, EnderecoAnimalService>();
            services.AddScoped<IenderecoClinicaRepository, EnderecoClinicaService>();
            services.AddScoped<IenderecoTutorRepository, EnderecoTutorService>();
            services.AddScoped<IMedicamentoRepository, MedicamentoService>();
            services.AddScoped<IprescricaoRepository, PrescricaoService>();
            services.AddScoped<ItutorRepository, TutorService>();
            services.AddScoped<IVetClinica, VetClinicasService>();
            services.AddScoped<IVeterinarioRepository, VeterinariosService>();

            services.AddScoped<AnimalRepository>();
            services.AddScoped<CarteiraVacinalRepository>();
            services.AddScoped<ConsultaRepository>();
            services.AddScoped<ClinicaRepository>();
            services.AddScoped<EnderecoAnimalRepository>();
            services.AddScoped<EnderecoClinicaRepository>();
            services.AddScoped<EnderecoTutorRepository>();
            services.AddScoped<MedicamentoRepository>();
            services.AddScoped<PrescricaoRepository>();
            services.AddScoped<TutorRepository>();
            services.AddScoped<VetClinicaRepository>();
            services.AddScoped<VeterinarioRepository>();

            services.AddScoped<AnimalOracleRepository>();
            services.AddScoped<CarteiraVacinalOracleRepository>();
            services.AddScoped<ConsultaOracleRepository>();
            services.AddScoped<ClinicaOracleRepository>();
            services.AddScoped<EnderecoAnimalOracleRepository>();
            services.AddScoped<EnderecoClinicaOracleRepository>();
            services.AddScoped<EnderecoTutorOracleRepository>();
            services.AddScoped<MedicamentoOracleRepository>();
            services.AddScoped<PrescricaoOracleRepository>();
            services.AddScoped<TutorOracleRepository>();
            services.AddScoped<VetClinicaOracleRepository>();
            services.AddScoped<VeterinarioOracleRepository>();


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