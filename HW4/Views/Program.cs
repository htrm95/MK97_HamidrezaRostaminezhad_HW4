﻿using HW4.Models;
using HW4.Repository;
using HW4.Repository.Exceptions;
using HW4.Service.Exceptions;
using HW4.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Userservice userservice = new Userservice();
            //this while is for exit from program 
            while (true)
            {
                UserMenu.loginOrRegester();
                string loginnumber = Console.ReadLine();
                int LoginN;
                if (int.TryParse(loginnumber, out LoginN))
                {
                    try
                    {
                        if (LoginN == 1)
                        {
                            try
                            {
                                UserMenu.loginGetUsername();
                                string Email = Console.ReadLine();
                                UserMenu.loginGetPassword();
                                string Password = Console.ReadLine();
                                Users user = userservice.LoginUser(Email, Password);
                                if (user != null)
                                {

                                    while (true)
                                    {
                                        UserMenu.Arival(user.Name);
                                        string UserFanc = Console.ReadLine();
                                        int UserFancNum;
                                        if (int.TryParse(UserFanc, out UserFancNum))
                                        {
                                            if (UserFancNum == 1)
                                            {
                                                try
                                                {
                                                    List<Users> users = userservice.GetAllUser();
                                                    if (users != null)
                                                    {
                                                        Console.Clear();
                                                        foreach (Users user1 in users)
                                                        {
                                                            Console.WriteLine($"ID: {user1.Id}\t Name:{user1.Name}\t LastName:{user1.Lastname}\t Email:{user1.Email}\t ");
                                                        }
                                                        UserMenu.WatingforContinue();
                                                    }
                                                    else
                                                    {
                                                        throw new NullFileError();
                                                    }
                                                }
                                                catch (Exception ex)
                                                { Console.WriteLine(ex.Message); }
                                            }
                                            else if (UserFancNum == 2)
                                            {
                                                Console.Clear();
                                                Users adduser = new Users();
                                                Console.WriteLine("Enter Email of user");
                                                adduser.Email = Console.ReadLine();
                                                Console.WriteLine("Enter Name of user");
                                                adduser.Name = Console.ReadLine();
                                                Console.WriteLine("Enter LastName of user");
                                                adduser.Lastname = Console.ReadLine();
                                                Console.WriteLine("Enter password of user");
                                                adduser.Password = Console.ReadLine();
                                                Console.WriteLine("Enter Mobile of user");
                                                while (true)
                                                {
                                                    string Mobile = Console.ReadLine();
                                                    if(Validations.IsValidPhone(Mobile))
                                                    {
                                                        adduser.Mobile = Mobile;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.Clear() ;
                                                        Console.WriteLine("Mobile is invalid try again:");
                                                    }

                                                }
                                                
                                                Console.WriteLine("Enter berth Day of user like this ( 2003/2/13 ):");
                                                while (true)
                                                {
                                                    string brithdate = Console.ReadLine();
                                                    if (Validations.CheckDateTime(brithdate))
                                                    {
                                                        if (DateTime.Parse(brithdate) < DateTime.Now)
                                                        {
                                                            adduser.BirthDate = DateTime.Parse(brithdate);
                                                            break;
                                                        }
                                                        else { Console.WriteLine("you cant enter date bigest today"); }

                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Enter true berth date like this 2003/2/13 or this 2003-2-13:");
                                                    }

                                                }
                                                Console.WriteLine("Enter Role of user \n1 = admin\t 2 = user :");
                                                while (true)
                                                {
                                                    int role;
                                                    if (int.TryParse(Console.ReadLine(), out role) && (role == 1 || role == 2))
                                                    {
                                                        adduser.RoleId = role;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Enter true Role Id (1 = admin\t 2 = user):");
                                                    }
                                                }
                                                if (userservice.AddUser(adduser))
                                                {
                                                    Console.WriteLine("add user id sucsess!!");
                                                    UserMenu.WatingforContinue();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("The email is duplicate!! ");
                                                    UserMenu.WatingforContinue();
                                                }


                                            }
                                            else if (UserFancNum == 3)
                                            {
                                                try
                                                {
                                                    List<Users> users = userservice.GetAllUser();
                                                    if (users != null)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Enter id to delete user:");
                                                        foreach (Users user1 in users)
                                                        {
                                                            Console.WriteLine($"ID: {user1.Id}\t Name:{user1.Name}\t LastName:{user1.Lastname}\t Email:{user1.Email}\t ");
                                                        }
                                                        while (true)
                                                        {
                                                            int deletId;
                                                            if (int.TryParse(Console.ReadLine(), out deletId))
                                                            {
                                                                if (userservice.DeleteUser(deletId))
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("Delete is sucsessfull");
                                                                }

                                                                else
                                                                {
                                                                    Console.WriteLine("ID not found!");
                                                                }
                                                                UserMenu.WatingforContinue();
                                                                break;

                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("number isn't valid!\n");
                                                                Console.WriteLine("Enter valid number:");
                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        throw new NullFileError();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }
                                            }
                                            else if (UserFancNum == 4)
                                            {
                                                List<Users> users = userservice.GetAllUser();
                                                Users updateuser = new Users();
                                                try
                                                {
                                                    if (users != null)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Enter ID to update user:");
                                                        foreach (Users user1 in users)
                                                        {
                                                            Console.WriteLine($"ID: {user1.Id}\t Name:{user1.Name}\t LastName:{user1.Lastname}\t Email:{user1.Email}\t ");
                                                        }
                                                        while (true)
                                                        {
                                                            int updatetId;
                                                            if (int.TryParse(Console.ReadLine(), out updatetId))
                                                            {
                                                                updateuser.Id = updatetId;
                                                                Console.Clear();
                                                                Console.WriteLine("Enter Email of user");
                                                                updateuser.Email = Console.ReadLine();
                                                                Console.WriteLine("Enter Name of user");
                                                                updateuser.Name = Console.ReadLine();
                                                                Console.WriteLine("Enter LastName of user");
                                                                updateuser.Lastname = Console.ReadLine();
                                                                Console.WriteLine("Enter password of user");
                                                                updateuser.Password = Console.ReadLine();
                                                                Console.WriteLine("Enter Mobile of user");
                                                                updateuser.Mobile = Console.ReadLine();
                                                                Console.WriteLine("Enter berth Day of user like this ( 2003/2/13 ):");
                                                                while (true)
                                                                {
                                                                    string brithdate = Console.ReadLine();
                                                                    if (Validations.CheckDateTime(brithdate))
                                                                    {
                                                                        if (DateTime.Parse(brithdate) < DateTime.Now)
                                                                        {
                                                                            updateuser.BirthDate = DateTime.Parse(brithdate);
                                                                            break;
                                                                        }
                                                                        else { Console.WriteLine("you cant enter date bigest today"); }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.Clear();
                                                                        Console.WriteLine("Enter true berth date like this 2003/2/13 or this 2003-2-13:");
                                                                    }

                                                                }
                                                                Console.WriteLine("Enter Role of user \n1 = admin\t 2 = user :");
                                                                while (true)
                                                                {
                                                                    int role;
                                                                    if (int.TryParse(Console.ReadLine(), out role) && (role == 1 || role == 2))
                                                                    {
                                                                        updateuser.RoleId = role;
                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.Clear();
                                                                        Console.WriteLine("Enter true Role Id (1 = admin\t 2 = user):");
                                                                    }
                                                                }
                                                                if (userservice.UpdateUser(updateuser))
                                                                {
                                                                    Console.WriteLine("Update is sucsess!!");
                                                                    UserMenu.WatingforContinue();
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("The ID not found!! ");
                                                                    UserMenu.WatingforContinue();
                                                                }
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.Clear();
                                                                Console.WriteLine("number isn't valid!\n");
                                                                Console.WriteLine("Enter valid id number:");
                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        throw new NullFileError();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex.Message);
                                                }

                                            }
                                            else if (UserFancNum == 5)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("the function id invalid");
                                                UserMenu.WatingforContinue();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("the function id invalid");
                                            UserMenu.WatingforContinue();
                                        }
                                    }
                                }
                                else
                                {
                                    throw new LoginError();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }

                        if (LoginN == 2)
                        {
                            Console.WriteLine("this function is not in homework ..");
                            Console.WriteLine("we add this comming soon..");
                            UserMenu.WatingforContinue();
                        }
                        else
                        {
                            throw new LoginError();
                        }
                    }
                    catch(Exception ex) 
                    { 
                        Console.WriteLine(ex.Message);
                    }



                }

                UserMenu.WatingforContinue();
            }
        }
    }
}