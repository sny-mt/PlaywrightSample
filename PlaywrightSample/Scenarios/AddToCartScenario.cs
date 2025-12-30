using PlaywrightSample.Infrastructure;

namespace PlaywrightSample.Scenarios;

/// <summary>
/// 商品をカートに追加するシナリオ
/// </summary>
public class AddToCartScenario : BaseScenario
{
    private readonly AddToCartScenarioOptions _options;

    /// <summary>
    /// DI コンテナから注入
    /// </summary>
    public AddToCartScenario(
        BrowserFactory browserFactory,
        AddToCartScenarioOptions? options = null) : base(browserFactory)
    {
        _options = options ?? new AddToCartScenarioOptions();
    }

    public override async Task ExecuteAsync()
    {
        Console.WriteLine("=== カート追加シナリオ開始 ===");

        try
        {
            // 1. ログインページに移動してログイン
            var loginPage = await StartAtLoginPageAsync();
            Console.WriteLine("ログインページを開きました");

            var inventoryPage = await loginPage.LoginAsync(_options.Username, _options.Password);
            await inventoryPage.WaitForLoadAsync();
            Console.WriteLine("ログイン完了");

            // 2. 商品一覧を表示
            var products = await inventoryPage.GetAllProductNamesAsync();
            Console.WriteLine("商品一覧：");
            foreach (var product in products)
            {
                Console.WriteLine($"  - {product}");
            }

            // 3. 指定商品をクリックして詳細ページへ
            var detailPage = await inventoryPage.ClickProductByNameAsync(_options.ProductName);
            Console.WriteLine($"商品詳細: {await detailPage.GetProductNameAsync()}");
            Console.WriteLine($"価格: {await detailPage.GetPriceAsync()}");

            // 4. カートに追加
            await detailPage.AddToCartAsync();
            Console.WriteLine($"{_options.ProductName} をカートに追加しました");

            // 5. スクリーンショット保存
            await detailPage.ScreenshotAsync("added_to_cart.png");
            Console.WriteLine("スクリーンショットを保存しました");

            // 6. 商品一覧に戻ってカート数を確認
            inventoryPage = await detailPage.BackToProductsAsync();
            var cartCount = await inventoryPage.GetCartCountAsync();
            Console.WriteLine($"カート内の個数: {cartCount}");

            Console.WriteLine("=== シナリオ完了 ===");
        }
        finally
        {
            await BrowserFactory.DisposeAsync();
        }
    }
}
