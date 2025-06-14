using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Persistence
{
    public interface IRepo<T>
    {
        /// <summary>
        /// Generic method for a repository to fetch a certain item based on an items identifier
        /// </summary>
        /// <param name="id">the identifier, in this case an id integer. If your object does not contain an id field, make an overloading method with the fitting parameter type then.</param>
        /// <returns>The first instance that matches the given parameter.</returns>
        T? GetById(int id);

        /// <summary>
        /// Generic method that fetches every single item within a repository.
        /// </summary>
        /// <returns>An enumarable collection of all items.</returns>
        List<T> GetAll();

        /// <summary>
        /// Adds a new item to the repository 
        /// </summary>
        /// <param name="item">The given item to be added to the repository</param>
        void Add(T item);

        /// <summary>
        /// Removes a given item from the repository
        /// </summary>
        /// <param name="item">The given item to remove.</param>
        void Remove(T item);

        /// <summary>
        /// Updates a certain item in the repository
        /// </summary>
        /// <param name="item">The Item to update</param>
        void Update(T item);
    }
}
