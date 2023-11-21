using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using LshGlobalSetting;

namespace LshMatrix
{
    public class MatrixFrameFileManager
    {
        private string _connectionString;

        public string ConnectionString { get => _connectionString; set => _connectionString = value; }

        public void SaveMatrixInOutFramesInfo(List<IMioFrame> MioFrames)
        {
            List<MioFrameControlInfo> mioFramesInfo = new List<MioFrameControlInfo>();
            foreach (MioFrame mioFrame in MioFrames)
            {
                MioFrameControlInfo mioFrameInfo = new MioFrameControlInfo();
                mioFrameInfo.DockStyle = mioFrame.Dock;
                mioFrameInfo.Location = mioFrame.Location;
                mioFrameInfo.Size = mioFrame.Size;
                mioFrameInfo.inputPort = mioFrame.MatrixChannelInput.Port;
                mioFrameInfo.outputPort = mioFrame.MatrixChannelOutput.Port;

                mioFramesInfo.Add(mioFrameInfo);
            }

            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Info\\";

            string filePath = Path.Combine(exePath, "mioFramesInfo.xml");

            SaveUserControlsToXml(mioFramesInfo, filePath);
        }

        private void SaveUserControlsToXml(List<MioFrameControlInfo> controls, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<MioFrameControlInfo>));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, controls);
            }
        }

        public async Task<List<IMioFrame>> LoadMatrixInOutFramesInfoAsync()
        {
            List<IMioFrame> MioFramesInfo = new List<IMioFrame>();

            // 현재 실행 중인 어셈블리의 위치를 얻음
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Info\\";

            // 파일 경로 설정
            string filePath = Path.Combine(exePath, "mioFramesInfo.xml");

            // XML 파일이 존재하는지 확인
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<MioFrameControlInfo>));

                // 파일을 비동기 모드로 열기
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    // XML 데이터를 비동기적으로 List<MatrixInOutFrameUserControlInfo> 타입으로 역직렬화
                    List<MioFrameControlInfo> mioFramesInfo =
                        (List<MioFrameControlInfo>)await Task.Run(() => serializer.Deserialize(fs));

                    // MatrixInOutFrameUserControlInfo 객체들을 MatrixInOutSelectFrameView 객체로 변환
                    foreach (var mioFrameInfo in mioFramesInfo)
                    {
                        MioFrame mioFrame = new MioFrame();
                        mioFrame.Dock = mioFrameInfo.DockStyle;
                        mioFrame.MatrixChannelInput.Port = mioFrameInfo.inputPort;
                        mioFrame.MatrixChannelOutput.Port = mioFrameInfo.outputPort;
                        mioFrame.Location = mioFrameInfo.Location;
                        mioFrame.Size = mioFrameInfo.Size;

                        MioFramesInfo.Add(mioFrame);
                    }
                }
            }

            return MioFramesInfo;
        }


        //public async Task<List<MatrixInOutSelectFrameView>> LoadMatrixInOutFramesInfoAsync()
        //{
        //    List<MatrixInOutSelectFrameView> MioFramesInfo = new List<MatrixInOutSelectFrameView>();

        //    // 현재 실행 중인 어셈블리의 위치를 얻음
        //    string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        //    // 파일 경로 설정
        //    string filePath = Path.Combine(exePath, "mioFramesInfo.xml");

        //    // XML 파일이 존재하는지 확인
        //    if (File.Exists(filePath))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(List<MatrixInOutFrameUserControlInfo>));

        //        // 파일을 읽기 모드로 열기
        //        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        //        {
        //            // XML 데이터를 List<MatrixInOutFrameUserControlInfo> 타입으로 역직렬화
        //            List<MatrixInOutFrameUserControlInfo> mioFramesInfo = (List<MatrixInOutFrameUserControlInfo>)serializer.Deserialize(fs);

        //            // MatrixInOutFrameUserControlInfo 객체들을 MatrixInOutSelectFrameView 객체로 변환
        //            foreach (var mioFrameInfo in mioFramesInfo)
        //            {
        //                MatrixInOutSelectFrame mioFrame = new MatrixInOutSelectFrame();
        //                mioFrame.ParentId = mioFrameInfo.ParentId;
        //                mioFrame.Dock = mioFrameInfo.DockStyle;
        //                mioFrame.MatrixChannelInput.Port = mioFrameInfo.inputPort;
        //                mioFrame.MatrixChannelOutput.Port = mioFrameInfo.outputPort;
        //                mioFrame.Location = mioFrameInfo.Location;
        //                mioFrame.Size = mioFrameInfo.Size;

        //                MioFramesInfo.Add(mioFrame);
        //            }
        //        }
        //    }

        //    return MioFramesInfo;
        //}





    }
}
