//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("QBid.Views.CardDetailView.xaml", "Views/CardDetailView.xaml", typeof(global::QBid.Views.CardDetailView))]

namespace QBid.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views/CardDetailView.xaml")]
    public partial class CardDetailView : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::QBid.ViewModels.CardDetailViewModel VM;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::QBid.Behaviors.CreaditCardMask CreditCard;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::QBid.Behaviors.CardExpiryDateMask ExpiryDate;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(CardDetailView));
            VM = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::QBid.ViewModels.CardDetailViewModel>(this, "VM");
            CreditCard = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::QBid.Behaviors.CreaditCardMask>(this, "CreditCard");
            ExpiryDate = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::QBid.Behaviors.CardExpiryDateMask>(this, "ExpiryDate");
        }
    }
}
