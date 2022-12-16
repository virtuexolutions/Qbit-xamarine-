using QBid.APILog;
using QBid.Helpers;
using QBid.Models;
using QBid.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OriginalFacilityServiceItems : ContentView
    {
       

        public OriginalFacilityServiceItems(string serviceItem, string originalPrice, string oEM, bool IsOemVisible, string negotiatedPrice, bool isAcceptedByMember, bool itemChecked, bool itemEnable, bool totalShow, string facilityId, string serviceiItemId)
        {
            InitializeComponent();
            try
            {
                isFirst = true;
                UserName.Text = serviceItem;
                Original_Price.Text = ConstantValues.CurencySymbal + originalPrice;
                OEM.Text = oEM;
                OEM.IsVisible = IsOemVisible;
                NegoName.Text = ConstantValues.CurencySymbal + negotiatedPrice;
                IsAcceptedByMember.IsChecked = itemChecked;
                IsAcceptedByMember.IsEnabled = itemEnable;
                IsAcceptedByMember.IsVisible = totalShow ? true : false;
                ServiceItemId.Text = serviceiItemId;
                QBidHelper.FacilityId = facilityId;
                isFirst = false;
            }
            catch(Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
        public bool isFirst = false;
       
        private void IsAcceptedByMember_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                if (!isFirst)
                {
                    var data = sender as CheckBox;
                    
                    SubmitServiceItem totaldata = new SubmitServiceItem();
                    if (IsAcceptedByMember.IsChecked)
                    {

                        QBidHelper.total += Convert.ToDecimal(Original_Price.Text.Replace(ConstantValues.CurencySymbal, string.Empty));
                        QBidHelper.negotiatedtotal += Convert.ToDecimal(NegoName.Text.Replace(ConstantValues.CurencySymbal, string.Empty));
                        if (Convert.ToDecimal(QBidHelper.negotiatedtotal) > 0)
                        {
                            QBidHelper.totalSaving = QBidHelper.total - QBidHelper.negotiatedtotal;
                            
                        }
                        else
                        {
                            QBidHelper.totalSaving = 0.00M;
                            
                        }
                        totaldata.TotalPrice = ConstantValues.CurencySymbal + Convert.ToString(QBidHelper.total);
                        totaldata.NegotiatedPrice = ConstantValues.CurencySymbal + Convert.ToString(QBidHelper.negotiatedtotal);
                        totaldata.TotalSaving = ConstantValues.CurencySymbal + Convert.ToString(QBidHelper.totalSaving);
                        QBidHelper.ServiceItemIdList.Add(Convert.ToString(ServiceItemId.Text));
                        totaldata.ServiceitemId = QBidHelper.ServiceItemIdList;

                        MessagingCenter.Send<SubmitServiceItem>(totaldata, "TotalAmount");
                    }
                    else
                    {
                        QBidHelper.total -= Convert.ToDecimal(Original_Price.Text.Replace(ConstantValues.CurencySymbal, string.Empty));
                        QBidHelper.negotiatedtotal -= Convert.ToDecimal(NegoName.Text.Replace(ConstantValues.CurencySymbal, string.Empty));
                        if (Convert.ToDecimal(QBidHelper.negotiatedtotal) > 0)
                        {
                            QBidHelper.totalSaving = QBidHelper.total - QBidHelper.negotiatedtotal;
                            
                        }
                        else
                        {
                            QBidHelper.totalSaving = 0.00M;
                            
                        }
                        totaldata.TotalPrice = ConstantValues.CurencySymbal + Convert.ToString(QBidHelper.total);
                        totaldata.NegotiatedPrice = ConstantValues.CurencySymbal + Convert.ToString(QBidHelper.negotiatedtotal);
                        totaldata.TotalSaving = ConstantValues.CurencySymbal + Convert.ToString(QBidHelper.totalSaving);
                        if (QBidHelper.ServiceItemIdList != null)
                        {
                            QBidHelper.ServiceItemIdList.Remove(ServiceItemId.Text);
                            totaldata.ServiceitemId = QBidHelper.ServiceItemIdList;
                        }
                        MessagingCenter.Send<SubmitServiceItem>(totaldata, "TotalAmount");
                    }
                }
            }
            catch(Exception ex)
            {
                LogManager.TraceErrorLog(ex);
            }
        }
    }
}