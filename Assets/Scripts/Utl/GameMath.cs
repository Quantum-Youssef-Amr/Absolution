using UnityEngine;

public static class GameMath
{

    public static Vector2 FromGridToIsometric(Vector2 GridPos, Vector2 offset, Vector2 mapTileSize)
    {
        return new Vector2(
            ((GridPos.x - GridPos.y) * mapTileSize.x) + offset.x, // x = (x - y) * w/2
            ((GridPos.x + GridPos.y) * mapTileSize.y) + offset.y  // y = (x + y) * h/2
        );
    }

    public static Vector2 FromIsometricToGrid(Vector2 IsoPos, Vector2 Offset, Vector2 mapTileSize)
    {
        IsoPos -= Offset;
        return new Vector2(
            IsoPos.x / (mapTileSize.x * 2) + IsoPos.y / (mapTileSize.y * 2),    // x = x/w + y/h
            -IsoPos.x / (mapTileSize.x * 2) + IsoPos.y / (mapTileSize.y * 2)    // y = -x/w + y/h
        );
    }


    public static bool IsInRange(Vector2 v, Vector2 mn, Vector2 mx)
    {
        if (v.x >= mn.x && v.x <= mx.x && v.y >= mn.y && v.y <= mx.y)
        {
            return true;
        }
        return false;
    }

}
