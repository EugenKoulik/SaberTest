﻿
namespace SaberTest
{
    public class ListRandom
    {
        public ListNode? Head;
        public ListNode? Tail;
        public int? Count;

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
            var listOfNodes = DeserializeHelper.CreateNodesData(s);
            var list = DeserializeHelper.CreateListRandom(listOfNodes);

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
                if (!DataIsEquals(firstCurrentNode!, secondCurrentNode!)) return false;

                firstCurrentNode = firstCurrentNode.Next;
                secondCurrentNode = secondCurrentNode.Next;
            }

            return true;
        }

        private bool DataIsEquals(ListNode firstListNode, ListNode secondListNode)
        {
            return  (firstListNode.Data == secondListNode.Data) && 
                    (firstListNode.Previous == secondListNode.Previous) && 
                    (firstListNode.Next == secondListNode.Next) && 
                    (firstListNode.Random == secondListNode.Random);
        }
    }
}
