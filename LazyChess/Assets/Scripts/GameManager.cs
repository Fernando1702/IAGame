using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
   public Board gameBoard;

   List<Piece> piecesInGame = new List<Piece>();

   public Color SelectedColor, DangerColor, DeathColor;

   public static GameManager Instance { get; private set; }

   public SpriteRenderer turnColor;

   public Team p1, p2;

   public bool finished = false;

   public void EndTurn(string teamId)
   {
      
      if (teamId == p1.teamId)
      {
         Debug.Log("turn AI");
         
         p2.teamController.OnTurnStart();
         p1.teamController.OnTurnEnd();
         turnColor.color = p2.teamColor;
         //Empieza turno 2
      }
      else
      {
         Debug.Log("turn Player");

         p1.teamController.OnTurnStart();
         p2.teamController.OnTurnEnd();
         turnColor.color = p1.teamColor;
      }

   }

   void Awake()
   {
      if(Instance!= null)
      {
         Destroy(this);
         Debug.LogError("Game Manager declared multiple times");
      }
      else
      {
         Instance = this;
      }
   }
   // Use this for initialization
   void Start()
   {
      EndTurn(p2.teamId);
   }

   public void AddPiece(Piece piece)
   {
      piecesInGame.Add(piece);
   }

   public void RemovePiece(Piece piece)
   {
      piecesInGame.Remove(piece);
   }

   public Piece pieceInPosition(Vector3Int position)
   {
      return piecesInGame.Find(x => x.position == position);
   }
  
   public void ResetPiecesColor()
   {
      for (int i = 0; i < piecesInGame.Count; i++)
      {
         piecesInGame[i].ResetOutlineColor();
      }
   }

   public TextMeshProUGUI winText;

   public Team GetTeamById(string id)
   {
      if (id == p1.teamId)
      {
         return p1;
      }
      else if (id == p2.teamId)
      {
         return p2;
      }

      else return null;
   }

   public void OnFlagKilled(string id)
   {
      if (id == p1.teamId)
      {
         winText.text = p2.teamId + " wins";
      }
      else if (id == p2.teamId)
      {
         winText.text = p1.teamId + " wins";
      }

      p1.teamController.OnTurnEnd();
      p2.teamController.OnTurnEnd();

      finished = true;
   }

   public byte[,] GetByteBoard()
   {
      byte[,] byteBoard =new byte[gameBoard.dimensions.x, gameBoard.dimensions.y];

      for (int i = 0; i < gameBoard.dimensions.y; i++)
      {
         for (int j = 0; j < gameBoard.dimensions.x; j++)
         {
            Piece piece = GameManager.Instance.pieceInPosition(new Vector3Int(j, 0, i));

            if (piece != null)
            {
               byteBoard[j, i] = EvaluatePieceByte(piece);
            }
            else
            {
               byteBoard[j, i] = 0;
            }

         }
      }

      return byteBoard;
   }

   byte EvaluatePieceByte(Piece piece)
   {
      byte pieceByte = 0;

      byte addition = (byte)((piece.team == p1.teamId) ? 1 : 0);
      

      switch (piece.pieceType)
      {
         case PieceType.flag:

            pieceByte = (byte)(1 + addition);

            break;
         case PieceType.cross:

            pieceByte = (byte)(5 + addition);

            break;
         case PieceType.square:

            pieceByte = (byte)(3 + addition);

            break;
      }

      return pieceByte;
   }
   
	// Update is called once per frame
	void Update () {
		
	}
}
