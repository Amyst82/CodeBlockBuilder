using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeTextBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTextBuilder b = new CodeTextBuilder() 
            { 
                new CodeTextLine("All the code is two tabs right", 0),
                "//Comment",
                new CodeTextBlock("if(true)")
                {
                   "return 0;"
                }
            };
            string a = b.Build(2);
            Console.WriteLine(a);
        }
    }
}
