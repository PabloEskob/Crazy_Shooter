using UnityEditor;
using UnityEditor.UI;

namespace Assets.Source.Scripts.UI.Menus.Rewards.Editor
{
    [CustomEditor(typeof(AdvertisementButton), editorForChildClasses: true)]
    public class AdsButtonEditor : ButtonEditor
    {
        private const string IconPath = "Assets/Modern Shooting UI Pack/Modern Shooting UI Resources/Sliced Elements/15Missions/Icon_Watch Videos.png";
        private const string TimeText = "Time Between Ads";

        private SerializedProperty _text;
        private SerializedProperty _defaultText;
        private SerializedProperty _time;
        private SerializedProperty _languageSwitcher;

        protected override void OnEnable()
        {
            base.OnEnable();

            _text = serializedObject.FindProperty(nameof(_text));
            _defaultText = serializedObject.FindProperty(nameof(_defaultText));
            _time = serializedObject.FindProperty(nameof(_time));
            _languageSwitcher = serializedObject.FindProperty(nameof(_languageSwitcher));
            UnityEngine.GUIContent iconContent = EditorGUIUtility.IconContent(IconPath);
            EditorGUIUtility.SetIconForObject(target, (UnityEngine.Texture2D)iconContent.image);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Ads Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_time, new UnityEngine.GUIContent(TimeText));
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

            EditorGUILayout.LabelField("Language Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_defaultText);
            EditorGUILayout.PropertyField(_languageSwitcher);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();

            EditorGUILayout.LabelField("Button Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(_text);

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
            EditorGUILayout.EndVertical();
        }
    }
}
