namespace EntLib5Sample.Commons
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ServiceLocation;

    public static class ConfigurationSourceBuilderExtensions
    {
        /// <summary>
        /// ConfigurationSourceBuilderの内容を元にIServiceProviderを作成します。
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static IServiceLocator CreateContainer(this ConfigurationSourceBuilder self)
        {
            var configuration = new DictionaryConfigurationSource();
            self.UpdateConfigurationWithReplace(configuration);
            return EnterpriseLibraryContainer.CreateDefaultContainer(configuration);
        }
    }
}
