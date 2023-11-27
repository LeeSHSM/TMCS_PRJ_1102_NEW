using System.Diagnostics;

namespace LshCamera
{
    internal class CameraManager
    {
        private CameraAmxServer _amxServer;
        private CameraDBManager _dbManager;

        List<ICamera> _cameras;

        public CameraManager()
        {
            _cameras = new List<ICamera>();
        }

        public void SetDBServer(CameraDBManager dbServer)
        {
            _dbManager = dbServer;
        }

        internal async Task SetCameraAsync(ICamera camera)
        {
            if (camera.Protocol is Visca cameraAction)
            {
                cameraAction.SetCameraId(camera.CameraId);
                cameraAction.SetAmxServer(_amxServer);
            }
            _cameras.Add(camera);
            camera.PresetGroup = await _dbManager.GetPresetAsync(camera);
        }

        public void SetAmxServer(CameraAmxServer amxServer)
        {
            _amxServer = amxServer;
        }

        public async Task CameraPanTilt(ICamera camera, int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            await camera.PanTiltAsync(panSpeed, tiltSpeed, panDir, tiltDir);
        }

        public async Task SavePreeset(ICamera camera, int preesetId)
        {
            if (camera == null)
            {
                return;
            }
            byte[] tat = await camera.SavePresetAsync();
            CameraPreset preset = camera.PresetGroup.Presets.FirstOrDefault(preset => preset.Presetid == preesetId);
            preset.Presetposition = tat;
            await _dbManager.SavePresetAsync(camera, preset);

            string tmp = BitConverter.ToString(tat, 0, 9);
            Debug.WriteLine(tmp);
        }

        public void LoadPreeset(ICamera camera, int preesetNum)
        {
            camera.LoadPresetAsync(preesetNum);
        }

        public async Task ChangePresetName(ICamera camera, int presetId, string presetName)
        {
            if(camera.PresetGroup == null)
            {
                return;
            }
            CameraPreset preset = camera.PresetGroup.Presets.FirstOrDefault(preset => preset.Presetid == presetId);
            preset.Presetname = presetName;
            await _dbManager.SavePresetAsync(camera, preset);
        }



    }


}
