# Math-lib

##Введение

Проект при компиляции формирует файл-библиотеку с именем lib.dll

Namespace по умолчанию ds.test.impl

При подключении доступен интерфейс IPlugin:

```c#
public interface IPlugin
{
  string PluginName { get; }
  string Version { get; }
  System.Drawing.Image Image { get; }
  string Description { get; }
  int? Run (int input1, int input2);
}
```
Также доступен статический класс Plugins со свойствами и методом:

```c#
  int PluginsCount { get; }
  string[] GetPluginNames { get; }
  IPlugin GetPlugin (string pluginName);
```

##Архитектура
![Иллюстрация к проекту](https://github.com/jon/coolproject/raw/master/image/image.png)

![Image alt](https://github.com/{username}/{repository}/raw/{branch}/{path}/image.png)

{username} — ваш ник на ГитХабе;
{repository} — репозиторий где хранятся картинки;
{branch} — ветка репозитория;
{path} — путь к месту нахождения картинки.

Часть реализаций интерфейса IPlugin должна быть унаследована от закрытого абстрактного
класса

##Описание

Метод Run должен реализовывать различные математические функции (сложение умножение и
т.п. их количество зависит от Вашего желания помочь экипажу)
Библиотека должна работать стабильно, должны быть реализованы все необходимые проверки,
должно использоваться документирование, так чтобы у экипажа не возникло вопросов как
пользоваться данной библиотекой.

##Использование

