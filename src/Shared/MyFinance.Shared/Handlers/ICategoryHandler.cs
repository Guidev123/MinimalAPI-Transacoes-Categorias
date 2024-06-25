using MyFinance.Shared.Entities;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Shared.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> CreateAsync(CreateCategoryCommand command);
        Task<Response<Category?>> UpdateAsync(UpdateCategoryCommand command);
        Task<Response<Category?>> DeleteAsync(DeleteCategoryCommand command);
        Task<Response<Category?>> GetByIdAsync(GetCategoryByIdCommand command);
        Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryCommand command);
    }
}
