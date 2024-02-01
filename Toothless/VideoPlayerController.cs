using System;
using System.IO;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Toothless {
    public class VideoPlayerController : MonoBehaviour {
        private static GameObject _gameObject;
        private static VideoPlayer _videoPlayer;
        private static Material _chromaKeyMaterial;
        public static void Initialize() {
            _gameObject = new GameObject("Toothless Object");
            _gameObject.AddComponent<VideoPlayerController>();
            Canvas canvas = CreateCanvas();
            CreateVideoPlayer(canvas);
        }

        private static Canvas CreateCanvas() {
            Canvas canvas = _gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 52309;
            CanvasScaler canvasScaler = _gameObject.AddComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            return canvas;
        }

        private static void CreateVideoPlayer(Canvas canvas) {
            GameObject videoPanel = new GameObject("VideoPanel");
            RectTransform panelTransform = videoPanel.AddComponent<RectTransform>();
            videoPanel.transform.SetParent(canvas.transform);
            panelTransform.sizeDelta = new Vector2(1050, 750);
            panelTransform.anchoredPosition = new Vector2(0, 100);
            _videoPlayer = videoPanel.AddComponent<VideoPlayer>();
            _videoPlayer.url = Path.Combine(Main.ModEntry.Path, "test.mp4");
            _videoPlayer.playOnAwake = false;
            RawImage rawImage = videoPanel.AddComponent<RawImage>();
            _videoPlayer.targetTexture = new RenderTexture(854, 476, 30);
            rawImage.texture = _videoPlayer.targetTexture;
            Shader shader = Main.AssetBundle.LoadAsset<Shader>("Assets/Shaders/ChromaKeyShader.shader");
            _chromaKeyMaterial = new Material(shader) {
                color = new Color(6, 255, 24)
            };
            rawImage.material = _chromaKeyMaterial;
            Main.Logger.Log("Create New Video");
        }

        public static void Show() {
            if(_gameObject == null) Initialize();
            _videoPlayer.time = 0;
            _videoPlayer.Play();
            DontDestroyOnLoad(_gameObject);
            Main.Logger.Log("Show Video");
        }

        public static void Hide() {
            if(_gameObject == null) return;
            DestroyImmediate(_gameObject);
            _videoPlayer.Stop();
        }
    }
}