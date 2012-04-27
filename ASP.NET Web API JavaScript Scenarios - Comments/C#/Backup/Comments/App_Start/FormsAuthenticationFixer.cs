using System.Web;
using Comments.App_Start;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(FormsAuthenticationFixer), "Start")]
namespace Comments.App_Start
{
    public static class FormsAuthenticationFixer
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(AjaxFormsAuthenticationModule));
        }
    }
}