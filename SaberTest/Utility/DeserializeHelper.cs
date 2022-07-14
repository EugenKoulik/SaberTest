namespace SaberTest
{
    public static class DeserializeHelper
    {
        public static List<NodeData> CreateNodesData(Stream stream)
        {
            var nodesData = new List<NodeData>();

            using (var sr = new StreamReader(stream))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    nodesData.Add(GetNodeData(line));
                }
            }

            return nodesData;
        }


        public static ListRandom CreateListRandom(List<NodeData> nodesData)
        {
            List<ListNode> listOfNodes = new List<ListNode>(nodesData.Count);

            for (int i = 0; i < nodesData.Count; i++)
            {
                listOfNodes.Add(new ListNode() { Data = nodesData[i].Data! });
            }

            for (int i = 1; i < listOfNodes.Count - 1; i++)
            {
                listOfNodes[i].Previous = listOfNodes[i - 1];
                listOfNodes[i].Next = listOfNodes[i + 1];
                listOfNodes[i].Random = listOfNodes[nodesData[i].RandomId].Random;
            }

            var list = new ListRandom
            {
                Count = nodesData.Count,
                Head = listOfNodes.First(),
                Tail = listOfNodes[listOfNodes.Count - 1]
            };

            return list;
        }

        private static NodeData GetNodeData(string line)
        {
            var properties = line.Split(new char[] {SerializeConstants.SPLIT_SYMBOL}, StringSplitOptions.RemoveEmptyEntries);
            var id = int.Parse(properties[0]);
            var randomId = int.Parse(properties[1]);
            string? data = null;

            if (properties.Length == 3)
            {
                data = properties[2];
            }

            return new NodeData() { Id = id, RandomId = randomId, Data = data };
        }

    }
}
