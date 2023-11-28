using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;

namespace LshCamera
{
    public partial class CarmeraControlerFrame : UserControl, ICameraControler
    {
        public event EventHandler testBtn;
        public event ICameraControler.delCameraPanTilt CameraPanTilt;
        public event EventHandler PresetSave;
        public event EventHandler PresetLoad;
        public event ICameraControler.delPresetValueChanged PresetNameChanged;

        public CarmeraControlerFrame()
        {
            InitializeComponent();
            InitializeEvent();
            Initialize();

            userSpeed = 25 - scrollbarPanTiltSpeed.Value;
            _panSpeed = userSpeed;
            _tiltSpeed = userSpeed;
            lblPanTiltSpeed.Text = userSpeed.ToString();

            tblMainBackground.BackColor = Color.FromArgb(30, 30, 30);
        }

        private void Initialize()
        {
            pnPreeset.AutoScroll = true;
        }

        private bool isCameraSelected = false;

        public void SelectCamera(ICamera camera)
        {
            pnPreeset.Controls.Clear();
            AddControls(camera.PresetGroup, 25);
            this.Focus();
            isCameraSelected = true;
        }

        public void EndKeyEvent()
        {
            FindForm().Focus();
            pnPreeset.Controls.Clear();
            isCameraSelected = false;
        }

        private const int ControlSpacing = 9;
        private const int ControlHeight = 25;

        private void AddControls(CameraPresetGroup presetGroup, int numberOfControls)
        {
            int yPos = ControlSpacing;

            for (int i = 0; i < numberOfControls; i++)
            {
                CameraPreset preset;
                if (presetGroup == null)
                {
                    preset = null;
                }
                else
                {
                    preset = presetGroup.Presets.FirstOrDefault(p => p.Presetid == i + 1);
                }

                string presetName;
                if (preset == null)
                {
                    presetName = $"프리셋 {i + 1}";
                }
                else
                {
                    presetName = preset.Presetname;
                }

                Label label = new Label
                {
                    Text = presetName,
                    Tag = i + 1,
                    Font = new Font("맑은 고딕", 9, FontStyle.Regular),
                    Location = new Point(ControlSpacing, yPos),
                    Size = new Size(150, ControlHeight)
                };
                label.MouseEnter += Label_MouseEnter;
                label.MouseLeave += Label_MouseLeave;
                label.MouseClick += Label_MouseClick;
                pnPreeset.Controls.Add(label);

                Button button = new Button
                {
                    Text = "실행",
                    Location = new Point(label.Left + label.Width + 20, yPos - 4),
                    Tag = i + 1,
                    Size = new Size(75, ControlHeight),
                    TabStop = false
                };
                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;
                button.Click += LoadPreset_Click;
                button.KeyUp += CarmeraControlerFrame_KeyUp;

                pnPreeset.Controls.Add(button);
                Button button2 = new Button
                {
                    Text = "저장",
                    Tag = i + 1,
                    Location = new Point(button.Width + button.Left + 20, yPos - 4),
                    Size = new Size(75, ControlHeight),
                    TabStop = false
                };
                button2.MouseEnter += Button_MouseEnter;
                button2.MouseLeave += Button_MouseLeave;
                button2.Click += SavePreset_Click;
                button2.KeyUp += CarmeraControlerFrame_KeyUp;
                pnPreeset.Controls.Add(button2);

                Button button3 = new Button
                {
                    Text = "이름변경",
                    Tag = i + 1,
                    Location = new Point(button2.Width + button2.Left + 20, yPos - 4),
                    Size = new Size(75, ControlHeight),
                    TabStop = false
                };
                button3.MouseEnter += Button_MouseEnter;
                button3.MouseLeave += Button_MouseLeave;
                button3.Click += SetPresetName_Click;
                button3.KeyUp += CarmeraControlerFrame_KeyUp;
                pnPreeset.Controls.Add(button3);

                Panel panel = new Panel
                {
                    Tag = i + 1,
                    Location = new Point(label.Left, yPos + label.Height),
                    Size = new Size(button3.Left + button3.Width, 3),
                    TabStop = false,
                    BorderStyle = BorderStyle.None,
                    BackColor = Color.Black,
                };
                pnPreeset.Controls.Add(panel);

                yPos += ControlHeight + ControlSpacing;
            }
        }

