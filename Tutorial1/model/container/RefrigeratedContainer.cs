using Tutorial1.exception;
using Tutorial1.model.product;

namespace Tutorial1.model.container;

public
    class RefrigeratedContainer(int cargoWeight, int height, int weight, int maxCargoWeight, ProductType productType, double temperature)
    : AbstractContainer(cargoWeight, height, weight, maxCargoWeight)
{

    public static readonly Dictionary<ProductType, double> ProductTypeTemperature = new()
        {
            { ProductType.Banana , 13.3},
            { ProductType.Chocolate , 18},
            { ProductType.Fish , 2},
            { ProductType.Meat , -15},
            { ProductType.IceCream , -18},
            { ProductType.FrozenPizza , -30},
            { ProductType.Cheese , 7.2},
            { ProductType.Sausages , 5},
            { ProductType.Butter , 20.5},
            { ProductType.Eggs , 19},
        };
    
    public ProductType StoredProductType { get; set; } = productType;

    private double _temperature = temperature;

    public double Temperature
    {
        get { return _temperature; }
        set
        {
            double minTemperature = ProductTypeTemperature[StoredProductType];
            if (value < minTemperature)
            {
                throw new LowTemperatureException(
                    $"Minimum temperature to store {StoredProductType} is {minTemperature}");
            }

            _temperature = value;
        }
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
        int unloadedWeight = CargoWeight;
        CargoWeight -= unloadedWeight;
        return unloadedWeight;
    }
    
    protected override void GenerateSerialNumber()
    {
        SerialNumber = String.Format(AbstractContainer.SERIAL_NUMBER_FORMAT, "C", GetGeneratedContainerId());
    }
    
}