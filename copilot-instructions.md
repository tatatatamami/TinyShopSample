### Backend
- ProductsプロジェクトがバックエンドAPIです。
- .NET Minimal APIsで構築されています。
- インメモリデータベースとしてEntity Framework Coreを使用しています。
- OpenAPIのベストプラクティスに従うべきです。
- 
### Frontend
- Storeプロジェクトは .NET 9 Blazor Serverアプリケーションです。
- デフォルトのBootstrapスタイルを使用しています。
- UIはモダンな外観・操作感を持つべきです。
- CSSは .razor.css ファイルに記述してください。

### Misc
- 画像を表示する際は、常にフロントエンドで `ImagePrefix` を使う必要があります。これは `Configuration["ImagePrefix"]` から取得でき、`@inject IConfiguration Configuration` で注入します。
- コード提案の際は、**lab** フォルダー内のすべての `.md` ファイルのコードを無視してください。
