namespace UIFramework.Interfaces;

public interface IMemento
{
}

public interface IOriginator
{
    IMemento CreateMemento();
    void Restore(IMemento memento);
}
