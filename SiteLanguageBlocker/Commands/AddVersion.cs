namespace SiteLanguageBlocker.Commands
{
    using Sitecore;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Shell.Framework.Commands;
    using Sitecore.Web.UI.Sheer;

    // TODO: \App_Config\include\AddVersion.config created automatically when creating AddVersion class. In this config include file, specify command name attribute value

    public class AddVersion : Sitecore.Shell.Framework.Commands.AddVersion
    {
        protected virtual void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            var id = args.Parameters["id"];
            var language = args.Parameters["language"];
            var version = args.Parameters["version"];
            var item = Context.ContentDatabase.Items[id, Language.Parse(language), Version.Parse(version)];
            if (item == null)
            {
                SheerResponse.Alert("Item not found");
                return;
            }

            if (Context.IsAdministrator || item.Access.CanWrite() && (item.Locking.CanLock() || item.Locking.HasLock()))
            {
                if (SheerResponse.CheckModified())
                {
                    Version[] versionNumbers = item.Versions.GetVersionNumbers(false);
                    Log.Audit(this, "Add version:{0}", AuditFormatter.FormatItem(item));
                    var newVersion = item.Versions.AddVersion();
                    if (newVersion != null)
                    {
                        Context.ClientPage.SendMessage(this,string.Format("item:versionadded(id={0},version={1},language={2})",newVersion.ID,newVersion.Version,newVersion.Language));
                    }

                }
            }
            else
            {
                SheerResponse.Alert("You don't have permissions to do this");
            }
        }

    }
}