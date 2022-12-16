using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Helpers
{
    /// <summary>
    /// This class is use to get resource file field name and show on view.
    /// </summary>
    [ContentProperty(ConstantValues.AppResourceText)]
    public class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = ConstantValues.AppResourceLocation;
        public string Text { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);
            return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }
    }
}