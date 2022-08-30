using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeTextBuilder
{
    /// <summary>
    /// _/_/_/¯(ツ)¯\_\_\_ Here goes a spider
    /// </summary>
    internal static class Util
    {
        /// <param name="Indent">Count of tabs to add.</param>
        /// <param name="Line">Line that will be supplemented.</param>
        /// <returns>The same line but with N tabs at the start.</returns>
        public static string Tab(int Indent, string Line)
        {
            string tabs = new string('\t', Indent);
            return tabs + Line;
        }
    }

    /// <summary>
    /// Base class with Indent property and Build string method.
    /// </summary>
    public abstract class CodeTextBuilderBase
    {
        /// <summary>
        /// Build method for base class.
        /// </summary>
        /// <param name="Indent">Count of tabs that will be added at the start of a line in derived class.</param>
        /// <returns>Redone line</returns>
        public abstract string Build(int Indent);
    }

    /// <summary>
    /// Code block. Represents any code block in real C# code surrounded with curly brackets. e.g if(true) { *here goes a code* }
    /// </summary>
    public class CodeTextBlock : CodeTextBuilderBase, IEnumerable
    {
        //Items of codeblock. Items might be either string or any class derived from CodeTextBuilderBase.
        List<object> Items = new List<object>();

        //Name of statement of codeblock. e.g if(true)
        string Name { get; set; } = "";
        /// <summary>
        /// Creates a CodeBlock instance with the statement of _name parameter.
        /// </summary>
        /// <param name="_name">Text to be used as statement text that goes before curly brackets. e.g. "if(true)"</param>
        public CodeTextBlock(string _name)
        {
            this.Name = _name;
        }

        //IEnumerable stuff
        public void Add(object line)
        {
            if (line is CodeTextBuilderBase || line is string)
                Items.Add(line);
        }
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Adds curly braces at the beggining and the end of items list and them combine all the items.
        /// </summary>
        /// <returns>Combined string with new lines as separator</returns>
        public override string Build(int _Indent)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i] is string)
                {
                    Items[i] = Util.Tab(_Indent + 1, (string)Items[i]);
                }
                if (Items[i] is CodeTextBuilderBase)
                {
                    Items[i] = (Items[i] as CodeTextBuilderBase).Build(_Indent + 1);
                }

            }
            Items.Insert(0, Util.Tab(_Indent, Name));
            Items.Insert(1, Util.Tab(_Indent, "{"));
            Items.Add(Util.Tab(_Indent, "}"));
            return string.Join("\n", Items);
        }
    }

    /// <summary>
    /// Just a line with variable indents at the start.
    /// </summary>
    public class CodeTextLine : CodeTextBuilderBase
    {
        /// <summary>
        /// Count of tabs to be added.
        /// </summary>
        public int Indent = -1;
        public string Line { get; set; } = String.Empty;
        /// <summary>
        /// Creates a line with some indents.
        /// </summary>
        /// <param name="line">¯\_(ツ)_/¯</param>
        /// <param name="indent">Counts of tabs that will be added. Default is 0. Use Build() if you set the indent different of zero.</param>
        public CodeTextLine(string line, int indent = -1)
        {
            this.Indent = indent;
            this.Line = line;
        }
        /// <summary>
        /// Build method.
        /// </summary>
        /// <param name="_Indent">Return with THAT count of tabs at the start of the line.</param>
        /// <returns>New line with N indents at the start.</returns>
        public override string Build(int _Indent)
        {
            return Util.Tab(_Indent, Line);
        }
        public string Build()
        {
            return Util.Tab(this.Indent, Line);
        }
    }

    /// <summary>
    /// Builder class. Parent of other CodeTextBuilderBase objects.
    /// </summary>
    public class CodeTextBuilder : IEnumerable
    {
        //Items of builder that will be built. Items might be either string or any class derived from CodeTextBuilderBase.
        List<object> Items = new List<object>();
        //Ienumerable stuff.
        public void Add(object line)
        {
            if (line is CodeTextBuilderBase || line is string)
                Items.Add(line);
        }
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        /// <summary>
        /// Build method. Parameter represents how many indents off the left side will be made.
        /// </summary>
        /// <param name="Indent">Base indent value that represents how many tabs off the left side will be made for the first level of elements in builder.</param>
        /// <returns>Formatted text.</returns>
        public string Build(int Indent = 0)
        {
            List<string> result = new List<string>();
            foreach (object line in Items)
            {
                if(line is CodeTextLine _line)
                {
                    if(_line.Indent > -1)
                        result.Add(_line.Build());
                }
                if (line is CodeTextBlock)
                {
                    result.Add((line as CodeTextBuilderBase).Build(Indent));
                }
                if (line is string)
                {
                    result.Add(Util.Tab(Indent, (string)line));
                }
            }
            return string.Join("\n", result);
        }

       
    }
}
