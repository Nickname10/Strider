using System.Collections;

namespace GameServerServices.SessionsServices.Models
{
    public class ClientTable
    {
        private static ClientTable _instance;
        private static readonly object SyncRoot = new object();
        private readonly Hashtable _loginSessionUsers; // key - session token // value login session client

        public static ClientTable Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClientTable();
                            return _instance;
                        }
                    }
                }

                return _instance;
            }
        }

        public void AddLoginSessionClient(LoginSessionClient client)
        {
            _loginSessionUsers.Add(client.SessionToken, client);
        }

        public void RemoveLoginSessionClient(string sessionToken)
        {
            _loginSessionUsers.Remove(sessionToken);
        }
        public LoginSessionClient SearchClient(string sessionToken)
        {
            return _loginSessionUsers[sessionToken] as LoginSessionClient;
        }
        
        private ClientTable()
        {
            _loginSessionUsers = new Hashtable();
        }

        
    }
}