using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsActions : MonoBehaviour
{
    [SerializeField]
    private GameObject _decisionDisplay;

    public ExtendedStats PlayerStats;

    public static event Action<int> OnChangeHp;
    public static event Action<float> OnChangeHumanity;

    public void YesAnswer()
    {
        PlayerStats.ChangeMaxHealth();
        if (OnChangeHp != null)
        {
            PlayerStats.Health += 1;
            OnChangeHp(2);

        }
        if (OnChangeHumanity != null)
        {
            PlayerStats.Humanity += -0.1f;
            OnChangeHumanity(-0.1f);
        }

        _decisionDisplay.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void NoAnswer()
    {
        _decisionDisplay.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
