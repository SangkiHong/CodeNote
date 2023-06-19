using System;
using UnityEngine;

// HSK::230508_해당 레이어의 오브젝트를 감지하여 이벤트를 호출하는 체크 트리거 컴포넌트
public class ObjectTriggerCheck : MonoBehaviour
{
    public Action<GameObject> OnEnterTrigger;
    public Action<GameObject> OnStayTrigger;
    public Action<GameObject> OnExitTrigger;

    [Header("Need")]
    [SerializeField] private LayerMask layerMask; 
    [SerializeField] private string targetTag;
    [SerializeField] private new Collider collider;

    [Header("Setting")]
    [SerializeField] private bool onEnterDisable;


    private void Awake()
    {
        if (collider == null)
            collider = GetComponent<Collider>();

        if (collider != null)
            collider.isTrigger = true;
    }

    public void SetActive(bool set)
    {
        enabled = set;
        collider.enabled = set;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionObject = other.gameObject;

        if (!string.IsNullOrEmpty(targetTag)) 
        {
            if (other.CompareTag(targetTag)) 
                LayerCheck(collisionObject, OnEnterTrigger);
            else
                LayerCheck(collisionObject, OnEnterTrigger);
        }
        else
        {
            LayerCheck(collisionObject, OnEnterTrigger);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject collisionObject = other.gameObject;

        if (!string.IsNullOrEmpty(targetTag)) 
        {
            if (other.CompareTag(targetTag))
                LayerCheck(collisionObject, OnStayTrigger);
            else
                LayerCheck(collisionObject, OnStayTrigger);
        }
        else
        {
            LayerCheck(collisionObject, OnStayTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject collisionObject = other.gameObject;

        if (!string.IsNullOrEmpty(targetTag)) 
        {
            if (other.CompareTag(targetTag))
                LayerCheck(collisionObject, OnExitTrigger);
            else
                LayerCheck(collisionObject, OnExitTrigger);
        }
        else
        {
            LayerCheck(collisionObject, OnExitTrigger);
        }
    }

    private void LayerCheck(GameObject collisionObject, Action<GameObject> action)
    {
        if (layerMask != 0) 
        {
            if (layerMask >> collisionObject.layer == 1) 
            {
                action?.Invoke(collisionObject);
                
                if (onEnterDisable) 
                    SetActive(false);
            }
        }
        else 
        {
            action?.Invoke(collisionObject);
            
            if (onEnterDisable)
                SetActive(false);
        }
    }
}
