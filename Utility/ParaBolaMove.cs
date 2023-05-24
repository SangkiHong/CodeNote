using System.Collections;
using UnityEngine;

public class ParaBolaMove : MonoBehaviour
{
    public float height = 12f;
    public float power = 1f;

    public float speed = 1.2f;
    public float dropSpeed = 20f;

    private float _progress;
    public float heightvalue;

    private Coroutine _coroutine;

    public void Move(Vector3 startPos, Vector3 destPos)
    {
        _coroutine = StartCoroutine(MoveCoroutine(startPos, destPos));
    }

    private IEnumerator MoveCoroutine(Vector3 startPos, Vector3 destPos)
    {
        _progress = 0;

        Transform _transform = transform;

        while (true)
        {
            // 포물선 이동
            if (_progress < 0.9f) 
            {
                _progress += speed * Time.deltaTime;

                Vector3 MovePos = MoveParaBola(startPos, destPos, _progress);

                _transform.position = MovePos;

                Vector3 nextPos = MoveParaBola(startPos, destPos, _progress + 0.01f);
                Vector3 forwardDir = (nextPos - MovePos).normalized;

                _transform.rotation = Quaternion.LookRotation(forwardDir, Vector3.up); 
            }
            // 점차 수직 낙하
            else 
            {
                Vector3 targetRot = _transform.rotation.eulerAngles;
                targetRot.x = Mathf.Lerp(targetRot.x, 90f, 0.05f);

                Vector3 movePos = _transform.position + (dropSpeed * Time.deltaTime * _transform.forward);

                _transform.SetPositionAndRotation(movePos, Quaternion.Euler(targetRot));
            }

            yield return null;
        }
    }

    private Vector3 MoveParaBola(Vector3 _start, Vector3 _end, float _time)
    {
        heightvalue = (power * height * _time) - (power * height * _time * _time);

        Vector3 pos = Vector3.Lerp(_start, _end, _time);
        pos.y += heightvalue;

        return pos;
    }

    public void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}
