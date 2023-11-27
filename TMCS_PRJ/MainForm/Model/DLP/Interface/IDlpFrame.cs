namespace LshDlp
{
    //DlpFrame는..... 모델에서 값을 받아서 얕은복사로 알아서 값들이 바뀔수있도록 작성... 여러 시도중
    internal interface IDlpFrame
    {
        event EventHandler DlpClick;


        internal void SetDlpFrame(string channelType);

        internal void UpdateDlpTest();
    }
}
