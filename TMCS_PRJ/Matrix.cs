using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCS_PRJ
{
    public class MatrixChannel : Label
    {
        public event EventHandler ChangeNameEvent;

        #region Properties
        private int _portNo;
        private string _channelName;
        private string _channelType;
        private int _inToOutRouteNo;
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
        /// 인풋채널에만 있음.. 인풋채널은 0 고정, out채널은 0은 신호없음, 나머진 1~ 최대 아웃카운트중
        /// </summary>
        public int InToOutRouteNo
        {
            get => _inToOutRouteNo;
            set
            {
                _inToOutRouteNo = value;
            }
        }
        #endregion
    }

    public class Matrix
    {
        #region Properties
        private int _inputChannelPortCount;
        private int _outputChannelPortCount;
        private string _videoType;
        private List<MatrixChannel> _inputChannels;
        private List<MatrixChannel> _outputChannels;

        public int InputChannelCount { get => _inputChannelPortCount; }
        public int OutputChannelCount { get => _outputChannelPortCount; }
        public List<MatrixChannel> InputChannel { get => _inputChannels; set => _inputChannels = value; }
        public List<MatrixChannel> OutputChannel { get => _outputChannels; set => _outputChannels = value; }
        public string VideoType { get => _videoType; }
        #endregion

        #region 생성자
        public Matrix(int inputChannelCount, int outputChannelCount, string videoType)
        {
            _inputChannelPortCount = inputChannelCount;
            _outputChannelPortCount = outputChannelCount;
            _inputChannels = new List<MatrixChannel>();
            _outputChannels = new List<MatrixChannel>();
            _videoType = videoType;
        }
        #endregion

        #region Public Methods
        public int getChannelPortCount(string channelType)
        {
            if (channelType == GlobalSetting.ChannelType.INPUT.ToString())
            {
                return _inputChannelPortCount;
            }
            else if (channelType == GlobalSetting.ChannelType.OUTPUT.ToString())
            {
                return _outputChannelPortCount;
            }

            throw new ArgumentException("Invalid channel type", nameof(channelType));
        }
        #endregion

        #region Private Methods
        private void InitializeChannels()
        {
            for (int i = 0; i < _inputChannelPortCount; i++)
            {
                _inputChannels.Add(new MatrixChannel { Port = i + 1, ChannelType = "INPUT" });
            }

            for (int i = 0; i < _outputChannelPortCount; i++)
            {
                _outputChannels.Add(new MatrixChannel { Port = i + 1, ChannelType = "OUTPUT" });
            }
        }
        #endregion
    }
}
