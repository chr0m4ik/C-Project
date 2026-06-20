using UIFramework.Models;

namespace UIFramework.Interfaces;

public interface IWidgetFactory
{
    IWidget CreateWidget(WidgetConfig config);
}
