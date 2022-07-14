using System.Text;

namespace SaberTest
{
    public static class SerializeHelper
    {
        public static List<ListNode> GetListOfNodes(ListRandom list)
        {         
            var listOfNodes = new List<ListNode>();
            ListNode? currentNode = list.Head;

            if(currentNode != null)
            {
                do
                {
                    listOfNodes.Add(currentNode);
                    currentNode = currentNode.Next;

                } while(currentNode != null);
            }

            return listOfNodes;
        }

        public static string GetSerializeString(List<ListNode> listNodes)
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < listNodes.Count; i++)
            {
                ListNode? currentNode = listNodes[i];
                stringBuilder.AppendLine($"{i}{SerializeConstants.SPLIT_SYMBOL}{GetId(listNodes, currentNode.Random)}{SerializeConstants.SPLIT_SYMBOL}{currentNode.Data}");
            }

            return stringBuilder.ToString();
        }

        private static int GetId(List<ListNode> listNodes, ListNode node)
        {
            return listNodes.IndexOf(node);
        }

    }
}
