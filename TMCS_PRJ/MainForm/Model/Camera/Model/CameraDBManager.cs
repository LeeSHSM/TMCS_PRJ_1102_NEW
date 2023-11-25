using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LshCamera
{
    public class CameraDBManager
    {
        private List<CameraPreset> _cameraPresets = new List<CameraPreset>();

        private string _connectString;

        public string ConnectString { get => _connectString; set => _connectString = value; }
        

        public void SavePreset(ICamera camera,int presetId, byte[] presetPosition)
        {
            var existingPreset = _cameraPresets.FirstOrDefault(p => p.Cameraid == camera.CameraId);

            if (existingPreset != null)
            {
                // 기존 프리셋이 있으면, 프리셋 업데이트
                existingPreset.Preset[presetId] = presetPosition;
            }
            else
            {
                _cameraPresets.Add(new CameraPreset { Cameraid = camera.CameraId, Preset = new Dictionary<int, byte[]>() { [presetId] = presetPosition } });
            }
        }

        public byte[] GetPreset(ICamera camera,int presetId)
        {
            var cameraPreset = _cameraPresets.FirstOrDefault(p => p.Cameraid == camera.CameraId);

            if (cameraPreset != null)
            {
                // 프리셋 딕셔너리에서 프리셋 ID에 해당하는 데이터 반환
                if (cameraPreset.Preset.TryGetValue(presetId, out byte[] presetData))
                {
                    return presetData;
                }
            }

            // 프리셋이 없으면 null 반환 (또는 필요에 따라 다른 기본값)
            return null;

        }




    }
}
