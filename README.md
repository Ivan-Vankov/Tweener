# Tweener
[![Releases](https://img.shields.io/github/release/Ivan-Vankov/Tweener.svg)](https://github.com/Ivan-Vankov/Tweener/releases)

A [UniTask](https://github.com/Cysharp/UniTask)-based tweening library for Unity.

UniTask is an async/await implementation for Unity.
You can learn more about it [here](https://github.com/Cysharp/UniTask).

A basic tween looks like this:
```csharp
Tweener.Move(transform, targetPosition)
       .Duration(2)
       .Ease(EaseType.OUT_ELASTIC);
```
More examples can be seen in the [examples project](https://github.com/Ivan-Vankov/Tweener-Examples).

## Installation
Add the following two lines as dependencies in **Packages/manifest.json**:
```
"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.3.1",
"com.vaflov.tweener": "https://github.com/Ivan-Vankov/Tweener.git",
```
