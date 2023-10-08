using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading;
using System.Xml.Linq;

namespace Assessment2
{
    class Room
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal RoomRate { get; set; }
        public bool IsOccupied { get; set; }
        public string GuestName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }


    }

    class HotelManagementSystem
    {
        private List<Room> rooms;

        public HotelManagementSystem()
        {
            rooms = new List<Room>();
        }

        public void AddRooms()
        {
            Console.Write("Enter the room number: ");
            int roomNumber;

            try
            {
                roomNumber = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Unhandled Exception: System.FormatException: Input string was not in a correct format.");
                return;
            }

            Console.Write("Enter the room type: ");
            string roomType = Console.ReadLine();

            Console.Write("Enter the room rate per night: $");
            decimal roomRate;

            try
            {
                roomRate = decimal.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid room rate format. Please enter a valid decimal number.");
                return;
            }

            Room room = new Room
            {
                RoomNumber = roomNumber,
                RoomType = roomType,
                RoomRate = roomRate,
                IsOccupied = false,
                GuestName = "",


            };

            rooms.Add(room);
            Console.WriteLine("Room added successfully!");
        }

        public void DisplayRooms()
        {
            Console.WriteLine("Hotel Rooms:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room Number: {room.RoomNumber}");
                Console.WriteLine($"Room Type: {room.RoomType}");
                Console.WriteLine($"Room Rate per Night: {room.RoomRate:C}");
                Console.WriteLine($"Occupied: {(room.IsOccupied ? "Yes" : "No")}");
                if (room.IsOccupied)
                {
                    Console.WriteLine($"Guest Name: {room.GuestName}");

                }
                Console.WriteLine("-------------------------");
            }
        }


        
       public void AllocateRooms()
          {
            Console.Write("Enter the room number to allocate: ");
            int roomNumber;

            try
            {
                roomNumber = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid room number format. Please enter a valid number.");
                return;
            }

            var room = rooms.Find(r => r.RoomNumber == roomNumber);

            if (room != null)
            {
                if (!room.IsOccupied)
                {
                    Console.Write("Enter the guest name: ");
                    room.GuestName = Console.ReadLine();

                    Console.Write("Enter check-in date (yyyy-MM-dd): ");
                    DateTime checkInDate;

                    try
                    {
                        checkInDate = DateTime.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd).");
                        return;
                    }

                    Console.Write("Enter check-out date (yyyy-MM-dd): ");
                    DateTime checkOutDate;

                    try
                    {
                        checkOutDate = DateTime.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid date format. Please enter a valid date (yyyy-MM-dd).");
                        return;
                    }

                    room.CheckInDate = checkInDate;
                    room.CheckOutDate = checkOutDate;
                    room.IsOccupied = true;
                    Console.WriteLine("Room allocated successfully!");
                }
                else
                {
                    Console.WriteLine("Room is already occupied.");
                }
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }
      
       
        public void DeAllocateRooms()
        {
            try
            {
                Console.Write("Enter room number to deallocate: ");
                int roomNumber = int.Parse(Console.ReadLine());

                var room = rooms.Find(r => r.RoomNumber == roomNumber);

                if (room != null)
                {
                    if (room.IsOccupied)
                    {
                        room.IsOccupied = false;
                        room.GuestName = "";
                        room.CheckInDate = DateTime.MinValue;
                        room.CheckOutDate = DateTime.MinValue;
                        Console.WriteLine("Room deallocated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Room is not occupied.");
                    }
                }
                else
                {
                    Console.WriteLine("invalid operation. Room is not found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid room number format. Please enter a valid number.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid operation. Room deallocation failed.");
            }
        }

        public void DisplayRoomAllocationDetails()
        {
            Console.Write("Enter the room number to display allocation details: ");
            int roomNumber;

            try
            {
                roomNumber = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid room number format. Please enter a valid number.");
                return;
            }

            var room = rooms.Find(r => r.RoomNumber == roomNumber);

            if (room != null)
            {
                if (room.IsOccupied)
                {
                    Console.WriteLine($"Room Number: {room.RoomNumber}");
                    Console.WriteLine($"Guest Name: {room.GuestName}");
                    Console.WriteLine($"Check-In Date: {room.CheckInDate.ToString("yyyy-MM-dd")}");
                    Console.WriteLine($"Check-Out Date: {room.CheckOutDate.ToString("yyyy-MM-dd")}");
                }
                else
                {
                    Console.WriteLine("Room is not occupied.");
                }
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }


        public void Billing()
        {
            Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!");
        }

        public void SaveRoomAllocationToFile()
        {
            // create a file
            FileStream fs = new FileStream("C:\\Users\\binaya\\source\\repos\\lhms_studentid.txt", FileMode.Create);
            fs.Close();

            try
            {
                string file = @"C:\Users\binaya\source\repos\lhms_studentid.txt";
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    foreach (var room in rooms)
                    {
                        if (room.IsOccupied)
                        {
                            // Add a timestamp to the entry
                            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            writer.WriteLine($"Timestamp: {timestamp}");
                            writer.WriteLine($"Room Number: {room.RoomNumber}");
                            writer.WriteLine($"Guest Name: {room.GuestName}");
                            writer.WriteLine($"Check-In Date: {room.CheckInDate.ToString("yyyy-MM-dd")}");
                            writer.WriteLine($"Check-Out Date: {room.CheckOutDate.ToString("yyyy-MM-dd")}");
                            writer.WriteLine("-------------------------");
                        }
                    }
                }

                Console.WriteLine("Room allocation details saved to the file 'lhms_studentid.txt' successfully!");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unauthorized access. Unable to save to the file.");
            }
            catch (IOException)
            {
                Console.WriteLine("An error occurred while saving the file.");
            }
        }


        public void ShowRoomAllocationFromFile()
        {
            // Specify the file path
            string filePath = "C:\\Users\\binaya\\source\\repos\\lhms_studentid.txt";

            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unauthorized access. Unable to read the file.");
            }
            catch (IOException)
            {
                Console.WriteLine("An error occurred while reading the file.");
            }
        }
        public void BackupRoomAllocation()
        {
            // Create backup file
            FileStream fs = new FileStream("C:\\Users\\binaya\\source\\repos\\lhms_studentid_backup.txt", FileMode.Create);
            fs.Close();

            try
            {
                string backupfile = @"C:\\Users\\binaya\\source\\repos\\lhms_studentid_backup.txt";
                string filePath = "C:\\Users\\binaya\\source\\repos\\lhms_studentid.txt";
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        using (StreamWriter writer = new StreamWriter(backupfile, true))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                // Append to the backup file
                                writer.WriteLine(line);
                            }
                        }
                    }

                    // Delete the content of the source file
                    File.WriteAllText(filePath, string.Empty);
                    Console.WriteLine("Backup completed successfully and the backup file is lhms_studentid_backup.txt");
                }
                else
                {
                    Console.WriteLine("Source file not found.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unauthorized access. Unable to perform the backup.");
            }
            catch (IOException)
            {
                Console.WriteLine("An error occurred while performing the backup.");
            }
        }




        static void Main(string[] args)
        {
            HotelManagementSystem hotelSystem = new HotelManagementSystem();
            while (true)
            {
                Console.WriteLine("********************************************************************");
                Console.WriteLine("LANGHAM HOTEL MANAGEMENT SYSTEM MENU: ");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save The Room Allocation To a File");
                Console.WriteLine("8. Show The Room Allocation From a File");
                Console.WriteLine("9. Exit");
                Console.WriteLine("10. BackupRoomAllocation");
                Console.WriteLine("********************************************************************");
                Console.Write("Please Enter your choice Here: ");

                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice format. Please enter a valid number.");
                    continue;
                }
                try
                {
                    switch (choice)
                    {
                        case 1:
                            hotelSystem.AddRooms();
                            break;
                        case 2:
                            hotelSystem.DisplayRooms();
                            break;
                        case 3:
                            hotelSystem.AllocateRooms();
                            break;
                        case 4:
                            hotelSystem.DeAllocateRooms();
                            break;
                        case 5:
                            hotelSystem.DisplayRoomAllocationDetails();
                            break;
                        case 6:
                            hotelSystem.Billing();
                            break;
                        case 7:
                            hotelSystem.SaveRoomAllocationToFile();
                            break;
                        case 8:
                            hotelSystem.ShowRoomAllocationFromFile();
                            break;
                        case 9:
                            Environment.Exit(0);
                            break;
                        case 10:
                            hotelSystem.BackupRoomAllocation();
                            break;
                       
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"FormatException: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"InvalidOperationException: {ex.Message}");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"FileNotFoundException: {ex.Message}");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"UnauthorizedAccessException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}