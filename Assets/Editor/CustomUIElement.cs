using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

[CustomEditor(typeof(RotationWindow))]
public class RotationWindowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(ButtonOBJ))]
public class ButtonOBJEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(ToggleOBJ))]
public class ToggleOBJEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(ImageColorSwitch))]
public class ImageColorSwitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ImageColorSwitch org = target as ImageColorSwitch;
        base.OnInspectorGUI();
        if (GUILayout.Button("Switch To ColorA"))
        {
            org.ToA();
        }
        if (GUILayout.Button("Switch To ColorB"))
        {
            org.ToB();
        }
    }
}
[CustomEditor(typeof(ImageSpriteSwitch))]
public class ImageSpriteSwitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ImageSpriteSwitch org = target as ImageSpriteSwitch;
        base.OnInspectorGUI();
        if (GUILayout.Button("Switch To ColorA"))
        {
            org.ToA();
        }
        if (GUILayout.Button("Switch To ColorB"))
        {
            org.ToB();
        }
    }
}
