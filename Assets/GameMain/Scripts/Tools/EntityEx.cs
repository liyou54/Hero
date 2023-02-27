using UnityGameFramework.Runtime;

public static class EntityEx
{
    private static int id = 0;

    private static int GetId()
    {
        return id++;
    }

    public static int ShowEntityEx<T>(this EntityComponent entityComponent, string assetsPath, string group)
        where T : EntityLogic
    {
        var uniId = GetId();
        entityComponent.ShowEntity<T>(uniId, assetsPath, group);
        return uniId;
    }
}