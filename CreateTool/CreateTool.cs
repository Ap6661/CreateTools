using ResoniteModLoader;
using FrooxEngine;
using System;
using System.Collections.Generic;


namespace CreateTool
{
    public class CreateTool : ResoniteMod
    {
        public override string Name => "CreateTool";
        public override string Author => "APnda";
        public override string Version => "2.0.1";
        
        public override string Link => "https://github.com/Ap6661/CreateTools";

        private void GenerateToolCategory(CategoryNode<Type> node, string path)
        {
            using (IEnumerator<Type> enumerator2 = node.Elements.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    Type tool = enumerator2.Current;
                    DevCreateNewForm.AddAction(path, tool.Name, delegate (Slot s)
                    {
                        var AAPMethod = typeof(DevCreateNewForm).GetMethod("AttachAndPosition");
                        var AAPOfTool = AAPMethod.MakeGenericMethod(new[] { tool });
                        AAPOfTool.Invoke(new DevCreateNewForm(), new object[] {s});
                    });
                }
            }
        }

        public override void OnEngineInit()
        {
            Engine.Current.RunPostInit(() =>
            {
                GenerateToolCategory(WorkerInitializer.ComponentLibrary.GetSubcategory("Tools"), "Tools");
                GenerateToolCategory(WorkerInitializer.ComponentLibrary.GetSubcategory("Tools/Brushes"), "Tools/Brushes");
            });
        }

       
    }
}