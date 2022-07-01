using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.Json;
using System.IO.Compression;
using System.Diagnostics;

namespace DazToUnrealOnlineInstaller
{
    enum InstallSteps
    { 
        Initial = 0,
        Download = 1,
        ChooseUnrealVersion = 2,
        StartDazStudio = 3,
        FindDazStudio = 4,
        CloseDazStudio = 5,
        InstallDazStudioPlugin = 6,
        StartUnrealEditor = 7,
        FindUnrealEditor = 8,
        CloseUnrealEditor = 9,
        BackupUnrealEditor = 10,
        InstallUnrealEditorPlugin = 11,
        Exit,
        Quit
    }

    public partial class MainForm : Form
    {
        private InstallSteps CurrentInstallStep = InstallSteps.Initial;
        private string TempDirectory;
        private string DazStudioPluginFolder;
        private string UnrealEditorPluginFolder;
        private const string DazStudioPluginName = "dzunrealbridge.dll";
        private const string UnrealEditorPluginName = "DazToUnreal";
        public MainForm()
        {
            InitializeComponent();

            TempDirectory = CreateTemporaryDirectory();
            TempDirectoryLabel.Text = String.Format("Downloads to temp directory: {0}", TempDirectory);
        }

        private void NextButton_MouseClick(object sender, MouseEventArgs e)
        {
            NextButton.Enabled = false;

            switch (CurrentInstallStep)
            {
                case InstallSteps.Initial:
                    MoveToNextStep(true);
                    break;

                case InstallSteps.Download:
                    InstructionLabel.Text = String.Format("Downloading Plugin. Please wait.");
                    if (DownloadUnrealPlugin(TempDirectory))
                    {
                        ExtractPlugin(Path.Combine(TempDirectory, "DazToUnreal.zip"), TempDirectory);
                        UnrealVersionComboBox.Visible = true;
                        UnrealVersionComboBox.Items.AddRange(GetUnrealVersions());
                        UnrealVersionComboBox.SelectedIndex = UnrealVersionComboBox.Items.Count - 1;
                        TempDirectoryLabel.Text = String.Format("Downloaded to temp directory: {0}", TempDirectory);
                        InstructionLabel.Text = String.Format("Choose an Unreal Version and click Next");
                        MoveToNextStep();
                    }
                    break;

                case InstallSteps.ChooseUnrealVersion:
                    UnrealVersionComboBox.Enabled = false;
                    MoveToNextStep(true);
                    break;

                case InstallSteps.StartDazStudio:
                    if (IsDazStudioRunning())
                    {
                        MoveToNextStep(true);
                        break;
                    }
 
                    InstructionLabel.Text = String.Format("Start Daz Studio and click Next");
                    break;

                case InstallSteps.FindDazStudio:
                    if(!IsDazStudioRunning())
                    {
                        break;
                    }

                    DazStudioPluginFolder = GetRunningDazStudioPluginFolder();
                    DazStudioFolderLabel.Text = string.Format("Daz Studio Plugin Folder found at {0}", DazStudioPluginFolder);
                    MoveToNextStep(true);
                    break;

                case InstallSteps.CloseDazStudio:
                    InstructionLabel.Text = String.Format("Close Daz Studio and click Next (May take a minute to fully close)");
                    if (IsDazStudioRunning())
                    {
                        break;
                    }
                    MoveToNextStep(true);
                    break;

                case InstallSteps.InstallDazStudioPlugin:
                    string sourceDllPath = Path.Combine(TempDirectory, UnrealVersionComboBox.Text, "DazToUnreal", "Resources", DazStudioPluginName);
                    string targetDllPath = Path.Combine(DazStudioPluginFolder, DazStudioPluginName);
                    File.Copy(sourceDllPath, targetDllPath, true);
                    MoveToNextStep();
                    break;

                case InstallSteps.StartUnrealEditor:
                    if (IsUnrealEditorRunning())
                    {
                        MoveToNextStep(true);
                        break;
                    }

                    InstructionLabel.Text = String.Format("Start Unreal Editor and click Next");
                    break;

                case InstallSteps.FindUnrealEditor:
                    if (!IsUnrealEditorRunning())
                    {
                        break;
                    }

                    UnrealEditorPluginFolder = GetRunningUnrealEditorPluginFolder();
                    UnrealEditorFolderLabel.Text = string.Format("Unreal Engine Plugin Folder found at {0}", UnrealEditorPluginFolder);
                    MoveToNextStep(true);
                    break;

                case InstallSteps.CloseUnrealEditor:
                    InstructionLabel.Text = String.Format("Close Unreal Editor and click Next");
                    if (IsUnrealEditorRunning())
                    {
                        break;
                    }
                    MoveToNextStep(true);
                    break;

                case InstallSteps.BackupUnrealEditor:
                    if(Directory.Exists(UnrealEditorPluginFolder) && !string.IsNullOrWhiteSpace(BackupFolderPathTextBox.Text))
                    {
                        if (Path.GetFullPath(BackupFolderPathTextBox.Text) == BackupFolderPathTextBox.Text)
                        {
                            InstructionLabel.Text = String.Format("Backing up plugin. Please Wait.");
                            Directory.CreateDirectory(BackupFolderPathTextBox.Text);
                            Directory.Move(UnrealEditorPluginFolder, Path.Combine(BackupFolderPathTextBox.Text, "DazToUnreal"));
                            MoveToNextStep(true);
                            break;
                        }
                    }
                    if(Directory.Exists(UnrealEditorPluginFolder))
                    {
                        InstructionLabel.Text = String.Format("Plugin Folder Exists in Engine Already.  Choose backup location and click Next.");
                        string drive = Path.GetPathRoot(UnrealEditorPluginFolder);
                        string recommendedBackupPath = Path.Combine(drive, "DazToUnrealBackup", DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss"));
                        BackupFolderPathTextBox.Text = recommendedBackupPath;
                        BackupFolderPathTextBox.Visible = true;
                        ChooseBackupFolderButton.Visible = true;
                        break;
                    }
                    MoveToNextStep(true);
                    break;

                case InstallSteps.InstallUnrealEditorPlugin:
                    InstructionLabel.Text = String.Format("Installing Plugin.  Please Wait.");
                    string sourcePluginPath = Path.Combine(TempDirectory, UnrealVersionComboBox.Text, UnrealEditorPluginName);
                    CopyDirectory(sourcePluginPath, UnrealEditorPluginFolder, true);
                    MoveToNextStep(true);
                    break;

                case InstallSteps.Exit:
                    if (Directory.Exists(UnrealEditorPluginFolder))
                    {
                        InstructionLabel.Text = String.Format("Plugin Installed.  Click Exit.");
                        NextButton.Text = "Exit";
                        MoveToNextStep();
                    }
                    break;

                case InstallSteps.Quit:
                    Application.Exit();
                    break;

            }

            NextButton.Enabled = true;
        }

        private void MoveToNextStep(bool AutoClick = false)
        {
            CurrentInstallStep++;
            if (AutoClick)
            {
                NextButton_MouseClick(null, null);
            };
        }

        public string CreateTemporaryDirectory()
        {
            string directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(directory);
            return directory;
        }

        public bool DownloadUnrealPlugin(string directory)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.UserAgent, "DazToUnrealOnlineInstaller");
            string data = client.DownloadString("https://api.github.com/repos/David-Vodhanel/DazToUnreal/releases/latest");

