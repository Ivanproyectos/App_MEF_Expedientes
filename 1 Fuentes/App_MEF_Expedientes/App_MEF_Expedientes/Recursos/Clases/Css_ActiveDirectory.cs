using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Configuration; 

namespace App_MEF_Expedientes.Recursos.Clases
{
    public class Css_ActiveDirectory
    {

        private readonly string sServer;
        private readonly string sDefaultOU;
        private readonly string sServiceUser;
        private readonly string sServicePassword;
        private readonly PrincipalContext pc;
        public Css_ActiveDirectory()
        {
            sServer = ConfigurationManager.AppSettings["ServerAD"].ToString();
            sDefaultOU = ConfigurationManager.AppSettings["DefaultOU"].ToString();
            sServiceUser = ConfigurationManager.AppSettings["UserAD"].ToString();
            sServicePassword = ConfigurationManager.AppSettings["PasswordAD"].ToString();
            pc = new PrincipalContext(ContextType.Domain, sServer, sDefaultOU, ContextOptions.SimpleBind, sServiceUser, sServicePassword);
        }

        public bool ValidateCredentials(string username, string password)
        {
            return pc.ValidateCredentials(username, password, ContextOptions.Negotiate);
        }

        public UserPrincipal FindByIdentity(string username)
        {
            return UserPrincipal.FindByIdentity(pc, username);
        }

        public List<string> GetGroupNames()
        {
            List<string> groups = new List<string>();

            GroupPrincipal principalGroup = new GroupPrincipal(pc);
            using (PrincipalSearcher search = new PrincipalSearcher(principalGroup))
            {
                search.FindAll().ToList().ForEach(f =>
                groups.Add(f.Name));
            }
            return groups;
        }

        public List<UserModel> GetUsersByDomain(string groupName)
        {
            List<UserModel> users = new List<UserModel>();

            using (var group = GroupPrincipal.FindByIdentity(pc, groupName))
            {
                try
                {
                    if (group != null)
                    {
                        foreach (UserPrincipal user in group.GetMembers(true))
                        {
                            string office = string.Empty;
                            DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);
                            if (de != null)
                            {
                                if (de.Properties.Contains("physicalDeliveryOfficeName"))
                                    office = de.Properties["physicalDeliveryOfficeName"][0].ToString();
                            }

                            if (!string.IsNullOrWhiteSpace(office))
                            {
                                users.Add(new UserModel
                                {
                                    Id = user.Guid.ToString(),
                                    Name = user.Name,
                                    Department = office,
                                    Position = user.Description,
                                    SamAccountName = user.SamAccountName
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

            return users;
        }

        public UserModel FindUser(string username, string groupName)
        {
            UserModel found = null;

            using (var group = GroupPrincipal.FindByIdentity(pc, groupName))
            {
                try
                {
                    if (group != null)
                    {
                        foreach (UserPrincipal user in group.GetMembers(true))
                        {
                            string office = string.Empty;
                            DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);
                            if (de != null)
                            {
                                if (de.Properties.Contains("physicalDeliveryOfficeName"))
                                    office = de.Properties["physicalDeliveryOfficeName"][0].ToString();
                            }

                            if (!string.IsNullOrWhiteSpace(office) && user.SamAccountName.Equals(username))
                            {
                                found = new UserModel
                                {
                                    Id = user.Guid.ToString(),
                                    Name = user.Name,
                                    Department = office,
                                    Position = user.Description,
                                    SamAccountName = user.SamAccountName
                                };
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex) { throw (ex); }
            }

            return found;
        }

        public UserModel FindByUserName(string username)
        {
            UserModel user = null;
            var userPrincipal = UserPrincipal.FindByIdentity(pc, username);

            if (userPrincipal != null)
            {
                string office = string.Empty;
                DirectoryEntry de = (userPrincipal.GetUnderlyingObject() as DirectoryEntry);
                if (de != null)
                    if (de.Properties.Contains("physicalDeliveryOfficeName"))
                        office = de.Properties["physicalDeliveryOfficeName"][0].ToString();


                user = new UserModel()
                {
                    Id = userPrincipal.Guid.ToString(),
                    Name = userPrincipal.Name,
                    Position = userPrincipal.Description,
                    Department = office,
                    SamAccountName = userPrincipal.SamAccountName,
                    EmailAddress = userPrincipal.EmailAddress
                };
            }

            return user;
        }

        public string GetOffice(UserPrincipal userPrincipal)
        {
            string office = string.Empty;
            if (userPrincipal != null)
            {
                DirectoryEntry de = (userPrincipal.GetUnderlyingObject() as DirectoryEntry);
                if (de != null)
                    if (de.Properties.Contains("physicalDeliveryOfficeName"))
                        office = de.Properties["physicalDeliveryOfficeName"][0].ToString();
            }

            return office;
        }

        public void Dispose()
        {
            if (pc != null)
            {
                pc.Dispose();
            }
        }


    }
}