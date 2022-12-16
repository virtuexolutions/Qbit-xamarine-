using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QBid.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomLoaderWait : ContentView
    {
        public CustomLoaderWait()
        {
            InitializeComponent();
            IsVisible = false;
            activityIndicator.IsRunning = false;
        }

        /// <summary>
        /// IsVisibleAndIsRunningProperty 
        /// </summary>
        public static readonly BindableProperty IsVisibleAndIsRunningProperty =
          BindableProperty.Create(
              nameof(IsVisibleAndIsRunning),
              typeof(bool),
              typeof(bool),
              false);
        /// <summary>
        /// IsVisibleAndIsRunning
        /// </summary>
        public bool IsVisibleAndIsRunning
        {
            get { return (bool)GetValue(IsVisibleAndIsRunningProperty); }
            set { SetValue(IsVisibleAndIsRunningProperty, value); }
        }
        /// <summary>
        ///Override OnPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsVisibleAndIsRunningProperty.PropertyName)
            {
                IsVisible = IsVisibleAndIsRunning;
                activityIndicator.IsRunning = IsVisibleAndIsRunning;
            }
        }
    }
}