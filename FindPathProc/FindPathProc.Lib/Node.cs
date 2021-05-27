namespace FindPathProc.Lib
{
    public class Node<T>
    {
        
        public Node<T> LChild { get; private set; }
        public Node<T> RChild { get; private set; }
        private readonly double _priority;
        private Node<T> _mother;

        public T Value { get; }

        public Node(Node<T> mother, T value, double priority)
        {
            _mother = mother;
            _priority = priority;
            Value = value;
        }

        public void DeleteNode()
        {
            if (RChild != null)
            {
                _mother.LChild = RChild;
                _mother.LChild._mother = _mother;
            }
            else
            {
                _mother.LChild = null;
            }
        }


        public static void AddNode(Node<T> parent, T element, double priority)
        {
            if (priority <= parent._priority)
            {
                if (parent.LChild == null)
                {
                    parent.LChild = new Node<T>(parent, element, priority);
                }
                else
                {
                    AddNode(parent.LChild, element, priority);
                }
            }
            else if (priority > parent._priority)
            {
                if (parent.RChild == null)
                {
                    parent.RChild = new Node<T>(parent, element, priority);
                }
                else
                {
                    AddNode(parent.RChild, element, priority);
                }
            }
        }
    }
}