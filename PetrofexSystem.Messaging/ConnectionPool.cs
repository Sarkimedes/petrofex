using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace PetrofexSystem.Messaging
{
    class ConnectionPool
    {
        private readonly ICollection<Connection> _connections;

        public ConnectionPool()
        {
            this._connections = new List<Connection>();
        }

        public Connection GetConnection(string clientId, TcpClient client)
        {
            var connection = this._connections.FirstOrDefault(c => c.ClientId == clientId);
            if (connection == null)
            {
                var newConnection = new Connection(client, clientId);
                this._connections.Add(newConnection);
                return newConnection;
            }
            return connection;
        } 

        public Connection GetConnection(TcpClient client)
        {
            return this._connections.FirstOrDefault(c => c.Endpoint.Equals(client.Client.LocalEndPoint));
        }

        public Connection GetConnection(string clientId)
        {
            return this._connections.FirstOrDefault(c => c.ClientId == clientId);
        }
    }
}
