using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BetterTravel.App.ViewModels
{
    public class BasePage<T> : ComponentBase, IDisposable where T : BaseViewModel
    {
        [Inject]
        public T ViewModel { get; set; }

        public void Dispose()
        {
            ViewModel?.Dispose();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (ViewModel is null) return;

            ViewModel.PropertyChanged += (o, e) => StateHasChanged();
            await ViewModel.StartLoadDataAsync();
        }
    }
}