// UGroup 1.5
// Simple Unity Editor Plugin
// Adds Menu: GameObject/Group 
// Places the selected objects inside a new empty go
// Adds Menu: GameObject/Group...
// Grouping wizzard with optional selections (now more selections)
// (c) 2012, 2013, 2018 G. Mattner

using UnityEngine;
using UnityEditor;

public class UGroup : EditorWindow
{
    static private GameObject groupParent;

    // [MenuItem("Edit/Group %#g", false, -999)]
	[MenuItem("GameObject/Group %#g", false, 35)]
    static void GroupMenu()
    {
        GroupSelectedTransforms();
    }

    static void GroupSelectedTransforms(string name = "Group", string tag = null, Vector3 position = default(Vector3), bool calcCenter = false, bool bottomY = false)
    {
        Transform[] currentSelection = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.Editable);
        if (currentSelection.Length == 0)
		{ 
            return;
		}
			
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2
        Undo.RegisterSceneUndo("Group Objects"); // V 3.5+
        groupParent = new GameObject(name);
#else
		groupParent = new GameObject(name);
		Undo.RegisterCreatedObjectUndo(groupParent, "Created group"); // V 4.3+
#endif
        if (calcCenter)
        {
            position = Vector3.zero;
            foreach (Transform groupMember in currentSelection)
            {
                position += groupMember.transform.position;
            }
            if (currentSelection.Length > 0)
                position /= currentSelection.Length;
			
			if (bottomY)
			{
				foreach (Transform groupMember in currentSelection)
				{
					if (groupMember.transform.position.y < position.y)
						position.y = groupMember.transform.position.y;
					if (groupMember.GetComponent<Collider>())
					if (groupMember.GetComponent<Collider>().bounds.min.y < position.y)
						position.y = groupMember.GetComponent<Collider>().bounds.min.y;
					if (groupMember.GetComponent<Renderer>())	
					if (groupMember.GetComponent<Renderer>().bounds.min.y < position.y)
						position.y = groupMember.GetComponent<Renderer>().bounds.min.y;
						
				}
			}
        }
        groupParent.transform.position = position;
        if (tag != null)
        {
            try
            {
                groupParent.tag = tag;
            }
            catch
            {
                Debug.Log("The tag '" + tag + "' is not defined, please add the tag in the tag manager!");
            }
        }
        foreach (Transform groupMember in currentSelection)
        {
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2
            groupMember.transform.parent = groupParent.transform;
#else
			Undo.SetTransformParent(groupMember.transform, groupParent.transform, "Group");
#endif
        }
        GameObject[] newSel = new GameObject[1];
        newSel[0] = groupParent;
        Selection.objects = newSel;
    }

    // [MenuItem("Edit/Group %#g", true, -999)]
	[MenuItem("GameObject/Group %#g", true, 35)]
    // [MenuItem("Edit/Group...", true, -998)]
	[MenuItem("GameObject/Group...", true, 36)]
    static bool ValidateSelection()
    {
        return Selection.activeTransform != null;
    }

    public Vector3 parentPosition = Vector3.zero;
    public bool centerPosition = false;
	public bool centerBottomY = false;
    public string groupName = "Group";
    public string gTag;

    // [MenuItem("Edit/Group...", false, -998)]
	[MenuItem("GameObject/Group...", false, 36)]
    public static void Init()
    {
        var win = GetWindow(typeof(UGroup)) as UGroup;
        win.autoRepaintOnSceneChange = true;
    }

    void OnGUI()
    {
        string helpString = "Groups the selected objects into an empty parent.\nYou can specify the position for the root object \nand it's name.\nYou can also select a tag for the group.\nCheck the center option to position the group\nat the center of the selected objects.";

        GUILayout.BeginHorizontal();
        GUILayout.Space(2);
        GUILayout.Label(helpString);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        gTag = EditorGUILayout.TagField("Tag group with:", gTag);
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Position parent in center of selected objects");
        centerPosition = EditorGUILayout.Toggle(centerPosition);
        GUILayout.EndHorizontal();
		if (!centerPosition)
			   GUI.enabled = false;
		GUILayout.BeginHorizontal();
        GUILayout.Label("... and at the bottom of the group (min Y)");
        centerBottomY = EditorGUILayout.Toggle(centerBottomY);
        GUILayout.EndHorizontal();
		GUI.enabled = true;
        GUILayout.BeginHorizontal();
        if (centerPosition)
            GUI.enabled = false;
        parentPosition = EditorGUILayout.Vector3Field("Parent Position", parentPosition);
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUI.enabled = true;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Group Name: ");
        groupName = GUILayout.TextField(groupName);
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }
        if (GUILayout.Button("Group"))
        {
            GroupSelectedTransforms(groupName, gTag, parentPosition, centerPosition, centerBottomY);
        }
        if (GUILayout.Button("Group & Close"))
		{
            GroupSelectedTransforms(groupName, gTag, parentPosition, centerPosition, centerBottomY);
            this.Close();
        }
	
        GUILayout.EndHorizontal();  
    }

    // [MenuItem("Edit/Ungroup %#h", false, -997)]
	[MenuItem("GameObject/Ungroup %#h", false, 37)]
    static void UngroupMenu()
    {
        Transform[] currentSelection = Selection.GetTransforms(SelectionMode.Editable);
        GameObject parentObject;
        if (currentSelection.Length == 0)
            return;

#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2
        Undo.RegisterSceneUndo("Ungroup Objects");
#endif

		foreach (Transform groupMember in currentSelection)
        {
            parentObject = groupMember.parent.gameObject;
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2
            groupMember.transform.parent = null;
#else
			Undo.SetTransformParent(groupMember.transform, null, "Ungroup");
#endif
            if (parentObject.GetComponents<Component>().Length < 2)
            {
                if (parentObject.transform.childCount == 0)
                {
                    Debug.Log("Empty group object \"" + parentObject.name + "\" deleted!");
#if UNITY_3_5 || UNITY_4_0 || UNITY_4_1 || UNITY_4_2
					DestroyImmediate(parentObject);
#else
					Undo.DestroyObjectImmediate(parentObject);
#endif
                }
            }
        }
    }

    static bool CheckGroupSelection()
    {
        if (Selection.activeTransform == null)
            return false;

        if (Selection.activeTransform.parent == null)
            return false;

        return true;
    }
		
    // [MenuItem("Edit/Ungroup %#h", true, -997)]
	[MenuItem("GameObject/Ungroup %#h", true, 37)]
    static bool ValidateGroupSelection()
    {
        return CheckGroupSelection();
    }
}



