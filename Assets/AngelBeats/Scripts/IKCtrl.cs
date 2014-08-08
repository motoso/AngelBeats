using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  

public class IKCtrl : MonoBehaviour {

	protected Animator animator;

	public bool ikActive = false;
	public Transform rightHandObj = null;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}

	// void Update(){
	// 	if(Input.GetKey("up")){
	// 		OnAnimatorIK();
	// 	}
	// }
        //IKを計算するコールバック
	void OnAnimatorIK()
	{
		if(animator) {

                        //IKがアクティブならば、位置や回転を直接、目標物に設定します。
			if(ikActive) {

                                //右手のweight = 1.0 は位置や回転が目標物に設定します（キャラクターが握りたい場所）
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1.0f);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1.0f);
				// Debug.Log("ikActive");
			        //右手の位置および回転を外部オブジェクトの位置に設定する
				if(rightHandObj != null) {
					animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
					animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
				}					

			}

                        //IKがアクティブでないならば、位置や回転を元の場所に設定を戻します。
			else {			
				// Debug.Log("ikNotActive");
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);				
			}
		}
	}	  
}