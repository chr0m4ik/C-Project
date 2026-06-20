using System.Diagnostics;
using CrossPlatformUISimulator.Patterns.FactoryMethod;
using CrossPlatformUISimulator.Patterns.AbstractFactory;
using CrossPlatformUISimulator.Patterns.Builder;
using CrossPlatformUISimulator.Patterns.Prototype;
using CrossPlatformUISimulator.Patterns.Adapter;
using CrossPlatformUISimulator.Patterns.Singleton;
using CrossPlatformUISimulator.Patterns.Composite;
using CrossPlatformUISimulator.Patterns.Bridge;
using CrossPlatformUISimulator.Patterns.Decorator;
using CrossPlatformUISimulator.Patterns.Facade;
using CrossPlatformUISimulator.Patterns.ChainOfResponsibility;
using CrossPlatformUISimulator.Patterns.Command;
using CrossPlatformUISimulator.Patterns.Iterator;
using CrossPlatformUISimulator.Patterns.Interpreter;
using CrossPlatformUISimulator.Patterns.Mediator;
using CrossPlatformUISimulator.Patterns.Memento;
using CrossPlatformUISimulator.Patterns.Observer;
using CrossPlatformUISimulator.Patterns.State;
using CrossPlatformUISimulator.Patterns.Strategy;
using CrossPlatformUISimulator.Patterns.TemplateMethod;
using CrossPlatformUISimulator.Patterns.Visitor;

Console.WriteLine("=== Часть 1-2: Factory Method + Abstract Factory ===");

IWidgetFactory standardFactory = new StandardWidgetFactory();
var widget = standardFactory.CreateWidget(new WidgetConfig { Type = WidgetType.Button, Label = "Сохранить" });
widget.Draw();

IThemeFactory fluentTheme = new FluentThemeFactory();
var host = new ApplicationHost(fluentTheme, standardFactory);
host.BuildTestWindow();

Console.WriteLine();
Console.WriteLine("=== Часть 4-6: Builder + Prototype ===");

var builder = new DialogBuilder();
var director = new ErrorDialogDirector();
var dialog = director.Build(builder, fluentTheme.ThemeName);
dialog.Show();

var clonedDialog = dialog.Clone();
clonedDialog.Title = "Ошибка (клон)";
clonedDialog.Show();

var template = new ButtonTemplate { Text = "Шаблонная кнопка" };
template.Tags.Add("primary");
var clonedTemplate = template.Clone();
clonedTemplate.Text = "Изменённый клон";
template.Print();
clonedTemplate.Print();

Console.WriteLine();
Console.WriteLine("=== Часть 7-8: Adapter + Singleton ===");

var legacyEngine = new LegacyGraphicsEngine();
IModernRenderer adapter = new LegacyEngineAdapter(legacyEngine);
adapter.DrawButton(10, 20, 100, 30, "Legacy кнопка");
adapter.ShowDialog("Legacy окно");

ApplicationTelemetrySingleton.Instance.LogOperation("Demo", "Start");
ApplicationTelemetrySingleton.Instance.LogOperation("Demo", "Start");
var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
foreach (var pair in counts)
{
    Console.WriteLine($"Метрика: {pair.Key} = {pair.Value}");
}

Console.WriteLine();
Console.WriteLine("=== Часть 10-11: Composite + Bridge ===");

var root = new ContainerComponent("root");
var panel = new ContainerComponent("panel1");
panel.AddChild(new LeafComponent("button1", "Кнопка ОК"));
panel.AddChild(new LeafComponent("button2", "Кнопка Отмена"));
root.AddChild(panel);
root.Render();

var found = root.FindById("button2");
Console.WriteLine($"Найден компонент: {found?.Id}");

IRenderingStrategy rasterStrategy = new RasterRenderingStrategy();
var bridgeButton = new ButtonComponent("bridgeButton1", rasterStrategy);
bridgeButton.Render();
bridgeButton.SwitchRenderingStrategy(new VectorRenderingStrategy());
bridgeButton.Render();

Console.WriteLine();
Console.WriteLine("=== Часть 13-14: Decorator + Facade ===");

IUIComponent decoratedLeaf = new LeafComponent("decoratedButton", "Декорированная кнопка");
decoratedLeaf = new BorderDecorator(decoratedLeaf);
decoratedLeaf = new RenderLogDecorator(decoratedLeaf);
decoratedLeaf.Render();

var facade = new UISystemFacade(fluentTheme, ApplicationTelemetrySingleton.Instance);
var facadeDialog = facade.CreateDialog("Диалог через фасад");
facadeDialog.Show();
facade.LogCurrentMetrics();

Console.WriteLine();
Console.WriteLine("=== Часть 19-20: Chain of Responsibility + Command ===");

IUIEventHandler chain = new ValidationHandler();
chain.SetNext(new ThrottlingHandler(5, TimeSpan.FromSeconds(1)))
     .SetNext(new RoutingHandler())
     .SetNext(new FallbackHandler());

var sampleEvent = new UIEvent("Click", "button1", null, DateTime.Now);
chain.Handle(sampleEvent);

var movable = new MovableComponent("movableButton", 0, 0);
var commandManager = new CommandManager();
commandManager.Execute(new MoveComponentCommand(movable, 50, 60));
Console.WriteLine($"После перемещения: ({movable.X},{movable.Y})");
commandManager.Undo();
Console.WriteLine($"После отмены: ({movable.X},{movable.Y})");

Console.WriteLine();
Console.WriteLine("=== Часть 22-23: Iterator + Interpreter ===");

