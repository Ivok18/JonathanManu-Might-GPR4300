using UnityEngine;
using UnityEditor;

namespace ProceduralGenLearning.Basic
{
    [CustomEditor(typeof(ProeceduralGeneration))]
    public class GenerationButton : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ProeceduralGeneration targetScript = (ProeceduralGeneration)target; //target means target obj
            if(GUILayout.Button("Generate New Map"))
            {
                targetScript.Generate();
            }
        }
    }
}