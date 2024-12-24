public class ParkingSystem
{
    private List<ParkingLot> parkingLots;

    public ParkingSystem(int totalLots)
    {
        parkingLots = new List<ParkingLot>();
        for (int i = 1; i <= totalLots; i++)
        {
            parkingLots.Add(new ParkingLot(i));
        }
    }

    public string Park(string registrationNumber, string color, string type)
    {
        if (!IsValidVehicleType(type))
            return "Invalid vehicle type. Only Car and Motorcycle are allowed.";

        var availableSlot = parkingLots.FirstOrDefault(x => !x.IsOccupied);
        if (availableSlot == null)
            return "Sorry, parking lot is full";

        availableSlot.Vehicle = new Vehicle(registrationNumber, color, type);
        availableSlot.IsOccupied = true;

        return $"Allocated slot number: {availableSlot.SlotNumber}";
    }

    public string Leave(int slotNumber)
    {
        var slot = parkingLots.FirstOrDefault(x => x.SlotNumber == slotNumber);
        if (slot == null)
            return "Invalid slot number";

        slot.Vehicle = null;
        slot.IsOccupied = false;
        return $"Slot number {slotNumber} is free";
    }

    public string Status()
    {
        var status = "Slot No. Registration No Type Colour\n";
        foreach (var lot in parkingLots.Where(x => x.IsOccupied))
        {
            status += $"{lot.SlotNumber} {lot.Vehicle.RegistrationNumber} {lot.Vehicle.Type} {lot.Vehicle.Color}\n";
        }
        return status;
    }

    public int GetVehicleCountByType(string type)
    {
        return parkingLots.Count(x => x.IsOccupied && x.Vehicle.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
    }

    public string GetRegistrationNumbersByOddPlate()
    {
        var oddPlates = parkingLots
            .Where(x => x.IsOccupied && IsOddPlate(x.Vehicle.RegistrationNumber))
            .Select(x => x.Vehicle.RegistrationNumber);
        return string.Join(", ", oddPlates);
    }

    public string GetRegistrationNumbersByEvenPlate()
    {
        var evenPlates = parkingLots
            .Where(x => x.IsOccupied && !IsOddPlate(x.Vehicle.RegistrationNumber))
            .Select(x => x.Vehicle.RegistrationNumber);
        return string.Join(", ", evenPlates);
    }

    public string GetRegistrationNumbersByColor(string color)
    {
        var vehicles = parkingLots
            .Where(x => x.IsOccupied && x.Vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Vehicle.RegistrationNumber);
        return string.Join(", ", vehicles);
    }

    public string GetSlotNumbersByColor(string color)
    {
        var slots = parkingLots
            .Where(x => x.IsOccupied && x.Vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .Select(x => x.SlotNumber.ToString());
        return string.Join(", ", slots);
    }

    public string GetSlotNumberByRegistrationNumber(string registrationNumber)
    {
        var slot = parkingLots.FirstOrDefault(x => x.IsOccupied && 
            x.Vehicle.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));
        return slot != null ? slot.SlotNumber.ToString() : "Not found";
    }

    private bool IsValidVehicleType(string type)
    {
        return type.Equals("Mobil", StringComparison.OrdinalIgnoreCase) || 
               type.Equals("Motor", StringComparison.OrdinalIgnoreCase);
    }

    private bool IsOddPlate(string registrationNumber)
    {
        var numbers = new string(registrationNumber.Where(char.IsDigit).ToArray());
        return numbers.Length > 0 && int.Parse(numbers[^1].ToString()) % 2 != 0;
    }
}