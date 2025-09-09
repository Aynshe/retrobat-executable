using System;
using System.Diagnostics;
using System.IO;

namespace RetroBat
{
    public static class AudioMixer
    {
        private static string _soundVolumeViewPath;

        static AudioMixer()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            _soundVolumeViewPath = Path.Combine(appFolder, "tools", "SoundVolumeView.exe");
        }

        public static void MuteEmulationStation()
        {
            ExecuteCommand("/Mute emulationstation.exe");
        }

        public static void UnmuteEmulationStation()
        {
            ExecuteCommand("/Unmute emulationstation.exe");
        }

        private static void ExecuteCommand(string arguments)
        {
            if (!File.Exists(_soundVolumeViewPath))
            {
                SimpleLogger.Instance.Warning($"SoundVolumeView.exe not found at: {_soundVolumeViewPath}");
                return;
            }

            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = _soundVolumeViewPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(processStartInfo))
                {
                    // We don't need to wait for exit, the command is instantaneous
                    SimpleLogger.Instance.Info($"Executed SoundVolumeView command: {arguments}");
                }
            }
            catch (Exception ex)
            {
                SimpleLogger.Instance.Error($"Failed to execute SoundVolumeView: {ex.Message}");
            }
        }
    }
}
