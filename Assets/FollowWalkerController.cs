using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class FollowWalkerController : MonoBehaviour
{
    Animator animator;
    private GameObject parentObject;
    private int fAnimationCount1 = 0;
    private int fAnimationCount2 = 0;
    int isWalkingParamIdx;
    public float vel = 1.0f;
    public GameObject mainPlayer;
    private float fDistance;
    private const float fMinDist = 1.7f;
    private bool fHeadingDir = true; // False => heading away, true = heading towards.

    public Canvas FollowWalkerDialogueUI;
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingParamIdx = Animator.StringToHash("isWalking");
        parentObject = gameObject; //animator.GetComponentInParent<Transform>().gameObject;
        // Debug.Log("Parent name = " + parentObject.name);
        fAnimationCount1 = 0;
        fAnimationCount2 = 0;

        inputField = FollowWalkerDialogueUI.GetComponentInChildren<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        bool walkCondition = fAnimationCount1 < 500;
        fAnimationCount1 = (fAnimationCount1 + 1) % 1000;
        fHeadingDir = fAnimationCount2 < 1333;
        fAnimationCount2 = (fAnimationCount2 + 1) % 3000;
        fDistance = Vector3.Distance(parentObject.transform.position, mainPlayer.transform.position);
        bool talkingDistance = fDistance < fMinDist;

        updateWalkAnimation(talkingDistance, walkCondition);

        updateInputField(talkingDistance);

        // voice.Speak("Hello there!", SpeechNoiseSpeakFlags.SVSFlagsAsync | SpeechNoiseSpeakFlags.SVSFPurgeBeforeSpeak);
    }

    void updateWalkAnimation(bool aIsTalking, bool aWalkCondition) {
        bool isWalking = animator.GetBool(isWalkingParamIdx);
        if ((!isWalking && aWalkCondition) && !aIsTalking) {
            animator.SetBool(isWalkingParamIdx, true);
            isWalking = true;
        } else if ((isWalking && !aWalkCondition) || aIsTalking) {
            animator.SetBool(isWalkingParamIdx, false);
            isWalking = false;
        }
        if (isWalking) {
            parentObject.transform.Translate(Vector3.forward * vel * Time.deltaTime);
        }
        parentObject.transform.LookAt(mainPlayer.transform);
        // if (!fHeadingDir && !aIsTalking) {
        //     // parentObject.transform.Rotate(Vector3.up, 180f);
        // }        
    }

    void updateInputField(bool aIsTalking) {
        bool uiEnabled = FollowWalkerDialogueUI.enabled;
        if (aIsTalking) {//} && !uiEnabled) {
            FollowWalkerDialogueUI.enabled = true;
            if (inputField != null) {
                inputField.interactable = true;
                inputField.Select();
                inputField.ActivateInputField();
            }
        } else if (!aIsTalking) { //} && uiEnabled) {
            FollowWalkerDialogueUI.enabled = false;
            if (inputField != null) {
                inputField.interactable = false;
            }
        }
    }
}
