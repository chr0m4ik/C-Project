using UIFramework.Interfaces;
using UIFramework.Models;

namespace UIFramework.Integration;

public class DepthFirstIterator : IEnumerator<IUIComponent>
{
    private readonly Stack<IUIComponent> _stack = new();
    private IUIComponent? _current;

    public DepthFirstIterator(IUIComponent root)
    {
        _stack.Push(root);
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
        if (_stack.Count == 0)
        {
            _current = null;
            return false;
        }

        _current = _stack.Pop();

        if (_current is ContainerComponent container)
        {
            for (int i = container.Children.Count - 1; i >= 0; i--)
            {
                _stack.Push(container.Children[i]);
            }
        }

        return true;
    }

    public void Reset()
    {
        throw new NotSupportedException("Reset не поддерживается для этого итератора, создайте новый");
    }

    public void Dispose()
    {
        _stack.Clear();
    }
}
