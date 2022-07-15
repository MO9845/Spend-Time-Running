//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusWidget : MonoBehaviour
{
    public Slider slider;

    void Update()
    {
        slider.value -= Time.deltaTime * (1f / 10f);

        if (slider.value <= 0)
            Destroy(gameObject);
    }
}
