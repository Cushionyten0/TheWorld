using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TheWorld.Models {
    public static class SampleData {
        public static void InitializeWorldContext (IServiceProvider serviceProvider) {
            var db = serviceProvider.GetService<WorldContext> ();
            db.Database.EnsureCreatedAsync ();
        }
    }
}