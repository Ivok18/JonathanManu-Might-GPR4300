using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ProceduralGenLearning.Advanced
{
    [CustomEditor(typeof(ProceduralGen))]
    public class ProceduralGenButton : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ProceduralGen targetSpript = (ProceduralGen)target;

            if(GUILayout.Button("Generate Caves"))
            {
                targetSpript.Generation();
            }
        }
    }
}
