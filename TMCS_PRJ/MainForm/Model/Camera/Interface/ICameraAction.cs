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
        Task PanTiltAsync(int panSpeed, int tiltSpeed, int panDir, int tiltDir);
        Task<byte[]> SavePresetAsync();
        Task LoadPresetAsync(byte[] presetPosition);

    }

    public class Visca : ICameraAction
    {
        CameraAmxServer _amxServer;

        int _cameraId;
        byte cameraId;

        public void SetAmxServer(CameraAmxServer amxServer)
        {
            _amxServer = amxServer;
        }

        public void SetCameraId(int CameraId)
        {
            _cameraId = CameraId;
        }

        public async Task PanTiltAsync(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            await _amxServer.PanTiltAsync(_cameraId, panSpeed, tiltSpeed, panDir, tiltDir);            
        }

        public async Task<byte[]> SavePresetAsync()
        {
            byte[] position = await _amxServer.SavePresetAsync(_cameraId);
            return position;
        }

        public async Task LoadPresetAsync(byte[] presetPosition)
        {
            await _amxServer.LoadPresetAsync(_cameraId, presetPosition);
        }
    }

    public class IpCamera : ICameraAction
    {
        NetworkStream AmxStream;

        public Task LoadPresetAsync(byte[] presetPosition)
        {
            throw new NotImplementedException();
        }

        public Task PanTiltAsync(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> SavePresetAsync()
        {
            throw new NotImplementedException();
        }

        public void SetCameraId(int CameraId)
        {
            throw new NotImplementedException();
        }
    }
}
