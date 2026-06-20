namespace CrossPlatformUISimulator.Patterns.Memento;

public interface IMemento
{
}

public interface IOriginator
{
    IMemento CreateMemento();
    void Restore(IMemento memento);
}
