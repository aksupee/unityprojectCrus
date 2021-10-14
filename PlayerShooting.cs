using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class PlayerShooting : NetworkBehaviour
{

    public TrailRenderer bulletTrail;

    public Transform gunBarrell;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            // shoot
            if (Input.GetButtonDown("Fire1"))
            {
                // actually shoot - tell the server, we have a shot
                ShootServerRpc();
            }
        }
    }

    // these run on the server called by client, client=>server
    [ServerRpc]
    void ShootServerRpc()
    {

        //do raycast on the server to see if we hit an enemy and take damage
        if (Physics.Raycast(gunBarrell.position, gunBarrell.forward, out RaycastHit hit, 200f))
        {
            // we hit something, is it a player?
            var enemyHealth = hit.transform.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {
                // it was a player, damage then
                enemyHealth.TakeDamage(10);
            }        }
        ShootClientRpc();
    }

    // Server=>Client
    [ClientRpc]

    void ShootClientRpc()
    {
        var bullet = Instantiate(bulletTrail, gunBarrell.position, Quaternion.identity);
        bullet.AddPosition(gunBarrell.position);
        if (Physics.Raycast(gunBarrell.position, gunBarrell.forward, out RaycastHit hit, 200f))
        {
            bullet.transform.position = hit.point;
        }

        else
        {
            bullet.transform.position = gunBarrell.position + (gunBarrell.forward * 200f);
        }
    }

}
