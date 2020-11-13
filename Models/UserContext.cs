using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using MySqlConnector;

namespace Labs.Models
{
    public class UserContext
    {
        public string ConnectionString { get; set; }
        public string NameBD { get; set; }

        public UserContext(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.NameBD = "rentcardb";
            if (FindRole("admin") == null) AddRole(new Role() { Id = 1, Name = "admin" });
            if (FindRole("user") == null) AddRole(new Role() { Id = 2, Name = "user" });
            if (FindRole("manager") == null) AddRole(new Role() { Id = 3, Name = "manager" });
            if (FindRole("supplier") == null) AddRole(new Role() { Id = 4, Name = "supplier" });
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
                MySqlCommand cmd = new MySqlCommand($"select status.name,password from user inner join status on user.id_status=status.id where email='{email}';", conn);

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
                using MySqlCommand cmd = new MySqlCommand($"select * from {NameBD}.user where email = '{email}'", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int? id_cl=null;
                    string str = reader["id_client"].ToString();
                    if (reader["id_client"].ToString() == "") id_cl = null;
                    else id_cl = Convert.ToInt32(reader["id_client"]);
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        dateofregistration = Convert.ToDateTime(reader["dateofregistration"]),
                        id_role = Convert.ToInt32(reader["id_role"]),
                        id_status = Convert.ToInt32(reader["id_status"]),
                        dateofbeginblock= Convert.ToDateTime(reader["date_beginblock"]),
                        dateofendbock = Convert.ToDateTime(reader["date_endblock"]),
                        id_client = reader["id_client"].ToString()=="" ?(int?)null: Convert.ToInt32(reader["id_client"]),
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
                using MySqlCommand cmd = new MySqlCommand($"select * from {NameBD}.user where email = '{email}' and password = '{password}'", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        dateofregistration = Convert.ToDateTime(reader["dateofregistration"]),
                        id_role = Convert.ToInt32(reader["id_role"]),
                        id_status = Convert.ToInt32(reader["id_status"]),
                        dateofbeginblock = Convert.ToDateTime(reader["date_beginblock"]),
                        dateofendbock = Convert.ToDateTime(reader["date_endblock"]),
                        id_client = reader["id_client"].ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_client"]),
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
                MySqlCommand cmd = new MySqlCommand($"select * from user where id = {id}", conn);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                        dateofregistration = Convert.ToDateTime(reader["dateofregistration"]),
                        id_role = Convert.ToInt32(reader["id_role"]),
                        id_status = Convert.ToInt32(reader["id_status"]),
                        dateofbeginblock = Convert.ToDateTime(reader["date_beginblock"]),
                        dateofendbock = Convert.ToDateTime(reader["date_endblock"]),
                        id_client = reader["id_client"].ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_client"]),

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
                MySqlCommand cmd = new MySqlCommand($"select * from role where name = '{name}'", conn);

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
                MySqlCommand cmd = new MySqlCommand($"select * from role where id = {id}", conn);

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

        public Address FindAddress(Address addr)
        {
            Address address = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string command = $"select * from address where country='{addr.country}' and " +
                    $"city='{addr.city}' and type1='{addr.type1}' and type2='{addr.type2}' and street='{addr.street}' and numhouse={addr.numhouse} and numapartment={addr.numapartment} and " +
                    $"address.index='{addr.index}' and housephone='{addr.housephone}' and mobilephone='{addr.mobilephone}' and email='{addr.email}';";
                MySqlCommand cmd = new MySqlCommand(command, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        address = new Address()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            country = reader["country"].ToString(),
                            type1 = Enum.Parse<TypeCity>(reader["type1"].ToString()),
                            city = reader["city"].ToString(),
                            type2 = Enum.Parse<TypeStreet>(reader["type2"].ToString()),
                            street = reader["street"].ToString(),
                            numhouse = Convert.ToInt32(reader["numhouse"]),
                            numapartment = Convert.ToInt32(reader["numapartment"]),
                            index = reader["index"].ToString(),
                            housephone = reader["housephone"].ToString(),
                            mobilephone = reader["mobilephone"].ToString(),
                            email = reader["email"].ToString(),
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
                            country = reader["country"].ToString(),
                            type1 = Enum.Parse<TypeCity>(reader["type1"].ToString()),
                            city = reader["city"].ToString(),
                            type2 = Enum.Parse<TypeStreet>(reader["type2"].ToString()),
                            street = reader["street"].ToString(),
                            numhouse = Convert.ToInt32(reader["numhouse"]),
                            numapartment = Convert.ToInt32(reader["numapartment"]),
                            index = reader["index"].ToString(),
                            housephone = reader["housephone"].ToString(),
                            mobilephone = reader["mobilephone"].ToString(),
                            email = reader["email"].ToString(),
                        };
                    }
                }
            }
            return address;
        }


