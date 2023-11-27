using System.ComponentModel;
using System.Diagnostics;

namespace LshCamera
{
    public partial class CameraFrame : UserControl, ICamera
    {
        public event EventHandler CameraSelected;
        public event EventHandler CameraSelectedClear;

        public CameraFrame()
        {
            InitializeComponent();
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            picCamera.MouseUp += Camera_MouseUp;
        }

        private string _cameraName;
        private int _cameraId;
        private CameraPresetGroup _presetGroup;
        private ICameraAction _protocol;

        private bool _selected = false;

        public string CameraName
        {
            get => _cameraName;
            set => _cameraName = value;
        }

        public int CameraId
        {
            get => _cameraId;
            set => _cameraId = value;
        }

        public CameraPresetGroup PresetGroup
        {
            get => _presetGroup;
            set => _presetGroup = value;
        }

        public ICameraAction Protocol
        {
            get => _protocol;
            set => _protocol = value;
        }

        public void ClearCameraSelect()
        {
            _selected = false;
            picCamera.BackgroundImage = TMCS_PRJ.Properties.Resources.cctv;
        }

        private void SetCameraSelect()
        {
            _selected = true;
            picCamera.BackgroundImage = TMCS_PRJ.Properties.Resources.cctvMouseOver;
        }


        public void SetCameraId(int CameraId)
        {
            _cameraId = CameraId;
        }

        public async Task PanTiltAsync(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            await _protocol.PanTiltAsync(panSpeed, tiltSpeed, panDir, tiltDir);
        }

        public async Task<byte[]> SavePresetAsync()
        {
            return await _protocol.SavePresetAsync();
        }
        public async Task LoadPresetAsync(int presetNum)
        {
            try
            {
                CameraPreset preset = PresetGroup.Presets.FirstOrDefault(p => p.Presetid == presetNum);

                if (preset != null)
                {
                    await _protocol.LoadPresetAsync(preset);
                }
                else
                {
                    // preset이 null일 때의 처리를 추가할 수 있습니다.
                    Debug.WriteLine("Preset not found.");
                }
            }
            catch (Exception ex)
            {
                // 예외가 발생한 경우의 처리를 여기에 추가할 수 있습니다.
                Debug.WriteLine("An error occurred: " + ex.Message);
            }
        }


        private void Camera_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selected)
            {
                ClearCameraSelect();
                CameraSelectedClear?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                SetCameraSelect();
                CameraSelected?.Invoke(this, EventArgs.Empty);
            }
        }


    }

    public class MyInterfaceTypeConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<ICameraAction> instances = new List<ICameraAction>
        {
            new Visca(),
            new Visca(),
            // 다른 ICameraProtocol 구현 추가
        };

            return new StandardValuesCollection(instances);
        }
    }
}
