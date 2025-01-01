using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        RegisterSubsystems();
        InitializeSubsystems();
    }

    private void RegisterSubsystems()
    {
        foreach (var subsystem in GetComponentsInChildren<ISubsystem>())
        {
                SubsystemLocator.RegisterSubsystem(subsystem);
        }
    }

    private void InitializeSubsystems()
    {
        foreach (var service in SubsystemLocator.GetAllSubsystems<ISubsystem>())
        {
            service.Initialize();
        }
    }

    private void OnDestroy()
    {
        foreach (var service in SubsystemLocator.GetAllSubsystems<ISubsystem>())
        {
            service.Shutdown();
        }

        SubsystemLocator.UnregisterAllSubsystems();
    }
}