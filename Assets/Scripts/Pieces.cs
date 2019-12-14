using System.Collections;
using System.Collections.Generic;

public class Pieces {

	public float[,] pieceCoordinates;

	public Pieces(string pieceType, float x, float y) {
		if (string.Equals(pieceType, "T")) {
			pieceCoordinates = TPiece(x, y);
		} else if (string.Equals(pieceType, "Square")) {
			pieceCoordinates = SquarePiece(x, y);
		} else if (string.Equals(pieceType, "Line")) {
			pieceCoordinates = LinePiece(x, y);
		} else if (string.Equals(pieceType, "L")) {
			pieceCoordinates = LPiece(x, y);
		} else if (string.Equals(pieceType, "Two")) {
			pieceCoordinates = TwoPiece(x, y);
		} else {
			pieceCoordinates = DotPiece(x, y);
		}
	}

	public float[,] TPiece(float x, float y) {
		return new float[4, 2] { { x, y }, { x + 1, y }, { x + 2, y }, { x + 1, y - 1 } };
	}

	public float[,] SquarePiece(float x, float y) {
		return new float[4, 2] { { x, y }, { x + 1, y }, { x, y - 1 }, { x + 1, y - 1 } };
	}

	public float[,] LinePiece(float x, float y) {
		return new float[3, 2] { { x, y }, { x + 1, y }, { x + 2, y } };
	}

	public float[,] LPiece(float x, float y) {
		return new float[3, 2] { { x, y }, { x, y - 1 }, { x + 1, y - 1 } };
	}

	public float[,] TwoPiece(float x, float y) {
		return new float[2, 2] { { x, y }, { x + 1, y } };
	}

	public float[,] DotPiece(float x, float y) {
		return new float[1, 2] { { x, y } };
	}
}
