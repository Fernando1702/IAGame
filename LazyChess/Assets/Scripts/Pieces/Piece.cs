using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType { flag, cross , square }

public class Piece : MonoBehaviour {

   public string team = "";

   public PieceType pieceType;

   public Vector3Int position;

   Color pieceColor;

   public Piece(string team, PieceType pieceType, Vector3Int position, Color pieceColor, Mesh mesh, Material mat)
   {
      gameObject.AddComponent<MeshRenderer>().material = mat;
      gameObject.AddComponent<MeshFilter>().mesh = mesh;
           
      this.team = team;
      this.pieceType = pieceType;
      this.position = position;
      this.pieceColor = pieceColor;

      SetMainColor(pieceColor);
      ResetOutlineColor();
   }

   public void Initialize(string team, PieceType pieceType, Vector3Int position, Color pieceColor, Mesh mesh, Material mat)
   {
      gameObject.AddComponent<MeshRenderer>().material = mat;
      gameObject.AddComponent<MeshFilter>().mesh = mesh;

      this.team = team;
      this.pieceType = pieceType;
      this.position = position;
      this.pieceColor = pieceColor;

      SetMainColor(pieceColor);
      ResetOutlineColor();

      GameManager.Instance.AddPiece(this);
   }
   
   void Update()
   {
      Move();
   }

   void Move()
   {
      transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 3);
   }

   public void MoveToPosition(Vector3Int pos)
   {      
      Piece other = GameManager.Instance.pieceInPosition(pos);
      
      position = pos;

      if (other && other.team != team)
      {
         other.KillPiece();
      }
   }

   public void KillPiece()
   {
      SetOutlineColor(GameManager.Instance.DeathColor);
      GameManager.Instance.RemovePiece(this);
      position = GameManager.Instance.GetTeamById(team).stackPosition + ((pieceType == PieceType.cross)? new Vector3Int(0,0,1) : Vector3Int.zero) ;

      if(pieceType == PieceType.flag)
      {
         Debug.Log("Banderitas");
         GameManager.Instance.OnFlagKilled(team);
      }

   }

   public void SetMainColor(Color color)
   {
      pieceColor = color;
      GetComponent<MeshRenderer>().material.color = pieceColor;
      ResetOutlineColor();
   }

   public void SetOutlineColor(Color color)
   {
      GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", color);
   }
   public void ResetOutlineColor()
   {
      Color newColor = new Color(1 - pieceColor.r, 1 - pieceColor.g, 1 - pieceColor.b);
      GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", newColor);
   }
}
