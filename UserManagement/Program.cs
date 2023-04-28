using System;
using System.Collections.Generic;
using System.Linq;

namespace UsertManagement
{
    public class Program
    {
        private static UserModel _admin;
        private static List<UserModel> _users;
        private static UserFileProcess _db; //db:database:veritabanı
        

        static void Main(string[] args)
        {
            _db = new UserFileProcess();
            _users = _db.Read();
            _admin = GetAdminInfo();
            Application();
        }

        static UserModel GetAdminInfo()
        {
            return new() { Name = "admin", Password = "1234" };
        }

        static void Application()
        {
            Login();
            Menu();
        }

        static void Login()
        {
            while (true)
            {
                Console.Write("Enter username ");
                string name = Console.ReadLine();

                Console.Write("Enter user password: ");
                string password = Console.ReadLine();

                if (name == _admin.Name && password == _admin.Password)
                {
                    Console.WriteLine("Successful login");
                    break;
                }
                else
                {
                    Console.WriteLine("Username or password is incorrect. Try again!");
                }
            }
        }

        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("1-Add New User");
                Console.WriteLine("2-Update User");
                Console.WriteLine("3-List Users");
                Console.WriteLine();

                Console.Write("Please make a choice: ");

                int election = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (election)
                {
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        UpdateUser();
                        break;
                    case 3:
                        ListUsers();
                        break;
                    default:
                        Console.WriteLine("Incorrect input. Try again!");
                        break;
                }
            }
        }

        static void AddUser()
        {
            var newUser = new UserModel();

            while (true)
            {
                Console.Write("New User Name: ");
                newUser.Name = Console.ReadLine();

                Console.Write("New User Password: ");
                newUser.Password = Console.ReadLine();

                if(string.IsNullOrEmpty(newUser.Name))
                {
                    Console.WriteLine("incorrect entry");
                }

                bool kayitVarMi = _users.Any(x => x.Name == newUser.Name);
                if (kayitVarMi)
                {
                    Console.WriteLine("User available. Try a different user!");
                }
                else
                {
                    _users.Add(newUser);
                    _db.Write(_users);
                    Console.WriteLine("New user added.");
                    break;
                }
            }
        }

        static void UpdateUser()
        {
            Console.Write("Enter the user name to be updated: ");
            String userName = Console.ReadLine();

           
            if(string.IsNullOrEmpty(userName))
            {
                Console.WriteLine("incorrect entry");
            }

            UserModel existUser = _users.FirstOrDefault(x => x.Name == userName);
            if (existUser != null)
            {
                while (true)
                {
                    Console.Write("Enter new password: ");
                    string newPassword = Console.ReadLine();

                    if (string.IsNullOrEmpty(newPassword))
                    {
                        Console.WriteLine("incorrect entry. Try again");
                    }
                    else
                    {
                        existUser.Password = newPassword;
                        _db.Write(_users);
                        Console.WriteLine("Update Successful");
                    }
                }
                
            }
            else
            {
                Console.WriteLine("No registered user found.");
            }

        }

        static void ListUsers()
        {
            if (_users.Count == 0)
            {
                Console.WriteLine("No users to show in the list!");
                return;
            }

            foreach (var item in _users)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
