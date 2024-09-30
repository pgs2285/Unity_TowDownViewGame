using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    private int hashAttackCount = Animator.StringToHash("AttackCount");
    [SerializeField] private GameManager _gameManager;
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
        if(Input.GetMouseButtonDown(0) && !_gameManager.isAction) {// 왼쪽 마우스 입력시 

            
            AttackCount = 0;
            animator.SetTrigger("Attack");
           
            
        }
    }

}
