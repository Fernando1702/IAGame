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

   public Team p1, p2;

   public void EndTurn(string teamId)
   {
      Debug.Log(teamId);

      if (teamId == p1.teamId)
      {
         p2.OnTurnStart();
         p1.OnTurnEnd();
         //Empieza turno 2
      }
      else
      {
         p1.OnTurnStart();
         p2.OnTurnEnd();
         //Empieza turno 1
      }

      //Comprobar condición de victoria
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
      p1.teamController.OnTurnStart();
      p2.teamController.OnTurnEnd();

      winText.text = "";
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
   }

	// Update is called once per frame
	void Update () {
		
	}
}
