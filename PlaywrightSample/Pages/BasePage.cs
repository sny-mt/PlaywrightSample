using Microsoft.Playwright;

namespace PlaywrightSample.Pages;

/// <summary>
/// 全ページの基底クラス
/// </summary>
public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }

    /// <summary>
    /// スクリーンショットを保存
    /// </summary>
    public async Task ScreenshotAsync(string path, bool fullPage = true)
    {
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = path,
            FullPage = fullPage
        });
    }

    /// <summary>
    /// ページタイトルを取得
    /// </summary>
    public Task<string> GetTitleAsync() => Page.TitleAsync();

    /// <summary>
    /// 前のページに戻る
    /// </summary>
    public Task GoBackAsync() => Page.GoBackAsync();

    /// <summary>
    /// デバッグ用に一時停止
    /// </summary>
    public Task PauseAsync() => Page.PauseAsync();
}
