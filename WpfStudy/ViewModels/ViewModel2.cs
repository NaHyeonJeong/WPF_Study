using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;

namespace WpfStudy.ViewModels
{
    /**
     * Receive의 역할을 하는 ViewModel
     */
    public class ViewModel2 : ObservableRecipient, IRecipient<ObservableCollection<Model1>>, IRecipient<Model1>
    {
        private readonly ILogger _logger;

        private ObservableCollection<Model1> details;
        public ObservableCollection<Model1> Details
        {
            get => details;
            set => SetProperty(ref details, value);
        }

        public ViewModel2(ILogger<ViewModel2> logger)
        {
            _logger = logger;
            _logger.LogWarning("{@ADWViewModel}", logger);
            this.IsActive = true; //이거 필수
        }

        public void Receive(ObservableCollection<Model1> message) //이거 필수
        {
            Details = new ObservableCollection<Model1>();
            _logger.LogInformation("{@Model1}", message);

            foreach (var item in message)
            {
                Details.Add(item);
            }
        }

        public void Receive(Model1 message)
        {
            Console.WriteLine("");
        }
    }
}
