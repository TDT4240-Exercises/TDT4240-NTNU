using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercise2
{
    class GameObject
    {
        private Vector2 position;
        private LinkedList<PositionListener> listeners;

        public GameObject()
        {
            position = new Vector2();
            listeners = new LinkedList<PositionListener>();
        }

        public void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;

            //Notify each listener that our position has changed
            foreach (PositionListener listener in listeners) listener.notifyPositionChanged(this);
        }

        public float getX()
        {
            return position.X;
        }

        public float getY()
        {
            return position.Y;
        }

        public void addListener(PositionListener listener)
        {
            listeners.AddLast(listener);
        }

        public void removeListener(PositionListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
