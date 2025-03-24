using System.Text;

namespace Tutorial1.model.container;

public
    abstract class AbstractContainer : IContainer
{
    
    private static long CONTAINER_ID_COUNTER = 1;
    
    protected static readonly string SERIAL_NUMBER_FORMAT = "KON-{0}-{1}";

    protected static readonly string DEFAULT_OVERFILL_MESSAGE =
        "Cargo weight is overfilling maximum possible containers %s weight which is %d kg.";

    public double CargoWeightKg { get; protected set; }

    public double HeightCm { get; set; }

    public double WeightKg { get; set; }

    public string SerialNumber { get; protected set; }

    public double MaxCargoWeightKg { get; set; }


    public AbstractContainer(double cargoWeightKg, double heightCm, double weightKg, double maxCargoWeightKg)
    {
        HeightCm = heightCm;
        WeightKg = weightKg;
        MaxCargoWeightKg = maxCargoWeightKg;
        GenerateSerialNumber();
        Load(cargoWeightKg);
    }

    public double GetTotalWeight()
    {
        return WeightKg + CargoWeightKg;
    }
    
    public double GetLoadedPercentage(double cargoWeight)
    {
        double newCargoWeight = cargoWeight + CargoWeightKg;
        return (newCargoWeight / MaxCargoWeightKg) * 100;
    }

    public double GetLoadedPercentage()
    {
        return ((double)CargoWeightKg / MaxCargoWeightKg) * 100;
    }

    protected long GetGeneratedContainerId()
    {
        return CONTAINER_ID_COUNTER++;
    }
    
    protected abstract void GenerateSerialNumber();
    
    public abstract void Load(double cargoWeight);

    public abstract double Unload();

    public override string ToString()
    {
        return $"Container: container mass: {WeightKg} kg, height: {HeightCm} cm, loaded {CargoWeightKg} / {MaxCargoWeightKg} kg, serial number {SerialNumber}";
    }
}