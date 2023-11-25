using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public interface ICameraAction
    {

        void SetCameraId(int CameraId);

        void PanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir);

        void SavePreset();
        void LoadPreset();

    }

    public class Visca : ICameraAction
    {
        NetworkStream AmxStream;

        int _cameraId;
        byte cameraId;

        public void SetAmxServer(NetworkStream amxStream)
        {
            AmxStream = amxStream;
        }

        public void SetCameraId(int CameraId)
        {
            _cameraId = CameraId;
            cameraId = (byte)(128 + _cameraId);
        }

        public void PanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            byte bytePanSpeed = (byte)panSpeed;
            byte bytetiltSpeed = (byte)tiltSpeed;
            byte bytepanDirection = (byte)panDir;
            byte bytetiltDirection = (byte)tiltDir;

            byte[] command = new byte[] { cameraId, 0x01, 0x06, 0x01, bytePanSpeed, bytetiltSpeed, bytepanDirection, bytetiltDirection, 0xFF };
            AmxStream.Write(command, 0, command.Length);
        }

        public void SavePreset()
        {
            throw new NotImplementedException();
        }

        public void LoadPreset()
        {
            throw new NotImplementedException();
        }
    }
}
