using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Splitwise.Clients.Interfaces;
using Splitwise.Responses.Category;

namespace Splitwise.Clients
{
    internal class CategoryClient : ICategoryClient
    {
        private readonly IRestClient _restClient;

        public CategoryClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IReadOnlyCollection<Category>> ListAsync()
        {
            var request = new RestRequest("get_categories");
            var response = await _restClient.GetAsync<CategoryResponse>(request);

            return response.Categories;
        }
    }
}