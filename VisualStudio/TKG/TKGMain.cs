using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace TKG
{
    internal sealed class TKGMain : MelonMod
    {
        public override void OnInitializeMelon()
        {
            //Any initialization code goes here.
            //This method can be deleted if no initialization is required.
        }
        public static bool loadedCookingTex;
        private static List<string> cookableGear = new List<string>();
        public static Material vanillaLiquidMaterial;

        public static Material InstantiateLiquidMaterial()
        {
            if (!vanillaLiquidMaterial)
            {
                vanillaLiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);

                vanillaLiquidMaterial.name = "Liquid";
            }

            return new Material(vanillaLiquidMaterial);
        }

        public override void OnSceneWasInitialized(int level, string name)
        {
            if (ModComponent.Public.IsLoaded())
            {
                if (!loadedCookingTex) // adding pot cooking textures
                {
                    cookableGear.Add("GroundBeef"); // case-sensitive
                    cookableGear.Add("GroundBeefOpened");
                    cookableGear.Add("BakedBeans");
                    cookableGear.Add("BakedBeansOpened");
                    cookableGear.Add("CrushedTomatoes");
                    cookableGear.Add("CrushedTomatoesOpened");
                    cookableGear.Add("SwedishMeatballs");
                    cookableGear.Add("SwedishMeatballsOpened");
                    cookableGear.Add("ChickenMeatballs");
                    cookableGear.Add("ChickenMeatballsOpened");
                    cookableGear.Add("PremiumPasta");
                    cookableGear.Add("MashedPotatoes");
                    cookableGear.Add("MacCheese");
                    cookableGear.Add("Sausages");
                    cookableGear.Add("SausagesOpened");
                    cookableGear.Add("Pineapples");
                    cookableGear.Add("PineapplesOpened");

                    Material potMat;
                    GameObject potGear;

                    for (int i = 0; i < cookableGear.Count; i++)
                    {
                        potGear = GearItem.LoadGearItemPrefab("GEAR_" + cookableGear[i]).gameObject;

                        if (potGear == null) continue;

                        Texture tex = potGear.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture;

                        potMat = InstantiateLiquidMaterial();
                        potMat.name = ("CKN_" + cookableGear[i] + "_MAT");

                        potMat.mainTexture = tex;
                        potMat.SetTexture("_Main_texture2", tex);

                        potGear.GetComponent<Cookable>().m_CookingPotMaterialsList = new Material[1] { potMat };
                    }

                    loadedCookingTex = true;
                }
            }
        }
    }
}