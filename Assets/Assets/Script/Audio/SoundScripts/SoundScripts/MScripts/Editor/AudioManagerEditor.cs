using UnityEngine;
using UnityEditor;
using FMODUnity;

[CustomEditor(typeof(AudioManagerEvents))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var local = target as AudioManagerEvents;
        if (local != null)
        {
            if (GUILayout.Button("Parse Children"))
            {
                var dic = local.Emitters;
                if (dic != null)
                {
                    dic.editorSerialize = true;
                    dic.Clear();
                    foreach (Transform child in local.transform)
                    {
                        var emitter = child.GetComponent<StudioEventEmitter>();
                        if (emitter != null)
                        {
                            var name = emitter.tag;
                            dic.TryAdd(name, emitter);
                        }
                    }
                    dic.editorSerialize = false;
                    EditorUtility.SetDirty(local);
                }
            }
        }
    }
}
