# **Web Farm Management** #
## Introduction ##

Web farm management utilities add basic functionality to support the following operations when hosting an application on a web farm:
  * Provides a load balancer health check that can be manually and programatically changed on a specific server for a specific application.
  * Provides a appliaction offline functionality which can put all the servers for a specific application offline simultaniously.  This is very useful when sites need to go offline so support database schema changes.
  * Provides a module for keeping specific urls using SSL and all others using http.  This helps with bandwith costs and keeps a consistent environment that can prevent ugly warning dialogs for images that are not ssl on a page that was never meant to be served via ssl
  * Provides a cache management page which allows specific entires in the Asp.Net api cache to be removed manually and programatically.
  * Provides a module which inserts a cache dependency for Asp.Net output cache. When this module is used with the cache management page, the management of cached content can be controlled on a string to support advanced deployment and content refresh scenarios.
  * Provides a version page which extracts the Assembly version from the main executing assembly.  This is useful in an automated deployment where a deployment script needs to verify it has deployed the application correctly.
  * Provides a page that lists all the assemblies loaded in the application domain and their assembly/product versions.
  * Provides a page to override the forced ssl pages.  This is useful when using an automated testing tool to run synthetic transactions / smoke tests against a web application to verify a deployment has worked correctly.

## Details ##

Add your content here.  Format your content with:
  * Text in **bold** or _italic_
  * Headings, paragraphs, and lists
  * Automatic links to other wiki pages