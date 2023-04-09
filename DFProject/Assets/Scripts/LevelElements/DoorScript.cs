using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IOpenable
{
    [SerializeField]
    private Animator _animator;
 
    public void Open()
    {
        _animator.Play("Open");
    }

    public void DisableDoor()
    {
        transform.gameObject.SetActive(false);
    }

}
