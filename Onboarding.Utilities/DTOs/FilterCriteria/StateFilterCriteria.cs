using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Onboarding.Data;
using Onboarding.Models.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Onboarding.DTOs.FilterCriteria
{
    public class StateFilterCriteria : IParameterFilter
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public StateFilterCriteria(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name.Equals("state", StringComparison.InvariantCultureIgnoreCase))
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<OnboardingDbContext>();
                    IEnumerable<State> states = _context.States.ToArray();

                    parameter.Schema.Enum = states.Select(p => new OpenApiString(p.Name)).ToList<IOpenApiAny>();
                }
            }
        }
    }
}
