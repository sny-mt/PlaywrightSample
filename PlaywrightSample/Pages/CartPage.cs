using Microsoft.Playwright;

namespace PlaywrightSample.Pages;

/// <summary>
/// カートページのページオブジェクト
/// </summary>
public class CartPage : BasePage
{
    // セレクター定義
    private const string CartItem = ".cart_item";
    private const string CartItemName = ".inventory_item_name";
    private const string ContinueShoppingButton = "[data-test='continue-shopping']";
    private const string CheckoutButton = "[data-test='checkout']";

    public CartPage(IPage page) : base(page)
    {
    }

    /// <summary>
    /// カート内の商品名一覧を取得
    /// </summary>
    public async Task<IReadOnlyList<string>> GetCartItemNamesAsync()
    {
        return await Page.Locator(CartItemName).AllInnerTextsAsync();
    }

    /// <summary>
    /// カート内の商品数を取得
    /// </summary>
    public async Task<int> GetItemCountAsync()
    {
        return await Page.Locator(CartItem).CountAsync();
    }

    /// <summary>
    /// 買い物を続ける（商品一覧に戻る）
    /// </summary>
    public async Task<InventoryPage> ContinueShoppingAsync()
    {
        await Page.ClickAsync(ContinueShoppingButton);
        return new InventoryPage(Page);
    }

    /// <summary>
    /// チェックアウトへ進む
    /// </summary>
    public async Task ClickCheckoutAsync()
    {
        await Page.ClickAsync(CheckoutButton);
        // CheckoutPage を返すように拡張可能
    }
}
