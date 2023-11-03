using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class MatrixChannel
    {
        public event EventHandler ChangeNameEvent;
        public event EventHandler ChangeRouteNo;

        #region Properties
        private int _portNo;
        private string _channelName;
        private string _channelType;
        private int _routeNo;
        public int Port
        {
            get => _portNo;
            set
            {
                _portNo = value;
            }
        }
        public string ChannelName
        {
            get => _channelName;
            set
            {
                _channelName = value;
                ChangeNameEvent?.Invoke(this, EventArgs.Empty);
            }
        }
        public string ChannelType
        {
            get => _channelType;
            set
            {
                _channelType = value;
            }
        }
        /// <summary>
        /// 아웃풋채널에만 있음.. 인풋채널은 0 고정, out채널은 0은 신호없음, 나머진 1~ 최대 아웃카운트중
        /// </summary>
        public int RouteNo
        {
            get => _routeNo;
            set
            {
                _routeNo = value;
                ChangeRouteNo?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }

    public class Matrix
    {
        #region Properties
        private string input = GlobalSetting.ChannelType.INPUT.ToString();
        private string output = GlobalSetting.ChannelType.OUTPUT.ToString();

        private int _inputChannelPortCount;
        private int _outputChannelPortCount;
        private List<MatrixChannel> _inputChannels;
        private List<MatrixChannel> _outputChannels;

        public int InputChannelCount { get => _inputChannelPortCount; }
        public int OutputChannelCount { get => _outputChannelPortCount; }
        public List<MatrixChannel> InputChannel { get => _inputChannels; set => _inputChannels = value; }
        public List<MatrixChannel> OutputChannel { get => _outputChannels; set => _outputChannels = value; }
        #endregion

        #region 생성자
        public Matrix(int inputChannelCount, int outputChannelCount)
        {
            _inputChannelPortCount = inputChannelCount;
            _outputChannelPortCount = outputChannelCount;
            _inputChannels = new List<MatrixChannel>();
            _outputChannels = new List<MatrixChannel>();
        }
        #endregion

        #region Public Methods
        public int getChannelPortCount(string channelType)
        {
            if(channelType == input) return _inputChannelPortCount;
            if(channelType == output) return _outputChannelPortCount;

            return 0;
        }
        public int getChannelInputPortCount()
        {
            return _inputChannelPortCount;
        }
        public int getChannelOutputPortCount()
        {
            return _outputChannelPortCount;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
