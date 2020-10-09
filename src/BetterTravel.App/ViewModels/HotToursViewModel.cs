using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MatBlazor;

namespace BetterTravel.App.ViewModels
{
    public class HotToursViewModel
    {
        private readonly HttpClient _http;

        public HotToursViewModel(HttpClient http) => 
            _http = http;

        public List<List<HotTourViewModel>> Data { get; private set; }
        public string Error { get; private set; }
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }

        public async Task InitializeContextAsync(object view)
        {
            PageSize = 12;
            PageIndex = 1;
            
            await FetchDataAsync(PageSize, PageIndex);
        }

        public async Task OnPaginationChange(MatPaginatorPageEvent e)
        {
            PageSize = e.PageSize;
            PageIndex = e.PageIndex;

            await FetchDataAsync(PageSize, PageIndex);
        }

        public async Task FetchDataAsync(int size, int page)
        {
            var url = $"http://localhost:8888/api/hottours/get?take={size}&skip={page - 1}";
            var response = await _http.GetFromJsonAsync<Envelop<PagedData<HotTourViewModel>>>(url);

            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                Error = response.ErrorMessage;
                return;
            }

            Data = response.Result.Data
                .Select((x, id) => new { x, id })
                .GroupBy(x => x.id / 3)
                .Select(g => g.Select(x => x.x).ToList())
                .ToList();
        }
    }
}
