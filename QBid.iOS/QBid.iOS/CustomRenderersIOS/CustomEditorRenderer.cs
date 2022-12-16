using UIKit;
using QBid.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using QBid.Controls;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace QBid.iOS.CustomRenderers
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    Control.Ended += Control_Ended1;
                }
            }
            catch (System.Exception ex)
            {
                
            }

        }

        private void Control_Ended1(object sender, System.EventArgs e)
        {
            try
            {
                var editor = sender as UITextView;
                
            }
            catch (System.Exception ex)
            {
                
            }
        }


    }
}