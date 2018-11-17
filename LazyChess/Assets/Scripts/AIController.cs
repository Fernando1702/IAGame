using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}

public class MinimaxBoard
{

   //45656565
   //00000000
   //00000000
   //00000000
   //23232321
   
   //0 = Casilla vacia

   //1 = bandera blanca
   //2 = + blanco
   //3 = x blanco

   //4 = bandera negra
   //5 = + negro
   //6 = x negro

   //bandera = 1000
   // + = 15
   // x = 10
   
   byte[,] board;

   public MinimaxBoard(byte [,] board)
   {
      this.board = board;
   }

   public MinimaxMove [] GetMoves(bool isRival)
   {
      return null;
   }

   public MinimaxBoard MakeMove( MinimaxMove movement, int index)
   {
      //board[movement.posInBoard.x, movement.posInBoard.y] = 0;

      //Vector2Int endPos = movement.PosInBoard(index);

      //board[endPos.x, endPos.y] = movement.typeOfPiece;

      return new MinimaxBoard(board);
   }

   public int Evaluate()
   {
     return 0;
   }

   bool CurrentPlayer()
   {
      return false;
   }

   bool IsGameOver()
   {
      return false;
   }
}

public struct MinimaxMove
{
   public byte typeOfPiece;

   public Vector2Int posInBoard;

   public Vector2Int[] movements;

   public Vector2Int PosInBoard(int index)
   {
      return new Vector2Int(movements[index].x + posInBoard.x, movements[index].y + posInBoard.y);
   }   
}