var iteratorFactory = new IteratorFactory();
var dfsIterator = iteratorFactory.CreateDfs(root);
while (dfsIterator.MoveNext())
{
    Console.WriteLine($"Обход: {dfsIterator.Current.Id}");
}

var parser = new DslParser();
var parsedExpression = parser.Parse("EXECUTE ApplyTheme('Fluent') -> SetPosition(10,20) -> Lock()");
var interpreterContext = new InterpreterContext();
parsedExpression.Interpret(interpreterContext);
foreach (var line in interpreterContext.Log)
{
    Console.WriteLine($"DSL: {line}");
}

Console.WriteLine();
Console.WriteLine("=== Часть 25-26: Mediator + Memento ===");

var mediator = new EventDrivenMediator();
var mediatorButton = new MediatorButton("medButton1", mediator);
var mediatorPanel = new MediatorPanel("medPanel1");
mediator.Register(mediatorButton);
mediator.Register(mediatorPanel);
mediatorButton.Click();

var window = new UIWindow { Title = "Окно 1", X = 10, Y = 10 };
var mementoManager = new UIMementoManager();
mementoManager.SaveCheckpoint("v1", window.CreateMemento());
window.X = 999;
Console.WriteLine($"После изменения: X={window.X}");
var checkpoint = mementoManager.GetCheckpoint("v1");
if (checkpoint != null)
{
    window.Restore(checkpoint);
}
Console.WriteLine($"После восстановления: X={window.X}");

Console.WriteLine();
Console.WriteLine("=== Часть 28-29: Observer + State ===");

var subject = new SafeObserverSubject();
subject.Attach(new TelemetryObserver());
subject.Attach(new ThemeSyncObserver());
subject.Notify("comp1", new UIStateChangeData("Visibility", "false", "true", DateTime.Now));

var statefulComponent = new StatefulComponent("statefulButton");
statefulComponent.Click();
statefulComponent.TransitionTo(new LoadingState());
statefulComponent.Click();
statefulComponent.TransitionTo(new NormalState());
statefulComponent.Click();

Console.WriteLine();
Console.WriteLine("=== Часть 31-32: Strategy + Template Method ===");

var layoutContainer = new LayoutContainer(new StackLayoutStrategy(), new List<string> { "a", "b", "c" });
var layoutContext = new LayoutContext(Padding: 5, Spacing: 10, AvailableWidth: 300);
var stackResult = layoutContainer.ApplyLayout(layoutContext);
foreach (var item in stackResult)
{
    Console.WriteLine($"Stack: {item.ComponentId} -> ({item.X},{item.Y})");
}

layoutContainer.SetLayoutStrategy(new GridLayoutStrategy(2));
var gridResult = layoutContainer.ApplyLayout(layoutContext);
foreach (var item in gridResult)
{
    Console.WriteLine($"Grid: {item.ComponentId} -> ({item.X},{item.Y})");
}

var lifecycle = new StandardComponentLifecycle();
lifecycle.ExecuteLifecycle(new UIContext { ComponentId = "lifecycleButton" });

Console.WriteLine();
Console.WriteLine("=== Финал: Visitor + Capstone ===");

var visitablePanel = new VisitablePanel("capstonePanel");
visitablePanel.Children.Add(new VisitableButton("capstoneButton1"));
visitablePanel.Children.Add(new VisitableButton("capstoneButton2"));
visitablePanel.Children.Add(new VisitableDialog("capstoneDialog1"));

var metricsVisitor = new MetricsCollectorVisitor();
visitablePanel.Accept(metricsVisitor);
Console.WriteLine($"Кнопок: {metricsVisitor.Report.ButtonCount}, диалогов: {metricsVisitor.Report.DialogCount}");

var accessibilityVisitor = new AccessibilityTreeVisitor();
visitablePanel.Accept(accessibilityVisitor);
foreach (var node in accessibilityVisitor.AccessibleNodes)
{
    Console.WriteLine($"Accessibility: {node}");
}

Console.WriteLine();
Console.WriteLine("=== Простой замер производительности (Stopwatch) ===");

var bigRoot = new ContainerComponent("bigRoot");
for (int i = 0; i < 1000; i++)
{
    bigRoot.AddChild(new LeafComponent($"leaf{i}", "узел"));
}

var watchDfs = Stopwatch.StartNew();
var bigIterator = iteratorFactory.CreateDfs(bigRoot);
int visitedCount = 0;
while (bigIterator.MoveNext())
{
    visitedCount++;
}
watchDfs.Stop();
Console.WriteLine($"Обошли {visitedCount} узлов за {watchDfs.ElapsedMilliseconds} мс (DFS итератор)");

var watchBuilder = Stopwatch.StartNew();
for (int i = 0; i < 1000; i++)
{
    var tempBuilder = new DialogBuilder();
    var tempDirector = new ErrorDialogDirector();
    tempDirector.Build(tempBuilder, "Fluent");
}
watchBuilder.Stop();
Console.WriteLine($"Создали 1000 диалогов через Builder за {watchBuilder.ElapsedMilliseconds} мс");

var watchClone = Stopwatch.StartNew();
var baseDialogForClone = director.Build(new DialogBuilder(), "Fluent");
for (int i = 0; i < 1000; i++)
{
    baseDialogForClone.Clone();
}
watchClone.Stop();
Console.WriteLine($"Создали 1000 диалогов через Clone за {watchClone.ElapsedMilliseconds} мс");

Console.WriteLine();
Console.WriteLine("Готово. Программа завершена.");
