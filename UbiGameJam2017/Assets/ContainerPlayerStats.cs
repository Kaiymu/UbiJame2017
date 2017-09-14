using UnityEngine;
using UnityEngine.UI;

public class ContainerPlayerStats : MonoBehaviour {

    public Text teamNameText;
    public Text colorationText;
    public RawImage colorationRaw;

    public void SetInfo(string name, string coloration, Texture playerTexture)
    {
        teamNameText.text = name;
        colorationText.text = coloration;

        colorationRaw.texture = playerTexture;
    }
}
