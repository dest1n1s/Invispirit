using Assets.Scripts.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class VisibilityManager
    {
        protected static Dictionary<Renderer, VisibilityManager> managerMap;
        protected Renderer renderer;
        protected Renderer[] childRenderer;
        protected float currentTime;//完全显形持续时间
        protected float maxTime;//完全显形到开始隐形所需时间
        protected float emergeTime;//从完全隐形开始显形到完全显形所需时间
        protected float fadeTime;//从完全显形开始隐形到完全隐形所需时间
        protected bool Show = false;//true: 处于显形过程或已完全显形；false: 处于隐形过程或已完全隐形
        protected VisibilityManager(Renderer renderer)
        {
            this.renderer = renderer;
            GetTime();
            VisibilityChangeBehavior.instance.StartCoroutine(CheckVisibility());
            childRenderer = renderer.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in childRenderer) r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0);
        }
        protected void GetTime()
        {
            maxTime = (float)ConfigManager.ReadTime("keepVisible");
            emergeTime = (float)ConfigManager.ReadTime("emerge");
            fadeTime = (float)ConfigManager.ReadTime("fade");
        }
        public static VisibilityManager GetInstance(Renderer renderer)
        {
            if (managerMap == null) managerMap = new Dictionary<Renderer, VisibilityManager>();
            if (managerMap.ContainsKey(renderer))
            {
                return managerMap[renderer];
            }
            else
            {
                managerMap.Add(renderer, new VisibilityManager(renderer));
                return managerMap[renderer];
            }
        }

        IEnumerator CheckVisibility()
        {
            while (true)
            {
                if (Show && renderer.material.color.a == 1)
                {
                    currentTime += Time.deltaTime;
                    if (currentTime >= maxTime)
                    {
                        currentTime = 0;
                        Show = false;
                    }
                }
                if (Show && renderer.material.color.a < 1)
                {
                    renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b,
                        (renderer.material.color.a + Time.deltaTime / emergeTime > 1 ? 1 : renderer.material.color.a + Time.deltaTime / emergeTime));
                }
                else if(!Show && renderer.material.color.a > 0)
                {
                    renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b,
                        (renderer.material.color.a - Time.deltaTime / fadeTime < 0 ? 0 : renderer.material.color.a - Time.deltaTime / emergeTime));
                }
                if (childRenderer != null)
                {
                    foreach (Renderer r in childRenderer)
                    {
                        r.material.color = new Color(r.material.color.a, r.material.color.g, r.material.color.b, renderer.material.color.a);
                    }
                }
                
                yield return null;
            }
            
        }

        public void Emerge()
        {
            Show = true;
            currentTime = 0;
        }
    }
}
