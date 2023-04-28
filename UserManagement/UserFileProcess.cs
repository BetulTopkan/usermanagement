using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsertManagement
{
    /// <summary>
    /// Kullanıcıların listesinin olduğu dosya işlemlerini yürüten sınıf
    /// </summary>
    public class UserFileProcess
    {
        //const: Sabit değişmeyecek değer
        private const String _filePath = "C:\\Users\\Betül\\source\\repos\\Osman-Betul Ortak Proje\\Osman-Betul Ortak Proje\\userList.txt";

        /// <summary>
        /// Kullanıcı listesinin son halini dosyaya yazar (günceller)
        /// Kullanıcı listesinin halini parametre olarak alır
        /// </summary>
        public void Write(List<UserModel> userList)
        {
            using (var writer = new StreamWriter(_filePath))
            {
                foreach (UserModel user in userList)
                {
                    writer.WriteLine(user.Name + " " + user.Password);
                }
            }
        }

        /// <summary>
        /// Kullanıcı listesini dosyadan okuyup parametre olarak döner
        /// </summary>
        public List<UserModel> Read()
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }
            var userList = new List<UserModel>();
            
            //TODO: _filePath içindeki dosya yoksa oluştursun

            using (var reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length == 2)
                    {
                        UserModel user = new UserModel
                        {
                            Name = parts[0],
                            Password = parts[1]
                        };
                        userList.Add(user);
                    }
                }
            }
            return userList;
        }
    }
}
