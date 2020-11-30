//Written by Gabriel Tupy 11-29-2020
//Last modified by Gabriel Tupy 11-29-2020


using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Animator animRefernce;
    public static Animator anim;

    private void Awake()
    {
        anim = animRefernce;
        print(anim.name);
    }

    public static void ShakeCamera()
    {
        int temp = Mathf.RoundToInt(Random.Range(0, 2));

        switch (temp)
        {
            case 0:
                Debug.Log("CameraShake1 Called");
                anim.SetTrigger("CameraShake1");
                break;
            case 1:
                Debug.Log("CameraShake2 Called");
                anim.SetTrigger("CameraShake2");
                break;
            case 2:
                Debug.Log("CameraShake3 Called");
                anim.SetTrigger("CameraShake3");
                break;
        }
    }
}
