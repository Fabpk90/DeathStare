using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine.AI;

// Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
[EditorTool("Platform Tool")]
class OffMeshLinkSpawns : EditorTool
{
	// Serialize this value to set a default value in the Inspector.
	[SerializeField]
	Texture2D m_ToolIcon;

	GUIContent m_IconContent;

	void OnEnable()
	{
		m_IconContent = new GUIContent()
		{
			image = m_ToolIcon,
			text = "OffMeshLinkSpawns",
			tooltip = "OffMeshLinkSpawns"
		};
	}

	public override GUIContent toolbarIcon
	{
		get { return m_IconContent; }
	}

	private Vector3 startPos;
	private bool startSaved = false;


	// This is called for each window that your tool is active in. Put the functionality of your tool here.
	public override void OnToolGUI(EditorWindow window)
	{
		EditorGUI.BeginChangeCheck();

		Event e = Event.current;

		Vector3 pos = SamplePos(GetMouseWorld()); ;
		if (e.isKey)
		{
			if(e.keyCode == KeyCode.Keypad1)
			{
				startPos = pos;
				startSaved = true;
				
			}else if (e.keyCode == KeyCode.Keypad2)
			{
				if(startSaved)
					CreateOffMeshLink(startPos, pos);
				startSaved = false;
			}
		}

		if (startSaved)
		{
			Handles.DrawWireCube(startPos,Vector3.one *2);
			Handles.DrawLine(pos, startPos);
		}else
		{
			Handles.DrawWireCube(pos, Vector3.one * 1);
		}
		

		/*Vector3 mousePosition = Event.current.mousePosition;
		Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
		mousePosition = ray.origin;
		Handles.DrawLine(Vector3.zero, mousePosition);*/

		if (EditorGUI.EndChangeCheck())
		{
			
		}
	}

	private void CreateOffMeshLink(Vector3 start, Vector3 end)
	{
		string containerName = "OffMeshLinks";
		GameObject container = GameObject.Find(containerName);
		if(container == null)
		{
			container = new GameObject(containerName);
			container.transform.position = Vector3.zero;
		}

		GameObject startParent = new GameObject("start");
		startParent.transform.parent = container.transform;
		GameObject endChild = new GameObject("end");
		endChild.transform.parent = startParent.transform;

		startParent.transform.position = start;
		endChild.transform.position = end;

		var offMesh = startParent.AddComponent<OffMeshLink>();
		offMesh.startTransform = startParent.transform;
		offMesh.endTransform = endChild.transform;
		offMesh.biDirectional = true;
	}

	private Vector3 GetMouseWorld()
	{
		Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit)){
			return hit.point;
		}
		return ray.origin;
	}

	private Vector3 SamplePos(Vector3 v)
	{
		NavMeshHit hit;
		if(NavMesh.SamplePosition(v, out hit, Mathf.Infinity, NavMesh.AllAreas))
		{
			return hit.position;
		}else
		{
			return v;
		}

	}
}