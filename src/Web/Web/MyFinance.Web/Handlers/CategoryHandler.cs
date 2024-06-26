using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;

namespace MyFinance.Web.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        public Task<Response<Category?>> CreateAsync(CreateCategoryCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category?>> DeleteAsync(DeleteCategoryCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category?>> GetByIdAsync(GetCategoryByIdCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category?>> UpdateAsync(UpdateCategoryCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
