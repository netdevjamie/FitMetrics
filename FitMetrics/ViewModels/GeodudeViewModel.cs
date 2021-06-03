using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.ComponentModel;

using FMetrics;
using System.Threading;
using System.Threading.Tasks;

namespace FitMetrics.ViewModels
{
    public class GeodudeViewModel : BaseViewModel, INotifyPropertyChanged, IDisposable
    {
        CancellationTokenSource _cts = new CancellationTokenSource();

        string _latitude;
        public string Latitude { get => _latitude; set { _latitude = value; this.OnPropertyChanged(nameof(Latitude)); } }

        string _longitude;
        private bool disposedValue;

        public string Longitude { get => _longitude; set { _longitude = value; this.OnPropertyChanged(nameof(Longitude)); } }

        public string Error { get; set; }

        public GeodudeViewModel()
        {
            Title = "Gps";
            Console.WriteLine("Creating a geo vm");
            // HACK: very questionable coding
            //this.Init().Wait();

            //_ = Poll(500);
        }

        public void Startup()
        {
            this.Init().Wait();

            _ = Poll(500);
        }

        async Task Poll(int sleep)
        {
            if(_cts.IsCancellationRequested)
            {
                return;
            }

            await Task.Delay(sleep);

            if (_cts.IsCancellationRequested)
            {
                return;
            }

            await this.Init();

            _ = Poll(sleep);
        }

        async Task Init()
        {
            Console.Error.WriteLine("geo, dude?");
            try
            {

                var location = await MainThread.InvokeOnMainThreadAsync(Geolocation.GetLastKnownLocationAsync);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _cts.Cancel();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GeodudeViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
