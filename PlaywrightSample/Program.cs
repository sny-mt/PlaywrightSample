using Microsoft.Extensions.DependencyInjection;
using PlaywrightSample.Infrastructure;
using PlaywrightSample.Scenarios;

class Program
{
    public static async Task Main()
    {
        // DI コンテナを構築
        var services = new ServiceCollection();

        // サービスを登録
        services.AddPlaywrightServices(browser =>
        {
            browser.BrowserType = BrowserType.Chromium;
            browser.Headless = false;
            browser.SlowMo = 150;
        });

        services.AddAddToCartScenario(options =>
          {
              options.Username = "standard_user";
              options.Password = "secret_sauce";
              options.ProductName = "Sauce Labs Backpack";
          });

        // サービスプロバイダーを作成
        var serviceProvider = services.BuildServiceProvider();

        // シナリオを取得して実行
        var scenario = serviceProvider.GetRequiredService<AddToCartScenario>();
        await scenario.ExecuteAsync();
    }
}
