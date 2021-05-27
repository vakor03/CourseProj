namespace FindPathProc.Lib
{
    public class PriorityQueue<T>
    {
        private Node<T> _root;

        public void AddElement(T element, double priority)
        {
            if (_root == null)
            {
                _root = new Node<T>(null, element, priority);
            }
            else
            {
                Node<T>.AddNode(_root, element, priority);
            }
        }

        public bool IsEmpty()
        {
            if (_root == null)
                return true;
            return false;
        }


        public T Dequeue()
        {
            Node<T> targetNode = Dequeue(_root);
            T value = targetNode.Value;
            if (targetNode == _root)
            {
                _root = _root.RChild;
            }
            else
            {
                targetNode.DeleteNode();
            }

            return value;
        }

        private Node<T> Dequeue(Node<T> currentNode)
        {
            if (currentNode.LChild != null)
            {
                return Dequeue(currentNode.LChild);
            }

            return currentNode;
        }
    }
}