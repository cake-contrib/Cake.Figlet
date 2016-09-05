using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Figlet
{
    /// <summary>
    /// Figlet aliases.
    /// </summary>
    [CakeAliasCategory("Figlet")]
    public static class FigletAliases
    {
        /// <summary>
        /// Returns ASCII art for the specified text using the default font.
        /// </summary>
        /// <example>
        /// <code>
        /// Setup(ctx =&gt; {
        ///    Information(Figlet("Cake.Figlet"));
        /// });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="text">The text to render as ASCII Art.</param>
        /// <returns>A string containing the ASCII art.</returns>
        [CakeMethodAlias]
        public static string Figlet(this ICakeContext context, string text)
        {
            var figlet = new Figlet();
            return figlet.ToAsciiArt(text);
        }

        /// <summary>
        /// Returns ASCII art for the specified text using
        /// a custom figlet font file.
        /// </summary>
        /// <example>
        /// <code>
        /// Setup(ctx =&gt; {
        ///     var fontFile = File("mini.flf");
        ///     Information(Figlet("Cake.Figlet", fontFile));
        /// });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="text">The text to render as ASCII Art.</param>
        /// <param name="fontFile">The Figlet font format file.</param>
        /// <returns>A string containing the ASCII art.</returns>
        public static string Figlet(this ICakeContext context, string text, FilePath fontFile)
        {
            var figlet = new Figlet(fontFile.FullPath);
            return figlet.ToAsciiArt(text);
        }
    }
}
