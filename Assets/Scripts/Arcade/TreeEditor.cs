using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class TreeEditor : EditorWindow
{
    [MenuItem("TreeEditor/Editor ...")]
    public static void OpenWindow()
    {
        TreeEditor wnd = GetWindow<TreeEditor>();
        wnd.titleContent = new GUIContent("TreeEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Arcade/TreeEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Arcade/TreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        //root.Query<Button>("HelloWorld").First().clicked += OnClick;
    }
    private void OnClick()
    {
        Debug.Log("Yeet");
    }
}