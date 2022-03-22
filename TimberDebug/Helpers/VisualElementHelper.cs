using System.Collections.Generic;
using System.Linq;
using TimberbornAPI.Internal;
using UnityEngine.UIElements;

namespace TimberDebug.Helpers
{
    public static class VisualElementHelper
    {
        public static void RecursiveElementChecker(this VisualElement element, int indent = 0)
        {
            string indentString = "";
            for (int i = 0; i < indent; i++)
            {
                indentString += "--";
            }
            
            TimberAPIPlugin.Log.LogFatal($"{indentString} Type: {element.GetType()}, Name: {element.name}");
            TimberAPIPlugin.Log.LogWarning($"{indentString} {string.Join(", ", element.GetClasses())}");

            foreach (VisualElement childElement in element.Children())
            {
                childElement.RecursiveElementChecker(indent + 1);
            }
        }
        
        public static void RecursiveElementCodeExporter(this VisualElement element)
        {
            List<VisualElement> visualElements = new List<VisualElement>();
            element.RecursiveElementCodeExporter(ref visualElements);
            visualElements = visualElements.GroupBy(x => x, new VisualElementComparer()).Select(y => y.Key).ToList();
            
            TimberAPIPlugin.Log.LogWarning($"Amount: {visualElements.Count()}");
            string UIMethods = "UI methods \r\n\r\n";
            foreach (VisualElement visualElement in visualElements)
            {
                UIMethods += visualElement.GenerateMethodFromVisualElement();
                UIMethods += "\r\n\r\n";
            }
            TimberAPIPlugin.Log.LogWarning(UIMethods);
        }
        
        public static void RecursiveElementCodeExporter(this VisualElement element, ref List<VisualElement> visualElements)
        {
            visualElements.Add(element);
            foreach (VisualElement childElement in element.Children())
            {
                childElement.RecursiveElementCodeExporter(ref visualElements);
            }
        }
        
        private static string GenerateMethodFromVisualElement(this VisualElement element)
        {
            string type = element.GetType().FullName?.Split(".").Last();
            string methodName = string.IsNullOrWhiteSpace(element.name) ? "AAAAAAAAAAAA" : element.name.Replace("-", "_");
            return $"[Obsolete(\"Is not been controlled/tested\")]\r\n" + 
                   $"public {type} {methodName}(string name = null, IEnumerable<string> customClasses = null)\r\n" +
                   "{\r\n" +
                   $"   VisualElement element = new {type}() {{ classList = {{ {string.Join(", ", StringListToQuoted(element.classList))} }} }};\r\n" +
                   "    this.SetElementName(ref element, name);\r\n" +
                   "    this.AddCustomClassesToElement(ref element, customClasses);\r\n" +
                   $"   return ({type})element;\r\n" +
                   "}";
        }

        private static List<string> StringListToQuoted(List<string> classes)
        {
            List<string> quotedStrings = new List<string>();
            foreach (string classString in classes)
            {
                quotedStrings.Add($"\"{classString}\"");
            }
            
            return quotedStrings;
        }
    }
    
    internal class VisualElementComparer : IEqualityComparer<VisualElement> {
        public bool Equals(VisualElement x, VisualElement y)
        {
            return y != null && x != null && x.classList.SequenceEqual(y.classList) && x.GetType() == y.GetType();
        }

        public int GetHashCode(VisualElement obj) {

            return obj.classList == null ? -1 : obj.classList.Count;
        }
    }
}