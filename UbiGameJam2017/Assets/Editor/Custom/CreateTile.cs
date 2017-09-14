using UnityEngine;
using UnityEditor;

public class CreateTile : MonoBehaviour
{

    [MenuItem("Custom tile creation/Tile")]
    public static void CreateTileElement()
    {
        Vector3 position = Vector3.zero;
        float offset = 1.3f;
        int numberLine = 95;
        int numberColumn = 160;

        GameObject tilePrefab = Resources.Load("Prefabs/CustomTile") as GameObject;

        var parent = _ReturnSelectedObject();
        for(int i = 0; i < numberLine; i++)
        {
            position = new Vector3(position.x, position.y + offset, position.z);

            for (int j = 0; j < numberColumn; j++)
            {
                if ((j% numberColumn) == 0) {
                    position.x = 0;
                }

                var o = Instantiate(tilePrefab, position, Quaternion.identity);
                o.name = "X : " + position.x + " Y : " + position.y;
                o.transform.parent = parent;
                position = new Vector3(position.x + offset, position.y, position.z);

            }
        }
    }

    public static Transform _ReturnSelectedObject()
    {
        if(Selection.activeObject != null)
        {
            GameObject gParent = Selection.activeObject as GameObject;
            return gParent.transform;
        }
        return null;
    }
}
