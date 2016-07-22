using System;
using System.Text.RegularExpressions;

namespace Cake.Figlet
{
    internal class Figlet
    {
        private readonly FigletFont _font;

        public Figlet()
        {
            _font = new FigletFont();
        }

        public Figlet(string flfFontFile)
        {
            _font = new FigletFont(flfFontFile);
        }
        
        public string ToAsciiArt(string text)
        {
            var res = "";
            for (var i = 1; i <= _font.Height; i++)
            {
                foreach (var car in text)
                {
                    res += GetCharacter(car, i);
                }

                res += Environment.NewLine;
            }
            return res;
        }

        public string GetCharacter(char car, int line)
        {
            var start = _font.CommentLines + ((Convert.ToInt32(car) - 32) * _font.Height);
            var temp = _font.Lines[start + line];
            var lineending = temp[temp.Length - 1];

            temp = Regex.Replace(temp, @"\" + lineending + "{1,2}$", "");
            return temp.Replace(_font.HardBlank, " ");
        }
    }
}