            JsonDocument ReleaseData = JsonDocument.Parse(data);
            JsonElement AssetsElement;
            if (ReleaseData.RootElement.TryGetProperty("assets", out AssetsElement))
            {
                foreach (JsonElement DownloadElement in AssetsElement.EnumerateArray())
                {

                    JsonElement FileNameProperty;
                    if (DownloadElement.TryGetProperty("name", out FileNameProperty))
                    {
                        String FileName = FileNameProperty.GetString();
                        if (FileName == "DazToUnreal.zip")
                        {
                            JsonElement DownloadURLProperty;
                            if (DownloadElement.TryGetProperty("browser_download_url", out DownloadURLProperty))
                            {
                                String DownloadURL = DownloadURLProperty.GetString();
                                client.DownloadFile(DownloadURL, Path.Combine(directory, "DazToUnreal.zip"));
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
        public bool ExtractPlugin(String zipFile, String directory)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipFile, directory);
            }
            catch
            {
                return false;
            }
            return true; ;
        }

        public string[] GetUnrealVersions()
        {
            string[] versionDirectories = Directory.GetDirectories(TempDirectory);
            string[] versions = new string[versionDirectories.Length];
            for(int i = 0; i < versionDirectories.Length; i++)
            {
                versions[i] = new DirectoryInfo(versionDirectories[i]).Name;
            }

            return versions;
        }

        public bool IsDazStudioRunning()
        {
            Process[] DazSutdioProcesses = Process.GetProcessesByName("DazStudio");
            return DazSutdioProcesses.Length > 0;
        }

        public string GetRunningDazStudioPluginFolder()
        {
            Process process = Process.GetProcessesByName("DazStudio").First();
            string exePath = process.MainModule.FileName;
            string pluginFolderPath = Path.Combine(Path.GetDirectoryName(exePath), "plugins");
            return pluginFolderPath;
        }

        public bool IsUnrealEditorRunning()
        {
            Process[] Unreal5Processes = Process.GetProcessesByName("UnrealEditor");
            if (Unreal5Processes.Length > 0) return true;

            Process[] Unreal4Processes = Process.GetProcessesByName("UE4Editor");
            if (Unreal4Processes.Length > 0) return true;

            return false;
        }

        public string GetRunningUnrealEditorPluginFolder()
        {
            Process[] Unreal5Processes = Process.GetProcessesByName("UnrealEditor");
            for(int i = 0; i < Unreal5Processes.Count(); i++)
            {
                string exePath = Unreal5Processes[i].MainModule.FileName;
                string binariesFolderPath = Path.GetDirectoryName(exePath);
                string pluginFolderPath = Path.GetFullPath(Path.Combine(binariesFolderPath, "..", "..", "Plugins", "Marketplace", UnrealEditorPluginName));
                return pluginFolderPath;
            }

            Process[] Unreal4Processes = Process.GetProcessesByName("UE4Editor");
            for (int i = 0; i < Unreal4Processes.Count(); i++)
            {
                string exePath = Unreal4Processes[i].MainModule.FileName;
                string binariesFolderPath = Path.GetDirectoryName(exePath);
                string pluginFolderPath = Path.GetFullPath(Path.Combine(binariesFolderPath, "..", "..", "Plugins", "Marketplace", UnrealEditorPluginName));
                return pluginFolderPath;
            }
            return string.Empty;
        }

        private void ChooseBackupFolderButton_Click(object sender, EventArgs e)
        {
            BackupFolderBrowserDialog.SelectedPath = BackupFolderPathTextBox.Text;
            BackupFolderBrowserDialog.ShowDialog();
            BackupFolderPathTextBox.Text = BackupFolderBrowserDialog.SelectedPath;
        }

        // From: https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    }
}
