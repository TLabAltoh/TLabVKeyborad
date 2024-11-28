using UnityEngine;
using UnityEditor;

namespace TLab.VKeyborad.Editor
{
    [CustomEditor(typeof(VKeyborad))]
    public class VKeyboradEditor : UnityEditor.Editor
    {
        private VKeyborad m_instance;

        private void OnEnable() => m_instance = target as VKeyborad;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Set Up Key"))
            {
                m_instance.SetUpKey();
                EditorUtility.SetDirty(m_instance);
            }

            if (GUILayout.Button("Set Up Key Visual"))
            {
                m_instance.SetUpKeyVisual();
                EditorUtility.SetDirty(m_instance);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
