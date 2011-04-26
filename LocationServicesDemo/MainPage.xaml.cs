using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;

namespace LocationServicesDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool canUseLocation = false;
        private readonly GeoCoordinateWatcher _geoWatcher;
        // Constructor
        public MainPage()
        {
            _geoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            //Don't enable geolocation tracking until the button gets pressed
            _geoWatcher.PositionChanged += GeoWatcherPositionChanged;
            InitializeComponent();
        }

        private void GeoWatcherPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var accuracy = e.Position.Location.HorizontalAccuracy;
            Dispatcher.BeginInvoke(() =>
                                       {
                                           txtAccuracy.Text = e.Position.Location.HorizontalAccuracy.ToString();
                                           txtLat.Text = e.Position.Location.Latitude.ToString();
                                           txtLong.Text = e.Position.Location.Longitude.ToString();
                                           txtAlt.Text = e.Position.Location.Altitude.ToString();
                                       });
        }

        private void btnGetLocation_Click(object sender, RoutedEventArgs e)
        {
            if(canUseLocation)
                _geoWatcher.Start();
        }

        private void btnStopLocation_Click(object sender, RoutedEventArgs e)
        {
            if (canUseLocation)
            _geoWatcher.Stop();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            Dispatcher.BeginInvoke(() =>
                                       {
                                           var result = MessageBox.Show(
                                               "This application uses your location. Do you wish to give it permission to use your location?",
                                               "User Location Data", MessageBoxButton.OKCancel);

                                           if(result == MessageBoxResult.Cancel)
                                           {
                                               MessageBox.Show(
                                                   "You realize that this app won't do anything now that you've declined location services, right?");
                                           }
                                           else
                                           {
                                               canUseLocation = true;
                                           }
                                       });
        }
    }
}