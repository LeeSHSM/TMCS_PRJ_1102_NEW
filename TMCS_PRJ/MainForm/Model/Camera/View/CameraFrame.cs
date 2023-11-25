using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LshCamera
{
    public partial class CameraFrame : UserControl, ICamera
    {
        public event EventHandler CameraSelected;
        public event EventHandler CameraSelectedClear;

        private string _cameraName;
        private int _cameraId;

        private bool _selected = false;


        public ICameraAction _protocol;

        public CameraFrame()
        {
            InitializeComponent();
            InitializeEvent();
        }

        private void InitializeEvent()
        {
            picCamera.MouseUp += Camera_MouseUp;

        }

        public string CameraName
        {
            get { return _cameraName; }
            set
            {
                _cameraName = value;

            }
        }

        public int CameraId
        {
            get => _cameraId;
            set => _cameraId = value;
        }

        public ICameraAction Protocol
        {
            get => _protocol;
            set
            {
                _protocol = value;
            }
        }

        private void Camera_MouseUp(object sender, MouseEventArgs e)
        {            
            if(_selected)
            {
                ClearCameraSelect();
                CameraSelectedClear?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                CameraSelected?.Invoke(this, e);
                _selected = true;
                picCamera.BackgroundImage = TMCS_PRJ.Properties.Resources.cctvMouseOver;
            }
            Debug.WriteLine($"선택!{CameraId}");
        }

        public void ClearCameraSelect()
        {
            _selected = false;
            picCamera.BackgroundImage = TMCS_PRJ.Properties.Resources.cctv;
        }

        public void SetCameraId(int CameraId)
        {
            _cameraId = CameraId;
        }

        public void PanTilt(int panSpeed, int tiltSpeed, int panDir, int tiltDir)
        {
            _protocol.PanTilt(panSpeed, tiltSpeed, panDir, tiltDir);
        }

        public async Task SavePreset()
        {
            _protocol.SavePreset();
        }

        public async Task LoadPreset(byte[] presetPosition)
        {
            _protocol.LoadPreset(presetPosition);
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
