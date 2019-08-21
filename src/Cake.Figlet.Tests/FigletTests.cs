using System;
using Shouldly;
using Xunit;

namespace Cake.Figlet.Tests
{
    public class FigletTests
    {
        [Fact]
        public void Figlet_can_render()
        {
            const string expected = @"
 _   _        _  _            __        __              _      _ 
| | | |  ___ | || |  ___      \ \      / /  ___   _ __ | |  __| |
| |_| | / _ \| || | / _ \      \ \ /\ / /  / _ \ | '__|| | / _` |
|  _  ||  __/| || || (_) | _    \ V  V /  | (_) || |   | || (_| |
|_| |_| \___||_||_| \___/ ( )    \_/\_/    \___/ |_|   |_| \__,_|
                          |/                                     
";
            FigletAliases.Figlet(null, "Hello, World").ShouldBeWithLeadingLineBreak(expected);
        }
    }

    public static class ShouldlyAsciiExtensions
    {
        /// <summary>
        /// Helper to allow the expected ASCII art to be on it's own line when declaring
        /// the expected value in the source code
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="expected">The expected.</param>
        public static void ShouldBeWithLeadingLineBreak(this string input, string expected)
        {
            (Environment.NewLine + input).ShouldBe(expected, StringCompareShould.IgnoreLineEndings);
        }
    }
}
