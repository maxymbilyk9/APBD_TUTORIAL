using Tutorial1.model;
using Tutorial1.model.container;
using Tutorial1.model.product;
using Tutorial1.Service;

public class Program
{

    public static void Main(string[] args)
    {

        Ship first = new Ship(10, 500, 20);
        Ship second = new Ship(10, 200, 30);
        
        ContainerService containerService = new ContainerService();
        
        ShipService firstShipService = new ShipService(first);
        ShipService secondShipService = new ShipService(second);
        
        firstShipService.DisplayInfo();
        secondShipService.DisplayInfo();

        LiquidContainer liquidContainer = containerService.CreateLiquidContainer(50, 100, 200, 100, false);
        RefrigeratedContainer refrigeratedContainer = containerService.CreateRefrigeratedContainer(400, 100, 200, 1000, ProductType.Banana, 20);
        GasContainer gasContainer1 = containerService.CreateGasContainer(50, 200, 100, 200, 2);
        GasContainer gasContainer2 = containerService.CreateGasContainer(10, 100, 100, 400, 1);

        ContainerService.DisplayInfo(liquidContainer);
        ContainerService.DisplayInfo(refrigeratedContainer);
        ContainerService.DisplayInfo(gasContainer1);
        ContainerService.DisplayInfo(gasContainer2);
        
        firstShipService.LoadContainer(liquidContainer);
        firstShipService.LoadContainer(refrigeratedContainer);
        
        firstShipService.DisplayInfo();
        
        secondShipService.LoadAll(new List<AbstractContainer>() {gasContainer1, gasContainer2});
        secondShipService.DisplayInfo();

        Console.WriteLine(liquidContainer.SerialNumber);

        secondShipService.RemoveContainer(gasContainer1);
        secondShipService.DisplayInfo();

        liquidContainer.Unload();
        ContainerService.DisplayInfo(liquidContainer);
        
        RefrigeratedContainer refrigeratedContainer2 = containerService.CreateRefrigeratedContainer(400, 100, 200, 1000, ProductType.Banana, 20);

        firstShipService.ReplaceContainer(refrigeratedContainer2, refrigeratedContainer.SerialNumber);
        firstShipService.DisplayInfo();
        
        ShipService.MoveContainer(first, second, refrigeratedContainer2.SerialNumber);
        firstShipService.DisplayInfo();
        secondShipService.DisplayInfo();
    }
    
}