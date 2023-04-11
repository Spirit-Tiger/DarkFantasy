using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Animator _chekPointAnimator;
    [SerializeField]
    private BoxCollider2D _checkPointTrigger;

    [SerializeField]
    private PlayerPosition _position;


    private void SavePosition(Vector3 position)
    {
        _position.Position = position;
    }
    public void PlayActiveCheckPointAnimation()
    {
        _chekPointAnimator.Play("Active");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _chekPointAnimator.Play("Activate");
            SavePosition(collision.transform.position);
            _checkPointTrigger.enabled = false;
        }
    }

}
