using System.Text;
using Tutorial1.exception;
using Tutorial1.service;

namespace Tutorial1.model.container;

public
    class GasContainer(double cargoWeightKg, double heightCm, double weightKg, double maxCargoWeightKg, double atmospheres)
    : AbstractContainer(cargoWeightKg, heightCm, weightKg, maxCargoWeightKg), IHazardNotifier
{

    public double Atmospheres { get; set; } = atmospheres;
    
    protected override void GenerateSerialNumber()
    {
        SerialNumber = String.Format(GasContainer.SERIAL_NUMBER_FORMAT, "G", GetGeneratedContainerId());
    }

    public override void Load(double cargoWeight)
    {
        double possibleWeight = CargoWeightKg + cargoWeight;
        
        if (possibleWeight > MaxCargoWeightKg)
        {
            throw new OverfillException(String.Format(DEFAULT_OVERFILL_MESSAGE, SerialNumber, MaxCargoWeightKg));
        }

        CargoWeightKg += cargoWeight;
    }

    public override double Unload()
    {
        double notLoadedWeight = CargoWeightKg * 0.05;
        double unloadedWeight = CargoWeightKg - notLoadedWeight;
        CargoWeightKg -= unloadedWeight;
        return unloadedWeight;
    }

    public void Notify(string message, string containerSerialNumber)
    {
        Console.WriteLine(message, containerSerialNumber);
    }

    public override string ToString()
    {
        String baseString = base.ToString();

        baseString = baseString.Replace("Container", "GasContainer");

        return $"{baseString}, atmospheres: {atmospheres} atm";
    }
    
}