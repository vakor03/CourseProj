namespace FindPathProc.Lib
{
    public class Node<T>
    {
        public Node<T> LChild => _lChild;
        public Node<T> RChild => _rChild;
        private Node<T> _mother;
        private Node<T> _lChild;
        private Node<T> _rChild;
        private readonly double _priority;
        
        public T Value { get; }

        public void DeleteNode()
        {
            if (_rChild!=null)
            {
                _mother._lChild = _rChild;
                _mother._lChild._mother = _mother;
            }
            else
            {
                _mother._lChild = null;
            }
            
        }
        public Node(Node<T> mother, T value, double priority)
        {
            _mother = mother;
            _priority = priority;
            Value = value;
        }

        

        
        
        public static void AddElement(Node<T> node, T element, double priority)
        {
            if (priority<= node._priority)
            {
                if (node._lChild==null)
                {
                    node._lChild = new Node<T>(node, element, priority);
                }
                else
                {
                    AddElement(node._lChild, element, priority);
                }

            }else if (priority > node._priority)
            {
                if (node._rChild==null)
                {
                    node._rChild = new Node<T>(node, element, priority);
                }
                else
                {
                    AddElement(node._rChild, element, priority);
                }
            }
        }
        
        
    }
}