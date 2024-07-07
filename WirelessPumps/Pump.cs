namespace WirelessPumps;

public class Pump
{
    public Pump(string name, string faction)
    {
        Name = name;
        Faction = faction;
    }

    public string Name { get; }
    
    public string Faction { get; }
}