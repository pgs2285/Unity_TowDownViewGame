using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float interactionDistance = 5f;  // 상호작용할 수 있는 거리
    public GameManager gameManager;
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactionDistance);
        if (Input.GetKeyDown(KeyCode.F))  // 'F' 키로 상호작용
        {
            InteractWithObject();
        }
    }

    void InteractWithObject()
    {
     
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            GameObject hitObject = hit.collider.gameObject; 
            
            if (hitObject.CompareTag("npc"))
            {
                gameManager.Action(hitObject);
            }
        }
    }
}