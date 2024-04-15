using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EntityAttack))]
public class EntityAttackEditor : Editor
{
    private void OnSceneGUI()
    {
        EntityAttack script = target as EntityAttack;
        if (!script) return;
        if (script.activateDebug == false) return;

        EditorGUI.BeginChangeCheck();
        Vector3 center = Handles.PositionHandle(script.transform.position + script.weapon.attackInfos[script.comboIndex].colliderInfo.center, Quaternion.identity)- script.transform.position;
        
        Vector3 halfsize = Handles.ScaleHandle(script.weapon.attackInfos[script.comboIndex].colliderInfo.halfsize, script.transform.position + script.weapon.attackInfos[script.comboIndex].colliderInfo.center, Quaternion.identity, 2);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(script, "Change weapon bounds");
            script.weapon.attackInfos[script.comboIndex].colliderInfo.center = center;
            script.weapon.attackInfos[script.comboIndex].colliderInfo.halfsize = halfsize;
            EditorUtility.SetDirty(script);
        }

        
        
    }
}
