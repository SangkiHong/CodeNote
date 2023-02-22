using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace HSK.Util
{
    public class HUtility
    {
        // 싱글톤 Instance
        private static HUtility _instance;
        public static HUtility Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HUtility();

                return _instance;
            }
        }

        // 거리 계산하여 반환하는 함수(최적화를 위해 루트 씌우지 않은 값을 반환)
        public float GetDistance(Vector3 currentPos, Vector3 targetPos)
        {
            // 거리 계산하기 위한 좌표 계산
            var x = targetPos.x - currentPos.x;
            var y = targetPos.y - currentPos.y;
            var z = targetPos.z - currentPos.z;

            // 타겟과의 거리 업데이트하여 변수에 저장
            return (x * x) + (y * y) + (z * z);
        }

        // return : -180 ~ 180 degree
        public float GetAngle(Vector3 vStart, Vector3 vEnd)
        {
            Vector3 v = vEnd - vStart;

            return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        }

        // 타임 체크용(애니메이션, 효과 등의 싱크를 맞출 경우)
        public IEnumerator Timer(float duration, float tick)
        {
            float elapsed = 0;

            while (elapsed <= duration) 
            {
                yield return new WaitForSeconds(tick);

                elapsed += tick;
                Debug.Log($"Timer::({elapsed})");
            }

            yield return null;
        }
    }

    // NavMesh 관련 확장 메소드(ExtensionMethods)
    public static class MyExtensionMethods
    {
        // 현재 위치에서 최대 거리 내 랜덤 위치 값(NavMesh)을 반환
        public static Vector3 GetRandomPosition(this NavMeshAgent navMeshAgent, Vector3 centerPosition, float distance)
        {
            // Random.insideUnitSphere으로 랜덤한 위치 값을 가져옴
            Vector3 randomDirection = Random.insideUnitSphere * distance;

            // 랜덤 위치 값에 순찰 시작 지점을 더하여 순찰 지점을 중심으로 랜덤 위치를 구함
            randomDirection += centerPosition;

            // NavMesh.SamplePosition 함수를 통해 랜덤 위치에 NavMeshAgent가 이동 가능한 위치인지 판별하여 _navHit에 할당
            NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, distance, NavMesh.AllAreas);

            // _navHit에 할당된 position 값을 반환
            return navHit.position;
        }

        // NavMeshAgent의 목적지에 대한 남은 거리를 반환(2019 버전 오류로 인한 대응 함수)
        // Ref url: https://stackoverflow.com/questions/61421172/why-does-navmeshagent-remainingdistance-return-values-of-infinity-and-then-a-flo
        public static float GetPathRemainingDistance(this NavMeshAgent navMeshAgent)
        {
            if (navMeshAgent.pathPending ||
                navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
                navMeshAgent.path.corners.Length == 0)
                return -1f;

            float distance = 0.0f;
            for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i) {
                distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
            }

            return distance;
        }
    }

    // Dotween 확장 메소드

    public static class DOTweenRotateExtensionMethods
    {
        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotateX
        (
            this Transform self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.DORotate
            (
                endValue: new Vector3(endValue, 0, 0),
                duration: duration,
                mode: mode
            );
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotateY
        (
            this Transform self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.DORotate
            (
                endValue: new Vector3(0, endValue, 0),
                duration: duration,
                mode: mode
            );
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotateZ
        (
            this Transform self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.DORotate
            (
                endValue: new Vector3(0, 0, endValue),
                duration: duration,
                mode: mode
            );
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalRotateX
        (
            this Transform self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.DOLocalRotate
            (
                endValue: new Vector3(endValue, 0, 0),
                duration: duration,
                mode: mode
            );
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalRotateY
        (
            this Transform self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.DOLocalRotate
            (
                endValue: new Vector3(0, endValue, 0),
                duration: duration,
                mode: mode
            );
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalRotateZ
        (
            this Transform self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.DOLocalRotate
            (
                endValue: new Vector3(0, 0, endValue),
                duration: duration,
                mode: mode
            );
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotateX
        (
            this GameObject self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.transform.DORotateX(endValue, duration, mode);
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotateY
        (
            this GameObject self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.transform.DORotateY(endValue, duration, mode);
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotateZ
        (
            this GameObject self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.transform.DORotateZ(endValue, duration, mode);
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalRotateX
        (
            this GameObject self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.transform.DOLocalRotateX(endValue, duration, mode);
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalRotateY
        (
            this GameObject self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.transform.DOLocalRotateY(endValue, duration, mode);
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalRotateZ
        (
            this GameObject self,
            float endValue,
            float duration,
            RotateMode mode = RotateMode.Fast
        )
        {
            return self.transform.DOLocalRotateZ(endValue, duration, mode);
        }
    }
}