//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020
using UnityEngine;

public class Follower2D : MonoBehaviour
{
    public GameObject Target = null;
    public Vector2 Offset = Vector2.zero;
    public AnimationCurve LerpTimeCurve = null;
    private float curLerpTime = 0;

    private void Update()
    {
        //Determines the LerpTime based on how far away the target is.
        if (Target != null && LerpTimeCurve != null)
        {
            curLerpTime = LerpTimeCurve.Evaluate(Vector2.Distance(Target.transform.position, this.transform.position));
        }
    }


    void LateUpdate()
    {
        //Follows target if target isn't null
        if (Target != null)
        {
            this.transform.position = Vector3.Slerp(this.transform.position, new Vector3(Target.transform.position.x + Offset.x, Target.transform.position.y + Offset.y, - 10), curLerpTime);
        }
    }
}
