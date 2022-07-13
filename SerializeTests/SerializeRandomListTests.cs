using Xunit;
using SaberTest;
using System;
using System.IO;

namespace SerializeTests
{
    public class SerializeRandomListTests
    {
        Random rnd = new Random();
        string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        string pathToFile = "TestAppFile.txt";

        [Fact]
        public void Test1()
        {
            var randomList1 = GetRandomLinkedList();
            var randomList2 = GetRandomLinkedList();

            Assert.True(!randomList1.Equals(randomList2));

        }

        [Fact]
        public void Test2()
        {
            var randomList1 = GetRandomLinkedList();
            var randomList2 = randomList1;

            Assert.True(randomList1.Equals(randomList2));
        }

        [Fact]
        public void Test3()
        {
            var serializeRandomList = GetRandomLinkedList();
            var expectedResult = serializeRandomList;

            FileStream serializeStream = new(pathToFile, FileMode.OpenOrCreate);
            serializeRandomList.Serialize(serializeStream);

            FileStream deserializeStream = new(pathToFile, FileMode.OpenOrCreate);
            serializeRandomList.Deserialize(deserializeStream);

            File.Delete(pathToFile);

            Assert.True(serializeRandomList.Equals(expectedResult));

        }


        private ListRandom GetRandomLinkedList()
        {
            ListRandom listRandom = new ListRandom();
            var countOfNodes = rnd.Next(0, 1000);

            for(int i = 0; i < countOfNodes; i++)
            {
                var newNode = new ListNode();
                AddNextNode(listRandom, newNode);             
            }

            FillListNodesRandomData(listRandom);
            return listRandom;
        }

        private void AddNextNode(ListRandom list, ListNode node)
        {
            var currentNode = list.Head;

            if(currentNode != null)
            {
                while (currentNode != list.Tail)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = node;
                node.Previous = currentNode;
                list.Tail = node;
                list.Count++;
            }
            else
            {
                list.Head = node;
                list.Tail = node;
                list.Count++;
            }
        }

        private void FillListNodesRandomData(ListRandom list)
        {
            var currentNode = list.Head;

            while(currentNode != null)
            {
                currentNode.Data = GetRandomString();
                currentNode.Random = GetNodeByNumber(list, rnd.Next(0, list.Count - 1));
                currentNode = currentNode.Next;
            }
        }

        private ListNode GetNodeByNumber(ListRandom list, int number)
        {
            var currentNode = list.Head;

            for(int i = 0; i < number; i++)
            {
                currentNode = currentNode.Next;
            }

            return currentNode;
        }

        private string GetRandomString()
        {
            return chars[rnd.Next(0, chars.Length)].ToString();
        }
    }
}