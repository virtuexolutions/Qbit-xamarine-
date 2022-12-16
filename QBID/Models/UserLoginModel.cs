using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QBid.Models
{
   public class UserLoginModel : BindableObject
    {
        private string email;
        /// <summary>
        /// Property for User Email
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                email = value;               
                OnPropertyChanged(nameof(Email));
            }
        }
        private string password;
        /// <summary>
        /// Property for User Password
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
