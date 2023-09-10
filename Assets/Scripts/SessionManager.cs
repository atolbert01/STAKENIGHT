using UnityEngine;
using Unity.Netcode;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private LaunchMenuManager _launchMenuManager;

    public bool HostMode { get; set; }
    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);

        //NetworkManager.Singleton.OnServerStarted += handleServerStarted;
        _launchMenuManager.GameStarted += handleGameStarted;
    }

    private void handleGameStarted()
    {
        if (HostMode)
        {
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            NetworkManager.Singleton.StartClient();
        }
    }


}
