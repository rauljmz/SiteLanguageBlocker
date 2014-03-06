Site Language Blocker
=================

This plugin recognises a new attribute on site definitions in the web.config: forceLanguage. It set to true, it will disable the language switching functionality with ?sc_lang and it will stop authors from creating content in any language other than the one specified in the language attribute of the site definition.

Caveats: 
* If the browser had a cookie with the language setting it is still enforced.
* Language prepending in the URL is still enforced (you must set languageEmbedding to false in the linkmanager).
* AddVersion button code is overwritten as there is a current bug, where if you cancel the AddingVersion event, it throws a null ref exception.