using UIFramework.Interfaces;

namespace UIFramework.Models;

public class ContainerComponent : IUIComponent
{
    public string Id { get; }
    private readonly List<IUIComponent> _children = new();

    public ContainerComponent(string id)
    {
        Id = id;
    }

    public IReadOnlyList<IUIComponent> Children => _children;

    public void AddChild(IUIComponent child)
    {
        if (ContainsId(child, Id))
        {
            throw new InvalidOperationException("Обнаружена циклическая ссылка при добавлении компонента");
        }

        if (FindById(child.Id) != null)
        {
            throw new InvalidOperationException($"Дублирование Id в поддереве: {child.Id}");
        }

        _children.Add(child);
    }

    public void RemoveChild(IUIComponent child)
    {
        _children.Remove(child);
    }

    public void Render()
    {
        Console.WriteLine($"Контейнер [{Id}]:");
        foreach (var child in _children)
        {
            child.Render();
        }
    }

    public IUIComponent? FindById(string id)
    {
        if (Id == id)
        {
            return this;
        }

        foreach (var child in _children)
        {
            var found = child.FindById(id);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    public ContainerComponent CloneDeep()
    {
        var copy = new ContainerComponent(Id);

        foreach (var child in _children)
        {
            if (child is ContainerComponent containerChild)
            {
                copy.AddChild(containerChild.CloneDeep());
            }
            else if (child is LeafComponent leafChild)
            {
                copy.AddChild(new LeafComponent(leafChild.Id, "клон"));
            }
        }

        return copy;
    }

    private bool ContainsId(IUIComponent node, string id)
    {
        if (node.Id == id)
        {
            return true;
        }

        if (node is ContainerComponent container)
        {
            foreach (var child in container.Children)
            {
                if (ContainsId(child, id))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
