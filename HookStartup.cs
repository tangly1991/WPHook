using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ame.WPHook
{
    public static class HookStartup
    {
        public static void HookSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFilterManager, FilterManager>();
            services.AddScoped<IActionManager, ActionManager>();
        }
    }
}
