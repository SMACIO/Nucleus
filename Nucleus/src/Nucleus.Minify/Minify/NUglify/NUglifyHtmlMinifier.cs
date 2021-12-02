using NUglify;
using Nucleus.Minify.Html;

namespace Nucleus.Minify.NUglify
{
    public class NUglifyHtmlMinifier : NUglifyMinifierBase, IHtmlMinifier
    {
        protected override UglifyResult UglifySource(string source, string fileName)
        {
            return Uglify.Html(source, sourceFileName: fileName);
        }
    }
}

