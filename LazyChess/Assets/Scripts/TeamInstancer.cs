using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamInstancer : MonoBehaviour {

   public Mesh flag, square, cross;
   public Material pieceMat;

	// Use this for initialization
	void Start ()
   {
      Invoke("InstantiateBoard", 0.1f);       
	}
	
   public void InstantiateBoard()
   {
      InstantiateLine(0, GameManager.Instance.p1, false, GameManager.Instance.gameBoard.dimensions.x);
      InstantiateLine(GameManager.Instance.gameBoard.dimensions.y-1, GameManager.Instance.p2, true, GameManager.Instance.gameBoard.dimensions.x);
   }

   public void InstantiateLine(int zCoord, Team team, bool direction, int numberOfPieces)
   {
      int startPointX = (direction)? 0 : numberOfPieces-1;

      InstantiatePiece(PieceType.flag, team, new Vector3Int(startPointX, 0, zCoord));

      if (direction)
      {
         for (int i = 1; i < numberOfPieces; i++)
         {
            PieceType t = (i % 2 == 0) ? PieceType.cross : PieceType.square;
            InstantiatePiece(t, team, new Vector3Int(i, 0, zCoord));
         }
      }
      else
      {
         for (int i = numberOfPieces - 1; i > 0; i--)
         {
            PieceType t = (i % 2 == 0) ? PieceType.square : PieceType.cross;
            InstantiatePiece(t, team, new Vector3Int(i-1, 0, zCoord));
         }
      }
   }

   
   void InstantiatePiece(PieceType type, Team team, Vector3Int pos)
   {
      Piece piece;
      piece = new GameObject().AddComponent<Piece>();

      switch (type)
      {
         case PieceType.flag:
          
            piece.Initialize(team.teamId, type, pos, team.teamColor, flag, pieceMat);

            break;
         case PieceType.cross:

            piece.Initialize(team.teamId, type, pos, team.teamColor, cross, pieceMat);

            break;
         case PieceType.square:

            piece.Initialize(team.teamId, type, pos, team.teamColor, square, pieceMat);

            break;
      }


   }
}
