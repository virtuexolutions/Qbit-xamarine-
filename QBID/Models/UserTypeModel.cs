using Xamarin.Forms;

namespace QBid.Models
{
    /// <summary>
    /// This class is used for address type
    /// </summary>
    public class UserTypeModel:BindableObject
    {
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
    }
}
