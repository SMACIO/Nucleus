using NUglify;
using Nucleus.Minify.Styles;

namespace Nucleus.Minify.NUglify
{
    public class NUglifyCssMinifier : NUglifyMinifierBase, ICssMinifier
    {
        protected override UglifyResult UglifySource(string source, string fileName)
        {
            return Uglify.Css(source, fileName);
        }
    }
}

