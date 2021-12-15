using Microsoft.JSInterop;
using Radzen.Blazor;

namespace FSM.Blazor.Shared
{
    public partial class MainLayout
    {
        RadzenSidebar sidebar0;
        RadzenBody body0;
        bool sidebarExpanded = true;
        bool bodyExpanded = false;

        dynamic themes = new[]
        {
        new { Text = "Default Theme", Value = "default"},
        new { Text = "Dark Theme", Value="dark" },
        new { Text = "Software Theme", Value = "software"},
        new { Text = "Humanistic Theme", Value = "humanistic" },
        new { Text = "Standard Theme", Value = "standard" }
    };

        string Theme
        {
            get
            {
                return $".css";
            }
        }

        protected override void OnInitialized()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                var example = ExampleService.FindCurrent(UriHelper.ToAbsoluteUri(UriHelper.Uri));

                await JSRuntime.InvokeVoidAsync("setTitle", ExampleService.TitleFor(example));
            }
        }

        void ChangeTheme(object value)
        {
            //ThemeState.CurrentTheme = value.ToString();
            UriHelper.NavigateTo(UriHelper.ToAbsoluteUri(UriHelper.Uri).ToString());
        }
    }
}
