using Banks.Clients;

namespace Banks.Banks;

public interface IObservableObject
{
    void AddObserverClient(IObserverObject client);
    void RemoveObserverClient(IObserverObject client);
    void NotifyClients();
}