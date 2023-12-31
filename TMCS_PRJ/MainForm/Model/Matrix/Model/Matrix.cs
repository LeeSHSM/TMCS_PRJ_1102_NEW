﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LshGlobalSetting;

namespace TMCS_PRJ
{
    public class MatrixChannel
    {
        #region Properties
        
        public event EventHandler MatrixChannelValueChanged;

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
                ChangedValue();
            }
        }
        public string ChannelName
        {
            get => _channelName;
            set
            {
                _channelName = value;
                ChangedValue();
            }
        }
        public string ChannelType
        {
            get => _channelType;
            set
            {
                _channelType = value;
                ChangedValue();
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
                ChangedValue();
            }
        }
        #endregion


        private void ChangedValue()
        {
            MatrixChannelValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Matrix
    {
        #region Properties
        private string INPUT = GlobalSetting.ChannelType.INPUT.ToString();
        private string OUTPUT = GlobalSetting.ChannelType.OUTPUT.ToString();

        private int _inputChannelPortCount;
        private int _outputChannelPortCount;
        private List<MatrixChannel> _inputChannels;
        private List<MatrixChannel> _outputChannels;

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

        public int getChannelPortCount(string channelType)
        {
            if(channelType == INPUT) return _inputChannelPortCount;
            if(channelType == OUTPUT) return _outputChannelPortCount;

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

    }
}
