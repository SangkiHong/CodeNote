using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UvAnimationExample : MonoBehaviour
{
    MeshRenderer renderer;
    Vector2 offset;
    public float scrollSpeed;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        offset = renderer.material.mainTextureOffset;
    }


    void Update()
    {
        offset.x += Time.deltaTime * scrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", offset);
    }
}
