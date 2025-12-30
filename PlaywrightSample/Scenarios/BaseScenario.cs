using PlaywrightSample.Infrastructure;
using PlaywrightSample.Pages;

namespace PlaywrightSample.Scenarios;

/// <summary>
/// シナリオの基底クラス
/// </summary>
public abstract class BaseScenario
{
    protected readonly BrowserFactory BrowserFactory;

    /// <summary>
    /// BrowserFactory を外部から注入（DI対応）
    /// </summary>
    protected BaseScenario(BrowserFactory browserFactory)
    {
        BrowserFactory = browserFactory ?? throw new ArgumentNullException(nameof(browserFactory));
    }

    /// <summary>
    /// シナリオを実行
    /// </summary>
    public abstract Task ExecuteAsync();

    /// <summary>
    /// ログインページから開始
    /// </summary>
    protected async Task<LoginPage> StartAtLoginPageAsync()
    {
        var page = await BrowserFactory.CreatePageAsync();
        var loginPage = new LoginPage(page);
        await loginPage.NavigateAsync();
        return loginPage;
    }
}
