using UnityEngine;
using System.Collections;

public class Controller2D :RaycastController {


	public CollisionInfo collisions;

	public LayerMask collisionMask2;

	public void Move(Vector3 velocity, bool standingOnPlatform = false)
	{
		UpdateRayCastOrigins ();
		collisions.Reset ();

		if(standingOnPlatform )
		{
			collisions.below = true;
		}

		if (velocity.x != 0) {
			HorizontalCollisions (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollisions (ref velocity);
		}

		transform.Translate (velocity);

		if(standingOnPlatform )
		{
			collisions.below = true;
		}
	}

	void HorizontalCollisions(ref Vector3 velocity)
	{


			float directionX = Mathf.Sign (velocity.x);

			float rayLength = Mathf.Abs (velocity.x) + skinWidth;

			for (int i =0; i< horizontalRayCount; i++) {
				Vector2 rayOrigin = (directionX==-1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

				Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);

				if(hit)
				{
					if(hit.distance ==0)
					{
						continue;
					}

					velocity.x = (hit.distance-skinWidth) * directionX;
					rayLength = hit.distance;

					collisions.left = directionX ==-1;
					collisions.right = directionX ==1;

				}
			}
	}

	void VerticalCollisions(ref Vector3 velocity)
	{
		float directionY = Mathf.Sign (velocity.y);

		float rayLength = Mathf.Abs (velocity.y) + skinWidth;

			for (int i =0; i< verticalRayCount; i++) {
				Vector2 rayOrigin = (directionY==-1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i +velocity.x);

				RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
				RaycastHit2D hit2 = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask2);

				Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);

				if(hit2)
				{
					if( directionY ==1)
					{
						continue;
					}
					else
					{
						velocity.y = (hit2.distance-skinWidth) * directionY;
						rayLength = hit2.distance;

						collisions.below = directionY ==-1;
						collisions.above = directionY ==1;
						continue;
					}
				}
				if(hit)
				{
					velocity.y = (hit.distance-skinWidth) * directionY;
					rayLength = hit.distance;

					collisions.below = directionY ==-1;
					collisions.above = directionY ==1;
				}
		}

	}


	public struct CollisionInfo{
		public bool above, below;
		public bool left, right;
		public void Reset(){
			above = below = false;
			left = right = false;
		}
	}
}
