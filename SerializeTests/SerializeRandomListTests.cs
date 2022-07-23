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
        public void EqualsTest_CreateTwoRandomLists_CorrectResult()
        {
            var randomList1 = GetRandomLinkedList();
            var randomList2 = GetRandomLinkedList();

            Assert.True(!randomList1.Equals(randomList2));
        }

        [Fact]
        public void EqualsTest_CreateTwoEqualsLists_CorrectResult()
        {
            var randomList1 = CreateList();
            var randomList2 = CreateList();

            Assert.True(randomList1.Equals(randomList2));
        }


        [Fact]
        public void SerializeTest_SerializeOneList_CorrectResult()
        {
            var serializeRandomList = CreateList();
            var expectedResult = CreateList();

            FileStream serializeStream = new(pathToFile, FileMode.OpenOrCreate);
            serializeRandomList.Serialize(serializeStream);

            FileStream deserializeStream = new(pathToFile, FileMode.OpenOrCreate);
            serializeRandomList.Deserialize(deserializeStream);

            File.Delete(pathToFile);

            Assert.True(serializeRandomList.Equals(expectedResult));

        }

        [Fact]
        public void SerializeTest_EmptyList_CorrectResult()
        {
            bool exceptionFlag = false;

            try
            {
                var randomList1 = GetRandomLinkedList(0);
            }
            catch (ArgumentNullException)
            {
                exceptionFlag = true;
            }

            Assert.True(!exceptionFlag);
        
        }

        [Theory]
        [InlineData("----")]
        [InlineData("-jvnfjrg-rvr")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("\n-\n\r")]
        [InlineData("\n")]
        [InlineData("\r\r\r")]

        public void SerializeTest_SpecialData_CorrectResult(string data)
        {
            var serializeRandomList = CreateList();
            var expectedResult = CreateList();

            serializeRandomList.Head!.Data = data;
            expectedResult.Head!.Data = data;

            FileStream serializeStream = new(pathToFile, FileMode.OpenOrCreate);
            serializeRandomList.Serialize(serializeStream);

            FileStream deserializeStream = new(pathToFile, FileMode.OpenOrCreate);
            serializeRandomList.Deserialize(deserializeStream);

            File.Delete(pathToFile);

            Assert.Equal(expectedResult.Head.Data, serializeRandomList.Head.Data);
        }

        private ListRandom CreateList()
        {
            ListRandom list = new ListRandom();

            for(int i = 0; i < 10; i++)
            {
                var newNode = new ListNode();
                AddNextNode(list, newNode);
            }

            var randomReference = new int[] { 0, 4, 1, 6, 6, 2, 9, 1, 4, 5 };
            var currentNode = list.Head;
            var count = 0;

            if (list.Count != null) count = list.Count - 1;

            for(int j = 0; j <= count; j++)
            {
                currentNode.Data = j.ToString();
                currentNode.Random = GetNodeByNumber(list, randomReference[j]);
                currentNode = currentNode.Next;
            }

            return list;
        }

        private ListRandom GetRandomLinkedList(int countOfNodes = -1)
        {
            ListRandom listRandom = new ListRandom();
            var count = countOfNodes;

            if (countOfNodes == -1)
            {             
                count = rnd.Next(0, 1000);
            }

            for (int i = 0; i < count; i++)
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

            var count = 0;

            if (list.Count != null) count = list.Count - 1;

            while(currentNode != null)
            {
                currentNode.Data = GetRandomString();
                currentNode.Random = GetNodeByNumber(list, rnd.Next(0, count));
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