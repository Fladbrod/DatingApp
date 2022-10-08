namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<string>> OnlineUsers = new Dictionary<string, List<string>>();

        /// <summary>
        /// Adds user to dict when they connect along with their <see cref="connectionId"/> 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="connectionId">User's SignalR connection id.</param>
        /// <returns>Represents an async operation. The operation returns a successfully completed task of online users.</returns>
        public Task<bool> UserConnected(string username, string connectionId)
        {
            bool isOnline = false;
            // Locks the dictionary until we're finished doing what we're doing
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(username))
                {
                    // Username is the key, and we're adding to a list of new connectionid
                    OnlineUsers[username].Add(connectionId);
                }
                else 
                {
                    OnlineUsers.Add(username, new List<string>{connectionId});
                    isOnline = true;
                }
            }

            return Task.FromResult(isOnline);
        }

        /// <summary>
        /// Checks to see if user is disconnected.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="connectionId">User's SignalR connection id.</param>
        /// <returns>Represents an async operation. The operation returns a successfully completed task.</returns>
        public Task<bool> UserDisconnected(string username, string connectionId)
        {
            bool isOffline = false;
            lock (OnlineUsers)
            {
                // Checks to see if we have a dictionary element with the key of the currently logged in username
                if (!OnlineUsers.ContainsKey(username)) return Task.FromResult(isOffline);

                OnlineUsers[username].Remove(connectionId);
                if (OnlineUsers[username].Count == 0)
                {
                    OnlineUsers.Remove(username);
                    isOffline = true;
                }
            }

            return Task.FromResult(isOffline);
        }

        /// <summary>
        /// Gets all users that are currently connected.
        /// </summary>
        /// <returns>Represents an async operation. The operation returns a successfully completed task of online users.></returns>
        public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(onlineUsers);
        }

        /// <summary>
        /// To handle if a user has multiple connections on various devices
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <returns>An async operation containing a list of connectionIds form same user</returns>
        public Task<List<string>> GetConnectionsForUser(string username)
        {
            List<string> connectionIds;
            lock (OnlineUsers)
            {
                connectionIds = OnlineUsers.GetValueOrDefault(username);
            }

            return Task.FromResult(connectionIds);
        }
    }
}