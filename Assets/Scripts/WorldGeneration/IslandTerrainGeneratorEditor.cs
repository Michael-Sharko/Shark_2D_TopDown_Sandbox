using UnityEditor;
using UnityEngine;

namespace Shark.WorldGeneration
{
    [CustomEditor(typeof(IslandTerrainGenerator))/*, ExecuteInEditMode*/]
    class IslandTerrainGeneratorEditor : Editor
    {
        IslandTerrainGenerator _generator;
        Editor _worldEditor;

        private void OnEnable()
        {
            _generator = (IslandTerrainGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if (check.changed)
                {
                    _generator.GenerateIsland();
                }
            }

            if (GUILayout.Button("Generate Island"))
            {
                _generator.GenerateIsland();
            }

            DrawSettingsEditor(_generator.worldSettings, _generator.OnIslandTerrainUpdated, ref _generator.worldSettingsFoldout, ref _worldEditor);
        }

        void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
        {
            if (settings != null)
            {
                foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
                using var check = new EditorGUI.ChangeCheckScope();
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        onSettingsUpdated?.Invoke();
                    }
                }
            }
        }
    }
}