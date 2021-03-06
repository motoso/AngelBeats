﻿using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]  

public class IKCtrl4LeapMotion : MonoBehaviour {
	protected Animator animator;
	protected int isEmbarrassedId;
	protected float IKGoalPosition = 0.0f;
	public bool ikActive = false;
	public float detectDistance = 1.0f;


	
	void Start () 
	{
		animator = GetComponent<Animator>();
		isEmbarrassedId = Animator.StringToHash("isEmbarrassed");
	}

	void Update()
	{
		// GameObject hand = GameObject.Find("SkeletalHand(Clone)");
		// Debug.Log("x" + hand.transform.position.x);

		if(Input.GetKey(KeyCode.UpArrow)){
			animator.SetBool(isEmbarrassedId, true);
		}else{
			animator.SetBool(isEmbarrassedId, false);
		}
	}

	void OnAnimatorIK()
	{
		if(animator) {
            //IKがアクティブならば、位置や回転を直接、目標物に設定します。
			if(ikActive) {
				GameObject hand = GameObject.Find("SkeletalHand(Clone)/palm");
				// GameObject hand = GameObject.Find("palm");

				if(hand){
					// 自分手が近くに来たときにのみ手を握ってくれる
					Vector3 unityChanPos = animator.bodyPosition;
					Vector3 myHandPos = hand.transform.position;
					float distanceTillUC = Vector3.Distance(unityChanPos, myHandPos);
					Debug.Log("distanceTillUC: " + distanceTillUC);					

					if(distanceTillUC < detectDistance){
						Transform rightHandObj = hand.transform;
					// Debug.Log(GameObject.Find("SkeletalHand(Clone)/palm"));
					// Debug.Log("x" + hand.transform.position.x);

                    //右手のweight = 1.0 は位置や回転が目標物に設定します（キャラクターが握りたい場所）
						animator.SetIKPositionWeight(AvatarIKGoal.RightHand,IKGoalPosition);
						animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1.0f);

					// ゆっくり近づける
						if(IKGoalPosition < 1.0f){
							IKGoalPosition += 0.005f;
						}

			        //右手の位置および回転を外部オブジェクトの位置に設定する
						if(rightHandObj != null) {
							animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
							animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
						}					
					}
				}else{
					//手がなくなったらゆっくり近づけるパラメタを初期化
					IKGoalPosition = 0.0f;
				}
			}
			//IKがアクティブでないならば、位置や回転を元の場所に設定を戻します。
			else {
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);				
			}
		}
	}	  
}