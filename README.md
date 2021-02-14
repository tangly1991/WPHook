# WPHook

此扩展能够帮助你快速的构建一个和 WordPress 一致的钩子机制。（NEGUT版本暂时只支持.NET5，如需支持其他版本可自行下载修改）

## 关于

操作是你希望在代码的某些点执行的代码片段。操作永远不会返回任何东西，而只是作为一个选项来连接到现有代码。

过滤器用于修改实体。必须有返回值。默认返回第一个参数，你也可以自定义返回值。

[查看更多关于过滤器的信息](http://www.wpbeginner.com/glossary/filter/)


[查看更多关于操作的信息](http://www.wpbeginner.com/glossary/action/)

## 安装

通过NUGET安装 `Ame.WPHook`

在 `Startup.cs` `ConfigureServices()` 里添加如下代码

```C#
using Ame.WPHook;
//.....省略其他代码

services.HookSetup(Configuration);
```

## Usage

### 操作

你可以在你的代码的任意位置添加新操作，以控制器为例：

```C#
using Ame.WPHook;
//.....省略其他代码

private readonly IActionManager _actionManager;

public Controller(IActionManager actionManager)
{
    _actionManager = actionManager;
}

_actionManager.DoAction("my.hook", "awesome");
```

第一个参数为钩子的名称；添加监听钩子时使用。所有后续参数都作为方法参数传递给操作。参数为object类型。

添加操作监听器的最佳位置是中间件的 `InvokeAsync()` 方法：

```C#
using Ame.WPHook;
//.....省略其他代码

public Task InvokeAsync(HttpContext context, IActionManager actionManager)
{
    //.....
    actionManager.AddAction("my.hook", (args) =>
    {
        Console.WriteLine(args[0]);
    }, 20);
    //.....
}
```

`actionManager.AddAction()` 方法的接收参数依次为，操作名称，回调函数，优先级。优先级数字越小，执行越早。

### 过滤器

过滤器的工作方式与操作基本相同，其构建方式与操作完全相同。区别是过滤器必须有返回值。

添加一个过滤器:

```C#
using Ame.WPHook;
//.....省略其他代码

private readonly IFilterManager _filterManager;

public 构造方法(IFilterManager filterManager)
{
    _filterManager = filterManager;
}

var value = _filterManager.ApplyFilters("my.hook", "awesome");
```

如果没有针对这个钩子添加监听器，你们过滤器将简单的返回 `"awesome"`。

给这个过滤器添加监听器（仍以中间件为例）：

```C#
using Ame.WPHook;
//.....省略其他代码

public Task InvokeAsync(HttpContext context, IFilterManager filterManager)
{
    //.....
    filterManager.AddFilter("my.hook", (args) =>
    {
        return "not " + args[0];
    }, 20);
    //.....
}
```

过滤器将返回 `"not awesome"`。

你甚至可以将动作与过滤器结合在一起使用：

```C#
using Ame.WPHook;
//.....省略其他代码

public Task InvokeAsync(HttpContext context, IActionManager actionManager, IFilterManager filterManager)
{
    //.....
    actionManager.AddAction("my.hook", (args) =>
    {
        args[0] = filterManager.ApplyFilters("my.hook", "awesome");
        Console.WriteLine("You are " + args[0]);
    }, 20);
    //.....
}
```

##

如果您喜欢，请点个star，谢谢！
