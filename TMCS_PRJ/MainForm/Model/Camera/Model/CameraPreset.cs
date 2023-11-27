namespace LshCamera
{
    public class CameraPreset
    {
        public event EventHandler PresetValueChanged;

        private int _presetId;
        private string _presetName;
        private byte[] _presetPosition;

        public int Presetid { get => _presetId; set => _presetId = value; }
        public string Presetname 
        { 
            get => _presetName;
            set
            {
                _presetName = value;
                PresetValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public byte[] Presetposition 
        { 
            get => _presetPosition; 
            set 
            {
                _presetPosition = value;
                PresetValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public class CameraPresetGroup
    {
        public event EventHandler PresetValueChanged;

        private int _cameraId;
        private List<CameraPreset> _presets;

        public int CameraId { get => _cameraId; set => _cameraId = value; }
        public List<CameraPreset> Presets 
        { 
            get => _presets; 
            set
            {
                _presets = value;            
            }
        }
    }



}
