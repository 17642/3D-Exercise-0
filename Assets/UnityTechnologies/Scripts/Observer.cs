using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    bool m_IsPlayerRange;
    RaycastHit raycastHit;

    public Transform player;
    public GaneEnding gameEnding;
    
    // Start is called before the first frame update
    private void Update()
    {
        if (m_IsPlayerRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;//Player와 물체간 거리

            Ray ray= new Ray(transform.position, direction);//로 광선 발사

            if (Physics.Raycast(ray,out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player) {
            m_IsPlayerRange = false;
        }
    }
}
