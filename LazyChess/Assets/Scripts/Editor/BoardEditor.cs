using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{
   public override void OnInspectorGUI()
   {
      DrawDefaultInspector();

      Board board = (Board)target;

      if (GUILayout.Button("Generate"))
      {
         board.GenerateBoard();
      }
   }

}
