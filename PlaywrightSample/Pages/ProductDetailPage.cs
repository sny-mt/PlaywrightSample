using Microsoft.Playwright;

namespace PlaywrightSample.Pages;

/// <summary>
/// 商品詳細ページのページオブジェクト
/// </summary>
public class ProductDetailPage : BasePage
{
    // セレクター定義
    private const string ProductName = ".inventory_details_name";
    private const string ProductPrice = ".inventory_details_price";
    private const string AddToCartButton = "button:has-text('Add to cart')";
    private const string RemoveButton = "button:has-text('Remove')";
    private const string BackButton = "[data-test='back-to-products']";

    public ProductDetailPage(IPage page) : base(page)
    {
    }

    /// <summary>
    /// 商品名を取得
    /// </summary>
    public async Task<string> GetProductNameAsync()
    {
        return await Page.Locator(ProductName).InnerTextAsync();
    }

    /// <summary>
    /// 価格を取得
    /// </summary>
    public async Task<string> GetPriceAsync()
    {
        return await Page.Locator(ProductPrice).InnerTextAsync();
    }

    /// <summary>
    /// カートに追加
    /// </summary>
    public async Task<ProductDetailPage> AddToCartAsync()
    {
        await Page.ClickAsync(AddToCartButton);
        return this;
    }

    /// <summary>
    /// カートから削除
    /// </summary>
    public async Task<ProductDetailPage> RemoveFromCartAsync()
    {
        await Page.ClickAsync(RemoveButton);
        return this;
    }

    /// <summary>
    /// 商品一覧に戻る
    /// </summary>
    public async Task<InventoryPage> BackToProductsAsync()
    {
        await Page.ClickAsync(BackButton);
        return new InventoryPage(Page);
    }
}
