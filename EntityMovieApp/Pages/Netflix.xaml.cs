using System;
using System.Windows;
using System.Windows.Input;
using EntityMovieApp.Classes;
using WPFPageSwitch;
using mshtml;

namespace EntityMovieApp.Pages
{
    /// <summary>
    /// Interaction logic for Netflix.xaml
    /// </summary>
    public partial class Netflix : ISwitchable
    {
        public CommandParameter Parameter { get; set; }


        //void WindowLoaded(object sender, RoutedEventArgs e)
        //{
        //    CustomCommands.BindCommandsToWindow(this);
        //}

        public Netflix()
        {
           Parameter = new CommandParameter {CanEditBeExecuted = false};
            InitializeComponent();
            Application.Current.MainWindow.SizeToContent = SizeToContent.WidthAndHeight;

            //Loaded += new RoutedEventHandler(WindowLoaded);

			
           LoadWatchInstantly();
        }

        public void LogInToNetflix()
        {
            
            // get the document
            IHTMLDocument2 doc = NetflixWindow.Document as IHTMLDocument2;
            if (doc != null)
            {
                ((IHTMLElement)doc.all.item("email")).setAttribute("value", Properties.Settings.Default.NetflixEmail);
                ((IHTMLElement)doc.all.item("password")).setAttribute("value", Properties.Settings.Default.NetflixPassword);
                // click a button
                ((HTMLButtonElement)doc.all.item("login-form-contBtn")).click();
            }
            //NetflixWindow.Visibility = Visibility.Visible;
        }

        public void LoadWatchInstantly()
        {
            string address = Properties.Settings.Default.NetflixWatchInstantly;
            try
            {
                NetflixWindow.Navigate(new Uri(address));
            }
            catch (Exception exception)
            {
                Helpers.ExceptionHelpers.ThrowFormattedException(exception);
            }
           
        }

        private void NetflixWindowLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string url = NetflixWindow.Source.AbsolutePath;
            const string loginPage = @"/Login";
            if (url == loginPage)
                LogInToNetflix();
            
          
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void LoginTextBlockMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Switch();
        }
    }
}
