using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICameraAction
    {
        Task PanAsync(int cameraId);
        Task TiltAsync();
        Task ZoomInAsync();
        Task ZoomOutAsync();
    }

    public class CameraToAmx_Visca : ICameraAction
    {
        private string cameraIpAddress;
        private int port;
        private HttpClient httpClient;

        public CameraToAmx_Visca(string ipAddress, int port = 80)
        {
            cameraIpAddress = ipAddress;
            this.port = port;
            httpClient = new HttpClient();
        }

        public async Task PanAsync(int cameraId)
        {
            Debug.WriteLine($"{cameraId}가 움직입니다~");
        }


        public async Task TiltAsync()
        {

        }

        public async Task ZoomInAsync()
        {

        }

        public async Task ZoomOutAsync()
        {

        }
    }

    public class IpCamera : ICameraAction
    {
        private string cameraIpAddress;
        private int port;
        private HttpClient httpClient;
        int pan = 50;
        int tilt = 50;

        public IpCamera(string ipAddress, int port = 80)
        {
            cameraIpAddress = ipAddress;
            this.port = port;
            httpClient = new HttpClient();
        }

        public async Task PanAsync(int cameraId)
        {
            string requestUri = $"http://{cameraIpAddress}:{port}/control?pan={pan}&tilt={tilt}";
            Debug.WriteLine("카메라 팬 움직임 시도중");
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Camera moved successfully.");
                }
                else
                {
                    Debug.WriteLine("Failed to move the camera.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task TiltAsync()
        {

        }

        public async Task ZoomInAsync()
        {

        }

        public async Task ZoomOutAsync()
        {

        }
    }



    //public class Serial : ICameraAction
    //{
    //    private SerialManager serialManager;

    //    public Serial(SerialManager serialManager)
    //    {
    //        this.serialManager = serialManager;
    //    }

    //    public async Task PanAsync(int cameraId)
    //    {

    //    }

    //    public async Task TiltAsync()
    //    {

    //    }

    //    public async Task ZoomInAsync()
    //    {

    //    }

    //    public async Task ZoomOutAsync()
    //    {

    //    }
    //}
}