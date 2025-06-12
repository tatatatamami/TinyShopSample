using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Products API �T�[�r�X��ǉ�
var productsApi = builder.AddProject<Projects.Products>("products");

// Store�iBlazor�j�T�[�r�X��ǉ����AProducts API ���Q��
var storeWeb = builder.AddProject<Projects.Store>("store")
    .WithReference(productsApi);

builder.Build().Run();
