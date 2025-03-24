using Tutorial1.service;

namespace Tutorial1.model.container;

public
    class LiquidContainer(double cargoWeightKg, double heightCm, double weightKg, double maxCargoWeightKg, bool isHazard)
    : AbstractContainer(cargoWeightKg, heightCm, weightKg, maxCargoWeightKg), IHazardNotifier
{

    public bool IsHazardCargo { get; set; } = isHazard;
    
    protected override void GenerateSerialNumber()
    {
        SerialNumber = String.Format(AbstractContainer.SERIAL_NUMBER_FORMAT, "L", GetGeneratedContainerId());
    }

    public override void Load(double cargoWeight)
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

        CargoWeightKg += cargoWeight;
    }

    public override double Unload()
    {
        double unloadedWeight = CargoWeightKg;
        CargoWeightKg -= unloadedWeight;
        return unloadedWeight;
    }

    public void Notify(string message, string containerSerialNumber)
    {
        Console.WriteLine($"Dangerous situation in container {containerSerialNumber}:\n{message}");
    }
    
    public override string ToString()
    {
        String baseString = base.ToString();

        baseString = baseString.Replace("Container", "Liquid Container");

        return $"{baseString}, is dangerous cargo: {IsHazardCargo}";
    }
    
}