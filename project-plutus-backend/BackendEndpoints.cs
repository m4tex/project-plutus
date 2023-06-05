using System.Collections.ObjectModel;

namespace project_plutus_backend;

public interface IBackendEndpoints
{
    public event EventHandler RefreshEvent;
    public MarketItem[] ItemMarket { get; set; }
    public MarketItem[] FollowedItems { get; set; }
    
    public void RefreshMarketItems();
    public float GetROI();
    public void FollowItem(string name);
    public void UnfollowItem(string name);
}