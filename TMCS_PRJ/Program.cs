using System.Diagnostics;
using System.Windows.Forms;

namespace TMCS_PRJ
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            LoadingForm loadingForm = new LoadingForm();
            MainForm view = new MainForm();

            IProgress<ProgressReport> progress = new Progress<ProgressReport>(report =>
            {
                // UI 업데이트는 여기서 안전하게 수행됩니다.
                if(report.Message != null) 
                {
                    loadingForm.Setlbl(report.Message);
                    GlobalSetting.Logger.LogInfo(report.Message);
                }

                if(report.Test != null)
                {
                    view.lblUpdate = report.Test;
                }
                
            });

            progress?.Report(new ProgressReport { Message = "메인프레젠터 로딩 시작" });
            MainPresenter presenter = new MainPresenter(view, progress);
            progress?.Report(new ProgressReport { Message = "메인프레젠터 로딩 완료" });

            ApplicationContext context = new ApplicationContext { MainForm = loadingForm };

            progress?.Report(new ProgressReport { Message = "메인프레젠터 비동기 초기화 시작" });
            Task.Run(async () =>
            {                
                await presenter.InitializeAsync(); // 비동기 초기화
                progress.Report(new ProgressReport { Message = "메인프레젠터 비동기 초기화 완료" });
                loadingForm.Invoke((MethodInvoker)delegate
                {
                    progress.Report(new ProgressReport { Message = "메인폼으로 전환시작" });
                    // 로딩 폼을 닫고, 메인 폼을 ApplicationContext의 메인 폼으로 설정 후 보여줍니다.
                    context.MainForm = view;
                    loadingForm.Close();
                    loadingForm.Dispose();
                    view.Visible = true; // 메인 폼을 화면에 표시합니다.
                    progress.Report(new ProgressReport { Message = "메인폼으로 전환완료" });
                });
            });

            Application.Run(context);
        }


        private static void ShowLoadingScreen(MainForm mainForm)
        {

            // 로딩 화면 표시 로직
        }

        private static void HideLoadingScreen(MainForm mainForm)
        {
            // 로딩 화면 숨기는 로직
        }

        private static void UpdateUIAfterInitialization(MainForm mainForm)
        {
            // 초기화 후 UI 업데이트 로직
        }
    }
}