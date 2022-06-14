using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnArmor : MonoBehaviour
{

    void OnCollisionEnter()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;

        Invoke("Respawn", 5);
    }

    void Respawn()
    {
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    // This is test!
}
