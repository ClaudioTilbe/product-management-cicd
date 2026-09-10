using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.UITests.Configuration
{
    public static class TestConfiguration
    {
        public static string BaseUrl => Environment.
            GetEnvironmentVariable("BASE_URL") ?? "http://localhost:4200";


        public static BrowserType Browser
        {
            get
            {
                var browser = Environment.GetEnvironmentVariable("BROWSER");
                if (Enum.TryParse(browser, true, out BrowserType result))
                    return result;
                return BrowserType.Chrome;
            }
        }

        // En local no existe esta variable → devuelve false
        public static bool UseGrid =>
            bool.TryParse(
                Environment.GetEnvironmentVariable("USE_GRID"),
                out var result
            ) && result;

        // En local no existe → devuelve string vacío
        public static string GridUrl =>
            Environment.GetEnvironmentVariable("GRID_URL") ?? string.Empty;
    }


}
