namespace Advent2024.Day5;

public class Puzzle
{
    public int GetSumOfMiddleNumbers(string pageOrderingRules, string updates)
    {
        var nodes = GetNodes(pageOrderingRules);
        var sum = 0;

        foreach (var update in updates.Split(Environment.NewLine))
        {
            var pages = update.Split(',').Select(int.Parse).ToArray();
            
            if (!nodes.TryGetValue(pages[0], out var node))
            {
                continue;
            }

            var correctOrder = true;
            for (var i = 0; i < pages.Length - 1; i++)
            {
                var nextPage = pages[i + 1];

                var nextNode = node.Children.FirstOrDefault(n => n.Value == nextPage);
                if (nextNode != null)
                {
                    node = nextNode;
                }
                else
                {
                    correctOrder = false;
                    break;
                }
            }

            if (correctOrder)
            {
                var midPoint = pages.Length / 2;
                var mid = pages[midPoint];

                sum += mid;
            }
        }

        return sum;
    }

    private static Dictionary<int, Node> GetNodes(string pageOrderingRules)
    {
        var rules = pageOrderingRules.Split(Environment.NewLine);
        var nodes = new Dictionary<int, Node>();
        foreach (var rule in rules)
        {
            var options = rule.Split('|');
            var left = int.Parse(options[0]);
            var right = int.Parse(options[1]);

            if (nodes.TryGetValue(left, out var node))
            {
                if (nodes.TryGetValue(right, out var linkedNode))
                {
                    node.Children.Add(linkedNode);
                }
                else
                {
                    var newNode = new Node(right);
                    nodes.Add(right, newNode);
                    node.Children.Add(newNode);
                }
            }
            else
            {
                if (nodes.TryGetValue(right, out var linkedNode))
                {
                    var newNode = new Node(left);
                    nodes.Add(left, newNode);
                    newNode.Children.Add(linkedNode);
                }
                else
                {
                    var leftNode = new Node(left);
                    var rightNode = new Node(right);
                    nodes.Add(left, leftNode);
                    nodes.Add(right, rightNode);
                    leftNode.Children.Add(rightNode);
                }
            }
        }

        return nodes;
    }

    public class Node
    {
        public int Value { get; }

        public List<Node> Children { get; } = [];

        public Node(int value)
        {
            Value = value;
        }
    }
}
