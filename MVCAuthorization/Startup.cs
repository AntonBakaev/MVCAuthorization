using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCAuthorization.Startup))]
namespace MVCAuthorization
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
		}
	}
}
