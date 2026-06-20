namespace UIFramework.Integration;

public class ParseException : Exception
{
    public ParseException(string message) : base(message)
    {
    }
}

public class DslParser
{
    public IExpression Parse(string script)
    {
        if (string.IsNullOrWhiteSpace(script))
        {
            throw new ParseException("Скрипт пуст");
        }

        var withoutExecute = script.Replace("EXECUTE", "").Trim();
        var parts = withoutExecute.Split("->");
        var actions = new List<IExpression>();

        foreach (var part in parts)
        {
            actions.Add(ParseSingleAction(part.Trim()));
        }

        return new ChainExpression(actions);
    }

    private IExpression ParseSingleAction(string action)
    {
        if (action.StartsWith("ApplyTheme"))
        {
            var themeName = ExtractStringArgument(action);
            return new ApplyThemeExpression(themeName);
        }

        if (action.StartsWith("SetPosition"))
        {
            var numbers = ExtractNumberArguments(action);
            return new SetPositionExpression(numbers[0], numbers[1]);
        }

        if (action.StartsWith("Lock"))
        {
            return new LockExpression();
        }

        throw new ParseException($"Неизвестное действие: {action}");
    }

    private string ExtractStringArgument(string action)
    {
        var start = action.IndexOf('\'');
        var end = action.LastIndexOf('\'');

        if (start == -1 || end == -1 || start == end)
        {
            throw new ParseException($"Не найден строковый аргумент в: {action}");
        }

        return action.Substring(start + 1, end - start - 1);
    }

    private int[] ExtractNumberArguments(string action)
    {
        var start = action.IndexOf('(');
        var end = action.IndexOf(')');

        if (start == -1 || end == -1)
        {
            throw new ParseException($"Не найдены аргументы в: {action}");
        }

        var inside = action.Substring(start + 1, end - start - 1);
        var parts = inside.Split(',');

        var numbers = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            numbers[i] = int.Parse(parts[i].Trim());
        }

        return numbers;
    }
}
