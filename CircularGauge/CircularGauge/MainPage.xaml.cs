using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;
//using Syncfusion.XForms.PopupLayout;
using System.Windows.Input;

namespace Jafaria
{
    public partial class Compass : ContentPage, INotifyPropertyChanged
    {
        double pointerValue = 0;

        public double PointerValue
        {
            get { return pointerValue; }

            set
            {
                pointerValue = value;
                NotifyPropertyChanged("PointerValue");
            }
        }

        private void scale_LabelCreated(object sender, Syncfusion.SfGauge.XForms.LabelCreatedEventArgs args)
        {
            switch ((string)args.LabelContent)
            {
                case "0":
                    args.LabelContent = "N";
                    break;
                case "45":
                    args.LabelContent = "NE";
                    break;
                case "90":
                    args.LabelContent = "E";
                    break;
                case "135":
                    args.LabelContent = "SE";
                    break;
                case "180":
                    args.LabelContent = "S";
                    break;
                case "225":
                    args.LabelContent = "SW";
                    break;
                case "270":
                    args.LabelContent = "W";
                    break;
                case "315":
                    args.LabelContent = "NW";
                    break;
                case "360":
                    args.LabelContent = "N";
                    break;
            }
        }


        public double Latitude = 20.593683;
        public double Longitude = 78.962883;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public class CompassLocations
        {
            public string Location { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public Compass()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetBackButtonTitle(this, "");
            this.BindingContext = this;
          ConvertLatitudeLongitudeToDegree(0, 0, Latitude, Longitude);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ConvertLatitudeLongitudeToDegree(0, 0, Latitude, Longitude);
        }

        async void btnCompassOtherLocations_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Places", "Ok", null, "India","Pakistan", "Australia");
            switch (action)
            {
                case "India":
                    LabelLocationName.Text = "Country : India";
                    LabelLocationLatitude.Text = "Latitide : 20.593683";
                    LabelLocationLogitude.Text = "Logitude : 78.962883";
                    ConvertLatitudeLongitudeToDegree(0, 0, Latitude, Longitude);
                    break;
                case "Pakistan":
                    LabelLocationName.Text = "Country : Pakistan";
                    LabelLocationLatitude.Text = "Latitude : 30.375320";
                    LabelLocationLogitude.Text = "Longitude : 69.345116";
                    ConvertLatitudeLongitudeToDegree(0, 0, 30.375320, 69.345116);
                    break;
                case "Australia":
                    LabelLocationName.Text = "Country : Australia";
                    LabelLocationLatitude.Text = "Latitude : -25.274399";
                    LabelLocationLogitude.Text = "Longitude : 133.775131";

                    ConvertLatitudeLongitudeToDegree(0, 0, -25.274399, 133.775131);
                    break;
                default:
                    break;
            }
        }

        void ConvertLatitudeLongitudeToDegree(double lat1, double long1, double lat2, double long2)
        {
            var y = Math.Sin(this.degreeToRadian(long2 - long1)) * Math.Cos(this.degreeToRadian(lat2));
            var x = Math.Cos(this.degreeToRadian(lat1)) * Math.Sin(this.degreeToRadian(lat2)) - Math.Sin(this.degreeToRadian(lat1)) * Math.Cos(this.degreeToRadian(lat2)) * Math.Cos(this.degreeToRadian(long2 - long1));
            var tempValue = 0.0;

            tempValue = Math.Atan2(y, x);
            tempValue = this.radianToDegree(tempValue);

            if (tempValue < 0) tempValue = 360 + tempValue;

            PointerValue = tempValue;
        }

        public double degreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public double radianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
    }
}
