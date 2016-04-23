using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;
using DataAccess.Repository;

namespace Eshop.Class
{
    public class EshopRoleProvider : RoleProvider
    {

        UnitOfWork unitOfWork = new UnitOfWork();

        public override bool IsUserInRole(string email, string roleName)
        {
            User user = unitOfWork.RepositoryUser.GetUserByEmail(email);

            return user != null && (user.Roles.Any(r => r.Identificator == roleName) ||
                (roleName == nameof(Role.ROLES.Customer) && user is Customer) ||
                (roleName == nameof(Role.ROLES.Administrator) && user is Administrator)
                );
        }

        public override string[] GetRolesForUser(string email)
        {
            User user = unitOfWork.RepositoryUser.GetUserByEmail(email);

            if (user == null || user.Roles == null)
            {
                return new string[] { };
            }

            var roles = user.Roles.Select(r => r.Identificator).ToList();
            if (user is Customer)
            {
               roles.Add(Role.ROLES.Customer.ToString());
            }
            if (user is Administrator)
            {
               roles.Add(Role.ROLES.Administrator.ToString());
            }

            return roles.ToArray<string>();

        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}