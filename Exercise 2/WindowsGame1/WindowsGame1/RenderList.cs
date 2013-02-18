using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercise2
{
    class RenderList : PositionListener
    {
        private LinkedList<GameObject> gameObjects;

        public RenderList()
        {
            gameObjects = new LinkedList<GameObject>();
        }

        public void add(GameObject newObject)
        {

            //Find out where to put this node
            for (LinkedListNode<GameObject> node = gameObjects.First; node != null; node = node.Next)
            {
                if (node.Value.getY() > newObject.getY())
                {
                    gameObjects.AddBefore(node, newObject);
                    return;
                }
            }

            gameObjects.AddLast(newObject);
        }

        public void PositionListener.notifyPositionChanged(GameObject gameObject)
        {
            bool hasBeenInserted = false;

            //Update position in list
            for (LinkedListNode<GameObject> node = gameObjects.First; node != null; node = node.Next)
            {
                if (gameObject == node.Value)
                {
                    //We are still in the correct position
                    if (node.Next == null) return;

                    //Remove our old position
                    gameObjects.Remove(node);
                    if (hasBeenInserted) return;
                    else continue;
                }

                if (!hasBeenInserted && node.Value.getY() > gameObject.getY())
                {
                    gameObjects.AddBefore(node, gameObject);
                    hasBeenInserted = true;
                }
            }

            if (!hasBeenInserted) gameObjects.AddLast(gameObject);
        }
    }
}
