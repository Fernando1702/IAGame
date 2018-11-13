using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager 
{
   public static Vector3Int posInBoard()
   {
      Vector3Int posInBoard = Vector3Int.one * -1;

      RaycastHit info;
      
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out info))
      {
         posInBoard = GameManager.Instance.gameBoard.WorldPosToBoardPos(info.point);                 
      }

      return posInBoard;
   }
}
