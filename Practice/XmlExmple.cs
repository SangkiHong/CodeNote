using System.Xml;
using UnityEngine;

namespace SK.Practice
{
    public class XmlExmple : MonoBehaviour
    {
        private void Start()
        {
            LoadItem();
            LoadItemElements("name");
        }

        private void LoadItem()
        {
            TextAsset txtAsset = (TextAsset)Resources.Load("XML/" + "Item");

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(txtAsset.text);

            XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/TestItem");

            foreach (XmlNode node in all_nodes)
            {
                Debug.Log("id :" + node.SelectSingleNode("id").Value);
                Debug.Log("name : " + node.SelectSingleNode("name").Value);
                Debug.Log("cost : " + node.SelectSingleNode("cost").Value);
            }
        }

        public void LoadItemElements(string elementName)
        {
            TextAsset txtAsset = (TextAsset)Resources.Load("XML/" + "Item");

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(txtAsset.text);

            XmlNodeList _Table = xmlDoc.GetElementsByTagName(elementName);

            foreach (XmlNode data in _Table)
            {
                Debug.Log("값 : " + data.InnerText);
            }
        }
    }
}