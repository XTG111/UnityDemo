using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    private PlayerBeheviour _playerBeheviour;
    
    [Header("人物属性")] 
    public int maxHp = 100;
    public int curHp = 70;

    public int maxShield = 50;
    public int curShield = 0;

    public float dieTime = 2.0f;
    [Header("恶魔之泪增加伤害")] 
    public float addDamage = 1.0f;

    [Header("免伤")] 
    public float invulnerableDuration = 5.0f;
    private float _invulnerableCount = 0.0f;
    public bool invulnerable;

    [Header("事件")] 
    public UnityEvent<Transform> OnTackDamge;
    public UnityEvent OnDeath;
    public UnityEvent<PlayerInfo> OnHealthChange;
    
    [Header("死亡后切换地图")]
    public SeceneLoadEventSO LoadEventSo;
    public GameSceneSO goToScene;
    public Vector3 goToLoc;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        _playerBeheviour = GetComponent<PlayerBeheviour>();
    }

    private void Start()
    {
        InitialHP();
    }

    public void InitialHP()
    {
        curHp = maxHp;
        OnHealthChange?.Invoke(this);
    }
    private void Update()
    {
        if (invulnerable)
        {
            _invulnerableCount -= Time.deltaTime;
            if (_invulnerableCount <= 0.0f)
            {
                invulnerable = false;
            }
        }
    }

    public void SetcurHp(int del)
    {
        if (del < 0)
        {
            del = math.clamp(del+curShield,del,0);
        }
        curHp = math.clamp(curHp + del, 0, maxHp);
    }
    
    public void SetcurShield(int del)
    {
        curShield = math.clamp(curShield + del, 0, maxShield);
    }

    public int GetCurHP()
    {
        return curHp;
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable) return;
        var realDamage = _playerBeheviour.bst == BoomState.Seco_TakeDamage
            ? attacker.attackdamage * addDamage
            : attacker.attackdamage;
        SetcurHp(-(int)realDamage);
        OnHealthChange?.Invoke(this);
        if (curHp > 0)
        {
            TriggerInvulnerable();
            OnTackDamge?.Invoke(attacker.transform);
        }
        else
        {
            //TODO : Die
            OnDeath?.Invoke();
            LoadEventSo.RaiseLoadRequestEvent(goToScene,goToLoc,true);
        }
        //Debug.Log("Damage");
        if (attacker.isBoom)
        {
            attacker.isBoom = false;
            StartCoroutine(ReturnLoc());
        }
    }

    IEnumerator ReturnLoc()
    {
        yield return new WaitForSeconds(2);
        transform.position = transform.position + transform.right * 4 + transform.up * 2;
    }
    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(dieTime);
        LoadEventSo.RaiseLoadRequestEvent(goToScene,goToLoc,true);
    }
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            _invulnerableCount = invulnerableDuration;
        }
    }
}
