using EntitiesLibrary;
using HashersLibrary;
using MoiveHubSystem.Commands;
using MoiveHubSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MoiveHubSystem.ViewModels
{
	class AuthenticationViewModel: INotifyPropertyChanged
    {
        IAccountRepository _accountRepo = new AccountRepository();
        IRoleRepository _roleRepo = new RoleRepository();

        public AuthenticationViewModel()
        {
            
        }

        string _passwordBuffer = "";


        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _passwordBuffer = String.Empty;
                _passwordBuffer = value;


                string buff = String.Empty;
				for (int i = 0; i < _passwordBuffer.Length; i++)
				{
                    buff += '*';
				}


                _password = buff;
                OnPropertyChanged("Password");
            }
        }


        public ICommand ExecRegistation
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    try
                    {
                        var isSuchLogin = _accountRepo.GetAll().Where(a => a.Login == Login).FirstOrDefault();
                        if (isSuchLogin != null)
                        {
                            throw new Exception("Such Login Already Exist");
                        }

                        Account newAcc = new()
                        {
                            Login = this.Login,
                            Password = SHA256Generator.ProduceSHA256Hash(_passwordBuffer),
                            RoleId = (int)Role_Id.User
                        };
                        

                        _accountRepo.Insert(newAcc);
                        ClearFields();

                        MessageBox.Show($"Your account has been registered!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show($"{err.InnerException?.Message ?? err.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => (!string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password)) && (ValidateLogin() && ValidatePassword()));
            }
        }

        public ICommand ExecLoginization
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    try
                    {
                        string hashedPasswordToCheck = SHA256Generator.ProduceSHA256Hash(_passwordBuffer);

                        Account accountByLogin = _accountRepo.GetAll().Where(acc => acc.Login == _login).FirstOrDefault();
                        if (accountByLogin == null)
                        {
                            throw new Exception("User with this login has not been found!");
                        }
                        if (accountByLogin.Password != hashedPasswordToCheck)
                        {
                            throw new Exception($"Password is incorrect!");
                        }


                        var entryRole = _roleRepo.GetAll().Where(role => role.Id == accountByLogin.RoleId).FirstOrDefault();
                        if (entryRole == null)
                        {
                            throw new Exception("Cannot proccess user with invalid role! Please add such role.");
                        }

                        ClearFields();

                        // hiding login/registration window
                        (obj as AuthenticationWindow).Visibility = Visibility.Hidden;
                        MainWindow mapWnd = new();
                        switch (entryRole.Id)
                        {
                            case (int)Role_Id.Moderator:
                                MessageBox.Show($"Welcome {entryRole.Name}!", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            case (int)Role_Id.User:
                                MessageBox.Show($"Welcome {entryRole.Name}!", "Client", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            default:
                                throw new Exception("Cannot proccess user with invalid role! Please add such role.");
                        }
                        

                        mapWnd.userName.Text = $"{accountByLogin.Login}";
                        mapWnd.ShowDialog();

                        (obj as AuthenticationWindow).Close();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show($"{err.InnerException?.Message ?? err.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
					}
					finally
					{
                        
                    }
                }, obj => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password));
            }
        }


        private bool ValidateLogin()
        {
            if (Login.Length >= 50)
            {
                return false;
            }

            Regex regExp = new(@"^(?=.*[A-Za-z0-9]$)[A-Za-z\d.-][A-Za-z\d.-]{0,19}");
            if (!regExp.IsMatch(Login))
            {
                return false;
            }
            return true;
        }

        private bool ValidatePassword()
        {
            Regex regExp = new(@"^(?=.*[A-Z])(?=.*\d)(?!.*(.)\1\1)[a-zA-Z0-9@]{6,12}$");
            if (!regExp.IsMatch(_passwordBuffer))
            {
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            Login = String.Empty;
            Password = String.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
