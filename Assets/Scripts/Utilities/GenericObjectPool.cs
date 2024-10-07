using System;
using System.Collections.Generic;

namespace CosmicCuration.Utilities
{
    public class GenericObjectPool<T> where T : class
    {
        private List<PooledItem<T>> poolItems = new List<PooledItem<T>>();

        protected T GetItem()
        {
            if (poolItems.Count > 0)
            {
                PooledItem<T> item = poolItems.Find(item => !item.isUsed);
                if (item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }

            return CreateNewPooledItem();
        }

        private T CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();
            newItem.Item = CreateItem();
            newItem.isUsed = true;
            poolItems.Add(newItem);
            return newItem.Item;
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("Child class don't have to implementation CreateItem");
        }

        public class PooledItem<T>
        {
            public T Item;
            public bool isUsed;
        }
    }
}