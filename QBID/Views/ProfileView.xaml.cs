using QBid.APILog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace QBid.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentPage
    {
       
        public ProfileView()
        {
            InitializeComponent();
            

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                VM.GetUserProfile();
        });
            
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                Device.BeginInvokeOnMainThread(() => {
                    UploadProfileBackground.IsVisible = false;
                    UploadProfile.IsVisible = false;
                });

            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            finally
            {

            }
        }

        

        private void TapGestureRecognizer_ShowDrawer(object sender, EventArgs e)
        {
                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                try
                {
                    Device.BeginInvokeOnMainThread(() => {
                        UploadProfileBackground.IsVisible = true;
                        UploadProfile.IsVisible = true;
                    });  

                }
            catch (Exception ex)
                {

                    LogManager.TraceErrorLog(ex);
                }
                finally
                {

                }

        }

        private void TapGestureRecognizer_CameraClick(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {
                    VM.CameraClick();
                });
                Device.BeginInvokeOnMainThread(() => {
                    UploadProfileBackground.IsVisible = false;
                    UploadProfile.IsVisible = false;
                });
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }

            finally
            {
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                    //Task.Delay(5000);

                //});
            }

        }
        private void TapGestureRecognizer_GalleryClick(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    VM.GalleryClick();
                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    UploadProfileBackground.IsVisible = false;
                    UploadProfile.IsVisible = false;
                });
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            finally
            {
              
            }            

        }

        private void TapGestureRecognizer_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {
                    VM.DeleteClick();
                });
                Device.BeginInvokeOnMainThread(() => {
                    UploadProfileBackground.IsVisible = false;
                    UploadProfile.IsVisible = false;
                });
             
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            finally
            {
             
            }
        }
        private void TapGestureRecognizer_HideDrawer(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {
                    UploadProfileBackground.IsVisible = false;
                    UploadProfile.IsVisible=false;
                });
               
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            finally
            {
           
            }

        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => {
                    UploadProfileBackground.IsVisible = false;
                    UploadProfile.IsVisible = false;
                });
            }
            catch (Exception ex)
            {

                LogManager.TraceErrorLog(ex);
            }
            finally
            {
         
            }

        }
    }
}