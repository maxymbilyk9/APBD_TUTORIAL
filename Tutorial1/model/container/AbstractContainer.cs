namespace Tutorial1.model.container;

public
    abstract class AbstractContainer : IContainer
{
    
    private static long CONTAINER_ID_COUNTER = 0;
    
    protected static readonly string SERIAL_NUMBER_FORMAT = "KON-%s-%d";

    protected static readonly string DEFAULT_OVERFILL_MESSAGE =
        "Cargo weight is overfilling maximum possible containers %s weight which is %d kg.";

    public int CargoWeight { get; set; }

    public int Height { get; set; }

    public int Weight { get; set; }

    public string SerialNumber { get; protected set; }

    public int MaxCargoWeight { get; set; }


    public AbstractContainer(int cargoWeight, int height, int weight, int maxCargoWeight)
    {
        CargoWeight = cargoWeight;
        Height = height;
        Weight = weight;
        MaxCargoWeight = maxCargoWeight;
        GenerateSerialNumber();
    }

    protected abstract void GenerateSerialNumber();


    public abstract void Load(int cargoWeight);

    public abstract int Unload();

    public double GetLoadedPercentage(int cargoWeigth)
    {
        double newCargoWeight = cargoWeigth + CargoWeight;
        return (newCargoWeight / MaxCargoWeight) * 100;
    }

    public double GetLoadedPercentage()
    {
        return ((double)CargoWeight / MaxCargoWeight) * 100;
    }

    protected long GetGeneratedContainerId()
    {
        return CONTAINER_ID_COUNTER++;
    }
}