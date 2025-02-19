using System.Collections;
using UnityEngine;

namespace Trucking.Graph
{
	/// <summary>
	/// The Follower.
	/// </summary>
	[ExecuteInEditMode]
	public class Follower : MonoBehaviour
	{

		[SerializeField] protected Graph m_Graph;
		[SerializeField] protected Node m_Start;
		[SerializeField] protected Node m_End;
		[SerializeField] protected float m_Speed = 0.01f;
		protected GraphPath mGraphPath = new GraphPath();
		protected Node m_Current;

		void Start()
		{
			mGraphPath = m_Graph.GetShortestPath(m_Start, m_End);
			Follow(mGraphPath);
		}

		/// <summary>
		/// Follow the specified path.
		/// </summary>
		/// <param name="graphPath">Path.</param>
		public void Follow(GraphPath graphPath)
		{
			StopCoroutine("FollowPath");
			mGraphPath = graphPath;
			transform.position = mGraphPath.nodes[0].transform.position;
			StartCoroutine("FollowPath");
		}

		/// <summary>
		/// Following the path.
		/// </summary>
		/// <returns>The path.</returns>
		IEnumerator FollowPath()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.update += Update;
#endif
			var e = mGraphPath.nodes.GetEnumerator();
			while (e.MoveNext())
			{
				m_Current = e.Current;

				// Wait until we reach the current target node and then go to next node
				yield return new WaitUntil(() => { return transform.position == m_Current.transform.position; });
			}

			m_Current = null;
#if UNITY_EDITOR
			UnityEditor.EditorApplication.update -= Update;
#endif
		}

		void Update()
		{
			if (m_Current != null)
			{
				transform.position = Vector3.MoveTowards(transform.position, m_Current.transform.position, m_Speed);
			}
		}

	}
}