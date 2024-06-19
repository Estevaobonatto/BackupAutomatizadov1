using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.IO;
using BackupAutomatizado8;

namespace BackupAutomatizado8
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnBackupClicked(object sender, EventArgs e)
        {
            try
            {
                string localPath = localPathEntry.Text;
                string backupPath = backupPathEntry.Text;

                if (string.IsNullOrEmpty(localPath) || string.IsNullOrEmpty(backupPath))
                {
                    await DisplayAlert("Erro", "Por favor, preencha ambos os campos de caminho.", "OK");
                    return;
                }
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }
                else

                CopyAll(new DirectoryInfo(localPath), new DirectoryInfo(backupPath));
                await DisplayAlert("Sucesso", "Backup realizado com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao realizar o backup: {ex.Message}", "OK");
            }
        }

        private async void OnRestoreClicked(object sender, EventArgs e)
        {
            try
            {
                string localPath = localPathEntry.Text;
                string backupPath = backupPathEntry.Text;

                if (string.IsNullOrEmpty(localPath) || string.IsNullOrEmpty(backupPath))
                {
                    await DisplayAlert("Erro", "Por favor, preencha ambos os campos de caminho.", "OK");
                    return;
                }

                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }
                else
                    CopyAll(new DirectoryInfo(localPath), new DirectoryInfo(backupPath));

                
                await DisplayAlert("Sucesso", "Backup realizado com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao realizar o backup: {ex.Message}", "OK");
            }
        }

        private void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            //LogService.Log($"Criando diretório: {target.FullName}");
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fileInfo in source.GetFiles())
            {
                string targetFilePath = Path.Combine(target.FullName, fileInfo.Name);
                //LogService.Log($"Copiando arquivo: {fileInfo.FullName} para {targetFilePath}");
                fileInfo.CopyTo(targetFilePath, true);
            }
            foreach (DirectoryInfo subDirectory in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(subDirectory.Name);
                CopyAll(subDirectory, nextTargetSubDir);
            }
        }        

    }
}
