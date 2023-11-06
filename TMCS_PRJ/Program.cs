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
                // UI ������Ʈ�� ���⼭ �����ϰ� ����˴ϴ�.
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

            progress?.Report(new ProgressReport { Message = "������������ �ε� ����" });
            MainPresenter presenter = new MainPresenter(view, progress);
            progress?.Report(new ProgressReport { Message = "������������ �ε� �Ϸ�" });

            ApplicationContext context = new ApplicationContext { MainForm = loadingForm };

            progress?.Report(new ProgressReport { Message = "������������ �񵿱� �ʱ�ȭ ����" });
            Task.Run(async () =>
            {                
                await presenter.InitializeAsync(); // �񵿱� �ʱ�ȭ
                progress.Report(new ProgressReport { Message = "������������ �񵿱� �ʱ�ȭ �Ϸ�" });
                loadingForm.Invoke((MethodInvoker)delegate
                {
                    progress.Report(new ProgressReport { Message = "���������� ��ȯ����" });
                    // �ε� ���� �ݰ�, ���� ���� ApplicationContext�� ���� ������ ���� �� �����ݴϴ�.
                    context.MainForm = view;
                    loadingForm.Close();
                    loadingForm.Dispose();
                    view.Visible = true; // ���� ���� ȭ�鿡 ǥ���մϴ�.
                    progress.Report(new ProgressReport { Message = "���������� ��ȯ�Ϸ�" });
                });
            });

            Application.Run(context);
        }


        private static void ShowLoadingScreen(MainForm mainForm)
        {

            // �ε� ȭ�� ǥ�� ����
        }

        private static void HideLoadingScreen(MainForm mainForm)
        {
            // �ε� ȭ�� ����� ����
        }

        private static void UpdateUIAfterInitialization(MainForm mainForm)
        {
            // �ʱ�ȭ �� UI ������Ʈ ����
        }
    }
}