using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanityRateController : MonoBehaviour
{
    [SerializeField]
    private Image _fillElement;

    public ExtendedStats PlayerState;

    private void OnEnable()
    {
        //PlayerInteraction.OnHumanityRateChange += ChangeRate;
        ButtonsActions.OnChangeHumanity += ChangeRate;
    }

    private void Start()
    {
        _fillElement.fillAmount = PlayerState.Humanity;
    }

    public void ChangeRate(float rate)
    {
        _fillElement.fillAmount += rate;
    }

    private void OnDisable()
    {
        ButtonsActions.OnChangeHumanity -= ChangeRate;
    }
}
