using System.Windows;
using System.Windows.Input;
using CommandParameter = EntityMovieApp.Pages.CommandParameter;

namespace EntityMovieApp.Classes
{
    public static class CustomCommands
    {
		private static RoutedUICommand _MainWindow;
		private static RoutedUICommand _EditContact;
		private static RoutedUICommand _DeleteContact;


		static CustomCommands()
		{
			_MainWindow = new RoutedUICommand("Change Window To Main", "MainWindow", typeof(CustomCommands));
			_EditContact = new RoutedUICommand("Edit an existing contact", "EditContact", typeof(CustomCommands));
			_DeleteContact = new RoutedUICommand("Delete an existing contact", "DeleteContact", typeof(CustomCommands));
		}

		// Command: MainWindow
		public static RoutedUICommand MainWindow
		{
			get { return _MainWindow; }
		}
		// Command: EditContact
		public static RoutedUICommand EditContact
		{
			get { return _EditContact; }
		}
		// Command: DeleteContact
		public static RoutedUICommand DeleteContact
		{
			get { return _DeleteContact; }
		}

		public static void MainWindow_Executed(object sender, ExecutedRoutedEventArgs e)
		{
            
            CommandParameter parameter = e.Parameter as CommandParameter;

            if (parameter != null)
                parameter.CanEditBeExecuted = true;
            
            //Window MainWindow = new MainWindow();
            //Application.Current.MainWindow.Close();
            //Application.Current.MainWindow = MainWindow;
            
            //MainWindow.Show();

            //MessageBox.Show("MainWindow command executed");
		}
		public static void MainWindow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

        //public static void EditContact_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    MessageBox.Show("Edit contact command executed");
        //}
        //public static void EditContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    CommandParameter parameter = e.Parameter as CommandParameter;

        //    if (parameter != null)
        //        e.CanExecute = parameter.CanEditBeExecuted;
        //    else
        //        e.CanExecute = false;
        //}

        //public static void DeleteContact_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    CommandParameter parameter = e.Parameter as CommandParameter;

        //    if (parameter != null)
        //        parameter.CanEditBeExecuted = false;

        //    MessageBox.Show("Delete contact command executed");
        //}
        //public static void DeleteContact_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

		public static void BindCommandsToWindow(Window window)
		{
			window.CommandBindings.Add(
				new CommandBinding(MainWindow, MainWindow_Executed, MainWindow_CanExecute));
            //window.CommandBindings.Add(
            //    new CommandBinding(EditContact, EditContact_Executed, EditContact_CanExecute));
            //window.CommandBindings.Add(
            //    new CommandBinding(DeleteContact, DeleteContact_Executed, DeleteContact_CanExecute));
		}
	}
}