        private bool isChangeName = false;

        private void SetPresetName_Click(object? sender, EventArgs e)
        {
            btnDown.Focus();
            Button btn = sender as Button;
            Label label = FindLabelWithTagOne((int)btn.Tag);
            label.Visible = false;
            TextBox tbox = new TextBox();
            tbox.Location = label.Location;
            tbox.Text = label.Text;
            tbox.Tag = label.Tag;
            tbox.Font = new Font("맑은 고딕", 9, FontStyle.Regular);
            tbox.Size = label.Size;
            tbox.ImeMode = ImeMode.Hangul;
            tbox.BackColor = pnPreeset.BackColor;
            pnPreeset.Controls.Add(tbox);
            tbox.Focus();
            tbox.KeyDown += Tbox_KeyDown;
            isChangeName = true;
        }

        private void Tbox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TextBox tbox = sender as TextBox;
                Label label = FindLabelWithTagOne((int)tbox.Tag);
                label.Text = tbox.Text;
                label.Visible = true;
                tbox.Visible = false;
                pnPreeset.Controls.Remove(tbox);
                isChangeName = false;

                this.Focus();
                PresetNameChanged?.Invoke((int)label.Tag, label.Text);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                TextBox tbox = sender as TextBox;
                Label label = FindLabelWithTagOne((int)tbox.Tag);
                label.Visible = true;
                tbox.Visible = false;
                pnPreeset.Controls.Remove(tbox);
                isChangeName = false;
                this.Focus();
            }
        }

        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            Label label = FindLabelWithTagOne((int)btn.Tag);
            label.Font = new Font("맑은 고딕", 9, FontStyle.Regular);
        }

        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            Label label = FindLabelWithTagOne((int)btn.Tag);
            label.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
        }

        private Label FindLabelWithTagOne(int tagNum)
        {
            foreach (Control control in pnPreeset.Controls)
            {
                if (control is Label label && (int)label.Tag == tagNum)
                {
                    return label;
                }
            }

            // Tag 값이 1인 Label 컨트롤을 찾지 못한 경우 null 반환
            return null;
        }

        private void Label_MouseEnter(object? sender, EventArgs e)
        {
            Label label = sender as Label;
            label.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
        }
        private void Label_MouseLeave(object? sender, EventArgs e)
        {
            Label label = sender as Label;
            label.Font = new Font("맑은 고딕", 9, FontStyle.Regular);
        }

        private void Label_MouseClick(object? sender, MouseEventArgs e)
        {
            this.Focus();
        }



        private void LoadPreset_Click(object? sender, EventArgs e)
        {
            this.Focus();
            PresetLoad?.Invoke(sender, e);
        }

        private void SavePreset_Click(object? sender, EventArgs e)
        {
            this.Focus();
            PresetSave?.Invoke(sender, EventArgs.Empty);
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



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (isChangeName)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (isCameraSelected)
            {
                PanTiltCamera(keyData);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PanTiltCamera(Keys key)
        {
            int rightMost6Bits = (int)key & 0x3F;
            if (!IsKeyPressed(Keys.ControlKey) && (key & Keys.Control) == Keys.Control)
            {
                Debug.WriteLine("??");
                SetKeyPressed(Keys.ControlKey, true);
                _panSpeed = userSpeed - 5;
                _tiltSpeed = userSpeed - 5;
                StartCameraPanTilt();
            }
            if (rightMost6Bits == 37 || rightMost6Bits == 38 || rightMost6Bits == 39 || rightMost6Bits == 40)
            {
                if (rightMost6Bits == 37 && !IsKeyPressed(Keys.Left))
                {
                    SetKeyPressed(Keys.Left, true);
                    PanLeft();
                }
                else if (rightMost6Bits == 38 && !IsKeyPressed(Keys.Up))
                {
                    SetKeyPressed(Keys.Up, true);
                    TiltUp();
                }
                else if (rightMost6Bits == 39 && !IsKeyPressed(Keys.Right))
                {
                    SetKeyPressed(Keys.Right, true);
                    PanRight();
                }
                else if (rightMost6Bits == 40 && !IsKeyPressed(Keys.Down))
                {
                    SetKeyPressed(Keys.Down, true);
                    TiltDown();
                }
            }
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



    }

}
