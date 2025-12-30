using Microsoft.Extensions.DependencyInjection;
using PlaywrightSample.Scenarios;

namespace PlaywrightSample.Infrastructure;

/// <summary>
/// DI コンテナへのサービス登録を行う拡張メソッド
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Playwright 関連サービスを登録
    /// </summary>
    public static IServiceCollection AddPlaywrightServices(this IServiceCollection services,
                                                            Action<BrowserSettings>? configureSettings = null)
    {
        // BrowserSettings を登録
        var settings = new BrowserSettings();
        configureSettings?.Invoke(settings);
        services.AddSingleton(settings);

        // BrowserFactory を登録（シナリオごとに新しいインスタンス）
        services.AddTransient<BrowserFactory>();

        return services;
    }

    /// <summary>
    /// カート追加シナリオを登録
    /// </summary>
    public static IServiceCollection AddAddToCartScenario(
      this IServiceCollection services,
      Action<AddToCartScenarioOptions>? configureOptions = null)
    {
        // オプションを登録
        var options = new AddToCartScenarioOptions();
        configureOptions?.Invoke(options);
        services.AddSingleton(options);

        // シナリオを登録
        services.AddTransient<AddToCartScenario>();

        return services;
    }
}
