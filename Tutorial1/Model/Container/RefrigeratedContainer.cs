using Tutorial1.exception;
using Tutorial1.model.product;

namespace Tutorial1.model.container;

public
    class RefrigeratedContainer(double cargoWeightKg, double heightCm, double weightKg, double maxCargoWeightKg, ProductType productType, double temperature)
    : AbstractContainer(cargoWeightKg, heightCm, weightKg, maxCargoWeightKg)
{

    protected static readonly Dictionary<ProductType, double> ProductTypeTemperatureDict = new()
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
            double minTemperature = ProductTypeTemperatureDict[StoredProductType];
            if (value < minTemperature)
            {
                throw new LowTemperatureException(
                    $"Minimum temperature to store {StoredProductType} is {minTemperature}");
            }

            _temperature = value;
        }
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
        double unloadedWeight = CargoWeightKg;
        CargoWeightKg -= unloadedWeight;
        return unloadedWeight;
    }
    
    protected override void GenerateSerialNumber()
    {
        SerialNumber = String.Format(AbstractContainer.SERIAL_NUMBER_FORMAT, "C", GetGeneratedContainerId());
    }

    public static double GetMinimumRequiredTemperature(ProductType productType)
    {
        return ProductTypeTemperatureDict[productType];
    }
    
    public override string ToString()
    {
        String baseString = base.ToString();

        baseString = baseString.Replace("Container", "Refrigerated Container");

        return $"{baseString}, stored products: {StoredProductType}, temperature: {_temperature}";
    }
    
}