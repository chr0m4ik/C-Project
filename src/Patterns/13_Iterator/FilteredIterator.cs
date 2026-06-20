using CrossPlatformUISimulator.Patterns.Composite;

namespace CrossPlatformUISimulator.Patterns.Iterator;

public class FilteredIterator : IEnumerator<IUIComponent>
{
    private readonly IEnumerator<IUIComponent> _innerIterator;
    private readonly Func<IUIComponent, bool> _predicate;
    private IUIComponent? _current;

    public FilteredIterator(IEnumerator<IUIComponent> innerIterator, Func<IUIComponent, bool> predicate)
    {
        _innerIterator = innerIterator;
        _predicate = predicate;
    }

    public IUIComponent Current
    {
        get
        {
            if (_current == null)
            {
                throw new InvalidOperationException("Обход не начат или уже завершён");
            }

            return _current;
        }
    }

    object System.Collections.IEnumerator.Current => Current;

    public bool MoveNext()
    {
        while (_innerIterator.MoveNext())
        {
            if (_predicate(_innerIterator.Current))
            {
                _current = _innerIterator.Current;
                return true;
            }
        }

        _current = null;
        return false;
    }

    public void Reset()
    {
        throw new NotSupportedException("Reset не поддерживается, создайте новый итератор");
    }

    public void Dispose()
    {
        _innerIterator.Dispose();
    }
}
