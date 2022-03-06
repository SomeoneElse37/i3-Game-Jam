using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private new Collider2D collider;

    private float time = 0;

    public TextMeshProUGUI timerDisplay;

    public float Time { get => time; }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 5 && UnityEngine.Time.timeScale > 0)
        {
            timerDisplay.text = "Success!";
        }
        else if (collider.IsTouchingLayers() && UnityEngine.Time.timeScale > 0)
        {
            time += UnityEngine.Time.deltaTime;
            timerDisplay.text = string.Format("{0:f2}", time);
        }
        else
        {
            time = 0;
            //timerDisplay.text = "Build a tower into this area for 5 seconds to win!";
        }
    }
}
