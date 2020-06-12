using System;
using System.Collections.Generic;
using System.Linq;

namespace ds.test.impl
{
    public static class PluginsFactory
    {
        internal static List<IPlugin> pluginsList = new List<IPlugin>
        {
            new Plugin ("Addition", "0.1a", Lib.Properties.Resources.Addition, "Addition of two variables (+)", (input1, input2) => input1 + input2),
            new Plugin ("Subtraction", "0.1a", Lib.Properties.Resources.Subtraction, "Subtraction of two variables (-)", (input1, input2) => input1 - input2),
            new Plugin ("Multiplication", "0.1a", Lib.Properties.Resources.Multiplication, "Multiplication of two variables (*)", (input1, input2) => input1 * input2),
            new Plugin ("Division", "0.1a", Lib.Properties.Resources.Division, "Division of two variables (/)", (input1, input2) => input1 / input2),
            new Plugin ("Remainder", "0.1a", Lib.Properties.Resources.Remainder, "Remainder of division of two variables (%)", (input1, input2) => input1 % input2),
            new Plugin ("Logarithm", "0.1a", Lib.Properties.Resources.Logarithm, "Returns the logarithm of a specified number [X] in a specified base (A) (log(A)[X])", (input1, input2) => (int)Math.Log(input1,input2)),
            new Plugin ("Logical_AND", "0.1b", Lib.Properties.Resources.Logical_AND, "The & operator computes the bitwise logical AND of its operands (&)", (input1, input2) => input1 & input2),
            new Plugin ("Logical_XOR", "0.1b", Lib.Properties.Resources.Logical_XOR, "The ^ operator computes the bitwise logical exclusive OR of its operands (^)", (input1, input2) => input1 ^ input2),
            new Plugin ("Logical_OR", "0.1b", Lib.Properties.Resources.Logical_OR, "The | operator computes the bitwise logical OR of its operands (|)", (input1, input2) => input1 | input2),
            new Plugin ("Max", "0.1c", Lib.Properties.Resources.Max, "Returns the larger of two specified numbers (Max)", (input1, input2) => input1 >= input2?input1:input2),
            new Plugin ("Min", "0.1c", Lib.Properties.Resources.Min, "Returns the smaller of two numbers (Min)", (input1, input2) => input1 <= input2?input1:input2),
            new SwapPlugin ("SwapSubtraction", "0.2d", Lib.Properties.Resources.Subtraction, "Subtraction of two swapped variables (-)", (input1, input2) => input1 - input2),
            new SwapPlugin ("SwapDivision", "0.2d", Lib.Properties.Resources.Division, "Division of two swapped variables (/)", (input1, input2) => input1 / input2),
            new SwapPlugin ("SwapRemainder", "0.2d", Lib.Properties.Resources.Remainder, "Remainder of division of two swapped variables (%)", (input1, input2) => input1 % input2)
        };
        public static int PluginsCount => pluginsList.Count;
        public static string[] GetPluginNames => pluginsList.Select(p=>p.PluginName).ToArray();
        public static IPlugin GetPlugin(string _pluginName) =>
            pluginsList?.SingleOrDefault(p=>p.PluginName==_pluginName);
    }
}
