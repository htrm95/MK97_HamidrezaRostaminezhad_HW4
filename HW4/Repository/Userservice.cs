﻿using CsvHelper;
using HW4.Abstracts;
using HW4.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HW4.Repository
{
    public class Userservice : IUserService
    {
        List<Users> users = new List<Users>();
        public string filePath = "C:\\Users\\DeveloperPC\\Source\\Repos\\MK97_HamidrezaRostaminezhad_HW4\\HW4\\Files\\New Microsoft Excel Worksheet.csv";
        public bool AddUser(Users user)
        {            
            users = GetAllUser();
            var serarchuser = from  a in users
                              where a.Email == user.Email
                              select a.Email ;
            if (serarchuser != null)
            {
                users.Add(user);
                SaveOnCsv(users);
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public bool DeleteUser(Users user)
        {
            users = GetAllUser();
            var serarchuser = from a in users
                              where a.Id == user.Id
                              select a.Id;
            if (serarchuser!= null)
            {
                List<Users> tempUsers = new List<Users>();
                foreach (Users item in users)
                {
                    if(item.Id != user.Id)
                        tempUsers.Add(item);
                }
                SaveOnCsv(tempUsers);
                return true;
            }
            return false;
        }

        public List<Users> GetAllUser()
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Users>();
                if (records != null)
                {
                    return records.ToList();
                }                    
                else return null;
            }
            
        }

        public bool UpdateUser(Users user)
        {
            users = GetAllUser();
            var serarchuser = from a in users
                              where a.Id == user.Id
                              select a.Id;
            if (serarchuser != null)
            {
                List<Users> tempUsers = new List<Users>();
                foreach (Users item in users)
                {
                    if (item.Id != user.Id)
                        tempUsers.Add(item);
                    else
                    {
                        tempUsers.Add(user);
                    }
                }
                SaveOnCsv(tempUsers);
                return true;
            }
            return false;
        }
        public bool SaveOnCsv(List<Users> users)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Users>();
                    csv.NextRecord();
                    foreach (var User in users)
                    {
                        csv.WriteRecord(User);
                        csv.NextRecord();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
