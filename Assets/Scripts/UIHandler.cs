using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI playButtonText;
    public Transform physicsObjectsParent;
    protected Dictionary<Transform, (Vector3, Quaternion)> savedTransforms;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        savedTransforms = new Dictionary<Transform, (Vector3, Quaternion)>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onPlayButton()
    {
        if (Time.timeScale == 0)
        {
            saveTransforms();
            Time.timeScale = 1;
            playButtonText.SetText("Reset");
        }
        else
        {
            Time.timeScale = 0;
            playButtonText.text = "Play";
            loadTransforms();
        }
    }

    protected void saveTransforms()
    {
        foreach (Transform t in physicsObjectsParent)
        {
            savedTransforms[t] = (t.localPosition, t.localRotation);
        }
    }

    protected void loadTransforms()
    {
        foreach (Transform t in physicsObjectsParent)
        {
            (t.localPosition, t.localRotation) = savedTransforms[t];
            Rigidbody2D rb = t.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }
} 
