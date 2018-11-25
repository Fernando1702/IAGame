using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : GameController
{   
   public override void OnTurnStart()
   {
      active = true;

      Invoke("Negamax", 1);
   }

   void Negamax()
   {
      byte[,] byteBoard = GameManager.Instance.GetByteBoard();

      //string aux = "";

      //for (int i = byteBoard.GetLength(1) - 1; i >= 0; i--)
      //{
      //   for (int j = 0; j < byteBoard.GetLength(0); j++)
      //   {
      //      aux += byteBoard[j, i];
      //   }

      //   aux += "\n";
      //}

      //Debug.Log(aux);

      Debug.Log((byteBoard[0, byteBoard.GetLength(1) - 1]));
      Debug.Log(byteBoard[byteBoard.GetLength(0) - 1, 0]);

      BoardNegamax board = new BoardNegamax(1, byteBoard); //Extraer del tablero
                                                           //Llamar a negamax

      MoveAndScore result = NegamaxAlphaBeta(board, 4, 0, -100000, 100000);

      Debug.Log(result);

      Piece pieceToMove = GameManager.Instance.pieceInPosition(new Vector3Int(result.move.from.x, 0, result.move.from.y));
      pieceToMove.MoveToPosition(new Vector3Int(result.move.to.x, 0, result.move.to.y));
      

      GameManager.Instance.EndTurn(team);
   }

   public override void OnTurnEnd()
   {
      active = false;
   }

   public class BoardNegamax
   {
      public byte currentPlayer;
      public byte[,] board;
           
      public BoardNegamax(byte currentPlayer, byte[,] board)
      {
         this.currentPlayer = currentPlayer;
         this.board = board;
      }

      // Jugador: este
      // Byte: piezas que le pertenecer

      public Move[] GetMoves()
      {
         List<Move> moves = new List<Move>();

         for (int x = 0; x < board.GetLength(0); x++)
         {
            for (int y = 0; y < board.GetLength(1); y++)
            {
               if (board[x, y] != 0)
               {
                  if
                  (
                     (board[x, y] % 2 == 0 && currentPlayer == 2) || (board[x, y] % 2 != 0 && currentPlayer == 1)
                  )
                  {
                     if (board[x, y] != 1 && board[x, y] != 2)
                     {
                        moves.AddRange(EvaluatePiece(new Vector2Int(x, y), board[x, y]));
                     }
                  }
               }
            }
         }

         return moves.ToArray();
      }

      private Move[] EvaluatePiece(Vector2Int positionPiece, byte typeOfPiece)
      {
         List<Move> moves = new List<Move>();

         if (typeOfPiece == 3 || typeOfPiece == 4) // +
         {
            Move move = null;

            Vector2Int movement = new Vector2Int(positionPiece.x, positionPiece.y + 1);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }

            movement = new Vector2Int(positionPiece.x + 1, positionPiece.y);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }

            movement = new Vector2Int(positionPiece.x, positionPiece.y - 1);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }

            movement = new Vector2Int(positionPiece.x - 1, positionPiece.y);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }
         }

         else if (typeOfPiece == 5 || typeOfPiece == 6)  // x
         {
            Move move = null;

            Vector2Int movement = new Vector2Int(positionPiece.x + 1, positionPiece.y + 1);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }

            movement = new Vector2Int(positionPiece.x + 1, positionPiece.y - 1);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }

            movement = new Vector2Int(positionPiece.x - 1, positionPiece.y - 1);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }

            movement = new Vector2Int(positionPiece.x - 1, positionPiece.y + 1);

            move = GetMove(movement, typeOfPiece, positionPiece);

            if (move != null)
            {
               moves.Add(move);
               move = null;
            }
         }

         return moves.ToArray();

      }

      private Move GetMove(Vector2Int movement, byte typeOfPiece, Vector2Int positionPiece)
      {
         Move move = null;

         if (IsInBoard(movement))
         {
            if (board[movement.x, movement.y] % 2 != typeOfPiece % 2 || board[movement.x, movement.y] == 0)
            {
               move = new Move(positionPiece, movement);
            }
         }

         return move;
      }

      private bool IsInBoard(Vector2Int position)
      {
         return (position.x >= 0 && position.x < board.GetLength(0) && position.y >= 0 && position.y < board.GetLength(1));
      }

      public BoardNegamax MakeMove(Move move)
      {
         byte[,] newBoard = new byte[board.GetLength(0), board.GetLength(1)];

         for (int i = 0; i < board.GetLength(0); i++)
         {
            for (int j = 0; j < board.GetLength(1); j++)
            {
               newBoard[i, j] = board[i, j];
            }
         }

         byte player = (byte)((currentPlayer == 1) ? 2 : 1);

         byte pieceId = newBoard[move.from.x, move.from.y];

         newBoard[move.from.x, move.from.y] = 0;
         newBoard[move.to.x, move.to.y] = pieceId;

         BoardNegamax newBoardNegamax = new BoardNegamax(player, newBoard);

         return newBoardNegamax;
      }

      public bool IsGameOver()
      {
         //Debug.Log((board[0, board.GetLength(1) - 1] != 1 || board[board.GetLength(0) - 1, 0] != 2));

         return (board[0, board.GetLength(1) - 1] != 1 || board[board.GetLength(0) - 1, 0] != 2);
      }

      public int Evaluate()
      {
         int score = 0;
         int pMultiplier = (currentPlayer == 1) ? -1 : 1;

         //Debug.Log(board.GetLength(0)); //= 9
         //Debug.Log(board.GetLength(1)); //= 5

         for (int i = 0; i < board.GetLength(0); i++)
         {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Vector2 rigthCorner = new Vector2(board.GetLength(0), 0);
                Vector2 leftCorner = new Vector2(0, board.GetLength(1));

                float maxDist = Vector2.Distance(rigthCorner, leftCorner);
                float dist = 0;
                float distMul = 2;
               
                if (board[i, j] == 2)
                {
                   score += 10000;
                }
                else if (board[i, j] == 1)
                {
                   score -= 10000;
                }
                else if (board[i, j] == 3)
                {
                   score -= 15;
                   dist = Vector2.Distance(new Vector2(i, j), leftCorner);
                   score -= (int)(distMul * (dist / maxDist));
                }
                else if (board[i, j] == 4)
                {
                   score += 15;
                   dist = Vector2.Distance(new Vector2(i, j), rigthCorner);
                   score += (int)(distMul * (dist / maxDist));
                }
                else if (board[i, j] == 5)
                {
                   score -= 10;
                   dist = Vector2.Distance(new Vector2(i, j), leftCorner);
                   score -= (int)(distMul * (dist / maxDist));
                }
                else if (board[i, j] == 6)
                {
                   score += 10;
                   dist = Vector2.Distance(new Vector2(i, j), rigthCorner);
                   score += (int)(distMul * (dist / maxDist));
                }                
            }
         }

            if (board[board.GetLength(0) - 2, 0] == 0)
            {
                score -= 50;
            }

            else if (board[board.GetLength(0) - 2, 0] % 2 == 0)
            {
                score += 1000;
            }

            else if (board[board.GetLength(0) - 2, 0] % 2 != 0)
            {
                score -= 5000;
            }

            if (board[board.GetLength(0) - 1, 1] == 0)
            {
                score -= 50;
            }

            else if (board[board.GetLength(0) - 1, 1] % 2 == 0)
            {
                score += 1000;
            }

            else if (board[board.GetLength(0) - 1, 1] % 2 != 0)
            {
                score -= 5000;
            }

            if (board[board.GetLength(0) - 2, 1] == 0)
            {
                score -= 50;
            }

            else if (board[board.GetLength(0) - 2, 1] % 2 == 0)
            {
                score += 1000;
            }

            else if (board[board.GetLength(0) - 2, 1] % 2 != 0)
            {
                score -= 5000;
            }

            return score * pMultiplier;
      }

      
   }

   public class MoveAndScore
   {
      public MoveAndScore(Move move, int score)
      {
         this.move = move;
         this.score = score;
      }

      public Move move;
      public int score;
   }

   MoveAndScore NegamaxAlphaBeta(BoardNegamax board, int maxDepth, int currentDepth, int alpha, int beta)
   {
      if (board.IsGameOver() || currentDepth == maxDepth)
      {
         return new MoveAndScore(null, board.Evaluate());
      }

      MoveAndScore bestMoveAndScore = new MoveAndScore(null, -1000000);

      foreach (Move move in board.GetMoves())
      {
         BoardNegamax newBoard = board.MakeMove(move);

         MoveAndScore moveAndScoreRecurse = NegamaxAlphaBeta(newBoard, maxDepth, currentDepth + 1, -beta, -Mathf.Max(alpha, bestMoveAndScore.score));

         int currentScore = -moveAndScoreRecurse.score;

         if (currentScore > bestMoveAndScore.score)
         {
            bestMoveAndScore.score = currentScore;
            bestMoveAndScore.move = move;

            if (bestMoveAndScore.score >= beta)
            {
               return bestMoveAndScore;
            }
         }
      }

      return bestMoveAndScore;
   }

   public class Move
   {
      public Move(Vector2Int from, Vector2Int to)
      {
         this.from = from;
         this.to = to;
      }


      // Referencia a la pieza movida.
      public Vector2Int from, to;
      // Índice de posición en fila.
      // Índice de posición en columna.
   }
}


   //24646464
   //00000000
   //00000000
   //00000000
   //35353531
   
   //0 = Casilla vacia

     // blanco +
     // negro -

   //1 = bandera blanca
   //3 = + blanco
   //5 = x blanco

   //2 = bandera negra
   //4 = + negro
   //6 = x negro

   //bandera = 1000
   // + = 15
   // x = 10



