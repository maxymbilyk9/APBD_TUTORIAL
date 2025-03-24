using Tutorial1.exception;
using Tutorial1.model;
using Tutorial1.model.container;

namespace Tutorial1.Service;

public class ShipService
{
    
    private Ship _ship;

    public ShipService(Ship _ship)
    {
        this._ship = _ship;
    }


    public void LoadContainer(AbstractContainer container)
    {
        double currentTones = _ship.GetLoadedWeightTones();
        double possibleKilos = _ship.GetLoadedWeightKilos();
        possibleKilos += container.GetTotalWeight();

        if (ShipService.KilogramsToTones(possibleKilos) > _ship.MaxToneWeight)
        {
            throw new OverfillException(
                $"Can't add another container {container.SerialNumber}, max weight {_ship.MaxToneWeight} will be reached. Available {_ship.MaxToneWeight - currentTones} / {_ship.MaxToneWeight} tones.");
        }

        if (_ship.Containers.Count == _ship.MaxContainerCapacity)
        {
            throw new OverfillException(
                $"Can't add another container {container.SerialNumber}, because max capacity {_ship.MaxContainerCapacity} containers reached.");
        }

        try
        {
            AbstractContainer duplicate = ContainerService.findBySerialNumber(_ship.Containers, container.SerialNumber);
        } catch (ContainerNotFoundException e)
        {
            _ship.Containers.Add(container);
        }
        
    }

    public bool RemoveContainer(AbstractContainer container)
    {
        AbstractContainer foundContainer =
            ContainerService.findBySerialNumber(_ship.Containers, container.SerialNumber);
        return _ship.Containers.Remove(foundContainer);
    }

    
    public void LoadAll(List<AbstractContainer> containers)
    {
        foreach (AbstractContainer containerToLoad in containers)
        {
            LoadContainer(containerToLoad);
        }
    }


    public void ReplaceContainer(AbstractContainer newContainer, string containerToReplace)
    {
        AbstractContainer replaceContainer = ContainerService.findBySerialNumber(_ship.Containers, containerToReplace);

        _ship.Containers.Remove(replaceContainer);
        _ship.Containers.Add(newContainer);
    }
    
    public static void MoveContainer(Ship from, Ship to, string containerNumber)
    {
        AbstractContainer foundContainer = ContainerService.findBySerialNumber(from.Containers, containerNumber);
        to.Containers.Add(foundContainer);
    }

    public void DisplayInfo()
    {
        Console.WriteLine(_ship + "\n");
    }
    
    public static double KilogramsToTones(double kilograms)
    {
        return kilograms / 1000;
    }
}