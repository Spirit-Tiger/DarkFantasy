using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPFunctions : MonoBehaviour
{
    //public RectTransform hp;
    private Vector2 initPos = new Vector2(114f, -0.2f);
    public GameObject Heart;
    public GameObject EmptyHeart;
    public Stats Stats;
    private List<GameObject> hpList = new List<GameObject>();

    private void OnEnable()
    {
        PlayerInteraction.OnHPChange += Place;
    }
    private void Start()
    {
        Place(Stats.Health);
    }
    public void Place(int hp)
    {
        hpList.Clear();
        for (int i = 0; i < Stats.MaxHealth; i++)
        {
            if (hp > i)
            {
                var instance = Instantiate(Heart, transform.position, Quaternion.identity);
                instance.transform.SetParent(transform);
                instance.GetComponent<RectTransform>().anchoredPosition = initPos + new Vector2(120 * i, -0.2f);
                hpList.Add(instance);
            }
            else if (hp <= i)
            {
                var instance = Instantiate(EmptyHeart, transform.position, Quaternion.identity);
                instance.transform.SetParent(transform);
                instance.GetComponent<RectTransform>().anchoredPosition = initPos + new Vector2(120 * i, -0.2f);
                hpList.Add(instance);
            }

        }
        Debug.Log("333" + hpList.Count);
    }

    private void OnDisable()
    {

        PlayerInteraction.OnHPChange -= Place;

    }
}

