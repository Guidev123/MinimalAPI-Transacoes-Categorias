using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MyFinance.Infra.Context;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Handlers
{
    public class CategoryHandler(MyFinanceDbContext _context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryCommand command)
        {
            var category = new Category
            {
                UserId = command.UserId,
                Title = command.Title,
                Description = command.Description
            };
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return new Response<Category?>(category, 201, "Categoria criada com sucesso!");
            }

            catch
            {
                return new Response<Category?>(null, 500, "Não foi possível criar a categoria");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryCommand command)
        {
            try
            {
                var category = await _context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == command.Id && x.UserId == command.UserId);
                if (category is null)
                    return new Response<Category?>(null, 404, "Categoria não encontrada");
                
                category.Title = command.Title;
                category.Description = command.Description;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria atualizada com sucesso");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possível alterar a categoria");
            }
        }
        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryCommand command)
        {
            try
            {
                var category = await _context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == command.Id && x.UserId == command.UserId);

                if (category is null) return new Response<Category?>(null, 404, "Categoria não encontrada");

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria excluída com sucesso!");
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possível excluir a categoria");
            }
        }


        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdCommand command)
        {
            try
            {
                var category = await _context
                    .Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == command.Id && x.UserId == command.UserId);

                return category is null
                    ? new Response<Category?>(null, 404, "Categoria não encontrada")
                    : new Response<Category?>(category);
            }
            catch
            {
                return new Response<Category?>(null, 500, "Não foi possível recuperar a categoria");
            }
        }
        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryCommand command)
        {
            try
            {
                var query = _context
                    .Categories
                    .AsNoTracking()
                    .Where(x => x.UserId == command.UserId)
                    .OrderBy(x => x.Title);

                var categories = await query
                    .Skip((command.PageNumber - 1) * command.PageSize)
                    .Take(command.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(
                    categories,
                    count,
                    command.PageNumber,
                    command.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>?>(null, 500, "Não foi possível consultar as categorias");
            }
        }
    }
}
