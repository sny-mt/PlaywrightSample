using Microsoft.Playwright;

namespace PlaywrightSample.Pages;

/// <summary>
/// 商品一覧ページのページオブジェクト
/// </summary>
public class InventoryPage : BasePage
{
    // セレクター定義
    private const string InventoryList = ".inventory_list";
    private const string InventoryItemName = ".inventory_item_name";
    private const string ShoppingCartBadge = ".shopping_cart_badge";
    private const string ShoppingCartLink = ".shopping_cart_link";

    public InventoryPage(IPage page) : base(page)
    {
    }

    /// <summary>
    /// 商品一覧が表示されるまで待機
    /// </summary>
    public async Task<InventoryPage> WaitForLoadAsync()
    {
        await Page.WaitForSelectorAsync(InventoryList);
        return this;
    }

    /// <summary>
    /// 全商品名を取得
    /// </summary>
    public async Task<IReadOnlyList<string>> GetAllProductNamesAsync()
    {
        return await Page.Locator(InventoryItemName).AllInnerTextsAsync();
    }

    /// <summary>
    /// 商品名をクリックして詳細ページへ遷移
    /// </summary>
    public async Task<ProductDetailPage> ClickProductByNameAsync(string productName)
    {
        await Page.ClickAsync($"text={productName}");
        return new ProductDetailPage(Page);
    }

    /// <summary>
    /// カート内の商品数を取得（バッジが無い場合は0）
    /// </summary>
    public async Task<int> GetCartCountAsync()
    {
        var badge = Page.Locator(ShoppingCartBadge);
        if (await badge.CountAsync() > 0)
        {
            var text = await badge.InnerTextAsync();
            return int.Parse(text);
        }
        return 0;
    }

    /// <summary>
    /// カートページへ移動
    /// </summary>
    public async Task<CartPage> GoToCartAsync()
    {
        await Page.ClickAsync(ShoppingCartLink);
        return new CartPage(Page);
    }
}
