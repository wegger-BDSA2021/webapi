using System.Threading.Tasks;

namespace ResourceBuilder
{
    class ClientExample
    {
        public static async Task Client()
        {
            var input = new InputFromAPI{
                Description = "test yo yo",
                InitialRating = 3,
                Title = "this is a test",
                UserId = 1,
                // Url = "https://medium.com/@ergojdev/a-simple-web-scraper-in-30-minutes-with-net-core-and-anglesharp-part-1-51fdf5ecafb1"
                // Url = "https://www.youtube.com/watch?v=zA3PxYEomIk&t=3s&ab_channel=DRNyheder"
                // Url = "https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5"
                // Url = "ub.com/WolfgangOfner/.NetCoreRepositoryAndUnitOfWorkPattern"
                Url = "https://www.uml-diagrams.org/component.html"
            };

            var builder = new Builder(input);
            var director = new Director(builder);

            var result = await director.Make();

            System.Console.WriteLine(result.ToString());
        }
    }
}