using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoiveHubSystem.Resources
{
	/// <summary>
	/// Логика взаимодействия для BindablePasswordBox.xaml
	/// </summary>
	/// 

	// Author: SingletonSean https://www.youtube.com/watch?v=G9niOcc5ssw&t=546s

	public partial class BindablePasswordBox : UserControl
	{
		private bool _passwordIsChanging;

		public string Password
		{
			get { return (string)GetValue(PasswordProperty); }
			set { SetValue(PasswordProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PasswordProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PasswordProperty =
			DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), 
				new FrameworkPropertyMetadata(string.Empty,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

		private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is BindablePasswordBox passwordBox)
			{
				passwordBox.UpdatePassword();
			}
		}

		private void UpdatePassword()
		{
			if (!_passwordIsChanging)
			{
				passwordBox.Password = Password;
			} 
		}

		public BindablePasswordBox()
		{
			InitializeComponent();
		}

		private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			_passwordIsChanging = true;
			Password = passwordBox.Password;
			_passwordIsChanging = false;
		}
	}
}
