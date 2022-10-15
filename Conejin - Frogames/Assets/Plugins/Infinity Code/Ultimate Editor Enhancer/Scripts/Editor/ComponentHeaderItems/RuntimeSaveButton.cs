/*           INFINITY CODE          */
/*     https://infinity-code.com    */

using System.Collections.Generic;
using InfinityCode.UltimateEditorEnhancer.Attributes;
using UnityEditor;
using UnityEngine;

namespace InfinityCode.UltimateEditorEnhancer.ComponentHeader
{
    [InitializeOnLoad]
    public static class RuntimeSaveButton
    {
        private static Dictionary<int, string> savedComponents = new Dictionary<int, string>();

        static RuntimeSaveButton()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        [ComponentHeaderButton]
        public static bool Draw(Rect rectangle, Object[] targets)
        {
            Object target = targets[0];
            if (!Validate(target)) return false;

            if (GUI.Button(rectangle, EditorIconContents.saveActive, Styles.iconButton))
            {
                string json = EditorJsonUtility.ToJson(target);
                int id = target.GetInstanceID();
                savedComponents[id] = json;
                Component c = target as Component;
                Debug.Log($"{c.gameObject.name}/{ObjectNames.NicifyVariableName(target.GetType().Name)} component state saved.");
            }

            return true;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode) savedComponents.Clear();
            else if (state == PlayModeStateChange.EnteredEditMode)
            {
                Undo.SetCurrentGroupName("Set saved state");
                int group = Undo.GetCurrentGroup();

                foreach (KeyValuePair<int, string> pair in savedComponents)
                {
                    Object obj = EditorUtility.InstanceIDToObject(pair.Key);
                    if (obj != null)
                    {
                        Undo.RecordObject(obj, "Set saved state");
                        EditorJsonUtility.FromJsonOverwrite(pair.Value, obj);
                        EditorUtility.SetDirty(obj);
                    }
                }

                Undo.CollapseUndoOperations(group);
            }
        }

        private static bool Validate(Object target)
        {
            if (!Prefs.saveComponentRuntime) return false;
            if (!EditorApplication.isPlaying) return false;
            if (!(target is Component)) return false;
            //if (target.GetInstanceID() < 0) return false;
            return true;
        }
    }
}
