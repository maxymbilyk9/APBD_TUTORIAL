using Tutorial1.exception;
using Tutorial1.model.container;
using Tutorial1.model.product;

namespace Tutorial1.Service;

public class ContainerService
{
    
    
    public ContainerService() {}
    
    public LiquidContainer CreateLiquidContainer(double cargoWeightKg, double heightCm, double weightKg,
        double maxCargoWeightKg, bool isHazard)
    {
        return new LiquidContainer(cargoWeightKg, heightCm, weightKg, maxCargoWeightKg, isHazard);
    }

    public GasContainer CreateGasContainer(double cargoWeightKg, double heightCm, double weightKg,
        double maxCargoWeightKg,
        double atmospheres)
    {
        return new GasContainer(cargoWeightKg, heightCm, weightKg, maxCargoWeightKg, atmospheres);
    }

    public RefrigeratedContainer CreateRefrigeratedContainer(double cargoWeightKg, double heightCm, double weightKg,
        double maxCargoWeightKg, ProductType productType, double temperature)
    {
        return new RefrigeratedContainer(cargoWeightKg, heightCm, weightKg, maxCargoWeightKg, productType, temperature);
    }

    public static void LoadCargo(AbstractContainer container, double weight)
    {
        container.Load(weight);
    }

    public static double UnloadCargo(List<AbstractContainer> containers, string containerSerialNumber, double weight)
    {
        AbstractContainer foundContainer = findBySerialNumber(containers, containerSerialNumber);
        return foundContainer.Unload();
    }
    
    public double UnloadCargo(AbstractContainer container)
    {
        return container.Unload();
    }
    
    public static void LoadCargo(List<AbstractContainer> containers, string containerSerialNumber, double weight)
    {
        AbstractContainer foundContainer = findBySerialNumber(containers, containerSerialNumber);
        foundContainer.Load(weight);
    }

    public static void DisplayInfo(AbstractContainer container)
    {
        Console.WriteLine(container.ToString() + "\n");
    }

    public static AbstractContainer findBySerialNumber(List<AbstractContainer> containers, string serialNumber)
    {
        AbstractContainer foundContainer = null;
        foreach (AbstractContainer container in containers)
        {
            if (container.SerialNumber.Equals(serialNumber))
            {
                foundContainer = container;
                break;
            }
        }

        CheckContainer(foundContainer, serialNumber);

        return foundContainer;
    }

    private static void CheckContainer(AbstractContainer container, string containerSerialNumber)
    {
        if (container == null)
        {
            throw new ContainerNotFoundException($"Container with serial number {containerSerialNumber} is not found");
        }
    }
    
}