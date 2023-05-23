using System.Collections;
using UnityEngine;

public class ParaBolaMove : MonoBehaviour
{
    public float height;
    public float power;

    public float speed;

    private float _timer = 0;

    private Coroutine _coroutine;

    public void Move(Vector3 startPos, Vector3 destPos)
    {
        _coroutine = StartCoroutine(MoveCoroutine(startPos, destPos));
    }

    private IEnumerator MoveCoroutine(Vector3 startPos, Vector3 destPos)
    {
        _timer = 0;

        while (true)
        {
            _timer += speed * Time.deltaTime;

            Vector3 MovePos = MoveParaBola(startPos, destPos, _timer);

            transform.position = MovePos;

            Vector3 nextPos = MoveParaBola(startPos, destPos, _timer + 0.01f);
            Vector3 forwardDir = (nextPos - MovePos).normalized;

            transform.rotation = Quaternion.LookRotation(forwardDir, Vector3.up);

            yield return null;
        }
    }

    private Vector3 MoveParaBola(Vector3 _start, Vector3 _end, float _time)
    {
        float heightvalue = -power * height * _time * _time + power * height * _time;

        Vector3 pos = Vector3.Lerp(_start, _end, _time);

        return new Vector3(pos.x, heightvalue + pos.y, pos.z);
    }

    public void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}
