using UnityEngine;
using System;
using System.IO;

public class CameraScreenShot : MonoBehaviour
{

    private static CameraScreenShot instance;

    private Camera myCamera;
    private bool takeSreenShotOnNextframe;
    int number = 1;
    AudioSource sound;

    public GameObject Logotype;
    string shootName;


    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();

    }

    private void Start()
    {
        Logotype.SetActive(false);
        sound = GetComponent<AudioSource>();

    }
    private string GetAndroidExternalStoragePath()
    {
        if (Application.platform != RuntimePlatform.Android)
            return Application.persistentDataPath;

        var jc = new AndroidJavaClass("android.os.Environment");
        var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", jc.GetStatic<string>("DIRECTORY_DCIM")).Call<string>("getAbsolutePath");
        return path;
    }
    private void OnPostRender()
    {
        if (takeSreenShotOnNextframe)
        {
           
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();
#if UNITY_ANDROID
            shootName = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(texture, Application.productName + " Captures", name));
#endif


#if UNITY_EDITOR
            shootName = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(texture, Application.productName + " Captures", name));
#endif


#if UNITY_IOS
            string fullPath = Path.Combine(Application.persistentDataPath, "LiA" + number + ".png");
            File.WriteAllBytes(fullPath, texture.EncodeToPNG());
            NativeGallery.SaveImageToGallery(fullPath, "PassionMaps", Path.GetFileName(fullPath));

            //string nameiOS = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            //string nameiOS ="LifeInAmber_" +Convert.ToString(number)+".png";
            //Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(texture, Application.productName + " Captures", nameiOS, null));// добавил null в функцию
# endif
            
            number++;
            Destroy(texture);//очистка
            Logotype.SetActive(false);
            takeSreenShotOnNextframe = false;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////   
        //Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        //Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        //renderResult.ReadPixels(rect, 0, 0);

        //byte[] byteArray = renderResult.EncodeToPNG();
        //System.IO.File.WriteAllBytes(Application.persistentDataPath + "/CameraScreenshot"+number+".png", byteArray);
        //number++;
        //Debug.Log("Saved screenshot");

        //RenderTexture.ReleaseTemporary(renderTexture);
        //myCamera.targetTexture = null;


    }
    public void TakePhoto()
    {

        //myCamera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16);
        if (Screen.orientation == ScreenOrientation.LandscapeLeft) { Logotype.transform.localPosition = new Vector3(0f, -0.0163f, 0.06f); }
        else if (Screen.orientation == ScreenOrientation.Portrait) { Logotype.transform.localPosition = new Vector3(0f, -0.03f, 0.06f); }
        Logotype.SetActive(true);
        takeSreenShotOnNextframe = true;
        if (!sound.isPlaying)
        {
            sound.Play();
        }

    }
    private void TakeScreenShot(int width, int height)
    {

        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeSreenShotOnNextframe = true;
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenShot(width, height);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraScreenShot.TakeScreenshot_Static(500, 500);
        }
        sound.volume = MainMenuManager.volumeLevel / 2f;
    }
}