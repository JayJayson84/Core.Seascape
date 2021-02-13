# Core.Seascape - Random image generator
![alt text](https://github.com/JayJayson84/Core.Seascape/blob/develop/Core.Seascape.UI/wwwroot/images/seascape-randomiser.png?raw=true)

### About
Seascape is a .Net Core 3.1 project that creates randomised computer generated images.

---

The original algorithm was coded in Visual Basic 6 and shared on a VBForums post titled '[The most amazing VB6 Code ever](https://www.vbforums.com/showthread.php?655280-The-most-amazing-VB6-Code-ever)' in 2011. It remained in my bookmarks until I recently took on the challenge of converting that source code into a C# library. A number of the hardcoded values have been reverse engineered for finer control over the colourscheme and artifacts such as the sun size etc.

### Technical Project Specification
- [x] ASP.Net Core 3.1 MVC
- [x] Dependency Injection
- [x] Model binding with AutoMapper
- [x] A typed layout model using generics which also allows viewmodels to be used indepently
- [x] An N-Layer project with a business logic layer to interact with the CGI service separately from the UI
- [x] jQuery Unobtrusive AJAX for POST'ing form data - [great tutorial here](https://www.learnrazorpages.com/razor-pages/ajax/unobtrusive-ajax)
- [x] IHtmlHelper extensions to render AJAX forms using the `@Html.BeginForm()` syntax
- [x] A custom HexColour structure for implicit and explicit type conversions from html colour codes to integers
- [x] ViewLocationFormats for cleaner file structuring of partials and views
- [x] DataAnnotations and EditorTemplates to keep on page logic separate from Views as much as possible
- [x] Gulp/Node/Webpack to compile, bundle and minify Javascript ES6 and SASS styles
- [x] SASS variables and mixins for breakpoints, media and transition selectors
- [x] Bootstrap 4 for responsive layout and native components such as the Tooltip
- [x] [Swiper](https://swiperjs.com/) which is an ES6 compatible touch enabled slider (though not compatible with IE)
- [x] SVG filters to apply the water animation effect - [great tutorial here](https://redstapler.co/realistic-water-effect-svg-turbulence-filter/)
- [x] Image compression for PNG images based on [NQuant](https://www.nuget.org/packages/nQuant/) (excludes the seascape image due to the loss of quality on gradients)
- [x] [Data URI's](https://css-tricks.com/data-uris/) to combine images into a single stylesheet and reduce the number of file requests
- [x] Unit tests to find the limitations of the CGI attributes to avoid overflow exceptions as much as possible

### Getting Started
1. Clone the branch
1. Open in Visual Studio
1. Restore Nuget packages
1. Using the Developer Powershell:
    1. Open in the **Core.Seascape.UI** project folder
    1. Run `npm i` to install node packages
1. Using the Task Runner Explorer window:
    1. Run the `default` gulp task to bundle, minify and watch for js and sass/css file changes
1. Build and run the project

### Hosting Reccomendations
- **Domain registration:** [Netistrar](https://netistrar.com/) for `.co.uk` and [Cloudflare](https://www.cloudflare.com/en-gb/) for `.com`
- **Content Delivery Network:** Cloudflare
- **DNS management:** Cloudflare DNS with proxy enabled
- **MX Records:** [Zoho Mail](https://www.zoho.com/mail/) for off-server domain e-mail hosting
- **SSL:** Cloudflare certificate with 15 year expiry
- **Web Hosting:** [eUKHost](https://www.eukhost.com/windows-hosting) shared Windows hosting with [Plesk](https://www.plesk.com/) management

### Useful Utilities/Resources
- [Affinity Designer](https://affinity.serif.com/en-gb/designer/): A reasonably priced and comprehensive image/vector editing tool
- [IcoFX](https://icofx.ro/): Powerful icon editor
- [SoureTree](https://www.sourcetreeapp.com/): Git workflow
- [Beyond Compare](https://www.scootersoftware.com/): Text/File comparison
- [Visual Studio Community Edition 2019](https://visualstudio.microsoft.com/downloads/): Development IDE
- [TinyPNG](https://tinypng.com/): Online tool for solid image compression that maintains gradients
- [FlashFXP](https://www.flashfxp.com/): FTP client
- [Pexels](https://www.pexels.com/): A free stock photography archive
- [TheHungryJpeg](https://thehungryjpeg.com/): A great archive for design assets
