# Math-lib

## Введение

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

## Архитектура

Рассмотрим cхему классов внутри проекта на UML-диаграмме: 

![Image alt](https://github.com/Nicojasy/Math-lib/raw/master/uml_diagram/diagram.png)

Как видно на диаграмме интерфейс IPlugin реализуется двумя классами: Plugin и SwapPlugin. SwapPlugin дополнительно реализует абстрактный класс SwapClass. Plugins в своей реализации создаёт список с объектами классов Plugin и SwapPlugin.

IPlugin и PluginFactory доступны, остальные имеют модификатор доступа internal.

Plugin реализуют метод Run, который выполняет математические операции. Метод Run в SwapPlugin сперва меняет переменные местами, а затем выполняет математическую операцию. Обмен переменных реализована в методе Swap, который унаследован из SwapClass.

## Описание

### IPlugin

IPlugin включает в себя: <br>
| Элемент  | Название | Описание |
| :------- | :------- | :------- |
| Свойство | `string PluginName` | Возвращает название |
| Свойство | `string Version` | Возвращает версию |
| Свойство | `string PluginName` | Возвращает название |
| Свойство | `string Version` | Возвращает версию |
| Свойство | `Image Image` | Возвращает изображение (расширение .bmp, размер 50x50) |
| Свойство | `string Description` | Возвращает описание |
| Метод | `int? Run(int input1, int input2)` | Математическая операция с 2 входными значениями <br> в виде целочисленных значений. Возвращает целочисленное <br> значение, которое может быть Nullable|

### PluginFactory

PluginFactory содержит свойства: <br>
| Элемент  | Название | Описание |
| :------- | :------- | :------- |
| Свойство | `int PluginsCount` | Возвращает кол-во встроенных математических операций |
| Свойство | `string[] GetPluginNames` | Возвращает массив из названий всех возможных математических операций |
| Метод | `IPlugin GetPlugin(string pluginName)` | Возвращает математическую операцию, название которой <br> совпадает с переданным в метод значением |

PluginFactory содержит в себе список основных математических операций, которые можно использовать при подключении. Вот список операций, где a и b соответствуют передаваемому в метод первому и второму значениям:
| Название | Описание |
| :------- | :------- |
| Addition | Сложение a с b |
| Subtraction | Вычитание b из а |
| Multiplication | Умножение a на b |
| Division | Деление a на b |
| Remainder | Остаток от деления a на b |
| Logarithm | Логарифм от a при основании b |
| Logical_AND | Побитовое умножение a на b |
| Logical_XOR | Логическое исключающее ИЛИ для a и b |
| Logical_OR | Побитовое сложение a и b |
| Max | Находим большее из a и b |
| Min | Находим меньшее из a и b |
| SwapSubtraction | Вычитание a из b |
| SwapDivision | Деление b на a |
| SwapRemainder | Остаток от деления b на a |

При передаче значений, результат математической операции которых не может быть расчитан (деление на 0, переполнение и т.п.), вернётся Nullable значение.

## Использование

Проверим работоспособность с помощью WPF проекта.

Добавим следующий код в MainWindow.xaml внутри тега Grid: <br>
```xaml
<Image x:Name="Image1" HorizontalAlignment="Left" Height="95" Margin="38,202,0,0" VerticalAlignment="Top" Width="90"/>
<TextBox x:Name="TextBox1" Text="TextBox" TextWrapping="Wrap" Margin="157,10,10,10"/>
<Button Content="Button" HorizontalAlignment="Left" Margin="10,10,0,0" VerticalAlignment="Top" Width="142" Click="Button_Click" Height="137"/>
```

Проверим подключённые библиотеки в MainWindow.xaml.cs:
```c#
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ds.test.impl;
```

В этом же файле объявим метод Button_Click: <br>
```c#
private async void Button_Click(object sender, RoutedEventArgs e)
{
  //PluginsCount
  TextBox1.Text += $"Count Plugins: {PluginsFactory.PluginsCount}\n";
  TextBox1.Text += "Names:\n";
  //GetPluginNames
  foreach (string name in PluginsFactory.GetPluginNames)
    TextBox1.Text += $">{name}\n";
  TextBox1.Text += "IPlugin:\n";
  //enumeration of all mathematical functions 
  foreach (var name in PluginsFactory.GetPluginNames)
  {
    IPlugin plugin = PluginsFactory.GetPlugin(name);
    if (plugin == null) continue;
    int? op = plugin.Run(2, 5);
    TextBox1.Text += $"{plugin.PluginName}> {plugin.Description}> {plugin.Version}> {(op != null ? op.ToString() : "Null")}\n";
    //Convert System.Drawing.Imaging to System.Windows.Media.Imaging.BitmapImage
    try
    {
      using var ms = new MemoryStream();
      if (plugin.Image != null)
      {
        var bitmap = new System.Windows.Media.Imaging.BitmapImage();
        bitmap.BeginInit();
        MemoryStream memoryStream = new MemoryStream();
        plugin.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
        memoryStream.Seek(0, SeekOrigin.Begin);
        bitmap.StreamSource = memoryStream;
        bitmap.EndInit();
        Image1.Source = bitmap;
      }
    }
    catch
    {
      TextBox1.Text += "Image upload failed";
    }
    await Task.Delay(1000);
  }
TextBox1.Text += "Good bye!";
}
```
