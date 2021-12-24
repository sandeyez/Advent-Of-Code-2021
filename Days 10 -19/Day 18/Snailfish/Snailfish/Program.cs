using System;
using System.Collections.Generic;

namespace Snailfish
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sande\Desktop\Projects\AoC2021\Days 10 -19\Day 18\Snailfish\input.txt");

            Node sumRoot;
            List<Node> numbers = new List<Node>();
            foreach (string line in lines)
                numbers.Add(parseString(line));

            int maxMagnitude = 0;

            for(int i = 0; i < numbers.Count; i++)
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (i != j)
                    {
                        sumRoot = new Node();

                        sumRoot.appendLeft(copyNode(numbers[i]));
                        sumRoot.appendRight(copyNode(numbers[j]));

                        updateDepths(sumRoot, 0);
                        reduceSnailnumber(sumRoot);

                        if (Magnitude(sumRoot) > maxMagnitude)
                            maxMagnitude = Magnitude(sumRoot);
                    }
                }
            Console.WriteLine(maxMagnitude);
        }

        static Node parseString(string line)
        {
            Node root = new Node();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '[':
                        Node leftChild = new Node();
                        stack.Peek().appendLeft(leftChild);
                        stack.Push(leftChild);
                        break;
                    case ']':
                        stack.Pop();
                        break;
                    case ',':
                        stack.Pop();
                        Node rightChild = new Node();
                        stack.Peek().appendRight(rightChild);
                        stack.Push(rightChild);
                        break;
                    default:
                        stack.Peek().value = int.Parse(line[i].ToString());
                        break;
                }
            }
            return root;
        }

        public static void updateDepths(Node node, int depth)
        {
            node.depth = depth;

            if (node.Left != null)
                updateDepths(node.Left, depth + 1);
            if (node.Right != null)
                updateDepths(node.Right, depth + 1);
        }

        static void reduceSnailnumber(Node root)
        {
            bool actionHappened = true;

            while (actionHappened)
            {
                actionHappened = false;

                // Check for explosions
                Node explosionNode = findDepthFour(root);

                if (explosionNode != null)
                {
                    // Node explodes
                    explode(explosionNode);
                    actionHappened = true;
                    continue;
                }

                // find node that needs to be split
                Node splitNode = findSplitNode(root);

                if (splitNode != null)
                {
                    // Node is split
                    actionHappened = true;
                    split(splitNode);
                    continue;
                }
            }
        }
        static Node findDepthFour(Node root)
        {
            Stack<Node> stack = new Stack<Node>();

            stack.Push(root);

            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                if (node.depth == 5 && node.value != null)
                    return node.Father;
                else
                {
                    if (node.Right != null)
                        stack.Push(node.Right);
                    if (node.Left != null)
                        stack.Push(node.Left);
                }
            }
            return null;
        }

        static void explode(Node node)
        {
            int left = (int)node.Left.value;
            int right = (int)node.Right.value;

            Node father = node.Father;

            // Carry the right number to the nearest number to the right and the left to the left
            Node rightNeighbour = findRightNeighbour(node);
            Node leftNeighbour = findLeftNeighbour(node);

            if (rightNeighbour != null)
                rightNeighbour.value += right;
            if (leftNeighbour != null)
                leftNeighbour.value += left;

            if (father.Left == node) // If we are the left kid of our father
                father.appendLeft(new Node(0));
            else // if we are the right kid of our father
                father.appendRight(new Node(0));
        }

        static Node findRightNeighbour(Node node)
        {
            // We want to find the leftmost child of the right node when we're left for the first time
            Node father = node;

            while (father.Father != null)
            {
                Node newFather = father.Father;

                // If you are the left node of newFather
                if (newFather.Left == father)
                {
                    newFather = newFather.Right;
                    while (newFather.Left != null)
                    {
                        newFather = newFather.Left;
                    }
                    return newFather;
                }
                else
                {
                    father = newFather;
                }
            }

            return null;
        }
        static Node findLeftNeighbour(Node node)
        {
            // We want to find the rightmost child of the left node when we're right for the first time
            Node father = node;

            while (father.Father != null)
            {
                Node newFather = father.Father;

                // If you are the left node of newFather
                if (newFather.Right == father)
                {
                    newFather = newFather.Left;
                    while (newFather.Right != null)
                    {
                        newFather = newFather.Right;
                    }
                    return newFather;
                }
                else
                {
                    father = newFather;
                }
            }

            return null;
        }
        static Node findSplitNode(Node root)
        {
            Stack<Node> stack = new Stack<Node>();

            stack.Push(root);

            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                if (node.Left == null && node.Right == null)
                {
                    if (node.value >= 10)
                        return node;
                }
                else
                {
                    stack.Push(node.Right);
                    stack.Push(node.Left);
                }
            }
            return null;
        }

        static void split(Node splitNode)
        {
            double value = (double)splitNode.value / 2;
            splitNode.value = null;

            splitNode.appendLeft(new Node((int)Math.Floor(value)));
            splitNode.appendRight(new Node((int)Math.Ceiling(value)));
        }

        static string printTree(Node node)
        {
            string result;

            if (node.value == null)
            {
                result = "[" + printTree(node.Left) + "," + printTree(node.Right) + "]";
            }
            else
                result = node.value.ToString();
            return result;
        }

        static int Magnitude(Node node)
        {
            int result = 0;

            if (node.value == null)
                result = 3 * Magnitude(node.Left) + 2 * Magnitude(node.Right);
            else
                result = (int)node.value;

            return result;
        }

        // Copy a node to be able to pass it without reference
        static Node copyNode(Node node)
        {
            string s = printTree(node);
            return (parseString(s));
        }
    }

    class Node
    {
        public int depth;
        public int? value = null;

        public Node Left, Right, Father;

        public Node(int value)
        {
            this.value = value;
        }

        public Node()
        {}

        public void appendLeft(Node node)
        {
            Program.updateDepths(node, this.depth + 1);
            Left = addFather(node);
        }
        public void appendRight(Node node)
        {
            Program.updateDepths(node, this.depth + 1);            
            Right = addFather(node);
        }

        public Node addFather(Node node)
        {
            node.Father = this;

            return node;
        }
    }
}
