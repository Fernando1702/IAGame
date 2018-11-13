using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameController
{   
	// Use this for initialization
	void Start ()
   {
    
	}
	
	// Update is called once per frame
	void Update ()
   {
      if (active)
      {
         if (Input.GetButtonDown("Fire1"))
         {
            Vector3Int posInBoard = InputManager.posInBoard();
            Piece piece = GameManager.Instance.pieceInPosition(posInBoard);

            if (selectedPiece != null)
            {
               if (PositionAviable(posInBoard))
               {
                  selectedPiece.MoveToPosition(posInBoard);

                  UnSelectPiece(selectedPiece);
               }
            }

            if (piece)
            {
               if (piece.team == team)
               {
                  SelectPiece(piece);
               }
            }
         }


      }
	}

   void SelectPiece(Piece piece)
   {
      if (selectedPiece)
      {
         UnSelectPiece(selectedPiece);
      }

      piece.SetOutlineColor(GameManager.Instance.SelectedColor);

      Vector3Int[] positions = PieceManager.GetPieceMovements(piece.pieceType);

      SetAviablePositions(positions,piece);

      GameManager.Instance.gameBoard.SetPieceColor(piece.position.x, piece.position.z, GameManager.Instance.SelectedColor);

      for (int i = 0; i < AviableMovementPositions.Count; i++)
      {
         Piece other = GameManager.Instance.pieceInPosition(AviableMovementPositions[i]);

         if (other)
         {
            other.SetOutlineColor(GameManager.Instance.DangerColor);
            GameManager.Instance.gameBoard.SetPieceColor(AviableMovementPositions[i].x, AviableMovementPositions[i].z, GameManager.Instance.DangerColor);
         }
         else
         {
            GameManager.Instance.gameBoard.SetPieceColor(AviableMovementPositions[i].x, AviableMovementPositions[i].z, GameManager.Instance.SelectedColor);
         }
      }

      selectedPiece = piece;
   }

   void UnSelectPiece(Piece piece)
   {
      GameManager.Instance.gameBoard.SetBoardColor();
      piece.ResetOutlineColor();
      GameManager.Instance.ResetPiecesColor();
      selectedPiece = null;
   }

}
