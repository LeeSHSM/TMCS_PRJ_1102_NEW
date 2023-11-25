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
        Task SavePreset();
        void LoadPreset(byte[] presetPosition);

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
            if(AmxStream == null)
            {
                return;
            }
            
            byte bytePanSpeed = (byte)panSpeed;
            byte bytetiltSpeed = (byte)tiltSpeed;
            byte bytepanDirection = (byte)panDir;
            byte bytetiltDirection = (byte)tiltDir;

            byte[] command = new byte[] { cameraId, 0x01, 0x06, 0x01, bytePanSpeed, bytetiltSpeed, bytepanDirection, bytetiltDirection, 0xFF };
            AmxStream.Write(command, 0, command.Length);
        }

        public async Task SavePreset()
        {
            byte[] command = new byte[] { cameraId, 0x09, 0x06, 0x12, 0xFF };
            AmxStream.Write(command, 0, command.Length);
        }

        public void LoadPreset(byte[] persetPosition)
        {
            List<byte> message = new List<byte> { cameraId };
            message.AddRange(new byte[] { 0x01, 0x06, 0x02, 0x18, 0x18 });
            message.AddRange(persetPosition);
            message.AddRange(new byte[] { 0xFF });


            byte[] command = message.ToArray();
            AmxStream.Write(command, 0, command.Length);
        }
    }

    public class IpCamera : ICameraAction
    {
        NetworkStream AmxStream;

        public void LoadPreset()
        {
            throw new NotImplementedException();
        }

        public void LoadPreset(byte[] presetPosition)
        {
            throw new NotImplementedException();
        }

        public void PanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            throw new NotImplementedException();
        }

        public void SavePreset()
        {
            throw new NotImplementedException();
        }

        public void SetCameraId(int CameraId)
        {
            throw new NotImplementedException();
        }

        Task ICameraAction.SavePreset()
        {
            throw new NotImplementedException();
        }
    }
}
