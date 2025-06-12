using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Products API サービスを追加
var productsApi = builder.AddProject<Projects.Products>("products");

// Store（Blazor）サービスを追加し、Products API を参照
var storeWeb = builder.AddProject<Projects.Store>("store")
    .WithReference(productsApi);

builder.Build().Run();
