namespace CrossPlatformUISimulator.Patterns.Builder;

public interface IDialogBuilder
{
    IDialogBuilder SetTitle(string title);
    IDialogBuilder AddButton(ButtonConfig config);
    IDialogBuilder SetIcon(IconSource source);
    IDialogBuilder SetTheme(string themeName);
    IDialogBuilder SetHasCheckBox(bool value);
    Dialog Build();
}

public class DialogBuilder : IDialogBuilder
{
    private string _title = "";
    private IconSource? _icon;
    private readonly List<ButtonConfig> _buttons = new();
    private bool _hasCheckBox;
    private string _themeName = "";

    public IDialogBuilder SetTitle(string title)
    {
        _title = title;
        return this;
    }

    public IDialogBuilder AddButton(ButtonConfig config)
    {
        _buttons.Add(config);
        return this;
    }

    public IDialogBuilder SetIcon(IconSource source)
    {
        _icon = source;
        return this;
    }

    public IDialogBuilder SetTheme(string themeName)
    {
        _themeName = themeName;
        return this;
    }

    public IDialogBuilder SetHasCheckBox(bool value)
    {
        _hasCheckBox = value;
        return this;
    }

    public Dialog Build()
    {
        Validate();

        return new Dialog
        {
            Title = _title,
            Icon = _icon,
            Buttons = new List<ButtonConfig>(_buttons),
            HasCheckBox = _hasCheckBox,
            ThemeName = _themeName
        };
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(_title))
        {
            throw new InvalidOperationException("Диалог не может быть собран без заголовка");
        }

        if (_buttons.Count == 0)
        {
            throw new InvalidOperationException("Диалог не может быть собран без хотя бы одной кнопки");
        }

        if (string.IsNullOrWhiteSpace(_themeName))
        {
            throw new InvalidOperationException("Диалог не может быть собран без указанной темы");
        }
    }
}
