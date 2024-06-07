using System;
using LightWeightFramework.Components.ViewComponents;
using UnityEngine;
using Zenject;

namespace Mario.Components.Movement
{
    public class MoveViewComponent : ViewComponent<IMovementModelObserver>, IFixedTickable
    {
        private const float MinMoveDistance = 0.001f;
        private const float ShellRadius = 0.01f;
        
        [SerializeField] private Rigidbody2D rigidbody;
        
        private ContactFilter2D contactFilter;
        private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        private Vector2 groundNormal;

        protected override void OnInit()
        {
            base.OnInit();
            transform.position = Model.StartPosition;
            rigidbody.isKinematic = true;
            
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            contactFilter.useLayerMask = true;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            rigidbody.isKinematic = false;
        }

  

        public void FixedTick()
        {
            if (Model.Velocity.y < 0)
            {
                Model.Velocity += Model.GravityModifier * Physics2D.gravity * Time.fixedDeltaTime;
            }
            else
            {
                Model.Velocity += Physics2D.gravity * Time.fixedDeltaTime;
            }

            Model.SetVelocityOnAxisX(Model.TargetVelocity.x);
            Model.IsGrounded = false;

            Vector2 deltaPosition = Model.Velocity * Time.fixedDeltaTime;

            Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            PerformMovement(move, false);

            move = Vector2.up * deltaPosition.y;

            PerformMovement(move, true);
        }

        private void PerformMovement(Vector2 move, bool yMovement)
        {
            var distance = move.magnitude;

            if (distance > MinMoveDistance)
            {
                //check if we hit anything in current direction of travel
                var count = rigidbody.Cast(move, contactFilter, hitBuffer, distance + ShellRadius);
                for (var i = 0; i < count; i++)
                {
                    var currentNormal = hitBuffer[i].normal;

                    //is this surface flat enough to land on?
                    if (currentNormal.y > Model.MinGroundNormalY)
                    {
                        Model.IsGrounded = true;
                        // if moving up, change the groundNormal to new surface normal.
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }
                

                    if (Model.IsGrounded)
                    {
                        //how much of our velocity aligns with surface normal?
                        var projection = Vector2.Dot(Model.Velocity, currentNormal);
                        if (projection < 0)
                        {
                            //slower velocity if moving against the normal (up a hill).
                            Model.Velocity -= projection * currentNormal;
                        }
                    }
                    else
                    {
                        //We are airborne, but hit something, so cancel vertical up and horizontal velocity.
                        Model.ResetVelocity();
                    }

                    //remove shellDistance from actual move distance.
                    float modifiedDistance = hitBuffer[i].distance - ShellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }
             rigidbody.position += move.normalized * distance;
        }
    }
}