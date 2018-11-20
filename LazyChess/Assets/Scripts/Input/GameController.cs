using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   public bool active = true;

   [SerializeField]
   protected string team = "";
   protected Piece selectedPiece = null;
   protected List<Vector3Int> AviableMovementPositions = new List<Vector3Int>();
   

   public void SetTeam(string team)
   {
      this.team = team;
   }
   
   public virtual void OnTurnStart()
   {
      Debug.Log("Activate");
      active = true;
   }

   public virtual void OnTurnEnd()
   {
      Debug.Log("Deactivate");
      active = false;
   }
   
   protected void SetAviablePositions(Vector3Int[] positions, Piece piece)
   {
      List<Vector3Int> endPositions = new List<Vector3Int>();

      for (int i = 0; i < positions.Length; i++)
      {
         if (GameManager.Instance.gameBoard.ItsInsideBoard(positions[i] + piece.position))
         {
            Piece other = GameManager.Instance.pieceInPosition(positions[i] + piece.position);

            if (other)
            {
               if(other.team != team)
               {
                  endPositions.Add(positions[i] + piece.position);
               }
            }
            else
            {
               endPositions.Add(positions[i] + piece.position);
            }

         }
      }
      AviableMovementPositions = endPositions; 
   }
   
   public bool PositionAviable(Vector3Int pos)
   {
      for (int i = 0; i < AviableMovementPositions.Count; i++)
      {
         if (AviableMovementPositions[i] == pos)
         {
            return true;
         }
      }

      return false;
   }
}
