using System;
using System.Drawing;

namespace ds.test.impl
{
    public interface IPlugin
    {
        string PluginName { get; }
        string Version { get; }
        Image Image { get; }
        string Description { get; }
        int? Run(int input1, int input2);
    }

    internal class Plugin : IPlugin
    {
        internal readonly Func<int, int, int> runFunc;
        internal Plugin(string _pluginName, string _version, Image _image, string _description, Func<int, int, int> _runFunc)
        {
            PluginName = _pluginName;
            Version = _version;
            Image = _image;
            Description = _description;
            runFunc = _runFunc;
        }
        public string PluginName { get; set; }
        public string Version { get; set; }
        public Image Image { get; set; }
        public string Description { get; set; }
        public int? Run(int input1, int input2)
        {
            try
            {
                int? output = runFunc.Invoke(input1, input2);
                return output;
            }
            catch { return null; }
        }
    }
    internal abstract class SwapClass
    {
        public abstract void Swap<T>(ref T lhs, ref T rhs);

    }

    internal class SwapPlugin : SwapClass, IPlugin
    {
        internal readonly Func<int, int, int> runFunc;
        internal SwapPlugin(string _pluginName, string _version, Image _image, string _description, Func<int, int, int> _runFunc)
        {
            PluginName = _pluginName;
            Version = _version;
            Image = _image;
            Description = _description;
            runFunc = _runFunc;
        }
        public string PluginName { get; set; }
        public string Version { get; set; }
        public Image Image { get; set; }
        public string Description { get; set; }
        public override void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        public int? Run(int input1, int input2)
        {
            try
            {
                Swap(ref input1, ref input2);
                int? output = runFunc.Invoke(input1, input2);
                return output;
            }
            catch { return null; }
        }
    }
}