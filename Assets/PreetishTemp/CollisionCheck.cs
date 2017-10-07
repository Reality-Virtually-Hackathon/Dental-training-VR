using System.Collections;
using UnityEngine;
namespace ViveController
{
	public class CollisionCheck : MonoBehaviour 
	{
		[SerializeField]
		public GameObject obj;
		[SerializeField]
		public GameObject objTwo;
		[SerializeField]
		public bool useThisObject = true;
		public bool dismiss = false;
	}
}
