using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

public class ClawAnimationController : MonoBehaviour
{
    private Animator[] _clawAnimators;
    private int index = 0;
    private int animatorCount;

    void Awake()
    {
        _clawAnimators = GetComponentsInChildren<Animator>();
        animatorCount = _clawAnimators.Length;
    }
    
    private void OnEnable()
    {
        SubscribeEvents();
    }
    
    private void SubscribeEvents()
    {
        BagSignals.Instance.onItemSelected += PlayClawAnimation;
    }

    void PlayClawAnimation(ObjectType obj)
    {
        _clawAnimators[index].SetTrigger("Collect");

        index = (index + 1) % animatorCount;
    }
    
    private void UnSubscribeEvents()
    {
        BagSignals.Instance.onItemSelected -= PlayClawAnimation;

    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}

