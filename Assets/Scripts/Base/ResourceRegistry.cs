using System.Collections.Generic;

public class ResourceRegistry
{
    private List<Resource> _freeResources = new();
    private List<Resource> _busyResources = new();

    public void RegisterScannedResources(List<Resource> foundResources)
    {
        foreach (Resource resource in foundResources)
        {
            if (!_freeResources.Contains(resource) && !_busyResources.Contains(resource))
            {
                _freeResources.Add(resource);
            }
        }
    }

    public Resource GetFreeResource()
    {
        if (_freeResources.Count == 0)
            return null;

        Resource target = _freeResources[0];

        _freeResources.RemoveAt(0);
        _busyResources.Add(target);

        return target;
    }

    public void CompleteResource(Resource resource)
    {
        _busyResources.Remove(resource);
    }
}
