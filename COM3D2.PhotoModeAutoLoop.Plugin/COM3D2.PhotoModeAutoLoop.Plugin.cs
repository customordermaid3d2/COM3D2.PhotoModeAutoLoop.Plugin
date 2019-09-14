using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityInjector;
using UnityInjector.Attributes;


namespace COM3D2.PhotoModeAutoLoop.Plugin
{
    #region PluginMain
    ///=========================================================================
    /// <summary>スタジオモードモーション自動ループ</summary>
    /// <remarks>
    ///	COM3D2.PhotoModeAutoLoop.Plugin : スタジオモードでモーションを自動でループするようにする UnityInjector/Sybaris 用クラス
    ///
    /// </remarks>
    ///=========================================================================
    [PluginFilter("COM3D2x64"),
        PluginName("COM3D2.PhotoModeAutoLoop.Plugin edit by Lilly"),
        PluginVersion("190911")]
    public class PhotoModeAutoLoop : PluginBase
    {
        //private bool isStudio = false;
        private int isLoop = 0;
        private float deltaTotal = 0;

        public void Awake()
        {
            Log("Awake");
            try
            {
                GameObject.DontDestroyOnLoad(this);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            catch (Exception e)
            {
                LogErr(e.ToString());
            }
        }

        private static void LogErr(object e)
        {
            Debug.LogError("PhotoModeAutoLoop:" + e);
        }

        private void Log(object o)
        {
            Console.WriteLine("PhotoModeAutoLoop:" + o);
        }

        public void Update()
        {
            try
            {
                //if (isStudio)
                //{
                //SHIFT+L
                //if (
                //	(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                //	Input.GetKeyDown(KeyCode.L)
                //    )
                //SHIFT+CTR+L
                if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&

                    Input.GetKeyDown(KeyCode.L))
                //SHIFT+CTR+L
                //if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                //	(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                //	Input.GetKeyDown(KeyCode.L))
                {                    
                    isLoop += 1;
                    //if (isLoop > 5)
                    if (isLoop > 2)
                    {
                        isLoop = 0;
                    }
                    switch (isLoop)
                    {
                        //case 1:
                        //    Console.WriteLine("Case Once");
                        //    //BepInEx.Logger.Log(LogLevel.Info, "Case Once");
                        //    break;
                        //case 2:
                        //    Console.WriteLine("Case Loop");
                        //    //BepInEx.Logger.Log(LogLevel.Info, "Case Loop");
                        //    break;
                        case 1:                            
                            Log("Case PingPong. ");
                            //BepInEx.Logger.Log(LogLevel.Info, "Case PingPong");
                            break;
                        case 2:
                            Log("Case Default");
                            //BepInEx.Logger.Log(LogLevel.Info, "Case Default");
                            break;
                        //case 5:
                        //    Console.WriteLine("Case ClampForever");
                        //    //BepInEx.Logger.Log(LogLevel.Info, "Case ClampForever");
                        //    break;
                        default:
                            Log("case game");
                            //BepInEx.Logger.Log(LogLevel.Info, "Case case");
                            break;
                    }
                }
                if (isLoop > 0)
                {

                    deltaTotal += Time.deltaTime;
                    if (deltaTotal > 0.5f)
                    {
                        deltaTotal = 0;
                        List<Maid> maids = GameMain.Instance.CharacterMgr.GetStockMaidList();
                        if (maids != null)
                        {
                            foreach (Maid m in maids)
                            {
                                if (m != null && m.Visible)
                                {
                                    Animation anm = m.GetAnimation();
                                    if (anm != null)
                                    {
                                        //스위치 방식으로 변경
                                        switch (isLoop)
                                        {
                                            //case 1:
                                            //    anm.wrapMode = WrapMode.Once;
                                            //    break;
                                            //case 2:
                                            //    anm.wrapMode = WrapMode.Loop;
                                            //    break;
                                            case 1:
                                                anm.wrapMode = WrapMode.PingPong;
                                                //Console.WriteLine("Case 2 PingPong");
                                                break;
                                            case 2:
                                                anm.wrapMode = WrapMode.Default;
                                                //Console.WriteLine("Case 3 Default");
                                                break;
                                            //case 5:
                                               // anm.wrapMode = WrapMode.ClampForever;
                                                //Console.WriteLine("Case 4 ClampForever");
                                                //break;
                                                //default:
                                                //    Console.WriteLine("Default case");
                                                //    break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //}
            }
            catch (Exception e)
            {
                LogErr(e.ToString());
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            //Log(scene.name);
            try
            {
                if (scene.name == "ScenePhotoMode")
                //if (scene.buildIndex == 27)
                {
                    //スタジオモード
                    //isStudio = true;
                    isLoop = 2;
                    Log("Case Loop");
                }
                else
                {
                    //スタジオモード以外
                    //isStudio = false;
                    Log("Case Default");
                    isLoop = 0;
                }
            }
            catch (Exception e)
            {
                LogErr(e.ToString());
            }
        }

        public void OnGUI()
        {
            try
            {
            }
            catch (Exception e)
            {
                LogErr(e.ToString());
            }
        }

        private void Initialize()
        {
            try
            {
            }
            catch (Exception e)
            {
                LogErr(e.ToString());
            }
        }





    }
    #endregion
}