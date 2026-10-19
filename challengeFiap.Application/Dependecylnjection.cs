using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using challengeFiap.Application.Diagnostics;
using challengeFiap.Application.Service;
using challengeFiap.Domain.Interfaces;

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