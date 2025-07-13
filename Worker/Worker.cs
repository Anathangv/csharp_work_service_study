namespace WorkerStydy
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private const int _interval = 5000;
        private IHttpClientFactory _httpClientFactory;
        private string _url = "https://jsonplaceholder.typicode.com/users/1/todos";

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var client = _httpClientFactory.CreateClient();

            //run in docker it is a linux container, the file is saved in /tmp within the file system container
            //run in windows, the file is saved in C:\temp within the file system
            var folderPath = OperatingSystem.IsWindows() ? @"C:\temp" : "/tmp";

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var response = await client.GetAsync(_url);

                var targetFilePath = Path.Combine(folderPath, $"{Guid.NewGuid()}.txt");

                var responseText = await response.Content.ReadAsStringAsync();

                try
                {
                    Directory.CreateDirectory(folderPath);
                    File.WriteAllText(targetFilePath, responseText);
                }
                catch (Exception ex) 
                {
                    _logger.LogError("ERRRO! {error}", ex.Message);
                }


                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
