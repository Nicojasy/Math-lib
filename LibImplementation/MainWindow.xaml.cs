using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ds.test.impl;

namespace LibImplementation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class CustomPlugin : IPlugin
        {
            public readonly Func<int, int, int> runFunc;
            internal CustomPlugin(string _pluginName, string _version, System.Drawing.Image _image, string _description, Func<int, int, int> _runFunc)
            {
                PluginName = _pluginName;
                Version = _version;
                Image = _image;
                Description = _description;
                runFunc = _runFunc;
            }
            public string PluginName { get; set; }
            public string Version { get; set; }
            public System.Drawing.Image Image { get; set; }
            public string Description { get; set; }
            public int? Run(int input1, int input2)
            {
                return runFunc.Invoke(input1, input2);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            //(new CustomPlugin() { "min","0.1",null, ""1minus2"", null});
            TextBox1.Text += $"Count Plugins: {PluginsFactory.PluginsCount}\n";
            TextBox1.Text += "Names:\n";
            foreach (string name in PluginsFactory.GetPluginNames)
                TextBox1.Text += $">{name}\n";
            TextBox1.Text += "IPlugin:\n";
            foreach (var name in PluginsFactory.GetPluginNames)
            {
                IPlugin plugin = PluginsFactory.GetPlugin(name);
                if (plugin == null) continue;
                //int? op = plugin.Run(int.MinValue, int.MinValue);
                //int? op = plugin.Run(int.MaxValue, int.MaxValue);
                int? op = plugin.Run(5, 0);
                //int? op = plugin.Run(0, 5);
                //int? op = plugin.Run(2, 5);
                TextBox1.Text += $"{plugin.PluginName}> {plugin.Description}> {plugin.Version}> {(op!=null?op.ToString():"Null")}\n";
                try
                {
                    using var ms = new MemoryStream();
                    if (plugin.Image != null)
                    {
                        var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                        bitmap.BeginInit();
                        System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                        plugin.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
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
    }
}
