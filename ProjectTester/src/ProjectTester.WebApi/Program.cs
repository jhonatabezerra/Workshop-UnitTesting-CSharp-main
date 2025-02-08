using System.Diagnostics.CodeAnalysis;

namespace ProjectTester.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// Create host builder.
        /// </summary>
        /// <param name="args">Arguments collection.</param>
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        /// <summary>
        /// Create host builder according Startup.
        /// </summary>
        /// <param name="args">Arguments collection.</param>
        /// <returns>Host instance.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}