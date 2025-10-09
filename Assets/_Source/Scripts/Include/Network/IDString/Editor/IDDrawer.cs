using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IDAttribute))]
public class IDDrawer : PropertyDrawer
{
    private const string PathToListIDs = "id_system/";
    private const float DistanceBetweenElements = 5f;
    private const int ElementsAmount = 3;

    private const float PopUpPart = .5f;
    private const float InputFieldPart = .3f;
    private const float ButtonPart = .2f;

    private string _inputFieldText;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        IDAttribute idAttribute = attribute as IDAttribute;

        var selectedID = property.stringValue;
        var idsList = Resources.Load<IDsList>(PathToListIDs + idAttribute.ListName);

        if (idsList == null)
        {
            EditorGUI.LabelField(position, label.text, $"ID List with name {idAttribute.ListName} doesn't");
            return;
        }

        var selectedIndex = 0;
        if (idsList.Contains(selectedID))
            selectedIndex = idsList.GetIDIndex(selectedID);

        var offsetedWidth = position.width - DistanceBetweenElements * (ElementsAmount - 1);
        var popUpWidth = offsetedWidth * PopUpPart;
        var inputFieldWidth = offsetedWidth * InputFieldPart;
        var buttonWidth = offsetedWidth * ButtonPart;

        var popUpRect = new Rect(position.x, position.y, popUpWidth, position.height);
        var inputRect = new Rect(position.x + popUpWidth + DistanceBetweenElements, position.y, inputFieldWidth, position.height);
        var buttonRect = new Rect(position.x + popUpWidth + inputFieldWidth + DistanceBetweenElements * 2f, position.y, buttonWidth, position.height);

        if (property.propertyType == SerializedPropertyType.String)
        {
            var newIndex = EditorGUI.Popup(popUpRect, selectedIndex, idsList.IDs.ToArray());
            
            _inputFieldText = EditorGUI.TextArea(inputRect, _inputFieldText);

            if (GUI.Button(buttonRect, "Add new ID"))
            {
                if (String.IsNullOrEmpty(_inputFieldText))
                {
                    Debug.LogError("ID Can't be empty");
                }
                else if (idsList.Contains(_inputFieldText))
                {
                    Debug.LogError($"ID {_inputFieldText} already exist");
                }
                else
                {
                    idsList.AddNewID(_inputFieldText);
                    _inputFieldText = "";
                    newIndex = idsList.IDs.Count - 1;
                    EditorUtility.SetDirty(idsList);
                }
            }

            property.stringValue = idsList.IDs[newIndex];
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use ID with string only");
        }
    }
}