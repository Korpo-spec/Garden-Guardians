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
        
        EditorGUI.BeginChangeCheck();
        script.weapon.colliderInfo[script.comboIndex].center = Handles.PositionHandle(script.transform.position + script.weapon.colliderInfo[script.comboIndex].center, Quaternion.identity)- script.transform.position;
        
        script.weapon.colliderInfo[script.comboIndex].halfsize = Handles.ScaleHandle(script.weapon.colliderInfo[script.comboIndex].halfsize, script.transform.position + script.weapon.colliderInfo[script.comboIndex].center, Quaternion.identity, 2);
        if (EditorGUI.EndChangeCheck())
        {
            
        }

        
        
    }
}
