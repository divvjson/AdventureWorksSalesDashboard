using Microsoft.JSInterop;
using System.Text.Json;

namespace AdventureWorksSalesDashboard.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            var valueSerialized = JsonSerializer.Serialize(value);

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, valueSerialized);
        }

        public async Task<T?> GetItemAsync<T>(string key)
        {
            var value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            var valueDeserialized = JsonSerializer.Deserialize<T>(value);

            return valueDeserialized;
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}
