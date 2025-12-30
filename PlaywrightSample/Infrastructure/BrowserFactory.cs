using Microsoft.Playwright;

namespace PlaywrightSample.Infrastructure;

/// <summary>
/// ブラウザインスタンスの生成を管理するファクトリークラス
/// </summary>
public class BrowserFactory : IAsyncDisposable
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    public BrowserSettings Settings { get; }

    public BrowserFactory(BrowserSettings? settings = null)
    {
        Settings = settings ?? new BrowserSettings();
    }

    /// <summary>
    /// ブラウザを起動して返す
    /// </summary>
    public async Task<IBrowser> CreateBrowserAsync()
    {
        _playwright = await Playwright.CreateAsync();

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = Settings.Headless,
            SlowMo = Settings.SlowMo
        };

        _browser = Settings.BrowserType switch
        {
            BrowserType.Firefox => await _playwright.Firefox.LaunchAsync(launchOptions),
            BrowserType.Webkit => await _playwright.Webkit.LaunchAsync(launchOptions),
            _ => await _playwright.Chromium.LaunchAsync(launchOptions)
        };

        return _browser;
    }

    /// <summary>
    /// 新しいページを作成して返す
    /// </summary>
    public async Task<IPage> CreatePageAsync()
    {
        if (_browser == null)
        {
            await CreateBrowserAsync();
        }

        return await _browser!.NewPageAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
        _playwright?.Dispose();
    }
}

/// <summary>
/// ブラウザの設定
/// </summary>
public class BrowserSettings
{
    public BrowserType BrowserType { get; set; } = BrowserType.Chromium;
    public bool Headless { get; set; } = false;
    public float SlowMo { get; set; } = 150;
}

/// <summary>
/// 対応ブラウザの種類
/// </summary>
public enum BrowserType
{
    Chromium,
    Firefox,
    Webkit
}
