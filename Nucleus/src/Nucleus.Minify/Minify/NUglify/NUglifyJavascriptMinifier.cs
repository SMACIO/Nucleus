using NUglify;
using Nucleus.Minify.Scripts;

namespace Nucleus.Minify.NUglify
{
    public class NUglifyJavascriptMinifier : NUglifyMinifierBase, IJavascriptMinifier
    {
        protected override UglifyResult UglifySource(string source, string fileName)
        {
            return Uglify.Js(source, fileName);
        }
    }
}

