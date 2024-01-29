using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Tools
{
    public class ConvertFBXToPNG : MonoBehaviour
    {
        [SerializeField] private GameObject _fbx;
        [SerializeField] private Camera _camera;
       
        [Button]
        public void Convert()
        {
            ///Надо у камеры поставить в настройках что на фоне цвет и альфу у него сделать 0
            var render = _camera.targetTexture;
            SaveRenderTexture(render, $"Assets/_Project/Sprites/{_fbx.name}.png");
        }
        
        public void SaveRenderTexture(RenderTexture sourceRenderTexture, string savePath)
        {
            var tempTexture = new Texture2D(sourceRenderTexture.width, sourceRenderTexture.height, TextureFormat.ARGB32, false);
            RenderTexture.active = sourceRenderTexture;
            tempTexture.ReadPixels(new Rect(0, 0, sourceRenderTexture.width, sourceRenderTexture.height), 0, 0);
            tempTexture.Apply();
            RenderTexture.active = null;
        
            var textureBytes = tempTexture.EncodeToPNG();
            File.WriteAllBytes(savePath, textureBytes);
        }
        
        public void LoadImageToRenderTexture(ref RenderTexture destinationRenderTexture, string loadPath)
        {
            var loadedTexture = new Texture2D(destinationRenderTexture.width, destinationRenderTexture.height);
            var imageBytes = File.ReadAllBytes(loadPath);
            loadedTexture.LoadImage(imageBytes);
            
            Graphics.Blit(loadedTexture, destinationRenderTexture);
        }
    }
}