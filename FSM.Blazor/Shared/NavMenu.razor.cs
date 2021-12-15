using FSM.Blazor.Data.CommonServices;
using Microsoft.AspNetCore.Components;

namespace FSM.Blazor.Shared
{
    public partial class NavMenu
    {
        [Parameter]
        public bool Expanded { get; set; }

        bool sidebarExpanded = true;
        bool bodyExpanded = false;

        IEnumerable<Example> examples;

        protected override void OnInitialized()
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null &&
                 httpContextAccessor.HttpContext.Request != null && httpContextAccessor.HttpContext.Request.Headers.ContainsKey("User-Agent"))
            {
                var userAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].FirstOrDefault();
                if (!string.IsNullOrEmpty(userAgent))
                {
                    if (userAgent.Contains("iPhone") || userAgent.Contains("Android") || userAgent.Contains("Googlebot"))
                    {
                        sidebarExpanded = false;
                        bodyExpanded = true;
                    }
                }
            }

            examples = ExampleService.Examples;

        }

        void FilterPanelMenu(ChangeEventArgs args)
        {
            var term = args.Value.ToString();

            examples = ExampleService.Filter(term);
        }
    }
}
