using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;

namespace MyFinance.Web.Pages.Categories
{
    public partial class GetAllCategoryPage : ComponentBase
    {
        // ================= PROPERTIES =================

        public bool IsBusy { get; set; } = false;
        public List<Category> Categories { get; set; } = [];


        // ================= SERVICES =================

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;


        // ================= OVERRIDES =================

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var command = new GetAllCategoryCommand();
                var result = await Handler.GetAllAsync(command);
                if (result.IsSucces)
                    Categories = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await Dialog.ShowMessageBox(
                "ATENÇÃO",
                $"Tem certeza que deseja excluir a categoria {title}?",
                yesText: "Excluir",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, title);

            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id, string title)
        {
            try
            {
                var request = new DeleteCategoryCommand
                {
                    Id = id
                };
                await Handler.DeleteAsync(request);
                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria {title} removida", Severity.Warning);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}
