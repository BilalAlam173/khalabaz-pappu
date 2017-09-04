using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public float speed=150f;
	public float jetSpeed=200f;
	public Vector2 maxVelocity=new Vector2(60,100);
	public bool standing;
	public float standingThreshold=4f;
	public float airSpeedMultiplier=.3f;

	private Rigidbody2D body2D;
	private SpriteRenderer renderer2D;
	private playerController controller;
	private Animator animator;
	// Use this for initialization
	void Start () {
		body2D = GetComponent<Rigidbody2D> ();
		renderer2D = GetComponent<SpriteRenderer> ();
		controller =GetComponent<playerController>();
		animator=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		var absVelX=Mathf.Abs(body2D.velocity.x);
		var absVelY=Mathf.Abs(body2D.velocity.y);
		var absVel= new Vector2(absVelX,absVelY);

		var forceX=0f; 
		var forceY=0f;

		if(absVel.y<=standingThreshold){
			standing=true;
		}else{
			standing=false;
		}

		if(controller.moving.x!=0){
			if(absVel.x < maxVelocity.x){
				var newSpeed=speed* controller.moving.x;
				forceX=standing? newSpeed:(newSpeed*airSpeedMultiplier);
			}

			renderer2D.flipX=forceX<0;
			animator.SetInteger("animState",1);
			}else{
				animator.SetInteger("animState",0);
			}

			if(controller.moving.y!=0){
				if(absVel.y<maxVelocity.y){
					var newJetSpeed=jetSpeed*controller.moving.y;
					forceY=newJetSpeed;
				}
				animator.SetInteger("animState",2);
			}else if(absVel.y>0&&standing==false){
				animator.SetInteger("animState",3);
			}

			body2D.AddForce(new Vector2(forceX,forceY));
	}
}
