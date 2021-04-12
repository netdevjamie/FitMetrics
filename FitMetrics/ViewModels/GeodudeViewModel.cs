using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.ComponentModel;

using FMetrics;
using System.Threading.Tasks;

namespace FitMetrics.ViewModels
{
    public class GeodudeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Error { get; set; }

        public GeodudeViewModel()
        {
            Title = "Gps";
            Console.WriteLine("Creating a geo vm");
            // HACK: very questionable coding
            this.Init().Wait();
        }

        async Task Init()
        {
            Console.Error.WriteLine("geo, dude?");
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    Console.Error.WriteLine("No geo, dude");
                }
                else
                {
                    Latitude = location.Latitude.ToString();
                    Longitude = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Error = nameof(FeatureNotSupportedException).Before("Exception");
                // demonstrate a sample C# logging call
                Console.Error.WriteLine($"{nameof(FeatureNotSupportedException)}:{fnsEx.Message}");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Error = nameof(FeatureNotEnabledException).Before("Exception");
                // demonstrate calling the F# logger
                FMetrics.BReusable.Logging.logEx(fneEx);
                //Console.Error.WriteLine($"{nameof(FeatureNotEnabledException)}:{fneEx.Message}");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Error = nameof(PermissionException).Before("Exception");
                FMetrics.BReusable.Logging.logEx(pEx);

            }
            catch (Exception ex)
            {
                // Unable to get location
                Error = ex.Message;
                FMetrics.BReusable.Logging.logEx(ex);
            }
        }
    }
}
