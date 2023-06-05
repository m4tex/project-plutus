using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;

namespace project_plutus_backend;

public class BackendApplication : IBackendEndpoints
{
    private readonly HttpClient _http = new();
    private const string RequestItemsTemplate = "https://plutus-3b13e-default-rtdb.europe-west1.firebasedatabase.app/item_market.json";

    public event EventHandler RefreshEvent;
    public MarketItem[] ItemMarket { get; set; }
    private List<MarketItem> _followedItems;
    public MarketItem[] FollowedItems
    {
        get => _followedItems.ToArray();
        set {}
    }

    public async void RefreshMarketItems()
    {
        MessageBox.Show(((int) 5 / (int) 2).ToString());
        
        try
        { 
            String response = await _http.GetStringAsync(RequestItemsTemplate);
            using JsonDocument json = JsonDocument.Parse(response);
            JsonElement itemsObject = json.RootElement;

            List<MarketItem> marketItems = new();
            if (itemsObject.ValueKind == JsonValueKind.Object)
            {
                foreach (var item in itemsObject.EnumerateObject())
                {
                    marketItems.Add(new MarketItem(item.Name, item.Value.GetSingle(), null));
                }
            }

            ItemMarket = marketItems.ToArray();
        }
        catch (Exception e)
        {
            MessageBox.Show("Exception caught while retrieving price." + e.Message, "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        ItemMarket = Array.Empty<MarketItem>();
    }

    public float GetROI()
    {
        return 0.0f;
    }

    public void FollowItem(string name)
    {
        MessageBox.Show(name);
        MarketItem item = ItemMarket.First(item => item.Name == name);
        _followedItems.Add(item);
    }

    public void UnfollowItem(string name)
    {
        MarketItem item = ItemMarket.First(item => item.Name == name);
        _followedItems.Remove(item);
    }
}