using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private EnemyAI _ai;

    private void Start()
    {
        _ai = GetComponentInParent<EnemyAI>();

        if (_ai == null)
        {
            Debug.LogError("The EnemyAI is NULL!!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ai.StartAttack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ai.StopAttack();
        }
    }
}
