using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Labs.Models
{
    public class UserContext
    {
        public string ConnectionString { get; set; }

        public UserContext(string connectionString)
        {
            this.ConnectionString = connectionString;
            if (FindRole("admin") == null) AddRole(new Role() { Id = 1, Name = "admin" });
            if (FindRole("user") == null) AddRole(new Role() { Id = 2, Name = "user" });
            if (FindRole("director") == null) AddRole(new Role() { Id = 3, Name = "director" });
            if (FindRole("manager") == null) AddRole(new Role() { Id = 4, Name = "manager" });
            if (FindStatus("notblock") == null) AddStatus(new Status() { Id = 1, Name = "notblock" });
            if (FindStatus("block") == null) AddStatus(new Status() { Id = 2, Name = "block" });
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public Tuple<string, string> Login(string email)
        {
            string name = null;
            string pass = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select status.name,password from users inner join status on users.id_status=status.id where email='{email}';", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    name = reader["name"].ToString();
                    pass = reader["password"].ToString();
                }
            }
            return Tuple.Create(name, pass);
        }

        public User FindUser(string email)
        {
            User user = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using MySqlCommand cmd = new MySqlCommand($"select * from users where email = '{email}'", conn);
                //MySqlCommand cmd = new MySqlCommand($"select * from users where email = '{email}'", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        dataofregistration = Convert.ToDateTime(reader["dataofregistration"]),
                        RoleId = Convert.ToInt32(reader["id_role"]),
                        StatusId = Convert.ToInt32(reader["id_status"]),
                        IdName1 = Convert.ToInt32(reader["idname1"]),
                        IdName2 = Convert.ToInt32(reader["idname2"]),
                        IdName3 = Convert.ToInt32(reader["idname3"]),
                        id_client = Convert.ToInt32(reader["id_client"]),
                    };
                }
            }
            if (user == null) return null;
            else return user;

        }




        public User FindUser(string email, string password)
        {
            User user = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using MySqlCommand cmd = new MySqlCommand($"select * from users where email = '{email}' and password = '{password}'", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        dataofregistration = Convert.ToDateTime(reader["dataofregistration"]),
                        RoleId = Convert.ToInt32(reader["id_role"]),
                        StatusId = Convert.ToInt32(reader["id_status"]),
                        IdName1 = Convert.ToInt32(reader["idname1"]),
                        IdName2 = Convert.ToInt32(reader["idname2"]),
                        IdName3 = Convert.ToInt32(reader["idname3"]),
                        id_client = Convert.ToInt32(reader["id_client"]),
                    };
                }
            }
            if (user == null) return null;
            else return user;

        }


        public User FindUser(int id)
        {
            User user = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from users where id = {id}", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        dataofregistration = Convert.ToDateTime(reader["dataofregistration"]),
                        RoleId = Convert.ToInt32(reader["id_role"]),
                        StatusId = Convert.ToInt32(reader["id_status"]),
                        IdName1 = Convert.ToInt32(reader["idname1"]),
                        IdName2 = Convert.ToInt32(reader["idname2"]),
                        IdName3 = Convert.ToInt32(reader["idname3"]),
                        id_client = Convert.ToInt32(reader["id_client"]),

                    };
                }
            }
            if (user == null) return null;
            else return user;

        }

        public Role FindRole(string name)
        {
            Role role = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from roles where name = '{name}'", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    role = new Role()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["name"].ToString(),
                    };
                }
            }
            if (role == null) return null;
            else return role;
        }

        public Role FindRole(int id)
        {
            Role role = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from roles where id = {id}", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    role = new Role()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["name"].ToString(),
                    };
                }
            }
            if (role == null) return null;
            else return role;
        }

        public Status FindStatus(string name)
        {
            Status status = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from status where name = '{name}'", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status = new Status()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["name"].ToString(),
                    };
                }
            }
            if (status == null) return null;
            else return status;
        }

        public Status FindStatus(int id)
        {
            Status status = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from status where id = {id}", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status = new Status()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["name"].ToString(),
                    };
                }
            }
            if (status == null) return null;
            else return status;
        }


        public Name1 FindName1(string name)
        {
            Name1 name1 = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from name1 where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name1 = new Name1()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString()
                        };
                    }
                }
            }
            return name1;
        }

        public Name2 FindName2(string name)
        {
            Name2 name2 = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from name2 where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name2 = new Name2() { Id = Convert.ToInt32(reader["Id"]), Name = reader["name"].ToString() };
                    }
                }
            }
            return name2;
        }

        public Name3 FindName3(string name)
        {
            Name3 name3 = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from name3 where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name3 = new Name3() { Id = Convert.ToInt32(reader["Id"]), Name = reader["name"].ToString() };
                    }
                }
            }
            return name3;
        }

        public Name1 FindName1(int id)
        {
            Name1 name1 = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from name1 where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name1 = new Name1()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString()
                        };
                    }
                }
            }
            return name1;
        }

        public Name2 FindName2(int id)
        {
            Name2 name2 = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from name2 where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name2 = new Name2() { Id = Convert.ToInt32(reader["Id"]), Name = reader["name"].ToString() };
                    }
                }
            }
            return name2;
        }

        public Name3 FindName3(int id)
        {
            Name3 name3 = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from name3 where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name3 = new Name3() { Id = Convert.ToInt32(reader["Id"]), Name = reader["name"].ToString() };
                    }
                }
            }
            return name3;
        }

        public Bank FindBank(int id)
        {
            Bank bank = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from bank_details where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bank = new Bank()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            bank_name = reader["bank_name"].ToString(),
                            innclient = reader["innclient"].ToString(),
                            bank_account = reader["bank_account"].ToString(),
                            id_address = Convert.ToInt32(reader["id_address"]),
                            bank_bik = reader["bank_bik"].ToString(),
                        };
                    }
                }
            }
            return bank;
        }

        public Bank FindBank(string name)
        {
            Bank bank = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from bank_details where bank_name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bank = new Bank()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            bank_name = reader["bank_name"].ToString(),
                            innclient = reader["innclient"].ToString(),
                            bank_account = reader["bank_account"].ToString(),
                            id_address = Convert.ToInt32(reader["id_address"]),
                            bank_bik = reader["bank_bik"].ToString(),
                        };
                    }
                }
            }
            return bank;
        }

        public Country FindCountry(string name)
        {
            Country country = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from country where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        country = new Country()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return country;
        }

        public Country FindCountry(int id)
        {
            Country country = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from country where id = {id};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        country = new Country()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return country;
        }

        public Region FindRegion(string name)
        {
            Region reg = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from region where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reg = new Region()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return reg;
        }

        public Region FindRegion(int id)
        {
            Region reg = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from region where id = {id};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reg = new Region()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return reg;
        }

        public District FindDistrict(string name)
        {
            District distr = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from district where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        distr = new District()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return distr;
        }

        public District FindDistrict(int id)
        {
            District distr = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from district where id = {id};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        distr = new District()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return distr;
        }


        public City FindCity(string name)
        {
            City city = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from city where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        city = new City()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return city;
        }

        public City FindCity(int id)
        {
            City city = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from city where id={id};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        city = new City()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return city;
        }


        public Street FindStreet(string name)
        {
            Street str = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from street where name = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        str = new Street()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return str;
        }

        public Street FindStreet(int id)
        {
            Street str = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from street where id={id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        str = new Street()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        };
                    }
                }
            }
            return str;
        }


        public Address FindAddress(Address addr)
        {
            Address address = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string command = $"select * from address where id_country={addr.id_country} and id_region={addr.id_region} and id_district={addr.id_district} and " +
                    $"id_city={addr.id_city} and type1='{addr.type1}' and type2='{addr.type2}' and id_street={addr.id_street} and num1='{addr.num1}' and num2={addr.num2} and num3='{addr.num3}' and " +
                    $"address.index={addr.index} and num4={addr.num4} and address.code={addr.code} and mobile={addr.mobile} and email='{addr.email}';";
                MySqlCommand cmd = new MySqlCommand(command, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        address = new Address()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            id_country = Convert.ToInt32(reader["id_country"]),
                            id_region = Convert.ToInt32(reader["id_region"]),
                            id_district = Convert.ToInt32(reader["id_district"]),
                            type1 = Enum.Parse<TypeCity>(reader["type1"].ToString()),
                            id_city = Convert.ToInt32(reader["id_city"]),
                            type2 = Enum.Parse<TypeStreet>(reader["type2"].ToString()),
                            id_street = Convert.ToInt32(reader["id_street"]),
                            num1 = reader["num1"].ToString(),
                            num2 = Convert.ToInt32(reader["num2"]),
                            num3 = reader["num3"].ToString(),
                            index = Convert.ToInt32(reader["index"]),
                            num4 = Convert.ToInt32(reader["num4"]),
                            code = Convert.ToInt32(reader["code"]),
                            mobile = Convert.ToInt32(reader["mobile"]),
                            email = reader["num4"].ToString(),
                        };
                    }
                }
            }
            return address;
        }


        public Address FindAddress(int id)
        {
            Address address = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string command = $"select * from address where id={id};";
                MySqlCommand cmd = new MySqlCommand(command, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        address = new Address()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            id_country = Convert.ToInt32(reader["id_country"]),
                            id_region = Convert.ToInt32(reader["id_region"]),
                            id_district = Convert.ToInt32(reader["id_district"]),
                            type1 = Enum.Parse<TypeCity>(reader["type1"].ToString()),
                            id_city = Convert.ToInt32(reader["id_city"]),
                            type2 = Enum.Parse<TypeStreet>(reader["type2"].ToString()),
                            id_street = Convert.ToInt32(reader["id_street"]),
                            num1 = reader["num1"].ToString(),
                            num2 = Convert.ToInt32(reader["num2"]),
                            num3 = reader["num3"].ToString(),
                            index = Convert.ToInt32(reader["index"]),
                            num4 = Convert.ToInt32(reader["num4"]),
                            code = Convert.ToInt32(reader["code"]),
                            mobile = Convert.ToInt32(reader["mobile"]),
                            email = reader["num4"].ToString(),
                        };
                    }
                }
            }
            return address;
        }


        public Supplier FindSupplier(string name)
        {
            Supplier supp = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from suppliers where firmname = '{name}'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        supp = new Supplier()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            firmname = reader["firmname"].ToString(),
                            id_address = Convert.ToInt32(reader["id_legaladdress"]),
                            unn = reader["unn"].ToString(),
                            id_bank_details = Convert.ToInt32(reader["id_bank_details"]),
                        };
                    }
                }
            }
            return supp;
        }

        public Supplier FindSupplier(int id)
        {
            Supplier supp = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from suppliers where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        supp = new Supplier()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            firmname = reader["firmname"].ToString(),
                            id_address = Convert.ToInt32(reader["id_legaladdress"]),
                            unn = reader["unn"].ToString(),
                            id_bank_details = Convert.ToInt32(reader["id_bank_details"]),
                        };
                    }
                }
            }
            return supp;
        }


        public Car FindCar(int id)
        {
            Car car = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from cars where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        car = new Car()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Model = reader["model"].ToString(),
                            Mark = reader["mark"].ToString(),
                            Color = reader["color"].ToString(),
                            Goverment_number = reader["model"].ToString(),
                            Year = Convert.ToInt32(reader["year"]),
                            id_supplier = Convert.ToInt32(reader["id_supplier"]),
                            Price = Convert.ToInt32(reader["price"]),
                        };
                    }
                }
            }
            return car;
        }


        public Car FindCar(Car c)
        {
            Car car = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from cars where model='{c.Model}' and mark='{c.Mark}' and color='{c.Color}' and goverment_number='{c.Goverment_number}' and " +
                    $"year={c.Year} and id_supplier={c.id_supplier} and price={c.Price};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        car = new Car()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Model = reader["model"].ToString(),
                            Mark = reader["mark"].ToString(),
                            Color = reader["color"].ToString(),
                            Goverment_number = reader["model"].ToString(),
                            Year = Convert.ToInt32(reader["year"]),
                            id_supplier = Convert.ToInt32(reader["id_supplier"]),
                            Price = Convert.ToInt32(reader["price"]),
                        };
                    }
                }
            }
            return car;
        }


        public Passport FindPassport(Passport pass)
        {
            Passport passport = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"select * from passport where 'passport1'='{pass.passport1}' and 'passport2'={pass.passport2} and 'passport3'='{pass.passport3}' and 'date1'='{pass.date1.ToString("yyyy-MM-dd")}' " +
                    $"and 'date2'='{pass.date2.ToString("yyyy-MM-dd")}' and 'authority'='{pass.authority}' and 'sex'='{pass.sex.ToString()}' and 'date3'='{pass.date3.ToString("yyyy-MM-dd")}' " +
                    $"and 'id_name1'={pass.id_name1} and 'id_name2'={pass.id_name2} and 'id_name3'={pass.id_name3};";


                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        passport = new Passport()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            passport1 = reader["passport1"].ToString(),
                            passport2 = Convert.ToInt32(reader["passport2"]),
                            passport3 = reader["passport3"].ToString(),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            authority = reader["authority"].ToString(),
                            sex = Enum.Parse<Sex>(reader["sex"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            id_name1 = Convert.ToInt32(reader["id_name1"]),
                            id_name2 = Convert.ToInt32(reader["id_name2"]),
                            id_name3 = Convert.ToInt32(reader["id_name3"]),
                        };
                    }
                }
            }
            return passport;

        }


        public Passport FindPassport(int id)
        {
            Passport passport = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"select * from passport where id={id};";


                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        passport = new Passport()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            passport1 = reader["passport1"].ToString(),
                            passport2 = Convert.ToInt32(reader["passport2"]),
                            passport3 = reader["passport3"].ToString(),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            authority = reader["authority"].ToString(),
                            sex = Enum.Parse<Sex>(reader["sex"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            id_name1 = Convert.ToInt32(reader["id_name1"]),
                            id_name2 = Convert.ToInt32(reader["id_name2"]),
                            id_name3 = Convert.ToInt32(reader["id_name3"]),
                        };
                    }
                }
            }
            return passport;

        }


        public Clients FindClient(Clients cl)
        {
            Clients client = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"select * from clients where id_passport={cl.id_passport} and id_address={cl.id_address} and id_bank_details={cl.id_bank_details};";
                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Clients()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            id_passport = Convert.ToInt32(reader["id_passport"]),
                            id_address = Convert.ToInt32(reader["id_address"]),
                            id_bank_details = Convert.ToInt32(reader["id_bank_details"]),
                        };
                    }
                }
            }
            return client;

        }


        public Clients FindClient(int id)
        {
            Clients client = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"select * from clients where id={id}";
                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Clients()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            id_passport = Convert.ToInt32(reader["id_passport"]),
                            id_address = Convert.ToInt32(reader["id_address"]),
                            id_bank_details = Convert.ToInt32(reader["id_bank_details"]),
                        };
                    }
                }
            }
            return client;

        }


        public Sales FindSale(int id)
        {
            Sales sale = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from sales where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sale = new Sales()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            price = Convert.ToInt32(reader["price"]),
                        };
                    }
                }
            }
            return sale;
        }

        public Sales FindSale(Sales s)
        {
            Sales sale = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from sales where date1='{s.date1.ToString("yyyy-MM-dd")}' and id_client={s.id_client} and id_car={s.id_car} and " +
                    $"id_payment={s.id_payment} and date2='{s.date2.ToString("yyyy-MM-dd")}' and date3='{s.date3.ToString("yyyy-MM-dd")}' and price={s.price};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sale = new Sales()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            price = Convert.ToInt32(reader["price"]),
                        };
                    }
                }
            }
            return sale;
        }


        public bool AddName1(string name1)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO name1(name) VALUES('{name1}');";
                cmd.CommandText = command;

                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool AddName2(string name2)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO name2(name) VALUES('{name2}');";
                cmd.CommandText = command;

                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool AddName3(string name3)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO name3(name) VALUES('{name3}');";
                cmd.CommandText = command;

                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }



        public bool AddNewUser(User user)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                /*
                string command = "INSERT INTO users(Email,password,dataofregistration,id_role,id_status,idname1,idname2,idname3) " +
                        $"VALUES('{user.Email}','{user.Password}','{user.dataofregistration.ToString("yyyy-MM-dd HH:mm:ss")}',{user.RoleId},{user.StatusId},{user.IdName1},{user.IdName2},{user.IdName3});";
                        */
                string command;
                if (user.dataofbeginblock.Equals(null))
                {
                    command = "INSERT INTO users(Email,password,dataofregistration,id_role,id_status,data_beginblock,data_endblock,idname1,idname2,idname3,id_client) " +
                                        $"VALUES('{user.Email}','{user.Password}','{user.dataofregistration.ToString("yyyy-MM-dd HH:mm:ss")}',{user.RoleId},{user.StatusId}," +
                                        $"0, 0,{user.IdName1},{user.IdName2},{user.IdName3},{user.id_client});";
                }
                else
                {
                    command = "INSERT INTO users(Email,password,dataofregistration,id_role,id_status,data_beginblock,data_endblock,idname1,idname2,idname3,id_client) " +
                                        $"VALUES('{user.Email}','{user.Password}','{user.dataofregistration.ToString("yyyy-MM-dd HH:mm:ss")}',{user.RoleId},{user.StatusId}," +
                                        $"'{user.dataofbeginblock.Value.ToString("yyyy-MM-dd HH:mm:ss")}', '{user.dataofendbock.Value.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                                        $"{user.IdName1},{user.IdName2},{user.IdName3},{user.id_client});";
                }
                /*
                string command = "INSERT INTO users(Email,password,dataofregistration,id_role,id_status,data_beginblock,data_endblock,idname1,idname2,idname3) " +
                                        $"VALUES('{user.Email}','{user.Password}','{user.dataofregistration.ToString("yyyy-MM-dd HH:mm:ss")}',{user.RoleId},{user.StatusId}," +
                                        $"'{(user.dataofbeginblock.Equals(null) ? null : user.dataofbeginblock.Value.ToString("yyyy-MM-dd HH:mm:ss"))}', '{(user.dataofendbock.Equals(null) ? null : user.dataofendbock.Value.ToString("yyyy-MM-dd HH:mm:ss"))}', " +
                                        $"{user.IdName1},{user.IdName2},{user.IdName3});";*/
                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }
        public bool AddBank(Bank bank)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command;

                command = "INSERT INTO bank_details(bank_name,innclient,bank_account,id_address,bank_bik) " +
                                    $"VALUES('{bank.bank_name}','{bank.innclient}','{bank.bank_account}',{bank.id_address},'{bank.bank_bik}');";

                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }
        public bool AddCountry(Country country)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO country (name) SELECT* FROM(SELECT '{country.Name}') AS tmp WHERE NOT EXISTS(SELECT * FROM country WHERE name = '{country.Name}');";

                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }
        public bool AddRegion(Region reg)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO region (name) SELECT* FROM(SELECT '{reg.Name}') AS tmp WHERE NOT EXISTS(SELECT * FROM region WHERE name = '{reg.Name}');";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }
        public bool AddDistrict(District distr)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO district (name) SELECT* FROM(SELECT '{distr.Name}') AS tmp WHERE NOT EXISTS(SELECT * FROM district WHERE name = '{distr.Name}');";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool AddCity(City city)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO city (name) SELECT* FROM(SELECT '{city.Name}') AS tmp WHERE NOT EXISTS(SELECT * FROM city WHERE name = '{city.Name}');";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool AddStreet(Street street)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO street (name) SELECT* FROM(SELECT '{street.Name}') AS tmp WHERE NOT EXISTS(SELECT * FROM street WHERE name = '{street.Name}');";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool AddAddress(Address address)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO address (`id_country`, `id_region`, `id_district`, `type1`, `id_city`, `type2`, `id_street`, `num1`, `num2`, `num3`, `index`, `num4`, `code`, `mobile`, `email`) " +
                    $"VALUES({address.id_country},{address.id_region},{address.id_district},'{address.type1}',{address.id_city},'{address.type2}',{address.id_street},'{address.num1}',{address.num2}," +
                    $"'{address.num3}',{address.index},{address.num4},{address.code},{address.mobile},'{address.email}');";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool AddSupplier(Supplier supp)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into suppliers(firmname,id_legaladdress,unn,id_bank_details) " +
                    $"VALUES('{supp.firmname}',{supp.id_address},'{supp.unn}',{supp.id_bank_details});";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool AddCar(Car car)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into cars(mark,model,color,goverment_number,year,id_supplier,price) " +
                    $"VALUES('{car.Mark}','{car.Model}','{car.Color}','{car.Goverment_number}',{car.Year},{car.id_supplier},{car.Price});";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public int AddPassport(Passport pass)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into passport(`passport1`,`passport2`, `passport3`, `date1`, `date2`,`authority`,`sex`, `date3`, `id_name1`, `id_name2`, `id_name3`) " +
                    $"VALUES('{pass.passport1}',{pass.passport2},'{pass.passport3}','{pass.date1.ToString("yyyy-MM-dd")}','{pass.date2.ToString("yyyy-MM-dd")}','{pass.authority}'," +
                    $"'{pass.sex}','{pass.date3.ToString("yyyy-MM-dd")}',{pass.id_name1},{pass.id_name2},{pass.id_name3});";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();


                command = $"select * from passport where id=(select max(id) from passport);";


                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["id"]);                      
                    }
                }
                return id;


                //if (res == 1) return true;
                //else return false;
            }
        }


        public int AddClient(Clients cl)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into clients(`id_passport`, `id_address`, `id_bank_details`) values({cl.id_passport},{cl.id_address},{cl.id_bank_details}); ";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();

                command = $"select * from clients where id=(select max(id) from clients);";


                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["id"]);
                    }
                }
                return id;

                //if (res == 1) return true;
                //else return false;
            }
        }


        public int AddPayment(Payments pay)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into payments(`date`, `price`, `account_number`, `payer_number`) values('{pay.date.ToString("yyyy-MM-dd")}',{pay.price},'{pay.account_number}','{pay.payer_number}'); ";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();

                command = $"select * from payments where id=(select max(id) from payments);";


                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["id"]);
                    }
                }
                return id;
            }
        }


        public int AddSale(Sales sale)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into sales(`date1`,`id_client`,`id_car`,`id_payment`,`date2`,`date3`,`price`) values(now(),{sale.id_client},{sale.id_car}, {sale.id_payment}, " +
                    $"'{sale.date2.ToString("yyyy-MM-dd")}','{sale.date3.ToString("yyyy-MM-dd")}',{sale.price}); ";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();

                command = $"select * from sales where id=(select max(id) from sales);";


                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["id"]);
                    }
                }
                return id;
            }
        }

        public bool BackupUserAfterDelete(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = "Lock table users write, user_backup read; " +
                    "INSERT INTO users(id, Email, password, dataofregistration, id_role, id_status, data_beginblock, data_endblock, idname1, idname2, idname3,id_client) " +
                    "(select id_user, email, password, dataofregistration, id_role, id_status, date_beginblock, date_endblock, idname1, idname2, idname3, id_client from user_backup " +
                    $"WHERE dateofevent = (SELECT  MAX(dateofevent) from user_backup where type = 'delete') and user_backup.type = 'delete' and id_user={id}); " +
                    "delete from user_backup where dateofevent = (select * from(SELECT  MAX(dateofevent) from user_backup where user_backup.type = 'delete') as T) and user_backup.type = 'delete';" +
                    "unlock tables;";

                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool BackupUserAfterDelete(string email)
        {
            User user = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = $"use mydb; select* from user_backup where type = 'delete' and email = '{email}' order by id desc limit 1; ";
                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime? temp1, temp2;
                        try
                        {
                            temp1 = Convert.ToDateTime(reader["date_beginblock"]);
                            temp2 = Convert.ToDateTime(reader["date_endblock"]);
                        }
                        catch (InvalidCastException)
                        {
                            temp1 = null;
                            temp2 = null;
                        }


                        user = new User()
                        {
                            Id = Convert.ToInt32(reader["id_user"]),
                            Email = reader["email"].ToString(),
                            Password = reader["password"].ToString(),
                            dataofregistration = Convert.ToDateTime(reader["dataofregistration"]),
                            RoleId = Convert.ToInt32(reader["id_role"]),
                            StatusId = Convert.ToInt32(reader["id_status"]),
                            dataofbeginblock = temp1,
                            dataofendbock = temp2,
                            IdName1 = Convert.ToInt32(reader["idname1"]),
                            IdName2 = Convert.ToInt32(reader["idname2"]),
                            IdName3 = Convert.ToInt32(reader["idname3"]),
                            id_client= Convert.ToInt32(reader["id_client"]),
                        };
                    }
                }

                if (user != null)
                {
                    bool result = AddNewUser(user);

                    if (result) return true;
                    else return false;
                }
                return false;
            }
        }

        public bool BackupUserAfterUpdate(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = "Lock table users write, user_backup read; " +
                    $"delete from users where id = {id}; " +
                    "delete from user_backup where id_user = _id and type = 'delete';" +
                    "INSERT INTO users(id, Email, password, dataofregistration, id_role, id_status, data_beginblock, data_endblock, idname1, idname2, idname3,id_client) " +
                    "(select id_user, email, password, dataofregistration, id_role, id_status, date_beginblock, date_endblock, idname1, idname2, idname3, id_client from user_backup " +
                    "WHERE dateofevent = (SELECT  MAX(dateofevent) from user_backup where type = 'update') and user_backup.type = 'update'and id_user={id}); " +
                    $"delete from user_backup where dateofevent = (select * from(SELECT  MAX(dateofevent) from user_backup where user_backup.type = 'update') as T) and user_backup.type = 'update' and id_user = {id};" +
                    "unlock tables;";


                string command1 = "use mydb;Lock table users write, user_backup read;SET @DISABLE_TRIGGERS = 1;update users,(select * from user_backup WHERE dateofevent = " +
                    "(SELECT  MAX(dateofevent) from user_backup where type = 'update' and id_user = 1) and user_backup.type = 'update' and user_backup.id_user = 1) as T2" +
                    "set users.email = T2.email, users.password = T2.password, users.dataofregistration = T2.dataofregistration, users.id_role = T2.id_role, users.id_status = T2.id_status," +
                    "users.data_beginblock = T2.date_beginblock, users.data_endblock = T2.date_endblock, users.idname1 = T2.idname1, users.idname2 = T2.idname2, users.idname3 = T2.idname3, users.id_client=T2.id_client" +
                    "where users.id = T2.id_user;SET @DISABLE_TRIGGERS = 0;unlock tables;";

                cmd.CommandText = command1;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool BackupUserAfterUpdate(string email)
        {
            User user = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = $"use mydb; select* from user_backup where type = 'update' and email = '{email}' order by id desc limit 1; ";
                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime? temp1, temp2;
                        try
                        {
                            temp1 = Convert.ToDateTime(reader["date_beginblock"]);
                            temp2 = Convert.ToDateTime(reader["date_endblock"]);
                        }
                        catch (InvalidCastException)
                        {
                            temp1 = null;
                            temp2 = null;
                        }

                        user = new User()
                        {
                            Id = Convert.ToInt32(reader["id_user"]),
                            Email = reader["email"].ToString(),
                            Password = reader["password"].ToString(),
                            dataofregistration = Convert.ToDateTime(reader["dataofregistration"]),
                            RoleId = Convert.ToInt32(reader["id_role"]),
                            StatusId = Convert.ToInt32(reader["id_status"]),
                            dataofbeginblock = temp1,
                            dataofendbock = temp2,
                            IdName1 = Convert.ToInt32(reader["idname1"]),
                            IdName2 = Convert.ToInt32(reader["idname2"]),
                            IdName3 = Convert.ToInt32(reader["idname3"]),
                            id_client = Convert.ToInt32(reader["id_client"]),
                        };
                    }
                }

                if (user != null)
                {
                    bool result = UpdateUser(user.Id, user);

                    if (result) return true;
                    else return false;
                }
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from users where id={id}";
                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool DeleteBank(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from bank_details where id={id}";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool DeleteSupplier(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from suppliers where id={id}";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool DeleteCar(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from cars where id={id}";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool DeleteClient(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from clients where id={id}";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool UpdateUser(int id, User user)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command;

                if (user.dataofbeginblock.Equals(null))
                    command = $"Update users set Email='{user.Email}',password='{user.Password}',id_role={user.RoleId},id_status={user.StatusId}," +
                    $"data_beginblock=null,data_endblock=null," +
                    $"idname1={user.IdName1},idname2={user.IdName2},idname3={user.IdName3},id_client={user.id_client}  where id={id}";

                else command = $"Update users set Email='{user.Email}',password='{user.Password}',id_role={user.RoleId},id_status={user.StatusId}," +
                $"data_beginblock='{user.dataofbeginblock.Value.ToString("yyyy-MM-dd HH:mm:ss")}',data_endblock='{user.dataofendbock.Value.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $"idname1={user.IdName1},idname2={user.IdName2},idname3={user.IdName3},id_client={user.id_client}   where id={id}";


                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UpdatePassport(Passport pass)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;


                string command = $"update passport set passport1='{pass.passport1}', passport2={pass.passport2}, passport3='{pass.passport3}', date1='{pass.date1}', " +
                    $"date2='{pass.date2}', authority='{pass.authority}', sex='{pass.sex}', date3='{pass.date3}', id_name1={pass.id_name1}, id_name2={pass.id_name2}, id_name3={pass.id_name3} where id={pass.Id}";


                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UpdateAddress(Address addr)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;


                string command = $"update address set id_country={addr.id_country}, id_region={addr.id_region}, id_district={addr.id_district}, " +
    $"id_city={addr.id_city}, type1='{addr.type1}', type2='{addr.type2}', id_street={addr.id_street}, num1='{addr.num1}', num2={addr.num2}, num3='{addr.num3}', " +
    $"address.index={addr.index}, num4={addr.num4}, address.code={addr.code}, mobile={addr.mobile}, email='{addr.email}' where id={addr.Id}";

                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UpdateClient(Clients cl)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = $"update clients set id_passport={cl.id_passport}, id_address={cl.id_address}, id_bank_details={cl.id_bank_details} where id={cl.Id};";

                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UpdateCar(Car car)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = $"update cars set `mark` = '{car.Mark}',`model` = '{car.Model}', `color` ='{car.Color}', `goverment_number` = '{car.Goverment_number}', `year` = {car.Year}," +
                    $" `id_supplier` = {car.id_supplier}, `price` = {car.Price} where 'id' = {car.Id};";

                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }




        public bool UpdateSale(Sales sale)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"update sales set `date1`=now(),`id_client`={sale.id_client},`id_car`={sale.id_car},`id_payment`={sale.id_payment}, " +
                    $"`date2`='{sale.date2.ToString("yyyy-MM-dd")}',`date3`='{sale.date3.ToString("yyyy-MM-dd")}',`price`={sale.price};";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool UpdateRole(int id, Role role)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"Update users set id_role={role.Id} where id={id}";
                cmd.CommandText = command;

                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool UpdateStatus(int id, Status status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"Update users set id_status={status.Id} where id={id}";
                cmd.CommandText = command;

                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool BlockUser(int id, User user)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command;

                command = $"use mydb; Update users set id_status={user.StatusId}, data_beginblock='{user.dataofbeginblock.Value.ToString("yyyy-MM-dd HH:mm:ss")}',data_endblock='{user.dataofendbock.Value.ToString("yyyy-MM-dd HH:mm:ss")}' where id={id};" +
                    $" Drop event if exists unblock_user_{id}; Create event unblock_user_{id} On schedule at '{user.dataofendbock.Value.ToString("yyyy-MM-dd HH:mm:ss")}' Do" +
                    $" update users set id_status = (select id from status where name = 'notblock'), data_beginblock = 0, data_endblock = 0 where id = {id};";

                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool BlockUserForever(int id, User user)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command;

                command = $"Update users set id_status={user.StatusId}, data_beginblock=0, data_endblock=0 where id={id};";
                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UnblockUser(int id, User user)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"Update users set id_status={user.StatusId}, data_beginblock=0, data_endblock=0 where id={id};";
                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool AddRole(Role role)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = "INSERT INTO roles(id,name) " + $"VALUES({role.Id},'{role.Name}');";
                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }

        }


        public bool AddStatus(Status status)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = "INSERT INTO status(id,name) " + $"VALUES({status.Id},'{status.Name}');";
                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;

            }

        }

        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Email = reader["email"].ToString(),
                            Password = reader["password"].ToString(),
                            dataofregistration = Convert.ToDateTime(reader["dataofregistration"]),
                            RoleId = Convert.ToInt32(reader["id_role"]),
                            StatusId = Convert.ToInt32(reader["id_status"]),
                            IdName1 = Convert.ToInt32(reader["idname1"]),
                            IdName2 = Convert.ToInt32(reader["idname2"]),
                            IdName3 = Convert.ToInt32(reader["idname3"]),
                            id_client = Convert.ToInt32(reader["id_client"]),
                        });
                    }
                }
            }
            return list;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> list = new List<Role>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from roles;", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Role()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["name"].ToString(),
                        });
                    }
                }
            }
            return list;
        }


        public List<Status> GetAllStatuses()
        {
            List<Status> list = new List<Status>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from status;", conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Status()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["name"].ToString(),
                    });
                }
            }
            return list;
        }


        public List<Bank> GetAllBanks()
        {
            List<Bank> list = new List<Bank>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from bank_details", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Bank()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            bank_name = reader["bank_name"].ToString(),
                            innclient = reader["innclient"].ToString(),
                            bank_account = reader["bank_account"].ToString(),
                            id_address = Convert.ToInt32(reader["id_address"]),
                            bank_bik = reader["bank_bik"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> list = new List<Supplier>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from suppliers", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Supplier()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            firmname = reader["firmname"].ToString(),
                            id_address = Convert.ToInt32(reader["id_legaladdress"]),
                            unn = reader["unn"].ToString(),
                            id_bank_details = Convert.ToInt32(reader["id_bank_details"]),

                        });
                    }
                }
            }
            return list;
        }

        public List<Car> GetAllCars()
        {
            List<Car> list = new List<Car>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from cars", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Car()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Model = reader["model"].ToString(),
                            Mark = reader["mark"].ToString(),
                            Color = reader["color"].ToString(),
                            Goverment_number = reader["model"].ToString(),
                            Year = Convert.ToInt32(reader["year"]),
                            id_supplier = Convert.ToInt32(reader["id_supplier"]),
                            Price = Convert.ToInt32(reader["price"]),
                        });
                    }
                }
            }
            return list;
        }

        public List<Car> GetAllCars(string mark,string color,int? year)
        {
            List<Car> list = new List<Car>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string command = null;
                if(string.IsNullOrEmpty(mark) && string.IsNullOrEmpty(color) && year==null)
                {
                    command = "select * from cars;";
                }
                else if (string.IsNullOrEmpty(mark) && string.IsNullOrEmpty(color))
                {
                    
                    command = $"select * from cars where year={year};";
                }
                else if (string.IsNullOrEmpty(mark) && year == null)
                {
                    command = $"select * from cars where color='{color}';";
                }
                else if (string.IsNullOrEmpty(color) && year == null)
                {
                    command = $"select * from cars where mark='{mark}';";
                }
                else if (year == null)
                {
                    command = $"select * from cars where mark='{mark}' and color='{color}';";
                }
                else if (string.IsNullOrEmpty(mark))
                {
                    command = $"select * from cars where year={year} and color='{color}';";
                }
                else if (string.IsNullOrEmpty(color))
                {
                    command = $"select * from cars where year={year} and mark='{mark}';";
                }
                else command = $"select * from cars where mark='{mark}' and year={year} and color='{color}';";


                MySqlCommand cmd = new MySqlCommand(command, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Car()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Model = reader["model"].ToString(),
                            Mark = reader["mark"].ToString(),
                            Color = reader["color"].ToString(),
                            Goverment_number = reader["model"].ToString(),
                            Year = Convert.ToInt32(reader["year"]),
                            id_supplier = Convert.ToInt32(reader["id_supplier"]),
                            Price = Convert.ToInt32(reader["price"]),
                        });
                    }
                }
            }
            return list;
        }


        public List<Clients> GetAllClients()
        {
            List<Clients> list = new List<Clients>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from clients", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Clients()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            id_passport = Convert.ToInt32(reader["id_passport"]),
                            id_address = Convert.ToInt32(reader["id_address"]),
                            id_bank_details = Convert.ToInt32(reader["id_bank_details"]),

                        });
                    }
                }
            }
            return list;
        }

        public List<Sales> GetAllSales()
        {
            List<Sales> list = new List<Sales>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sales", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sales()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            price = Convert.ToInt32(reader["price"]),
                        });
                    }
                }
            }
            return list;
        }


        public string SelectHashPassword(string email)
        {
            string resstr = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select password from users where email = '{email}'", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        resstr = reader["password"].ToString();
                    }
                }
                return resstr;

            }

        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }


        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return AreHashesEqual(buffer3, buffer4);
        }


        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }


    }
}
