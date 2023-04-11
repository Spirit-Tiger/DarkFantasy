using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimikAwake : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _mimikCollider;
    [SerializeField]
    private BoxCollider2D _mimikTrigger;
    [SerializeField]
    private Animator _mimikAnimator;


    public void PlayActiveAnimation()
    {
        _mimikAnimator.Play("Active");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _mimikCollider.enabled = true;
            _mimikAnimator.Play("Transform");
            _mimikTrigger.enabled = false;
        }
    }


}
