using AdventureWorksSalesDashboard.Entities;
using AdventureWorksSalesDashboard.Services;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksSalesDashboard.Pages.Regional
{
    public class RegionalFilterService
    {
        private readonly LocalStorageService _localStorageService;
        private readonly IDbContextFactory<AdventureWorksContext> _dbFactory;

        public event Action? OnRegionalFilterChanged;
        public RegionalFilterState RegionalFilterState { get; set; } = new();

        public RegionalFilterService(LocalStorageService localStorageService, IDbContextFactory<AdventureWorksContext> dbFactory)
        {
            _localStorageService = localStorageService;
            _dbFactory = dbFactory;
        }

        public async Task Initialize()
        {
            var regionalFilterStateFromLocalStorage = await _localStorageService.GetItemAsync<RegionalFilterState>(nameof(RegionalFilterState));

            if (regionalFilterStateFromLocalStorage is null)
            {
                await Reset();
            }
            else
            {
                RegionalFilterState = regionalFilterStateFromLocalStorage;

                OnRegionalFilterChanged?.Invoke();
            }
        }

        public async Task Reset()
        {
            var context = _dbFactory.CreateDbContext();

            var query = context.SalesOrderHeaders
                .Where(salesOrderHeader => salesOrderHeader.Status != 6 && salesOrderHeader.Status != 4)
                .Select(salesOrderHeader => salesOrderHeader.OrderDate);

            var minDate = await query.MinAsync();
            var maxDate = await query.MaxAsync();

            RegionalFilterState = new()
            {
                MinDate = minDate,
                MaxDate = maxDate
            };

            await _localStorageService.SetItemAsync(nameof(RegionalFilterState), RegionalFilterState);

            OnRegionalFilterChanged?.Invoke();
        }

        public async Task Update()
        {
            await _localStorageService.SetItemAsync(nameof(RegionalFilterState), RegionalFilterState);
            
            OnRegionalFilterChanged?.Invoke();
        }
    }
}
