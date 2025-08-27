using DataEntities;
using Microsoft.EntityFrameworkCore;

namespace Products.Data;

public class ProductDataContext : DbContext
{
    public ProductDataContext (DbContextOptions<ProductDataContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Product { get; set; } = default!;
}

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ProductDataContext>();
        context.Database.EnsureCreated();
        DbInitializer.Initialize(context);
    }
}


public static class DbInitializer
{
    public static void Initialize(ProductDataContext context)
    {
        if (context.Product.Any())
            return;

        var products = new List<Product>
        {
            new Product { Name = "Milk Chocolate Chunk", Description = "これぞ定番。人気ナンバーワン！ミルクチョコレートチャンクがゴロっと入った、メジャークッキー5のひとつ。迷ったらまずこれ。", Price = 300m, ImageUrl = "product1.png" },
            new Product { Name = "Dark Chocolate Chunk", Description = "ほろ苦いダークチョコレートが大人な味わい。赤ワインにもぴったり。メジャークッキー5のひとつ。", Price = 300m, ImageUrl = "product2.png" },
            new Product { Name = "Ginger & Dark Chocolate", Description = "ダークチョコレートクッキーにスパイシーなジンジャーが入った大人なクッキー。刺激が欲しい時にぴったり。", Price = 360m, ImageUrl = "product3.png" },
            new Product { Name = "Matcha & White Chocolate", Description = "ほろ苦い抹茶を練り込んだクッキー生地に、甘いホワイトチョコレートとのバランスが抜群に美味しい！", Price = 360m, ImageUrl = "product4.png" },
            new Product { Name = "White Chocolate & Macadamia", Description = "ゴロゴロ入ったホワイトチョコレートとマカダミアは見つけたらマストバイ！", Price = 320m, ImageUrl = "product5.png" },
            new Product { Name = "Coconut", Description = "ココナッツを練り込んだ生地の上に、ココナッツフレークをあしらった香ばしくも爽やかなやみつきになるクッキー。ノンチョコレート。", Price = 250m, ImageUrl = "product6.png" },
            new Product { Name = "Oatmeal & Raisin", Description = "レーズンとジャンボオートがたっぷり入ったシナモン香るヘルシーなクッキー。朝食にどうぞ！ノンチョコレート。", Price = 350m, ImageUrl = "product7.png" },
            new Product { Name = "LEMON", Description = "レモンの風味が爽やかなクッキー。あっさりなのでぺろっとたくさん食べちゃえます。ノンチョコレート。", Price = 250m, ImageUrl = "product8.png" },
            new Product {  Name = "Fruit & Nut", Description = "ミルクチョコレートクッキーにレーズンを練り込み、ヘーゼルナッツをトッピング。フルーツとナッツとチョコレートを一度に味わえるのはこれだけ！", Price = 360m, ImageUrl = "product9.png" },
        };

        context.AddRange(products);

        context.SaveChanges();
    }
}