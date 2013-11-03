using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace Bardock.Utils.Web.Bundles
{
    public class MyScriptBundle : ScriptBundle
    {
        public MyScriptBundle(string virtualPath)
            : base(virtualPath)
        {
            this.Builder = new MyBundleBuilder();
        }
        public MyScriptBundle(string virtualPath, string cdnPath) : base(virtualPath, cdnPath) { }
    }

    public class MyBundleBuilder : IBundleBuilder
    {
        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            var content = new StringBuilder();
            foreach (var bundleFile in files)
            {
                var parser = new Microsoft.Ajax.Utilities.JSParser(Read(bundleFile));
                parser.Settings.AddRenamePair("delete", "___delete");
                parser.Settings.AddRenamePair("default", "___default");
                parser.Settings.AddRenamePair("class", "\"class\"");
                content.Append(parser.Parse(parser.Settings).ToCode());
                content.Append(";");
            }

            return content.ToString();
        }

        private string Read(BundleFile file)
        {
            using (var s = file.VirtualFile.Open())
            {
                using (var r = new StreamReader(s))
                {
                    return r.ReadToEnd();
                }
            }
        }
    }
}