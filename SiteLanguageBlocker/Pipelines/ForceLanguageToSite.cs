namespace SiteLanguageBlocker.Pipelines
{
    using Sitecore.Pipelines.HttpRequest;

    // TODO: \App_Config\include\ForceLanguageToSite.config created automatically when creating ForceLanguageToSite class.

    public class ForceLanguageToSite : LanguageResolver
    {
        public override void Process(HttpRequestArgs args)
        {
            var forceLanguage = Sitecore.Context.Site.Properties["forceLanguage"];
            if (forceLanguage == null || !forceLanguage.Equals("true", System.StringComparison.InvariantCultureIgnoreCase))
            {
                base.Process(args);
            }
        }
    }
}