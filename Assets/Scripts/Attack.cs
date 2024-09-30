using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    private int hashAttackCount = Animator.StringToHash("AttackCount");
    void Start()
    {
        TryGetComponent(out animator);
    }

    public int AttackCount
    {
        get => animator.GetInteger(hashAttackCount);
        set => animator.SetInteger(hashAttackCount, value);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {// 왼쪽 마우스 입력시 

    
            AttackCount = 0;
            animator.SetTrigger("Attack");
           
            
        }
    }

}
