using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class MatrixPresenter
    {

        public MatrixPresenter(int inputCount, int outputCount)
        {
            MatrixFrameControl = new MatrixFrameControl();
            _matrixManager = new MatrixManager(new Matrix(inputCount, outputCount));
            CreateAsync();
            _matrixManager.Connect = new RTVDMMatrixToIP(IPAddress.Parse("192.168.50.8"), 23);
        }

        private MatrixFrameView _matrixFrame;
        private List<MatrixInOutSelectFrameView> _matrixInOutFrame = new List<MatrixInOutSelectFrameView>();
        private MatrixManager _matrixManager;
        private MatrixChannel _mappingChannel;

        public MatrixFrameView MatrixFrameControl { get => _matrixFrame; set => _matrixFrame = value; }


        public async Task CreateAsync()
        {
            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await _matrixManager.InitializeChannels(); // MatrixManager의 초기화가 완료될 때까지 기다립니다.
            InitializeEvent();

            MatrixFrameControl.ChannelType = MatrixControl_ChangeMatrixChannelListClick("INPUT");
        }
        private void InitializeEvent()
        {
            MatrixFrameControl.ChangeMatrixChannelListClick += MatrixControl_ChangeMatrixChannelListClick;
            MatrixFrameControl.CellClick += MatrixControl_CellClick;
        }


        public MatrixInOutSelectFrame AddMatrixInOutFrameAsync()
        {
            MatrixInOutSelectFrame mc = new MatrixInOutSelectFrame();
            mc.matrixChannelInputClick += Mc_matrixChannelInputClick;
            mc.matrixChannelOutputClick += Mc_matrixChannelOutputClick;
            mc.MatrixChannelInput.Text = "테스트" + _matrixInOutFrame.Count;

            _matrixInOutFrame.Add(mc);

            return mc;
        }

        public void SetConnect()
        {
            _matrixManager.Connent();
            
        }

        public void sssss()
        {
            _matrixManager.SendMsg("hfd");
        }


        #region Event Handles

        private void Mc_matrixChannelOutputClick(object? sender, EventArgs e)
        {
            
        }

        private void Mc_matrixChannelInputClick(object? sender, EventArgs e)
        {
            var frame = sender as MatrixInOutSelectFrameView;
            bool check = false;
            foreach (MatrixInOutSelectFrameView mc in _matrixInOutFrame)
            {
                if (_mappingChannel.Port == mc.MatrixChannelInput.Port)
                {
                    MessageBox.Show("있어!!");
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                foreach (MatrixInOutSelectFrameView mc in _matrixInOutFrame)
                {
                    if (mc == frame)
                    {
                        mc.MatrixChannelInput = frame.MatrixChannelInput;
                        frame.SetMatrixInputChannel(_mappingChannel);
                    }
                }
            }
        }

        private void MatrixControl_CellClick(object? sender, EventArgs e)
        {
            MatrixChannel mc = sender as MatrixChannel;
            _mappingChannel = mc;
            if (mc != null)
            {
                Debug.WriteLine(mc.ChannelName);
            }
            else
            {
                Debug.WriteLine("mc == null");
            }
        }

        private string MatrixControl_ChangeMatrixChannelListClick(string channelType)
        {
            DataTable dt = CreateDataTableForMatrixChannelList(channelType);
            MatrixFrameControl.SetMatrixChannelList(dt);

            return channelType;

        }
        #endregion

        private DataTable CreateDataTableForMatrixChannelList(string channelType)
        {
            DataTable dt = new DataTable();
            DataTable List = _matrixManager.GetChannelListInfoToDataTable(channelType);
            dt.Columns.Add("  구  분");
            dt.Columns.Add("     소  스");

            foreach (DataRow row in List.Rows)
            {
                DataRow dr = dt.NewRow();
                string strTmp = row["ChannelType"].ToString() + " " + row["Port"];
                dr["  구  분"] = strTmp;
                dr["     소  스"] = row["Name"];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        
    }
}
