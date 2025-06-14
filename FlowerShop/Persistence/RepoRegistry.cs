using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Persistence
{
    public class RepoRegistry
    {
        /// <summary>
        /// Internal dictionary containing all of our repositories, which is mapped by our string key
        /// </summary>
        private static readonly Dictionary<string, object> _registries = new();

        /// <summary>
        /// Registers a repository with the given name. If the name already exists, it will be overwritten.
        /// </summary>
        /// <param name="name">The unique key to identify the repository.</param>
        /// <param name="repo">The repository instance to register.</param>
        public void Register<T>(string name, IRepo<T> repo)
        {
            _registries[name] = repo!;
        }

        /// <summary>
        /// Retrieves a registered repository by name.
        /// </summary>
        /// <param name="name">The name of the repository to retrieve.</param>
        /// <returns>The registered repository instance, or null if not found.</returns>
        public IRepo<T>? Get<T>(string name)
        {
            return _registries.TryGetValue(name, out var repo) ? repo as IRepo<T> : null;
        }

        /// <summary>
        /// Removes a repository from the registry by name.
        /// </summary>
        /// <param name="name">The name of the repository to remove.</param>
        public void Unregister(string name)
        {
            _registries.Remove(name);
        }
    }
}
