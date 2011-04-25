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
        private bool canQuery = false;
        private GeoCoordinateWatcher geoWatcher;
        // Constructor
        public MainPage()
        {
            geoWatcher = new GeoCoordinateWatcher();
            //Don't enable geolocation tracking until the button gets pressed
            geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(GeoWatcherPositionChanged);
            InitializeComponent();
        }

        private void GeoWatcherPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Dispatcher.BeginInvoke(() =>
                                       {
                                           txtLat.Text = e.Position.Location.Latitude.ToString();
                                           txtLong.Text = e.Position.Location.Longitude.ToString();
                                           txtAlt.Text = e.Position.Location.Altitude.ToString();
                                       });
        }

        private void btnGetLocation_Click(object sender, RoutedEventArgs e)
        {
            geoWatcher.Start();   
        }

        private void btnStopLocation_Click(object sender, RoutedEventArgs e)
        {
            geoWatcher.Stop();
        }
    }
}