namespace TicketToCode.Core.Services;
public class NavState
{
    public bool IsLoggedIn { get; private set; }
    public bool IsViewingAccount { get; private set; }

    public event Action? OnChange;

    public void SetLoggedIn(bool isLoggedIn)
    {
        IsLoggedIn = isLoggedIn;
        NotifyStateChanged();
    }

    public void SetViewingAccount(bool isViewingAccount)
    {
        IsViewingAccount = isViewingAccount;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}