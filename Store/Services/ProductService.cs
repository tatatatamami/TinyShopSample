using DataEntities;
using System.Text.Json;

namespace Store.Services;

public class ProductService
{
    HttpClient httpClient;
    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<List<Product>> GetProducts(string? sortBy = null, string? sortDirection = null)
    {
        List<Product>? products = null;
        
        var url = "/api/Product";
        if (!string.IsNullOrEmpty(sortBy))
        {
            url += $"?sortBy={sortBy}";
            if (!string.IsNullOrEmpty(sortDirection))
            {
                url += $"&sortDirection={sortDirection}";
            }
        }
        
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            products = await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.ListProduct);
        }

        return products ?? new List<Product>();
    }
    
}