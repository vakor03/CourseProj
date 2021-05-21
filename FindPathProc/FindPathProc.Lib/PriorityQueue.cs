namespace FindPathProc.Lib
{
    public class PriorityQueue<T>
    {
        private Node<T> _root;

        public PriorityQueue()
        {
        }

        public PriorityQueue(T element, double priority)
        {
            _root = new Node<T>(null, element, priority);
        }

        public void AddElement(T element, double priority)
        {
            if (_root == null)
            {
                _root = new Node<T>(null, element, priority);
            }
            else
            {
                Node<T>.AddElement(_root, element, priority);
            }
        }

        public bool IsEmpty()
        {
            if (_root == null)
                return true;
            return false;
        }


        private Node<T> Dequeue(Node<T> currentNode)
        {
            if (currentNode.LChild != null)
            {
                return Dequeue(currentNode.LChild);
            }

            return currentNode;
        }

        public T Dequeue()
        {
            Node<T> targetNode = Dequeue(_root);
            T value = targetNode.Value;
            if (targetNode == _root)
            {
                if (_root.RChild != null)
                {
                    _root = _root.RChild;
                }
                else
                {
                    _root = null;
                }
                
            }
            else
            {
                targetNode.DeleteNode();
            }

            return value;
        }
    }
}