using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecisionsSystem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    private int _deathCount = 0;

    public WaitForSeconds sec = new WaitForSeconds(2f);

    [SerializeField]
    private GameObject _decisionDisplay;

    private void OnEnable()
    {
        PlayerInteraction.OnPlayerDied += DeathChecker;
    }


    public void DeathChecker()
    {
        _deathCount++;
        Debug.Log("_deathCount " + _deathCount);
        if(_deathCount == 3)
        {
            DeathDesigion();
        }
    }

    public void DeathDesigion()
    {
        StartCoroutine(StopGame(sec));

    }

    IEnumerator StopGame(WaitForSeconds sec)
    {
        yield return sec;
        _text.text = "Increse HP +1. Decrease Humanity.";
        _decisionDisplay.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnPlayerDied -= DeathChecker;
    }

}