        public Supplier FindSupplierByClient(int idcl)
        {
            Supplier supp = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from {NameBD}.supplier where id_client = '{idcl}'", conn);

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
                            id_client = Convert.ToInt32(reader["id_client"]),
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
                MySqlCommand cmd = new MySqlCommand($"select * from supplier where id = {id}", conn);

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
                            id_client = Convert.ToInt32(reader["id_client"]),
                        };
                    }
                }
            }
            return supp;
        }


        public int GetIdSupplier(Supplier findsupp)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from supplier where firmname = {findsupp.firmname}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["Id"]);
                    }
                }
            }
            return id;
        }


        public Car FindCar(int id)
        {
            Car car = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from car where id = {id};", conn);

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
                            status = reader["status"].ToString(),
                            country= reader["country"].ToString(),
                            city = reader["city"].ToString(),
                            Image = reader["image"].ToString(),
                            ImageMimeType = reader["imagemimetype"].ToString(),
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
                MySqlCommand cmd = new MySqlCommand($"select * from car where model='{c.Model}' and mark='{c.Mark}' and color='{c.Color}' and goverment_number='{c.Goverment_number}' and " +
                    $"year={c.Year} and id_supplier={c.id_supplier} and price={c.Price} and status='{car.status}';", conn);

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
                            status = reader["status"].ToString(),
                            country = reader["country"].ToString(),
                            city = reader["city"].ToString(),
                            Image = reader["image"].ToString(),
                            ImageMimeType = reader["imagemimetype"].ToString(),
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
                    $"and 'surname'='{pass.surname}' and 'name'='{pass.name}' and 'patronymic'='{pass.patronymic}';";


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
                            surname = reader["surname"].ToString(),
                            name = reader["name"].ToString(),
                            patronymic = reader["patronymic"].ToString(),
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
                            surname = reader["surname"].ToString(),
                            name = reader["name"].ToString(),
                            patronymic = reader["patronymic"].ToString(),
                        };
                    }
                }
            }
            return passport;

        }


        public int GetIdClient(Client cl)
        {
            int id = -1;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"select * from client where id_passport={cl.id_passport} and id_address={cl.id_address};";
                cmd.CommandText = command;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["Id"]);
                    }
                }
            }
            return id;

        }


        public Client FindClient(int id)
        {
            Client client = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from client where id={id};", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new Client()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            id_passport = Convert.ToInt32(reader["id_passport"]),
                            id_address = Convert.ToInt32(reader["id_address"]),
                        };
                    }
                }
            }
            return client;
        }


        public Sale FindSale(int id)
        {
            Sale sale = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from sale where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sale = new Sale()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),      
                            id_payment = (reader["id_payment"]).ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            summ = Convert.ToInt32(reader["summ"]),
                            status = reader["status"].ToString(),
                        };
                    }
                }
            }
            return sale;
        }

        public Sale FindSale(Sale s)
        {
            Sale sale = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from sale where date1='{s.date1.ToString("yyyy-MM-dd HH:mm:ss")}' and id_client={s.id_client} and id_car={s.id_car} and " +
                    $"id_payment={s.id_payment} and date2='{s.date2.ToString("yyyy-MM-dd HH:mm:ss")}' and date3='{s.date3.ToString("yyyy-MM-dd HH:mm:ss")}' and price={s.summ};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sale = new Sale()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = (reader["id_payment"]).ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            summ = Convert.ToInt32(reader["price"]),
                            status = reader["status"].ToString(),
                        };
                    }
                }
            }
            return sale;
        }

        public Payment FindPayment(int id)
        {
            Payment pay = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from payment where id={id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pay = new Payment()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date = Convert.ToDateTime(reader["date"].ToString()),
                            amount = (decimal?)reader["amount"],
                            withdrawAmount= (decimal?)reader["withdrawAmount"],
                            sender= reader["sender"].ToString(),
                            operation_Id= reader["operation_id"].ToString(),                           
                        };
                    }
                }
            }
            return pay;
        }



        public bool AddNewUser(User user)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command;                
                command = $"INSERT INTO `{NameBD}`.`user`(`Email`, `password`, `dateofregistration`, `id_role`, `id_status`, `date_beginblock`, `date_endblock`, `id_client`) VALUES('{ user.Email }', '{ user.Password }', '{ user.dateofregistration.ToString("yyyy-MM-dd HH:mm:ss") }', '{ user.id_role}', '{user.id_status}', '{ user.dateofbeginblock.ToString("yyyy-MM-dd HH:mm:ss") }', '{user.dateofendbock.ToString("yyyy-MM-dd HH:mm:ss") }', null);";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }
        public int AddAddress(Address address)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"INSERT INTO address (`country`, `type1`, `city`, `type2`, `street`, `numhouse`, `numapartment`, `index`, `housephone`, `mobilephone`, `email`) " +
                    $"VALUES('{address.country}','{address.type1}','{address.city}','{address.type2}','{address.street}','{address.numhouse}',{address.numapartment}," +
                    $"'{address.index}','{address.housephone}','{address.mobilephone}','{address.email}');";

                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();


                command = $"select * from address where id=(select max(id) from address);";
                int id = 0;

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

        public int AddSupplier(Supplier supp)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into supplier(firmname,id_legaladdress,unn,id_client) VALUES('{supp.firmname}',{supp.id_address},'{supp.unn}','{supp.id_client}');";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();


                command = $"select * from supplier where id=(select max(id) from supplier);";
                int id = 0;

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

        public bool AddCar(Car car)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into car(mark,model,color,goverment_number,year,id_supplier,price,status,country,city,image,imagemimetype) " +
                    $"VALUES('{car.Mark}','{car.Model}','{car.Color}','{car.Goverment_number}',{car.Year},{car.id_supplier},{car.Price},'{car.status}','{car.country}'," +
                    $"'{car.city}','{car.Image}','{car.ImageMimeType}');";  //System.Text.Encoding.Default.GetString(car.Image)
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
                string command = $"insert into passport(`passport1`,`passport2`, `passport3`, `date1`, `date2`,`authority`,`sex`, `date3`, `surname`, `name`, `patronymic`) " +
                    $"VALUES('{pass.passport1}',{pass.passport2},'{pass.passport3}','{pass.date1.ToString("yyyy-MM-dd")}','{pass.date2.ToString("yyyy-MM-dd")}','{pass.authority}'," +
                    $"'{pass.sex}','{pass.date3.ToString("yyyy-MM-dd")}','{pass.surname}','{pass.name}','{pass.patronymic}');";
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
            }
        }


        public int AddClient(Client cl)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into client(`id_passport`, `id_address`) values({cl.id_passport},{cl.id_address}); ";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();

                command = $"select * from client where id=(select max(id) from client);";
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


        public int AddPayment(Payment pay)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into payment(`date`, `amount`, `withdrawAmount`, `sender`, `operation_id`) values('{pay.date.ToString("yyyy-MM-dd HH:mm:ss")}',{pay.amount},{pay.withdrawAmount},'{pay.sender}','{pay.operation_Id}'); ";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();

                command = $"select * from payment where id=(select max(id) from payment);";

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


        public int AddSale(Sale sale)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"insert into sale(`date1`,`id_client`,`id_car`,`date2`,`date3`,`summ`,`status`) values('{sale.date1.ToString("yyyy-MM-dd HH:mm:ss")}',{sale.id_client},{sale.id_car}, " +
                    $"'{sale.date2.ToString("yyyy-MM-dd HH:mm:ss")}','{sale.date3.ToString("yyyy-MM-dd HH:mm:ss")}',{sale.summ},'{sale.status}'); ";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();

                command = $"select * from sale where id=(select max(id) from sale);";


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

        public int CanAddSale(Sale sale)
        {
            if (sale.date2 > sale.date3 || sale.date2.AddSeconds(59) < DateTime.Now || sale.date3.AddSeconds(59) < DateTime.Now)
            {
                return 1;
            }

            var sales = GetSalesByCar(sale.id_car);
            foreach(Sale s in sales)
            {
                if((sale.date2>s.date2&&sale.date2<s.date3)|| (sale.date3 > s.date2 && sale.date3 < s.date3))
                {
                    return 2;
                }
                    
            }
            return 0;
        }

        public bool DeleteUser(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from user where id={id}";
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
                string command = $"delete from supplier where id={id}";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool DeleteSale(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from sale where id={id};";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool CancelSale(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"delete from sale where id={id};";
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
                string command = $"delete from car where id={id};";
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
                string command = $"delete from client where id={id}";
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

                if (user.dateofbeginblock.Equals(null))
                    command = $"Update user set Email='{user.Email}',password='{user.Password}',id_role={user.id_role},id_status={user.id_status}," +
                    $"date_beginblock=null,date_endblock=null," +
                    $"id_client={user.id_client}  where id={id}";

                else command = $"Update user set Email='{user.Email}',password='{user.Password}',id_role={user.id_role},id_status={user.id_status}," +
                $"date_beginblock='{user.dateofbeginblock.ToString("yyyy-MM-dd HH:mm:ss")}',date_endblock='{user.dateofendbock.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $"id_client={user.id_client}   where id={id}";


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
                    $"date2='{pass.date2}', authority='{pass.authority}', sex='{pass.sex}', date3='{pass.date3}', surname='{pass.surname}', name='{pass.name}', patronymic='{pass.patronymic}' where id={pass.Id}";


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


                string command = $"update address set id_country='{addr.country}', " +
    $"city='{addr.city}', type1='{addr.type1}', type2='{addr.type2}', street='{addr.street}', numhouse='{addr.numhouse}', numapartment='{addr.numapartment}', " +
    $"address.index='{addr.index}', housephone='{addr.housephone}', mobilephone='{addr.mobilephone}', email='{addr.email}' where id={addr.Id}";

                cmd.CommandText = command;


                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UpdateClient(Client cl)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = $"update client set id_passport={cl.id_passport}, id_address={cl.id_address} where id={cl.Id};";

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

                string command = $"update car set `mark` = '{car.Mark}',`model` = '{car.Model}', `color` ='{car.Color}', `goverment_number` = '{car.Goverment_number}', `year` = {car.Year}," +
                    $" `id_supplier` = {car.id_supplier}, `price` = {car.Price}, `status`='{car.status}', `country`='{car.country}', `city`='{car.city}',`image`='{car.Image}',`imagemimetype`='{car.ImageMimeType}'  where `id` = {car.Id};";

                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool UpdateCarWithoutImage(Car car)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                string command = $"update car set `mark` = '{car.Mark}',`model` = '{car.Model}', `color` ='{car.Color}', `goverment_number` = '{car.Goverment_number}', `year` = {car.Year}," +
                    $" `id_supplier` = {car.id_supplier}, `price` = {car.Price}, `status`='{car.status}', `country`='{car.country}', `city`='{car.city}'  where `id` = {car.Id};";

                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UpdateCarStatus(int id,string status)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"update car set `status`='{status}'  where `id` = {id};";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool UpdateSupplier(Supplier supp)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"update sale set `id_client`={supp.id_client},`firmname`='{supp.firmname}',`id_legaladdress`={supp.id_address}, " +
                    $"`unn`='{supp.unn}' where id={supp.Id};";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool UpdateSale(Sale sale)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"update sale set `date1`=now(),`id_client`={sale.id_client},`id_car`={sale.id_car},`id_payment`={sale.id_payment}, " +
                    $"`date2`='{sale.date2.ToString("yyyy-MM-dd HH:mm:ss")}',`date3`='{sale.date3.ToString("yyyy-MM-dd HH:mm:ss")}',`summ`={sale.summ}, `status`='{sale.status}'  where id={sale.Id};";
                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool UpdateSaleStatus(int id,string st)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command = $"update sale set `status`='{st}'  where id={id};";
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
                string command = $"Update user set id_role={role.Id} where id={id}";
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
                string command = $"Update user set id_status={status.Id} where id={id}";
                cmd.CommandText = command;

                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }


        public bool CreateEventStatusCar(Sale sale)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command;

                command = $"use {NameBD};" +
                    $" Create event set_status_car_{sale.Id} On schedule at '{sale.date2.ToString("yyyy-MM-dd HH:mm:ss")}' Do update car set status = 'Используется' where id = {sale.id_car};" +
                    $" Create event remove_status_car_{sale.Id} On schedule at '{sale.date3.ToString("yyyy-MM-dd HH:mm:ss")}' Do update car set status = 'Свободен' where id = {sale.id_car};";

                cmd.CommandText = command;
                int res = cmd.ExecuteNonQuery();
                if (res == 1) return true;
                else return false;
            }
        }

        public bool DropEventStatusCar(Sale sale)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string command;

                command = $"use {NameBD};" +
                    $"Drop event if exists set_status_car_{sale.Id}; " +
                    $"Drop event if exists remove_status_car_{sale.Id}; ";

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

                command = $"use {NameBD}; Update user set id_status={user.id_status}, date_beginblock='{user.dateofbeginblock.ToString("yyyy-MM-dd HH:mm:ss")}',date_endblock='{user.dateofendbock.ToString("yyyy-MM-dd HH:mm:ss")}' where id={id};" +
                    $" Drop event if exists unblock_user_{id}; Create event unblock_user_{id} On schedule at '{user.dateofendbock.ToString("yyyy-MM-dd HH:mm:ss")}' Do" +
                    $" update user set id_status = (select id from status where name = 'notblock'), date_beginblock = 0, date_endblock = 0 where id = {id};";

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

                command = $"Update user set id_status={user.id_status}, date_beginblock=0, date_endblock=0 where id={id};";
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
                string command = $"Update user set id_status={user.id_status}, date_beginblock=0, date_endblock=0 where id={id};";
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
                string command = "INSERT INTO role(id,name) " + $"VALUES({role.Id},'{role.Name}');";
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
                MySqlCommand cmd = new MySqlCommand($"select * from {NameBD}.user", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Email = reader["email"].ToString(),
                            Password = reader["password"].ToString(),
                            dateofregistration = Convert.ToDateTime(reader["dateofregistration"]),
                            id_role = Convert.ToInt32(reader["id_role"]),
                            id_status = Convert.ToInt32(reader["id_status"]),
                            //id_client = Convert.ToInt32(reader["id_client"]),
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
                MySqlCommand cmd = new MySqlCommand("select * from role;", conn);

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


        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> list = new List<Supplier>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from supplier", conn);

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
                MySqlCommand cmd = new MySqlCommand("select * from car", conn);

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
                            status = reader["status"].ToString(),
                            Image= reader["image"].ToString(),
                            ImageMimeType=reader["imagemimetype"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        public List<Car> GetAllCars(string mark, int? yearmin, int? yearmax)
        {
            List<Car> list = new List<Car>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string command = "select * from car ";
                bool hascr = false;
                if(mark!="Все")
                {
                    if (hascr) command += $" and mark='{mark}'";
                    else
                    {
                        command += $" where mark='{mark}'";
                        hascr = true;
                    }
                }
                else { }
                if (yearmin!=null)
                {
                    if (hascr) command += $" and year>={yearmin} ";
                    else
                    {
                        command += $" where year>={yearmin} ";
                        hascr = true;
                    }
                }
                if (yearmax != null)
                {
                    if (hascr) command += $" and year<={yearmax} ";
                    else
                    {
                        command += $" where year<={yearmax} ";
                        hascr = true;
                    }
                }


                


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
                            status = reader["status"].ToString(),
                            Image = reader["image"].ToString(),
                            ImageMimeType = reader["imagemimetype"].ToString(),
                        });
                    }
                }
            }
            return list;
        }


        public List<Car> GetAllCars(int id_supp)
        {
            List<Car> list = new List<Car>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string command = $"select * from car where id_supplier={id_supp};";
                
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
                            status = reader["status"].ToString(),
                            Image = reader["image"].ToString(),
                            ImageMimeType = reader["imagemimetype"].ToString(),
                        });
                    }
                }
            }
            return list;
        }


        public List<Client> GetAllClients()
        {
            List<Client> list = new List<Client>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from client", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Client()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            id_passport = Convert.ToInt32(reader["id_passport"]),
                            id_address = Convert.ToInt32(reader["id_address"]),

                        });
                    }
                }
            }
            return list;
        }

        public List<Sale> GetAllSales()
        {
            List<Sale> list = new List<Sale>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sale", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sale()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = (reader["id_payment"]).ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            summ = Convert.ToInt32(reader["price"]),
                            status= reader["status"].ToString(),
                        });
                    }
                }
            }
            return list;
        }
        public List<Sale> GetSalesByCar(int id_car)
        {
            List<Sale> list = new List<Sale>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from sale where id_car={id_car};", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sale()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = (reader["id_payment"]).ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            summ = Convert.ToInt32(reader["summ"]),
                            status = reader["status"].ToString(),
                        });
                    }
                }
            }
            return list;
        }
        public List<Sale> GetSalesByClient(int id_cl)
        {
            List<Sale> list = new List<Sale>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from sale where id_client={id_cl}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sale()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = (reader["id_payment"]).ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            summ = Convert.ToInt32(reader["summ"]),
                            status = reader["status"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

        public List<Sale> GetSalesBySupplier(int id_supp)
        {
            List<Sale> list = new List<Sale>();

            using (MySqlConnection conn = GetConnection())
            {
                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from sale where id_car in (select id from car where id_supplier = {id_supp});", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sale()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            date1 = Convert.ToDateTime(reader["date1"].ToString()),
                            id_client = Convert.ToInt32(reader["id_client"]),
                            id_car = Convert.ToInt32(reader["id_car"]),
                            id_payment = (reader["id_payment"]).ToString() == "" ? (int?)null : Convert.ToInt32(reader["id_payment"]),
                            date2 = Convert.ToDateTime(reader["date2"].ToString()),
                            date3 = Convert.ToDateTime(reader["date3"].ToString()),
                            summ = Convert.ToInt32(reader["summ"]),
                            status = reader["status"].ToString(),
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
                MySqlCommand cmd = new MySqlCommand($"select password from user where email = '{email}'", conn);
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
