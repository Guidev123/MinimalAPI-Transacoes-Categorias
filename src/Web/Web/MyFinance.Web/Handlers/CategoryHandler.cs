using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;
using System.Net.Http;
using System.Net.Http.Json;

namespace MyFinance.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClienteFactory) : ICategoryHandler
    {
        private readonly HttpClient _httpClient = httpClienteFactory.CreateClient(WebConfig.HttpClientName);
        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryCommand command)
         => await _httpClient.GetFromJsonAsync<PagedResponse<List<Category>?>>("v1/categories")
           ?? new PagedResponse<List<Category>?>(null, 400, "Não foi possível obter todas categorias");

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdCommand command)
            => await _httpClient.GetFromJsonAsync<Response<Category?>>($"v1/categories/{command.Id}")
           ?? new Response<Category?>(null, 400, "Não foi possível obter a categoria selecionada");
        public async Task<Response<Category?>> CreateAsync(CreateCategoryCommand command)
        {
            var result = await _httpClient.PostAsJsonAsync("v1/categories", command);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha na criação da categoria");
        }
        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryCommand command)
        {
            var result = await _httpClient.PutAsJsonAsync($"v1/categories/{command.Id}", command);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha na atualizacao da categoria");
        }
        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryCommand command)
        {
            var result = await _httpClient.DeleteAsync($"v1/categories/{command.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha na exclusao da categoria");
        }
    }
}
