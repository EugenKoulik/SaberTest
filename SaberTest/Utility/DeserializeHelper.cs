namespace SaberTest
{
    public static class DeserializeHelper
    {
        public static List<NodeData> CreateNodesData(Stream stream)
        {
            var nodesData = new List<NodeData>();

            using (BinaryReader reader = new BinaryReader(stream))
            {
                string data;
                int randomId;
                int id = 0;

                while (reader.PeekChar() != -1)
                {
                    data = reader.ReadString();
                    randomId = reader.ReadInt32();

                    nodesData.Add(new NodeData() { Id = id, RandomId = randomId, Data = data });
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

            for (int i = 0; i < listOfNodes.Count; i++)
            {
                if (i != 0) listOfNodes[i].Previous = listOfNodes[i - 1];             
                if (i != listOfNodes.Count - 1) listOfNodes[i].Next = listOfNodes[i + 1];

                listOfNodes[i].Random = listOfNodes[nodesData[i].RandomId];
            }

            var list = new ListRandom
            {
                Count = nodesData.Count,
                Head = listOfNodes.First(),
                Tail = listOfNodes[listOfNodes.Count - 1]
            };

            return list;
        }
    }
}
