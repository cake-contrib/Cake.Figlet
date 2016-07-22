using System.Diagnostics;
using Shouldly;
using Xunit;

namespace Cake.Figlet.Tests
{
    public class FigletTests
    {
        [Fact]
        public void Figlet_can_render()
        {
            var expected =
                @"
 _   _        _  _            __        __              _      _ 
| | | |  ___ | || |  ___      \ \      / /  ___   _ __ | |  __| |
| |_| | / _ \| || | / _ \      \ \ /\ / /  / _ \ | '__|| | / _` |
|  _  ||  __/| || || (_) | _    \ V  V /  | (_) || |   | || (_| |
|_| |_| \___||_||_| \___/ ( )    \_/\_/    \___/ |_|   |_| \__,_|
                          |/                                     
";
            FigletAliases.Figlet(null, "Hello, World").ShouldBeWithLeadingLineBreak(expected);
            Debug.WriteLine(FigletAliases.Figlet(null, "FIG"));
        }
    }

    public static class ShouldlyAsciiExtensions
    {
        public static void ShouldBeWithLeadingLineBreak(this string input, string expected)
        {
            ("\r\n" + input).ShouldBe(expected);
        }
    }
}
