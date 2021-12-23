using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfStudy
{
    /// <summary>
    /// 사용할 ViewModel과 Logging을 등록함
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices
                (new ServiceCollection()
                    .AddSingleton<ViewModel>() //viewmodel 등록
                    .AddLogging(builder => //log 관련 등록
                    {
                        var logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .CreateLogger();

                        builder.AddSerilog(logger);
                    }).BuildServiceProvider()
                );
            this.InitializeComponent();
        }

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            // ViewModel, View 등록
            services.AddSingleton<ViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
