using DataEntities;
using Microsoft.EntityFrameworkCore;
using Products.Data;

namespace Products.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product");

        group.MapGet("/", async (ProductDataContext db, string? sortBy = null, string? sortDirection = null) =>
        {
            var query = db.Product.AsQueryable();
            
            // Apply sorting based on parameters
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = sortDirection?.ToLower() == "desc" 
                            ? query.OrderByDescending(p => p.Name)
                            : query.OrderBy(p => p.Name);
                        break;
                    case "price":
                        query = sortDirection?.ToLower() == "desc"
                            ? query.OrderByDescending(p => p.Price)
                            : query.OrderBy(p => p.Price);
                        break;
                    default:
                        // Default sort by Name if invalid sortBy parameter
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                // Default sort by Name if no sorting specified
                query = query.OrderBy(p => p.Name);
            }
            
            return await query.ToListAsync();
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int id, ProductDataContext db) =>
        {
            return await db.Product.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int id, Product product, ProductDataContext db) =>
        {
            var affected = await db.Product
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, product.Id)
                  .SetProperty(m => m.Name, product.Name)
                  .SetProperty(m => m.Description, product.Description)
                  .SetProperty(m => m.Price, product.Price)
                  .SetProperty(m => m.ImageUrl, product.ImageUrl)
                );

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (Product product, ProductDataContext db) =>
        {
            db.Product.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Product/{product.Id}",product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int id, ProductDataContext db) =>
        {
            var affected = await db.Product
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteProduct")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
