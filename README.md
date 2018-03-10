# Xamarin Forms - ChatView

ChatView decides some things when working with chat such as reverse stack, long click on the cell, update the view.

## Images

![ScreenshotDroid](Screenshots/sample_android.png)![ScreenshotiOS](Screenshots/sample_ios.png)

## Getting Started

Install ChatView to core, android, ios projects.

Add namespace to page
```
xmlns:chatview="clr-namespace:ChatView.Shared;assembly=ChatView.Shared"
```
And use

```
<chatview:MessageListView 
      ItemsSource="{Binding List}"
      VerticalOptions="FillAndExpand"
      HorizontalOptions="FillAndExpand"/>
```

Add `MessageListViewRenderer.Initialize();` into `MainActivity` - `OnCreate` 

Add `MessageListViewRenderer.Initialize();` into `AppDelegate` - `FinishedLaunching`

## NuGet

https://www.nuget.org/packages/ChatView/0.9.0

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
