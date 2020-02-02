using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HeightFromContentSizeFitter : MonoBehaviour
{
    public RectTransform fitter = null;
    private RectTransform rect  = null;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fitter)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, fitter.sizeDelta.y);
        }
    }
}
