using System.Text;
using Tutorial1.exception;
using Tutorial1.model.container;
using Tutorial1.Service;

namespace Tutorial1.model;

public class Ship(int maxContainerCapacity, double maxToneWeight, double maxKnotsSpeed)
{

    private static long SHIP_ID_COUNT = 1;

    public long Id { get; } = SHIP_ID_COUNT++;
    
    public int MaxContainerCapacity { get; set; } = maxContainerCapacity;

    public List<AbstractContainer> Containers { get; set; } = new(maxContainerCapacity);

    public double MaxToneWeight { get; set; } = maxToneWeight;

    public double MaxKnotsSpeed { get; set; } = maxKnotsSpeed;
    
    
    public double GetLoadedWeightKilos()
    {
        double sum = 0;
        foreach (AbstractContainer container in Containers)
        {
            sum += container.GetTotalWeight(); // weight of Container and Cargo
        }

        return sum;
    }

    public double GetLoadedWeightTones()
    {
        double weightKilos = GetLoadedWeightKilos();
        return ShipService.KilogramsToTones(weightKilos);
    }

    public override string ToString()
    {
        StringBuilder containersBuilder = new StringBuilder("Loaded containers: ");

        if (Containers.Count == 0)
        {
            containersBuilder.Append("No containers on ship");
        }
        
        foreach (AbstractContainer container in Containers)
        {
            containersBuilder.Append(container).Append(";");
        }
        return $"Ship {Id} (speed: {maxKnotsSpeed} knots, containers capacity: {maxContainerCapacity}, max weight: {MaxToneWeight} tones.\nContainers loaded: {containersBuilder.ToString()}";
    }
}