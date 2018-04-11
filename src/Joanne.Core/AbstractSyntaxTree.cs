using System.Collections.Generic;

namespace Joanne.Core
{
    public class Class
    {
        public Class(string name, IEnumerable<Function> functions)
        {
            Name = name;
            Functions = functions;
        }

        public string Name { get; }

        public IEnumerable<Function> Functions { get; }
    }

    public enum Accesibility
    {
        None,
        Public,
        Private
    }

    public class FunctionDeclaration
    {
        public FunctionDeclaration(Accesibility accesibility, bool isStatic, string name)
        {
            Accesibility = accesibility;
            IsStatic = isStatic;
            Name = name;
        }

        public Accesibility Accesibility { get; }
        public bool IsStatic { get; }
        public string Name { get; }
    }

    public class Function
    {
        public Function(FunctionDeclaration functionDeclaration)
        {
            FunctionDeclaration = functionDeclaration;
        }
        
        public FunctionDeclaration FunctionDeclaration { get; }
    }
}