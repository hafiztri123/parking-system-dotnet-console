public class Vehicle
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public string Type { get; set; }
    public DateTime EntryTime { get; set; }

    public Vehicle(string registrationNumber, string color, string type)
    {
        RegistrationNumber = registrationNumber;
        Color = color;
        Type = type;
        EntryTime = DateTime.Now;
    }
}