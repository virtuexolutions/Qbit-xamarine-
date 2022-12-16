using QBid.Models.APIResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QBid.Models;
using QBid.DependencyServices;
using QBid.APILog;
using QBid.QBidResource;
using QBid.Helpers;

namespace QBid.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OriginalFacilityNegotiatorView : ContentView
    {      
        public OriginalFacilityNegotiatorView(string serviceItem, string originalPrice, string oEM,bool isOEMVisible, bool isAcceptedByMember, int Index, bool isNegotiable, string negotiablePrice, bool IsNegotiatedPriceSubmitted,bool isMeamberApprovedPrice)
        {
            InitializeComponent();

            UserName.Text = serviceItem;
            Original_Price.Text = ConstantValues.CurencySymbal + originalPrice;
            OEM.Text = oEM;
          
            OEMHeader.IsVisible = isOEMVisible ? true : false;
            ServiceIndex.Text = Convert.ToString(Index);
            if (IsNegotiatedPriceSubmitted)
            {
                NPLable.Text = ConstantValues.CurencySymbal+negotiablePrice;
                NPLable.IsVisible = true;
                NPEntry.IsVisible = false;
            }
            else
            {
                NPLable.IsVisible = false;
                NPEntry.IsVisible = true;
            }
            if (isOEMVisible)
            {
                OEM.Text = oEM;
                OEM.IsVisible = true;
            }
            else
            {
                OEM.Text = string.Empty;
                OEM.IsVisible = false;
            }
            if (isNegotiable)
            {
                if (!string.IsNullOrEmpty(negotiablePrice))
                {
                    if (Convert.ToDecimal(originalPrice) < Convert.ToDecimal(negotiablePrice))
                    {
                        NPEntryError.Text = ResourceValues.LabelInvalidPrice;
                        NPEntryError.IsVisible = true;
                        NPEntry.Text = negotiablePrice;    
                    }
                    else
                    {
                        ManageServicePrice data = new ManageServicePrice();
                        NPEntryError.Text = string.Empty;
                        NPEntryError.IsVisible = false;
                        NPEntry.Text = negotiablePrice;
                        data.Index = Convert.ToInt32(ServiceIndex.Text);
                        data.Price = negotiablePrice;
                    }
                }
                else
                {
                    ManageServicePrice data = new ManageServicePrice();
                    
                    NPEntryError.IsVisible = true;
                    data.Index = Convert.ToInt32(ServiceIndex.Text);
                    data.Price = string.Empty;
                    MessagingCenter.Send<ManageServicePrice>(data, "ServicePrice");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DependencyService.Get<IToastMessage>().LongAlert(ResourceValues.VailidNegoPriceMessage);
                    });
                }
            }
          

            IsAcceptedByMember.Text = isAcceptedByMember ? ResourceValues.YesButtontext: ResourceValues.NoButtontext;
            IsAcceptedByMember.TextColor = isAcceptedByMember ? Color.Green : Color.Red;


            IsAcceptedByMemberHeader.IsVisible = isMeamberApprovedPrice;
            //IsAcceptedByMemberHeader.IsVisible = true;
        }

       
        private void NPEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ManageServicePrice data = new ManageServicePrice();
                if (!string.IsNullOrEmpty(e.NewTextValue) && !string.IsNullOrEmpty(Original_Price.Text))
                {                    
                    if (Convert.ToDecimal(Original_Price.Text.Replace(ConstantValues.CurencySymbal, string.Empty)) < Convert.ToDecimal(e.NewTextValue))
                    {
                        if (e.NewTextValue.Contains("."))
                        {
                            if (e.NewTextValue.Length - 1 - e.NewTextValue.IndexOf(".") > 2)
                            {
                                var s = e.NewTextValue.Substring(0, e.NewTextValue.IndexOf(".") + 2 + 1);
                                NPEntry.Text = s;
                                NPEntry.SelectionLength = s.Length;

                            }
                            else
                            {
                                NPEntryError.Text = ResourceValues.LabelInvalidPrice;
                                NPEntryError.IsVisible = true;
                                data.Index = Convert.ToInt32(ServiceIndex.Text);
                                data.Price = e.NewTextValue;
                                MessagingCenter.Send<ManageServicePrice>(data, "ServicePrice");
                            }
                        }                        
                        else
                        {
                            NPEntryError.Text = ResourceValues.LabelInvalidPrice;
                            NPEntryError.IsVisible = true;
                            data.Index = Convert.ToInt32(ServiceIndex.Text);
                            data.Price = e.NewTextValue;
                            MessagingCenter.Send<ManageServicePrice>(data, "ServicePrice");
                        }
                       
                    }
                    else if (e.NewTextValue.Contains("."))
                    {
                        if (e.NewTextValue.Length - 1 - e.NewTextValue.IndexOf(".") > 2)
                        {
                            var s = e.NewTextValue.Substring(0, e.NewTextValue.IndexOf(".") + 2 + 1);
                            NPEntry.Text = s;
                            NPEntry.SelectionLength = s.Length;
                           
                        }
                        else
                        {
                          
                            NPEntryError.Text = string.Empty;
                            NPEntryError.IsVisible = false;
                            data.Index = Convert.ToInt32(ServiceIndex.Text);
                            data.Price = e.NewTextValue;
                            MessagingCenter.Send<ManageServicePrice>(data, "ServicePrice");
                        }
                    }
                    else
                    {
                        NPEntryError.Text = string.Empty;
                        NPEntryError.IsVisible = false;
                        data.Index = Convert.ToInt32(ServiceIndex.Text);
                        data.Price = e.NewTextValue;
                        MessagingCenter.Send<ManageServicePrice>(data, "ServicePrice");
                    }
                }
                else
                {
                    NPEntryError.Text = "Enter price";
                    NPEntryError.IsVisible = true;
                    data.Index = Convert.ToInt32(ServiceIndex.Text);
                    data.Price = string.Empty;
                    MessagingCenter.Send<ManageServicePrice>(data, "ServicePrice");
                }
            }
            catch (Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
    }
}