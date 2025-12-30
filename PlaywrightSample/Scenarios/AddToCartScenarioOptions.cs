namespace PlaywrightSample.Scenarios;

/// <summary>
/// カート追加シナリオの設定
/// </summary>
public class AddToCartScenarioOptions
{
    public string Username { get; set; } = "standard_user";
    public string Password { get; set; } = "secret_sauce";
    public string ProductName { get; set; } = "Sauce Labs Backpack";
}
