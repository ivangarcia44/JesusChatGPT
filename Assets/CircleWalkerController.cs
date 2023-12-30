using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CircleWalkerController : MonoBehaviour
{
    Animator animator;
    private GameObject parentObject;
    int animationCount = 0;
    int isWalkingParamIdx;
    public float vel = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingParamIdx = Animator.StringToHash("isWalking");
        parentObject = animator.GetComponentInParent<Transform>().gameObject;
        // Debug.Log("Parent name = " + parentObject.name);
        animationCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingParamIdx);
        bool walkCondition = animationCount > 500;
        animationCount = (animationCount + 1) % 1000;
        // Debug.Log(animationCount);
        if (!isWalking && walkCondition) {
            animator.SetBool(isWalkingParamIdx, true);
        } else if (isWalking && !walkCondition) {
            animator.SetBool(isWalkingParamIdx, false);
            parentObject.transform.Rotate(Vector3.up, 90f);
        }
        // Debug.Log(dirVec * vel * Time.deltaTime);
        if (isWalking) {
            parentObject.transform.Translate(Vector3.forward * vel * Time.deltaTime);
        }
    }
}
