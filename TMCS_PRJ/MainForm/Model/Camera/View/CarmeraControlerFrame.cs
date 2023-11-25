using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LshCamera
{
    public partial class CarmeraControlerFrame : UserControl, ICameraControler
    {
        public event EventHandler testBtn;
        public event ICameraControler.delCameraPanTilt CameraPanTilt;

        public CarmeraControlerFrame()
        {
            InitializeComponent();
            InitializeEvent();
            Initialize();

            userSpeed = 25 - scrollbarPanTiltSpeed.Value;
            _panSpeed = userSpeed;
            _tiltSpeed = userSpeed;
            lblPanTiltSpeed.Text = userSpeed.ToString();
        }

        private void Initialize()
        {
            pnPreeset.AutoScroll = true;
            //AddControls(10);
        }

        private const int ControlSpacing = 7;
        private const int ControlHeight = 25;

        private void AddControls(int numberOfControls)
        {
            int yPos = ControlSpacing;

            for (int i = 0; i < numberOfControls; i++)
            {
                Label label = new Label
                {
                    Text = $"프리셋{i + 1}",
                    Location = new Point(ControlSpacing, yPos),
                    Size = new Size(100, ControlHeight)
                };
                pnPreeset.Controls.Add(label);

                Button button = new Button
                {
                    Text = "실행",
                    Location = new Point(label.Width + 20, yPos - 4),
                    Size = new Size(75, ControlHeight)
                };
                pnPreeset.Controls.Add(button);
                Button button2 = new Button
                {
                    Text = "저장",
                    Location = new Point(200, yPos - 4),
                    Size = new Size(75, ControlHeight)
                };
                // 필요한 경우 button.Click += new EventHandler(Button_Click);
                pnPreeset.Controls.Add(button2);

                yPos += ControlHeight + ControlSpacing;
            }
        }


        private void vScrollBar_Scroll(object? sender, ScrollEventArgs e)
        {
            pnPreeset.Top = -e.NewValue;
        }

        private int userSpeed;

        /// <summary>
        /// 1 ~ 24
        /// </summary>
        private int _panSpeed;

        /// <summary>
        /// 1 ~ 24
        /// </summary>
        private int _tiltSpeed;

        /// <summary>
        /// 1 : Left / 2 : Right / 3 : Stop
        /// </summary>
        private int _panDir = 3;

        /// <summary>
        /// 1 : Up / 2 : Down / 3 : Stop
        /// </summary>
        private int _tiltDir = 3;

        private bool isUpPressed = false;
        private bool isDownPressed = false;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isControlKeyPressed = false;

        private void InitializeEvent()
        {
            btnRight.MouseDown += BtnArrow_MouseDown;
            btnLeft.MouseDown += BtnArrow_MouseDown;
            btnUp.MouseDown += BtnArrow_MouseDown;
            btnDown.MouseDown += BtnArrow_MouseDown;

            btnRight.MouseUp += BtnArrow_MouseUp;
            btnLeft.MouseUp += BtnArrow_MouseUp;
            btnUp.MouseUp += BtnArrow_MouseUp;
            btnDown.MouseUp += BtnArrow_MouseUp;

            this.KeyUp += CarmeraControlerFrame_KeyUp;
            btnDown.KeyUp += CarmeraControlerFrame_KeyUp;
            btnLeft.KeyUp += CarmeraControlerFrame_KeyUp;
            btnRight.KeyUp += CarmeraControlerFrame_KeyUp;
            btnUp.KeyUp += CarmeraControlerFrame_KeyUp;

            scrollbarPanTiltSpeed.ValueChanged += ScrollbarPanTiltSpeed_ValueChanged;
            tblArrow.Resize += TableLayoutPanel2_Resize;
        }

        private void TableLayoutPanel2_Resize(object? sender, EventArgs e)
        {
            this.tblArrow.ColumnStyles[0].Width = this.tblArrow.Height;
        }

        private void ScrollbarPanTiltSpeed_ValueChanged(object? sender, EventArgs e)
        {
            userSpeed = 25 - scrollbarPanTiltSpeed.Value;
            _panSpeed = userSpeed;
            _tiltSpeed = userSpeed;
            lblPanTiltSpeed.Text = userSpeed.ToString();
        }

        public void SelectedCamera()
        {
            this.Focus();
        }

        public void EndKeyEvent()
        {
            FindForm().Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Control) == Keys.Control && !IsKeyPressed(Keys.ControlKey))
            {
                Debug.WriteLine("??");
                SetKeyPressed(Keys.ControlKey, true);
                _panSpeed = userSpeed - 8;
                _tiltSpeed = userSpeed - 8;
                StartCameraPanTilt();
            }

            // 개별 키 처리
            if ((keyData & Keys.Right) == Keys.Right || (keyData & Keys.Left) == Keys.Left || (keyData & Keys.Up) == Keys.Up || (keyData & Keys.Down) == Keys.Down)
            {
                bool containsValue1 = (keyData & Keys.Left) == Keys.Left;
                bool containsValue2 = (keyData & Keys.Up) == Keys.Up;
                bool containsValue3 = (keyData & Keys.Right) == Keys.Right;
                bool containsValue4 = (keyData & Keys.Down) == Keys.Down;

                int arrow = 0; // 1 : 좌 / 2: 상 / 3 : 우 / 4 : 하
                if (containsValue1 && !containsValue2 && !containsValue3 && !containsValue4 && !IsKeyPressed(Keys.Left))
                {
                    SetKeyPressed(Keys.Left, true);
                    arrow = 1;
                }
                else if (containsValue2 && !containsValue1 && !containsValue3 && !containsValue4 && !IsKeyPressed(Keys.Up))
                {
                    SetKeyPressed(Keys.Up, true);
                    arrow = 2;
                }
                else if (containsValue3 && containsValue1 && containsValue2 && !containsValue4 && !IsKeyPressed(Keys.Right))
                {
                    SetKeyPressed(Keys.Right, true);
                    arrow = 3;
                }
                else if (containsValue4 && !containsValue1 && !containsValue2 && !containsValue3 && !IsKeyPressed(Keys.Down))
                {
                    SetKeyPressed(Keys.Down, true);
                    arrow = 4;
                }

                switch (arrow)
                {
                    case 0:
                        break;
                    case 1:
                        PanLeft();
                        break;
                    case 2:
                        TiltUp();
                        break;
                    case 3:
                        PanRight();
                        break;
                    case 4:
                        TiltDown();
                        break;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CarmeraControlerFrame_KeyUp(object? sender, KeyEventArgs e)
        {
            SetKeyPressed(e.KeyCode, false);

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    if (!isUpPressed && !isDownPressed)
                    {
                        TiliStop();
                        return;
                    }
                    else if (!isUpPressed && isDownPressed)
                    {
                        TiltDown();
                        return;
                    }
                    else if (isUpPressed && !isDownPressed)
                    {
                        TiltUp();
                        return;
                    }
                    break;
                case Keys.Left:
                case Keys.Right:
                    if (!isLeftPressed && !isRightPressed)
                    {
                        PanStop();
                        return;
                    }
                    else if (isLeftPressed && !isRightPressed)
                    {
                        PanLeft();
                        return;
                    }
                    else if (!isLeftPressed && isRightPressed)
                    {
                        PanRight();
                        return;
                    }
                    break;
                case Keys.ControlKey:
                    _panSpeed = userSpeed;
                    _tiltSpeed = userSpeed;
                    StartCameraPanTilt();
                    break;
            }
        }
        private void SetKeyPressed(Keys key, bool pressed)
        {
            switch (key)
            {
                case Keys.Up:
                    isUpPressed = pressed;
                    break;
                case Keys.Down:
                    isDownPressed = pressed;
                    break;
                case Keys.Left:
                    isLeftPressed = pressed;
                    break;
                case Keys.Right:
                    isRightPressed = pressed;
                    break;
                case Keys.ControlKey:
                    isControlKeyPressed = pressed;
                    break;
            }
        }

        private bool IsKeyPressed(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    return isUpPressed;
                case Keys.Down:
                    return isDownPressed;
                case Keys.Left:
                    return isLeftPressed;
                case Keys.Right:
                    return isRightPressed;
                case Keys.ControlKey:
                    return isControlKeyPressed;
                default:
                    return false;
            }
        }

        private void PanLeft()
        {
            _panDir = 1;
            StartCameraPanTilt();
        }

        private void PanRight()
        {
            _panDir = 2;
            StartCameraPanTilt();
        }

        private void PanStop()
        {
            _panDir = 3;
            StartCameraPanTilt();
        }

        private void TiltUp()
        {
            _tiltDir = 1;
            StartCameraPanTilt();
        }

        private void TiltDown()
        {
            _tiltDir = 2;
            StartCameraPanTilt();
        }

        private void TiliStop()
        {
            _tiltDir = 3;
            StartCameraPanTilt();
        }

        private void StartCameraPanTilt()
        {
            CameraPanTilt?.Invoke(_panSpeed, _tiltSpeed, _panDir, _tiltDir);
        }

        private void BtnArrow_MouseUp(object? sender, MouseEventArgs e)
        {

        }

        private void BtnArrow_MouseDown(object? sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                Debug.WriteLine("컨트롤 눌림!");
            }

            Button btn = sender as Button;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            testBtn?.Invoke(this, e);

        }
    }

}
