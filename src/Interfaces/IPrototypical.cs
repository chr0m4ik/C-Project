namespace UIFramework.Interfaces;

public interface IPrototypical<T> where T : class
{
    T Clone();
}
