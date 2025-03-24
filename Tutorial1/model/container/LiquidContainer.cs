using Tutorial1.service;

namespace Tutorial1.model.container;

public
    class LiquidContainer(int cargoWeight, int height, int weight, int maxCargoWeight, bool isHazard)
    : AbstractContainer(cargoWeight, height, weight, maxCargoWeight), IHazardNotifier
{

    public bool IsHazardCargo { get; set; } = isHazard;
    
    protected override void GenerateSerialNumber()
    {
        SerialNumber = String.Format(AbstractContainer.SERIAL_NUMBER_FORMAT, "L", GetGeneratedContainerId());
    }

    public override void Load(int cargoWeight)
    {
        double loadPercentage = GetLoadedPercentage();
        double possibleLoadPercentage = GetLoadedPercentage(cargoWeight);

        if (IsHazardCargo && possibleLoadPercentage > 51)
        {
            Notify($"Container having dangerous cargo is already loaded up to {loadPercentage}%/51 and can't be loaded to {possibleLoadPercentage}", SerialNumber);
            return;
        }

        if (possibleLoadPercentage > 90)
        {
            Notify($"Container is already loaded up to {loadPercentage}%/90 and can't be loaded to {possibleLoadPercentage}%", SerialNumber);
            return;
        }

        CargoWeight += cargoWeight;
    }

    public override int Unload()
    {
        int unloadedWeight = CargoWeight;
        CargoWeight -= unloadedWeight;
        return unloadedWeight;
    }

    public void Notify(string message, string containerSerialNumber)
    {
        Console.WriteLine($"Dangerous situation in container {containerSerialNumber}:\n{message}");
    }
    
}