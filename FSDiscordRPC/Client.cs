using DiscordRPC;
using System.Text.Json;

namespace FSDiscordRPC
{
    internal class Client : IDisposable
    {
        public FileInfo ConfigFile { get; }
        public int UpdateDelay;
        
        private DiscordRpcClient? _RPClient;
        private readonly FileSystemWatcher _Watcher;

        public Client(string path, int updateDelay = 1000)
        {
            UpdateDelay = updateDelay;
            ConfigFile = new FileInfo(path);
            _Watcher = new FileSystemWatcher
            {
                Path = ConfigFile.DirectoryName ?? string.Empty,
                Filter = ConfigFile.Name,
                NotifyFilter = NotifyFilters.Security
                    | NotifyFilters.FileName
                    | NotifyFilters.LastWrite
                    | NotifyFilters.Size,
                IncludeSubdirectories = false,
                EnableRaisingEvents = true
            };

            UpdatePresenceFromConfig();
            _Watcher.Created += OnUpdatePresence;
            _Watcher.Changed += OnUpdatePresence;
            _Watcher.Deleted += OnUpdatePresence;
        }

        private void OnUpdatePresence(object _, FileSystemEventArgs e)
        {
            Thread.Sleep(UpdateDelay);
            UpdatePresenceFromConfig();
        }

        private void UpdatePresence(RichPresenceConfig config)
        {
            bool shouldSetPresence = true;
            Console.WriteLine("Updating Presence.");
            if (_RPClient?.ApplicationID != config.ApplicationId)
            {
                Console.WriteLine("Application Id Changed.");
                _RPClient?.ClearPresence();
                shouldSetPresence = false;
                if (!string.IsNullOrEmpty(config.ApplicationId))
                {
                    _RPClient?.Dispose();
                    Console.WriteLine("Old Rich Presence Client Disposed.");
                    _RPClient = new(config.ApplicationId)
                    {
                        SkipIdenticalPresence = true
                    };
                    _RPClient.Initialize();
                    Console.WriteLine("New Rich Presence Client Initialized.");
                    shouldSetPresence = true;
                }
            }

            if (shouldSetPresence)
                _RPClient?.SetPresence(config.ToRichPresence());
        }

        private void UpdatePresenceFromConfig()
        {
            try
            {
                if (ConfigFile.Exists)
                {
                    var content = File.ReadAllText(ConfigFile.FullName);
                    var config = JsonSerializer.Deserialize<RichPresenceConfig>(content) ?? new();
                    UpdatePresence(config);
                    return;
                }
                UpdatePresence(new());
            }
            catch (Exception err)
            {
                Console.Error.WriteLine(err);
            }
        }

        public void Dispose()
        {
            _RPClient?.ClearPresence();
            _RPClient?.Dispose();
            _Watcher.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
