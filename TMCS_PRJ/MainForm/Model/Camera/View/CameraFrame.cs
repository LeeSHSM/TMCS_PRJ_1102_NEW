using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private string _cameraName;
        private int _cameraId;
        public ICameraAction _protocol;

        public CameraFrame()
        {
            InitializeComponent();
        } 

        public string CameraName
        {
            get { return _cameraName; }
            set
            {
                _cameraName = value;
                lblName.Text = _cameraName;
            }
        }

        public int CameraId { get => _cameraId; set => _cameraId = value; }

        public ICameraAction Protocol 
        {
            get => _protocol;
            set 
            {
                _protocol = value;
                //_protocol.SetCameraId(CameraId);
            }
        }        

        private void Camera_ViscaType_MouseUp(object sender, MouseEventArgs e)
        {
            CameraSelected?.Invoke(this, e);
        }

        public void SetCameraId(int CameraId)
        {
            _cameraId = CameraId;
        }

        public void PanStart(int speed, int Direction)
        {
            _protocol.PanStart(speed, Direction);
        }

        public void PanStop()
        {
            _protocol.PanStop();
        }

        public void TiltStart(int speed, int Direction)
        {
            _protocol.TiltStart(speed, Direction);
        }

        public void TiltStop()
        {
            _protocol.TiltStop();
        }

        public void PanTilt(int PanSpeed, int tiltSpeed, int panDirection, int tiltDirection)
        {
            _protocol.PanTilt(PanSpeed, tiltSpeed, panDirection, tiltDirection);
        }

        public void PanTiltStop()
        {
            _protocol.PanStop();
        }

        public void SetCameraActionConnectInfo(string serverIp, int serverPort)
        {
            throw new NotImplementedException();
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
