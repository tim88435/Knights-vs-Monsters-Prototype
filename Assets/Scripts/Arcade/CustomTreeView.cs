using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class TreeCustomView : GraphView
{
    public new class UxmlFactory : UxmlFactory<TreeCustomView, GraphView.UxmlTraits> { }
    public TreeCustomView()
    {

    }
}
