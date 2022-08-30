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
                new CodeTextBlock("if(true)")
                {
                   "return 0;"
                }
            };
            Console.WriteLine(b.Build(0));
        }
    }
}
