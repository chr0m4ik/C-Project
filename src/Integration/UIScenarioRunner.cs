using System.Diagnostics;
using UIFramework.Interfaces;
using UIFramework.Models;
using UIFramework.Builders;
using UIFramework.Directors;
using UIFramework.Factories;
using UIFramework.Decorators;
using UIFramework.Strategies;
using UIFramework.Commands;
using UIFramework.Visitors;
using UIFramework.States;
using UIFramework.Observers;

namespace UIFramework.Integration;

public class UIScenarioRunner
{
    public void RunFactoryAndAbstractFactoryDemo()
    {
        Console.WriteLine("=== Часть 1-2: Factory Method + Abstract Factory ===");

        IWidgetFactory standardFactory = new StandardWidgetFactory();
        var widget = standardFactory.CreateWidget(new WidgetConfig { Type = WidgetType.Button, Label = "Сохранить" });
        widget.Draw();

        IThemeFactory fluentTheme = new FluentThemeFactory();
        var host = new ApplicationHost(fluentTheme, standardFactory);
        host.BuildTestWindow();
    }

    public void RunBuilderAndPrototypeDemo()
    {
        Console.WriteLine();
        Console.WriteLine("=== Часть 4-6: Builder + Prototype ===");

        var builder = new DialogBuilder();
        var director = new ErrorDialogDirector();
        var dialog = director.Build(builder, "Fluent");
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
    }

    public void RunAdapterAndSingletonDemo()
    {
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
    }

    public void RunCompositeAndBridgeDemo()
    {
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
    }

    public void RunDecoratorAndFacadeDemo()
    {
        Console.WriteLine();
        Console.WriteLine("=== Часть 13-14: Decorator + Facade ===");

        IUIComponent decoratedLeaf = new LeafComponent("decoratedButton", "Декорированная кнопка");
        decoratedLeaf = new BorderDecorator(decoratedLeaf);
        decoratedLeaf = new RenderLogDecorator(decoratedLeaf);
        decoratedLeaf.Render();

        var facade = new UISystemFacade(new FluentThemeFactory(), ApplicationTelemetrySingleton.Instance);
        var facadeDialog = facade.CreateDialog("Диалог через фасад");
        facadeDialog.Show();
        facade.LogCurrentMetrics();
    }

    public void RunChainAndCommandDemo()
    {
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
    }

    public void RunIteratorAndInterpreterDemo()
    {
        Console.WriteLine();
        Console.WriteLine("=== Часть 22-23: Iterator + Interpreter ===");

        var root = new ContainerComponent("root2");
        root.AddChild(new LeafComponent("a", "A"));
        root.AddChild(new LeafComponent("b", "B"));

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
    }

    public void RunMediatorAndMementoDemo()
    {
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
    }

    public void RunObserverAndStateDemo()
    {
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
    }

    public void RunStrategyAndTemplateMethodDemo()
    {
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
    }

    public void RunVisitorAndCapstoneDemo()
    {
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
    }

    public void RunSimplePerformanceBenchmarks()
    {
        Console.WriteLine();
        Console.WriteLine("=== Простой замер производительности (Stopwatch) ===");

        var bigRoot = new ContainerComponent("bigRoot");
        for (int i = 0; i < 1000; i++)
        {
            bigRoot.AddChild(new LeafComponent($"leaf{i}", "узел"));
        }

        var iteratorFactory = new IteratorFactory();
        var watchDfs = Stopwatch.StartNew();
        var bigIterator = iteratorFactory.CreateDfs(bigRoot);
        int visitedCount = 0;
        while (bigIterator.MoveNext())
        {
            visitedCount++;
        }
        watchDfs.Stop();
        Console.WriteLine($"Обошли {visitedCount} узлов за {watchDfs.ElapsedMilliseconds} мс (DFS итератор)");

        var director = new ErrorDialogDirector();

        var watchBuilder = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            var tempBuilder = new DialogBuilder();
            director.Build(tempBuilder, "Fluent");
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
    }

    public void RunAll()
    {
        RunFactoryAndAbstractFactoryDemo();
        RunBuilderAndPrototypeDemo();
        RunAdapterAndSingletonDemo();
        RunCompositeAndBridgeDemo();
        RunDecoratorAndFacadeDemo();
        RunChainAndCommandDemo();
        RunIteratorAndInterpreterDemo();
        RunMediatorAndMementoDemo();
        RunObserverAndStateDemo();
        RunStrategyAndTemplateMethodDemo();
        RunVisitorAndCapstoneDemo();
        RunSimplePerformanceBenchmarks();

        Console.WriteLine();
        Console.WriteLine("Готово. Программа завершена.");
    }
}
