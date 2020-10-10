using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using MatBlazor;

namespace BetterTravel.App.ViewModels
{
    public class HotToursViewModel : BaseViewModel
    {
        private readonly HttpClient _http;

        public List<List<HotTourViewModel>> Data
        {
            get => Get<List<List<HotTourViewModel>>>(); 
            private set => Set(value);
        }

        public string Error
        {
            get => Get<string>(); 
            private set => Set(value);
        }

        public int PageSize
        {
            get => Get<int>();
            private set => Set(value);
        }

        public int PageIndex
        {
            get => Get<int>();
            private set => Set(value);
        }

        public HotToursViewModel(HttpClient http)
        {
            _http = http;

            PageSize = 12;
            PageIndex = 1;
        }

        protected override async Task LoadDataAsync(CancellationToken cancellationToken)
        {
            State = PageState.Loading;

            var take = PageSize;
            var skip = (PageIndex - 1) * PageSize;

            var requestUri = $"http://localhost:8888/api/hottours/get?take={take}&skip={skip}";
            var response = await _http.GetFromJsonAsync<Envelop<PagedData<HotTourViewModel>>>(requestUri, cancellationToken);
            
            IsLoadDataStarted = false;

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                Error = response.ErrorMessage;
                State = PageState.Error;
                return;
            }

            Data = response.Result.Data
                .Select((x, id) => new { x, id })
                .GroupBy(x => x.id / 3)
                .Select(g => g.Select(x => x.x).ToList())
                .ToList();

            State = Data.Any() ? PageState.Normal : PageState.Clean;
        }

        public async Task OnPaginationChangeAsync(MatPaginatorPageEvent e)
        {
            PageSize = e.PageSize;
            PageIndex = e.PageIndex;
            await StartLoadDataAsync();
        }
    }
}
