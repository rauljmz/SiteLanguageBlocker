using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Shell.Framework.Commands;

namespace SiteLanguageBlocker.Events
{
    public class StopCreationWrongLanguage
    {
        public void OnVersionRemoved(object sender, EventArgs args)
        {
            var sArgs = (SitecoreEventArgs)args;
      
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);           
            if(!IsLanguageAllowedForItem(item))
            {
                Sitecore.Context.ClientPage.ClientResponse.Alert("You can't create context in this language for this site");
                sArgs.Result.Cancel = true;
            }            
        }

        protected virtual bool IsLanguageAllowedForItem(Item item)
        {          
            var site = Sitecore.Sites.SiteManager.GetSites()
                .FirstOrDefault(s =>
                    "true".Equals(s.Properties["forceLanguage"], StringComparison.InvariantCultureIgnoreCase) &&
                    item.Paths.FullPath.StartsWith(s.Properties["rootPath"], StringComparison.InvariantCultureIgnoreCase));

            if (site != null)
            {
                return site.Properties["language"] == item.Language.ToString();
            }

            return true;
        }
    }
}
