using Tutorial1.exception;
using Tutorial1.service;

namespace Tutorial1.model.container;

public
    class GasContainer(int cargoWeight, int height, int weight, int maxCargoWeight, int atmospheres)
    : AbstractContainer(cargoWeight, height, weight, maxCargoWeight), IHazardNotifier
{

    public int Atmospheres { get; set; } = atmospheres;
    
    protected override void GenerateSerialNumber()
    {
        SerialNumber = String.Format(GasContainer.SERIAL_NUMBER_FORMAT, "G", GetGeneratedContainerId());
    }

    public override void Load(int cargoWeight)
    {
        int possibleWeight = CargoWeight + cargoWeight;
        
        if (possibleWeight > MaxCargoWeight)
        {
            throw new OverfillException(String.Format(DEFAULT_OVERFILL_MESSAGE, SerialNumber, MaxCargoWeight));
        }

        CargoWeight += cargoWeight;
    }

    public override int Unload()
    {
        double notLoadedWeight = CargoWeight * 0.05;
        int unloadedWeight = (int)(CargoWeight - notLoadedWeight);
        CargoWeight -= unloadedWeight;
        return unloadedWeight;
    }

    public void Notify(string message, string containerSerialNumber)
    {
        Console.WriteLine(message, containerSerialNumber);
    }
}