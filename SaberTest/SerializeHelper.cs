using System.Text;

namespace SaberTest
{
    public static class SerializeHelper
    {
        private const char splitSymbol = '-';

        public static List<ListNode> GetListOfNodes(ListRandom list)
        {
            var listOfNodes = new List<ListNode>(list.Count);
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
                stringBuilder.AppendLine($"{i}{splitSymbol}{GetId(listNodes, currentNode.Random)}{splitSymbol}{currentNode.Data}");
            }

            return stringBuilder.ToString();
        }

        public static Dictionary<int, Tuple<int, string>> CreateNodesData(Stream stream)
        {
            var nodesData = new Dictionary<int, Tuple<int, string>>();
            using (var sr = new StreamReader(stream))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var nodeData = GetNodeData(line);
                    nodesData.Add(nodeData.Key, nodeData.Value);
                }
            }

            return nodesData;
        }

        public static ListRandom CreateListRandom(int count, ListNode[] nodes)
        {
            var list = new ListRandom
            {
                Count = count,
                Head = nodes[0],
                Tail = nodes[^1]
            };

            return list;
        }

        public static ListNode[] InitNodeArray(int count)
        {
            var nodes = new ListNode[count];

            for (int i = 0; i < count; i++)
            {
                nodes[i] = new ListNode();
            }

            return nodes;
        }

        private static KeyValuePair<int, Tuple<int, string>> GetNodeData(string line)
        {
            var properties = line.Split(new char[] {splitSymbol}, StringSplitOptions.RemoveEmptyEntries);

            var id = int.Parse(properties[0]);
            var randomId = int.Parse(properties[1]);
            var data = properties[2];

            return new KeyValuePair<int, Tuple<int, string>>(id, new Tuple<int, string>(randomId, data));
        }

        public static void FillNodes(Dictionary<int, Tuple<int, string>> nodesData, ListNode[] nodes)
        {
            foreach (var nodeData in nodesData)
            {
                FillNode(nodeData, nodes);
            }
        }

        private static void FillNode(KeyValuePair<int, Tuple<int, string>> nodeData, ListNode[] nodes)
        {
            int id = nodeData.Key;
            int randomId = nodeData.Value.Item1;
            string data = nodeData.Value.Item2;

            nodes[id].Data = data;
            nodes[id].Previous = GetPreviousNode(nodes, id);
            nodes[id].Next = GetNextNode(nodes, id);
            nodes[id].Random = GetRandomNode(nodes, id, randomId);
        }

        private static ListNode GetRandomNode(ListNode[] nodes, int id, int randomId)
        {
            return nodes[randomId];
        }

        private static ListNode GetPreviousNode(ListNode[] nodes, int id)
        {
            return id == 0 ? null : nodes[id - 1];
        }

        private static ListNode GetNextNode(ListNode[] nodes, int id)
        {
            return id == nodes.Length - 1 ? null : nodes[id + 1];
        }

        private static int GetId(List<ListNode> listNodes, ListNode node)
        {
            return listNodes.IndexOf(node);
        }

    }
}
