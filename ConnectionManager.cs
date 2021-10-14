using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using UnityEngine.SceneManagement;
using MLAPI.Transports.UNET;

public class ConnectionManager : MonoBehaviour
{

    public GameObject connectionButtonPanel;

    public string ipAddress = "127.0.0.1";

    UNetTransport transport;


    // Happens on server
    public void Host()
    {
        connectionButtonPanel.SetActive(false);
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(GetRandomSpawn(), Quaternion.identity);
    }

    // Happens on server
    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        // checks the incoming data
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == "salasana";
        callback(true, null, approve, GetRandomSpawn(), Quaternion.identity);
    }

    public void Join()
    {
        transport = NetworkManager.Singleton.GetComponent<UNetTransport>();
        transport.ConnectAddress = ipAddress;

        connectionButtonPanel.SetActive(false);
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("salasana");
        NetworkManager.Singleton.StartClient();
    }

    public void Exit()
    {
        Application.Quit();
    }

    Vector3 GetRandomSpawn()
    {
        float x = Random.Range(-20f, 15f);
        float y = Random.Range(3f, 4f);
        float z = Random.Range(-20f, 20f);
        return new Vector3(x, y, z);
    }

    public void IPAddressChanged(string newAddress)
    {
        this.ipAddress = newAddress;
    }

}