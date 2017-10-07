using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ViveController
{
	public enum CollisionType 
	{
		StaticCollider,
		RigidbodyCollider,
		KinematicRigidbodyCollider,
		StaticTriggerCollider,
		RigidbodyTriggerCollider,
		KinematicRigidbodyTriggerCollider
	}

	public enum CollisionOutcome
	{
		Neither,
		OnCollisionEnter,
		OnTriggerEnter,
		Both
	}

	[CustomEditor(typeof(CollisionCheck))]
	public class CollisionCheckEditor : ControllerObjectEditor 
	{
		private CollisionCheck collisionCheck;
		public override void OnInspectorGUI()
		{
			collisionCheck = (CollisionCheck)target;
			if (!collisionCheck.dismiss && DependencyCheck(collisionCheck))
			{
				if (GUILayout.Button("Dismiss"))
					collisionCheck.dismiss = true;
			}
			if (collisionCheck.obj != null)
				EditorGUILayout.HelpBox("These objects will collide with: " + checkCollision((collisionCheck.useThisObject ? collisionCheck.gameObject : collisionCheck.objTwo), collisionCheck.obj), MessageType.Info);
			collisionCheck.useThisObject = EditorGUILayout.ToggleLeft("Use this object", collisionCheck.useThisObject);
			collisionCheck.obj = (GameObject)EditorGUILayout.ObjectField("Object: ", collisionCheck.obj, typeof(GameObject), true);
			if(!collisionCheck.useThisObject)
				collisionCheck.objTwo = (GameObject)EditorGUILayout.ObjectField("Object: ", collisionCheck.objTwo, typeof(GameObject), true);
		}

		public CollisionOutcome checkCollision(GameObject objectOne, GameObject objectTwo)
		{
			bool onCollision = false;
			bool onTrigger = false;
			ArrayList thisCol = collisionType(objectOne);
			ArrayList targetCol = collisionType(objectTwo);
			if (thisCol.Contains(CollisionType.StaticCollider))
			{
				if (targetCol.Contains(CollisionType.RigidbodyCollider))
					onCollision = true;
				if (targetCol.Contains(CollisionType.RigidbodyTriggerCollider) || targetCol.Contains(CollisionType.KinematicRigidbodyTriggerCollider))
					onTrigger = true;
			}
			if (thisCol.Contains(CollisionType.RigidbodyCollider))
			{
				if (targetCol.Contains(CollisionType.StaticCollider) || targetCol.Contains(CollisionType.RigidbodyCollider) || targetCol.Contains(CollisionType.KinematicRigidbodyCollider))
					onCollision = true;
				if (targetCol.Contains(CollisionType.StaticTriggerCollider) || targetCol.Contains(CollisionType.RigidbodyTriggerCollider) || targetCol.Contains(CollisionType.KinematicRigidbodyTriggerCollider))
					onTrigger = true;
			}
			if (thisCol.Contains(CollisionType.KinematicRigidbodyCollider))
			{
				if (targetCol.Contains(CollisionType.RigidbodyCollider))
					onCollision = true;
				if (targetCol.Contains(CollisionType.StaticTriggerCollider) || targetCol.Contains(CollisionType.RigidbodyTriggerCollider) || targetCol.Contains(CollisionType.KinematicRigidbodyTriggerCollider))
					onTrigger = true;
			}
			if (thisCol.Contains(CollisionType.StaticTriggerCollider))
			{
				if (targetCol.Contains(CollisionType.RigidbodyCollider) || targetCol.Contains(CollisionType.KinematicRigidbodyCollider)  || targetCol.Contains(CollisionType.RigidbodyTriggerCollider)  || targetCol.Contains(CollisionType.KinematicRigidbodyTriggerCollider))
					onTrigger = true;
			}
			if (thisCol.Contains(CollisionType.RigidbodyTriggerCollider) || thisCol.Contains(CollisionType.KinematicRigidbodyTriggerCollider))
			{
				if (targetCol.Count > 0)
					onTrigger = true;
			}
			return (onCollision && onTrigger) ? CollisionOutcome.Both : (!onCollision && !onTrigger) ? CollisionOutcome.Neither : (onCollision) ? CollisionOutcome.OnCollisionEnter : CollisionOutcome.OnTriggerEnter;
		}

		public ArrayList collisionType(GameObject go)
		{
			ArrayList colliders = new ArrayList();
			if (go != null)
			{
				Rigidbody rigidbody = go.GetComponent<Rigidbody>();
				Collider[] cols = go.GetComponents<Collider>();
				foreach (Collider c in cols)
				{
					int colType = 0;
					if (c.isTrigger)
						colType += 3;
					if (rigidbody != null)
					{ 
						if (rigidbody.isKinematic)
							colType += 2;
						else
							colType++;
					}
					colliders.Add((CollisionType)colType);
				}
			}
			return colliders;		
		}
	}
}
