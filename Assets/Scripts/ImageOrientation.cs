using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOrientation : MonoBehaviour
{
    Image TitleImage;
    // Start is called before the first frame update
    void Start()
    {
        TitleImage = GetComponent<Image>();
        //if (TitleImage != null) Debug.Log("картинка найдена");
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight|| Screen.orientation == ScreenOrientation.Landscape)
        {
            TitleImage.type = Image.Type.Tiled;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            TitleImage.type = Image.Type.Simple;
        }
    }
}
