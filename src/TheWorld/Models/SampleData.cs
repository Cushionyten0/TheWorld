using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TheWorld.Models {
    public static class SampleData {
        public static async Task InitializeWorldContext (IServiceProvider serviceProvider) {
            var db = serviceProvider.GetService<WorldContext> ();
            await db.Database.EnsureCreatedAsync ();
        }
    }
}