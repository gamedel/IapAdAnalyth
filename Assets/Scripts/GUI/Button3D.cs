using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button3D : MonoBehaviour
{
    public UnityEvent OnClick;

    private MeshRenderer renderer;
    private Color originalColor;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        originalColor = renderer.material.color;
    }

    void OnMouseDown()
    {
        OnClick.Invoke();
        renderer.material.color = Color.red;
    }

    void OnMouseUp()
    {
        renderer.material.color = originalColor;
    }

}
