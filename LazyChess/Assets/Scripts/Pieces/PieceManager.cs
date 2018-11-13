using System.Collections.Generic;
using UnityEngine;

public static class PieceManager
{
   public static Dictionary<PieceType, PieceMovement> movementsPerType;


   static void GenerateMovements()
   {
      movementsPerType = new Dictionary<PieceType, PieceMovement>();

      PieceMovement regularMove = new PieceMovement(Vector3Int.right,Vector3Int.left, new Vector3Int(0,0,1), new Vector3Int(0, 0, -1));

      movementsPerType.Add(PieceType.square,regularMove);

      PieceMovement diagonalMove = new PieceMovement(new Vector3Int(1, 0, 1), new Vector3Int(-1, 0, 1), new Vector3Int(1, 0, -1), new Vector3Int(-1, 0, -1));

      movementsPerType.Add(PieceType.cross, diagonalMove);

      PieceMovement flagMove = new PieceMovement();

      movementsPerType.Add(PieceType.flag, flagMove);
      
   }

   public static Vector3Int[] GetPieceMovements(PieceType type)
   {

      if(movementsPerType == null)
      {
         GenerateMovements();
      }

      return movementsPerType[type].targetPositions;
   }

}

public class PieceMovement
{
   public Vector3Int[] targetPositions;

   public PieceMovement (params Vector3Int [] positions)
   {
      targetPositions = positions;
   }
}