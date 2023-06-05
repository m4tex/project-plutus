using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace project_plutus_backend;

public class ViewModel : INotifyPropertyChanged
{
    private IBackendEndpoints _backendEndpoints; 
    
    public ICommand RefreshMarket { get; }
    public ICommand FollowItem { get; }
    public ICommand UnfollowItem { get; }
    
    public ViewModel(IBackendEndpoints endpoints)
    {
        _backendEndpoints = endpoints;
        RefreshMarket = new RelayCommand((object argument) => _backendEndpoints.RefreshMarketItems());
        FollowItem = new RelayCommand(FollowItemCommand);
        UnfollowItem = new RelayCommand(UnfollowItemCommand);
        endpoints.RefreshEvent += (sender, args) =>
        {
            RefreshOccured = true;
        };
    }

    private void FollowItemCommand(object name) //String
    {
        _backendEndpoints.FollowItem((string)name);
        OnPropertyChanged(nameof(FollowedItems));
    }
    
    private void UnfollowItemCommand(object name) //String
    {
        _backendEndpoints.UnfollowItem((string)name);
        OnPropertyChanged(nameof(FollowedItems));
    }
    
    private bool _refreshOccured;
    public bool RefreshOccured
    {
        get => _refreshOccured;
        private set
        {
            _refreshOccured = value;
            OnPropertyChanged();
        }
    }
    
    private float _roi;
    public float ROI
    {
        get => _roi;
        set
        {
            _roi = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<MarketItem> FollowedItems { get; set; } = new();
    
    public MarketItem[] ItemMarket => _backendEndpoints.ItemMarket;

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}