namespace Ln.SkipList
{
    /// <summary>
    /// the values of list is ascend.
    /// </summary>
    public class SkipList
    {
        private SkipNode head;
        private System.Random randomFa = new System.Random();

        public SkipList() { }
        /// <summary>
        /// Initialize skip list
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            head = new SkipNode(0, null, null);
            return true;
        }

        /// <summary>
        /// Clean skip list
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            head = null;
            return true;
        }

        /// <summary>
        /// Insert value to skip list
        /// </summary>
        /// <param name="value"></param>
        /// <returns>if the value exists in the list already, return false.</returns>
        public bool Insert(int value)
        {
            if (head.nextNode == null)
            {
                SkipNode newNode = new SkipNode(value, null, null);
                head.nextNode = newNode;
                return true;
            }

            //check if value existed in the list. check first because prevent duplicated level while raise level.
            if (Search(value) == true)
            {
                return false;
            }

            SkipNode current = head;
            if (head.nextNode.nextNode != null)
            {
                //If random > 0.7, raise the hight of level                
                double random = randomFa.NextDouble();      //get random between 0.0 and 1.0                
                if (random > 0.7)
                {
                    //Check vaule if exists in highest level
                    current = head.nextNode;
                    while (current != null)
                    {
                        if (value == current.value)
                        {
                            return false;
                        }
                        current = current.nextNode;
                    }

                    current = head;

                    if (head.nextNode.value < value)
                    {
                        SkipNode newNode = new SkipNode(head.nextNode.value, null, null);
                        newNode.downNode = head.nextNode;
                        head.nextNode = newNode;
                        newNode.nextNode = new SkipNode(value, null, null);
                    }
                    else
                    {
                        SkipNode newNode = new SkipNode(value, null, null);
                        newNode.nextNode = new SkipNode(head.nextNode.value, null, null);
                        newNode.nextNode.downNode = head.nextNode;
                        head.nextNode = newNode;
                    }

                    //If value insert to the first note of list
                    if (head.nextNode.value == value)
                    {
                        current = head.nextNode;
                        while (current.nextNode.downNode != null)
                        {
                            SkipNode newNode = new SkipNode(value, null, null);
                            newNode.nextNode = current.nextNode.downNode;
                            current.downNode = newNode;
                            current = current.downNode;
                        }
                        return true;
                    }

                    current = head.nextNode.downNode;
                    SkipNode upNode = head.nextNode.nextNode;
                    do
                    {
                        while (current.nextNode != null && value > current.nextNode.value)
                        {
                            current = current.nextNode;
                        }
                        if (current.nextNode != null && value == current.nextNode.value)
                        {
                            upNode.downNode = current.nextNode;
                            return false;
                        }
                        SkipNode newNode = new SkipNode(value, null, null);
                        upNode.downNode = newNode;
                        upNode = newNode;
                        newNode.nextNode = current.nextNode;
                        current.nextNode = newNode;
                        current = current.downNode;
                    } while (current != null);

                    return true;
                }
            }

            current = head;
            do
            {
                while (current.nextNode != null && value > current.nextNode.value)
                {
                    current = current.nextNode;
                }
                if (current.nextNode != null && value == current.nextNode.value)
                {
                    return false;
                }
                //insert in the lowest level
                if (current.downNode == null)
                {
                    SkipNode newNode = new SkipNode(value, null, null);
                    newNode.nextNode = current.nextNode;
                    current.nextNode = newNode;
                    return true;
                }

                //while gap>3, insert in current and lower level
                if (current.downNode.nextNode != null && current.downNode.nextNode.nextNode != null && value > current.downNode.nextNode.nextNode.value)
                {
                    SkipNode rootNode = new SkipNode(value, null, null);
                    rootNode.nextNode = current.nextNode;
                    current.nextNode = rootNode;
                    current = current.downNode.nextNode.nextNode;
                    while (current != null)
                    {
                        while (current.nextNode != null && value > current.nextNode.value)
                        {
                            current = current.nextNode;
                        }
                        if (current.nextNode != null && value == current.nextNode.value)
                        {
                            return false;
                        }
                        SkipNode newNode = new SkipNode(value, null, null);
                        rootNode.downNode = newNode;
                        rootNode = newNode;
                        newNode.nextNode = current.nextNode;
                        current.nextNode = newNode;
                        current = current.downNode;
                    }
                    return true;
                }
                current = current.downNode;
            } while (current != null);

            return true;
        }

        public bool Search(int value)
        {
            if (head.nextNode == null)
            {
                return false;
            }

            SkipNode current = head;
            do
            {
                while (current.nextNode != null && value > current.nextNode.value)
                {
                    current = current.nextNode;
                }
                if (current.nextNode != null && value == current.nextNode.value)
                {
                    return true;
                }
                current = current.downNode;
            } while (current != null);
            return false;
        }

        public bool Remove(int value)
        {
            if (head.nextNode == null)
            {
                return false;
            }
            SkipNode current = head;
            do
            {
                while (current.nextNode != null && value > current.nextNode.value)
                {
                    current = current.nextNode;
                }
                if (current.nextNode != null && value == current.nextNode.value)
                {
                    current.nextNode = current.nextNode.nextNode;
                    current = current.downNode;
                    continue;
                }
                current = current.downNode;
            }
            while (current != null);
            return true;
        }

        public string Display()
        {
            if (head.nextNode == null)
            {
                return "";
            }

            SkipNode current = head;
            SkipNode level = head.nextNode;
            string rlt = "";
            while (level != null)
            {
                current = level;
                do
                {
                    rlt += "-->" + current.value;
                    current = current.nextNode;
                } while (current != null);
                rlt += System.Environment.NewLine;
                level = level.downNode;
            }
            return rlt;
        }
    }

    class SkipNode
    {
        public int value;
        public SkipNode downNode;
        public SkipNode nextNode;

        public SkipNode()
        {
            value = 0;
            downNode = null;
            nextNode = null;
        }
        public SkipNode(int value, SkipNode downNode, SkipNode nextNode)
        {
            this.value = value;
            this.downNode = downNode;
            this.nextNode = nextNode;
        }
    }
}