using System;
using Joanne.Core;
using Xunit;

namespace Joanne.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Class()
        {
            var code = @"
class Program { }
";
            var expected = @"
var Program = (function() { // @class
    function Program() {
    }
    return Program;
}());
".TrimStart();
            var actual = JoanneCompiler.Compile(code);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Function()
        {
            var code = @"
class Program
{
    public static void Main() { }
}
";
            var expected = @"
var Program = (function() { // @class
    function Program() {
    }
    Program.Main = function() {
    };
    return Program;
}());
Program.Main();
".TrimStart();
            var actual = JoanneCompiler.Compile(code);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test1()
        {
            var code = @"
using System;

class Program
{
    public static void Main()
    {
        Console.WriteLine(""hoge"");
    }
}
".Trim();
            var actual = JoanneCompiler.Compile(code);

            var expected = @"
var Program = (function() {
    function Program() {
    }

    Program.Main = function() {
        console.log(""hoge"");
    }
    return Program;
}
());
Program.Main();".Trim();

            Assert.Equal(expected, actual);
        }
    }
}
