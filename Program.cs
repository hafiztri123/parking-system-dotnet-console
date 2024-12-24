class Program
{
    static void Main(string[] args)
    {
        ParkingSystem parkingSystem = null;
        string command;

        while (true)
        {
            command = Console.ReadLine();
            if (string.IsNullOrEmpty(command)) continue;

            var parts = command.Split(' ');
            var action = parts[0].ToLower();

            if (action == "exit") return;

            if (action == "create_parking_lot")
            {
                var totalLots = int.Parse(parts[1]);
                parkingSystem = new ParkingSystem(totalLots);
                Console.WriteLine($"Created a parking lot with {totalLots} slots");
                continue;
            }

            if (parkingSystem == null)
            {
                Console.WriteLine("Parking lot not created yet");
                continue;
            }

            try
            {
                HandleParkingCommand(parkingSystem, action, parts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void HandleParkingCommand(ParkingSystem parkingSystem, string action, string[] parts)
    {
        switch (action)
        {
            case "park":
                Console.WriteLine(parkingSystem.Park(parts[1], parts[2], parts[3]));
                break;

            case "leave":
                Console.WriteLine(parkingSystem.Leave(int.Parse(parts[1])));
                break;

            case "status":
                Console.WriteLine(parkingSystem.Status());
                break;

            case "type_of_vehicles":
                Console.WriteLine(parkingSystem.GetVehicleCountByType(parts[1]));
                break;

            case "registration_numbers_for_vehicles_with_ood_plate":
                Console.WriteLine(parkingSystem.GetRegistrationNumbersByOddPlate());
                break;

            case "registration_numbers_for_vehicles_with_event_plate":
                Console.WriteLine(parkingSystem.GetRegistrationNumbersByEvenPlate());
                break;

            case "registration_numbers_for_vehicles_with_colour":
                Console.WriteLine(parkingSystem.GetRegistrationNumbersByColor(parts[1]));
                break;

            case "slot_numbers_for_vehicles_with_colour":
                Console.WriteLine(parkingSystem.GetSlotNumbersByColor(parts[1]));
                break;

            case "slot_number_for_registration_number":
                Console.WriteLine(parkingSystem.GetSlotNumberByRegistrationNumber(parts[1]));
                break;

            default:
                Console.WriteLine("Invalid command");
                break;
        }
    }
}