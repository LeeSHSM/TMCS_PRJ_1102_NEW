using LshDlp;
using LshGlobalSetting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TMCS_PRJ;

namespace LshDlp
{
    internal class DlpFrameFileManager
    {
        public void SaveDlpsInfo(DlpStruct dlpStruct)
        {
            List<DlpFrameControlInfo> dlpControls = new List<DlpFrameControlInfo>();
            foreach (Dlp dlp in dlpStruct.Dlps)
            {
                DlpFrameControlInfo dlpControl = new DlpFrameControlInfo(dlp.DlpId, dlp.TileMode, dlp.Row, dlp.Col, dlp.MatrixPort, dlp.InputChannel.Port);
                dlpControls.Add(dlpControl);
            }
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Info\\";

            string filePath = Path.Combine(exePath, "dlps.xml");

            SaveUserControlsToXml(dlpControls, filePath);
        }


        private void SaveUserControlsToXml(List<DlpFrameControlInfo> controls, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<DlpFrameControlInfo>));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, controls);
            }
        }

        public async Task<List<DlpFrameControlInfo>> LoadDlpsInfoAsync()
        {
            List<DlpFrameControlInfo> dlps = new List<DlpFrameControlInfo>();

            // 현재 실행 중인 어셈블리의 위치를 얻음
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Info\\";

            // 파일 경로 설정
            string filePath = Path.Combine(exePath, "dlps.xml");

            // XML 파일이 존재하는지 확인
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DlpFrameControlInfo>));

                // 파일을 비동기 모드로 열기
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                {
                    List<DlpFrameControlInfo> xmlDlpsInfo = new List<DlpFrameControlInfo>();
                    try
                    {
                        xmlDlpsInfo =
                        (List<DlpFrameControlInfo>)await Task.Run(() => serializer.Deserialize(fs));
                    }
                    catch(Exception ex) 
                    {
                    Debug.WriteLine(ex);
                    }
                    

                    foreach (var xmldlpInfo in xmlDlpsInfo)
                    {
                        DlpFrameControlInfo dlpFramesInfo = new DlpFrameControlInfo(xmldlpInfo.DlpId, xmldlpInfo.TileMode, xmldlpInfo.Row, xmldlpInfo.Col, xmldlpInfo.MatrixPort, xmldlpInfo.InputChannelPort);
                        dlps.Add(dlpFramesInfo);
                    }
                }
            }

            return dlps;
        }
    }
}
