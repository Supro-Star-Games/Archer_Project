using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Archer _target;

    protected Enemy _enemy;

    public State TargetState => _targetState;
    
    public bool NeedTransit {get; protected set; }

    public void Init(Archer target, Enemy enemy)
    {
        _target = target;
        _enemy = enemy;
        if (_enemy == null)
        {
            Debug.Log("EnemyNull");
        }
    }
}
