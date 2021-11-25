using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sounds : MonoBehaviour
{
    private Slider sounds_slider;
    private AudioSource[] audios;
    private float slider_value;
    // Start is called before the first frame update
    void Start()
    {
        sounds_slider = GetComponent<Slider>();
        audios = FindObjectsOfType<AudioSource>();
        slider_value = sounds_slider.value;
    }

    // Update is called once per frame
    void Update()
    {

        if (slider_value != sounds_slider.value)
        {
            for (int i = 0; i < audios.Length; i++)
            {
                audios[i].volume = sounds_slider.value;
            }
            slider_value = sounds_slider.value;
        }
    }
}
