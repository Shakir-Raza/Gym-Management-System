using System;
using System.Collections.Generic;
using System.IO;

namespace GymManagementSystem
{
    class Program
    {
        static List<Equipment> equipments = new List<Equipment>();
        static List<Member> members = new List<Member>();
        static List<Staff> staffs = new List<Staff>();

        static void Main(string[] args)
        {
            LoadData();

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Welcome to the Gym Management System");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1. Add Equipment");
                Console.WriteLine("2. Add Member");
                Console.WriteLine("3. Delete Member");
                Console.WriteLine("4. Add Staff");
                Console.WriteLine("5. Delete Staff");
                Console.WriteLine("6. Login");
                Console.WriteLine("7. Search Member");
                Console.WriteLine("8. View Equipment");
                Console.WriteLine("9. Save and Exit");
                Console.WriteLine("------------------------------------");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddEquipment();
                        break;
                    case 2:
                        AddMember();
                        break;
                    case 3:
                        DeleteMember();
                        break;
                    case 4:
                        AddStaff();
                        break;
                    case 5:
                        DeleteStaff();
                        break;
                    case 6:
                        Login();
                        break;
                    case 7:
                        SearchMember();
                        break;
                    case 8:
                        ViewEquipment();
                        break;
                    case 9:
                        SaveData();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddEquipment()
        {
            Console.WriteLine("Add Equipment");
            Console.WriteLine("-------------");

            Console.Write("Enter the equipment name: ");
            string name = Console.ReadLine();

            Console.Write("Enter the equipment description: ");
            string description = Console.ReadLine();

            Equipment equipment = new Equipment(name, description);
            equipments.Add(equipment);

            Console.WriteLine("Equipment added successfully!");
        }

        static void AddMember()
        {
            Console.WriteLine("Add Member");
            Console.WriteLine("----------");

            Console.Write("Enter the member name: ");
            string name = Console.ReadLine();

            Console.Write("Enter the member ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Member member = new Member(name, id);
            members.Add(member);

            Console.WriteLine("Member added successfully!");
        }

        static void DeleteMember()
        {
            Console.WriteLine("Delete Member");
            Console.WriteLine("-------------");

            Console.Write("Enter the member ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Member member = members.Find(m => m.ID == id);
            if (member != null)
            {
                members.Remove(member);
                Console.WriteLine("Member deleted successfully!");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        static void AddStaff()
        {
            Console.WriteLine("Add Staff");
            Console.WriteLine("----------");

            Console.Write("Enter the staff name: ");
            string name = Console.ReadLine();

            Console.Write("Enter the staff ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Staff staff = new Staff(name, id);
            staffs.Add(staff);

            Console.WriteLine("Staff added successfully!");
        }

        static void DeleteStaff()
        {
            Console.WriteLine("Delete Staff");
            Console.WriteLine("-------------");

            Console.Write("Enter the staff ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Staff staff = staffs.Find(s => s.ID == id);
            if (staff != null)
            {
                staffs.Remove(staff);
                Console.WriteLine("Staff deleted successfully!");
            }
            else
            {
                Console.WriteLine("Staff not found.");
            }   
        }

        static void Login()
        {
            Console.WriteLine("Login");
            Console.WriteLine("-----");

            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            
        }

        static void SearchMember()
        {
            Console.WriteLine("Search Member");
            Console.WriteLine("-------------");

            Console.Write("Enter the member ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Member member = members.Find(m => m.ID == id);
            if (member != null)
            {
                Console.WriteLine("Member found:");
                Console.WriteLine($"Name: {member.Name}, ID: {member.ID}");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        static void ViewEquipment()
        {
            Console.WriteLine("View Equipment");
            Console.WriteLine("--------------");

            foreach (var equipment in equipments)
            {
                Console.WriteLine($"Name: {equipment.Name}, Description: {equipment.Description}");
            }
        }

        static void SaveData()
        {
            Console.WriteLine("Saving data to file...");

            using (StreamWriter writer = new StreamWriter("gym_data.txt"))
            {
                foreach (var equipment in equipments)
                {
                    writer.WriteLine($"Equipment: {equipment.Name}, Description: {equipment.Description}");
                }

                writer.WriteLine();

                foreach (var member in members)
                {
                    writer.WriteLine($"Member: {member.Name}, ID: {member.ID}");
                }

                writer.WriteLine();

                foreach (var staff in staffs)
                {
                    writer.WriteLine($"Staff: {staff.Name}, ID: {staff.ID}");
                }
            }

            Console.WriteLine("Data saved successfully!");
        }

        static void LoadData()
        {
            Console.WriteLine("Loading data from file...");

            if (File.Exists("gym_data.txt"))
            {
                using (StreamReader reader = new StreamReader("gym_data.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Equipment:"))
                        {
                            string[] parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            string name = parts[1].Trim();
                            string description = parts[2].Trim();
                            Equipment equipment = new Equipment(name, description);
                            equipments.Add(equipment);
                        }
                        else if (line.StartsWith("Member:"))
                        {
                            string[] parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            string name = parts[1].Trim();
                            int id = int.Parse(parts[2].Trim());
                            Member member = new Member(name, id);
                            members.Add(member);
                        }
                        else if (line.StartsWith("Staff:"))
                        {
                            string[] parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            string name = parts[1].Trim();
                            int id = int.Parse(parts[2].Trim());
                            Staff staff = new Staff(name, id);
                            staffs.Add(staff);
                        }
                    }
                }

                Console.WriteLine("Data loaded successfully!");
            }
            else
            {
                Console.WriteLine("No data file found. Starting with empty data.");
            }
        }
    }

    class Equipment
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Equipment(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    class Member
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Member(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }

    class Staff
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Staff(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}
