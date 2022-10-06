using Autofac;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Natech_Weather
{
    public partial class App : Application
    {

        public static IContainer Container;

        public App()
        {
            InitializeComponent();
            var builder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces()
                .AsSelf();

            Container = builder.Build();

            MainPage = Container.Resolve<MainPage>();
        }
    }
}
