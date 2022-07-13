
namespace SaberTest
{
    public class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
            var listOfNodes = SerializeHelper.GetListOfNodes(this);
            var serializeString = SerializeHelper.GetSerializeString(listOfNodes);

            using (StreamWriter streamWriter = new StreamWriter(s))
            {
                streamWriter.Write(serializeString);
            }
        }

        public void Deserialize(Stream s)
        {
            var nodesData = SerializeHelper.CreateNodesData(s);
            int count = nodesData.Count;
            var nodes = SerializeHelper.InitNodeArray(count);
            SerializeHelper.FillNodes(nodesData, nodes);

            var list = SerializeHelper.CreateListRandom(count, nodes);
            Count = list.Count;
            Head = list.Head;
            Tail = list.Tail;
        }

        public override bool Equals(object? obj)
        {
            var item = obj as ListRandom;

            if ((item == null) || (item.Count != this.Count))
            {
                return false;
            }

            var firstCurrentNode = item.Head;
            var secondCurrentNode = this.Head;

            while((firstCurrentNode != null) || (secondCurrentNode != null))
            {
                if (firstCurrentNode.Data != secondCurrentNode.Data)
                {
                    return false;
                }

                if (firstCurrentNode.Next != null && secondCurrentNode.Next != null)
                {
                    if (firstCurrentNode.Next.Data != secondCurrentNode.Next.Data)
                    {
                        return false;
                    }
                }

                if (firstCurrentNode.Previous != null && secondCurrentNode.Previous != null)
                {
                    if (firstCurrentNode.Previous.Data != secondCurrentNode.Previous.Data)
                    {
                        return false;
                    }
                }

                if (firstCurrentNode.Random.Data != secondCurrentNode.Random.Data)
                {
                    return false;
                }

                firstCurrentNode = firstCurrentNode.Next;
                secondCurrentNode = secondCurrentNode.Next;
            }

            return true;
        }
    }
}
