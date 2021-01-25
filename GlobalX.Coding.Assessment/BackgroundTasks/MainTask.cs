using GlobalX.Coding.Assessment.Infrastructure;
using GlobalX.Coding.Assessment.Infrastructure.IServices;
using GlobalX.Coding.Assessment.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalX.Coding.Assessment.BackgroundTasks
{
    public class MainTask : BackgroundService
    {
        private readonly ILogger<MainTask> _logger;
        private readonly IValidateService _validateService;
        private readonly ISortService _sortService;
        private readonly IWriteFileService _writeFileService;

        public MainTask(
                ILogger<MainTask> logger,
                IValidateService validateService,
                ISortService sortService,
                IWriteFileService writeFileService
            )
        {
            _logger = logger;
            _validateService = validateService;
            _sortService = sortService;
            _writeFileService = writeFileService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation($"{Program.AppName} is executing.");

                Result<List<Person>> validation = await _validateService.Validate();
                if (!validation.Success)
                    _logger.LogError($"{validation.Errors.First()}");

                Result<List<Person>> sortPeople = _sortService.Sort(validation.Value);
                if (!sortPeople.Success)
                    _logger.LogError($"{sortPeople.Errors.First()}");

                Result<bool> result = await _writeFileService.Write(sortPeople.Value);
                if (!result.Success)
                    _logger.LogError($"{result.Errors.First()}");

                _logger.LogInformation($"People sorted.");

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{Program.AppName} is stopping.");

            await base.StopAsync(stoppingToken);
        }

        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{Program.AppName} is starting.");

            await base.StartAsync(stoppingToken);
        }
    }
}
