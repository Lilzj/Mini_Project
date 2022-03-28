using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Onboarding.Data;
using Onboarding.Models.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Onboarding.DTOs.FilterCriteria
{
    public class LGAFilterCriteria : IParameterFilter
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public LGAFilterCriteria(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name.Equals("lga", StringComparison.InvariantCultureIgnoreCase))
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<OnboardingDbContext>();
                    IEnumerable<LGA> locals = _context.LGAs.ToArray();

                    parameter.Schema.Enum = locals.Select(p => new OpenApiString(p.Name)).ToList<IOpenApiAny>();
                }
            }
        }
    }
}
