using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : Singleton<CharacterState>
{
    private int _maxHp = 100;

    public int MaxHp
    {
        get => _maxHp;
        set => _maxHp = value;
    }
    private int _hp = 100;
    public int HP
    {
        get => _hp;
        set => _hp = value;
    }
    private Slider _hpbar;
    private void Start()
    {
        _hpbar = GameObject.FindGameObjectWithTag("HP").GetComponent<Slider>();
    }
    private void Update()
    {
        if (_hpbar != null)
        {
            _hpbar.value = (int) _hp/ _maxHp;
        }
    }
}
