namespace EIM.Core.Registry
{
    public interface IRegistryHost : IManageServiceInstances,
        IManageHealthChecks,
        IResolveServiceInstances
    {
    }
}
