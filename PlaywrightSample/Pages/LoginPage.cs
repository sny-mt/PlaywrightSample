using Microsoft.Playwright;

namespace PlaywrightSample.Pages;

/// <summary>
/// ログインページのページオブジェクト
/// </summary>
public class LoginPage : BasePage
{
    private const string Url = "https://www.saucedemo.com/";

    // セレクター定義
    private const string UserNameInput = "#user-name";
    private const string PasswordInput = "#password";
    private const string LoginButton = "#login-button";

    public LoginPage(IPage page) : base(page)
    {
    }

    /// <summary>
    /// ログインページに移動
    /// </summary>
    public async Task<LoginPage> NavigateAsync()
    {
        await Page.GotoAsync(Url);
        return this;
    }

    /// <summary>
    /// ユーザー名を入力
    /// </summary>
    public async Task<LoginPage> EnterUsernameAsync(string username)
    {
        await Page.FillAsync(UserNameInput, username);
        return this;
    }

    /// <summary>
    /// パスワードを入力
    /// </summary>
    public async Task<LoginPage> EnterPasswordAsync(string password)
    {
        await Page.FillAsync(PasswordInput, password);
        return this;
    }

    /// <summary>
    /// ログインボタンをクリック
    /// </summary>
    public async Task<InventoryPage> ClickLoginAsync()
    {
        await Page.ClickAsync(LoginButton);
        return new InventoryPage(Page);
    }

    /// <summary>
    /// ログイン処理（一括）
    /// </summary>
    public async Task<InventoryPage> LoginAsync(string username, string password)
    {
        await EnterUsernameAsync(username);
        await EnterPasswordAsync(password);
        return await ClickLoginAsync();
    }
}
