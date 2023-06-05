namespace project_plutus_backend;

public struct MarketItem
{
    public MarketItem(string name, float price, float[]? priceHistory)
    {
        Name = name;
        Price = price;
        PriceHistory = priceHistory ?? Array.Empty<float>();
    }

    public string Name { get; set; }

    public float Price { get; set; }
    
    public float[] PriceHistory { get; set; }
}