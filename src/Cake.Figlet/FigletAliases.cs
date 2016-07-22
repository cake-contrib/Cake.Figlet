using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Figlet
{
    /// <summary>
    /// File helper aliases.
    /// </summary>
    [CakeAliasCategory("Figlet")]
    public static class FigletAliases
    {
        /// <summary>
        /// Returns Ascii art for the specified text using the default font
        /// </summary>
        /// <returns>The file's text.</returns>
        /// <param name="context">The context.</param>
        /// <param name="text">The text to render as Ascii Art.</param>
        [CakeMethodAlias]
        public static string Figlet(this ICakeContext context, string text)
        {
            var figlet = new Figlet();
            return figlet.ToAsciiArt(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="text">The text to render as Ascii Art.</param>
        /// <param name="fontFile">The </param>
        /// <returns></returns>
        public static string Figlet(this ICakeContext context, string text, string fontFile)
        {
            var figlet = new Figlet(fontFile);
            return figlet.ToAsciiArt(text);
        }
    }
}
