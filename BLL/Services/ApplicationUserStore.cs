//using DAL;
//using DAL.Contexts;
//using DAL.Entities;
//using IdentityServer4.EntityFramework.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Text;

//namespace BLL.Services
//{
//    public class ApplicationUserStore : UserStore<User, UserRole, SocialNetworkDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
//    {
//        protected override RoleClaim CreateRoleClaim(UserRole role, Claim claim)
//        {
//            return new RoleClaim
//            {
//                RoleId = role.Id,
//                ClaimType = claim.Type,
//                ClaimValue = claim.Value
//            };
//        }

//        protected override UserClaim CreateUserClaim(User user, Claim claim)
//        {
//            var userClaim = new UserClaim { UserId = user.Id };
//            userClaim.InitializeFromClaim(claim);
//            return userClaim;
//        }

//        protected override UserLogin CreateUserLogin(User user, UserLoginInfo login)
//        {
//            return new UserLogin
//            {
//                UserId = user.Id,
//                ProviderKey = login.ProviderKey,
//                LoginProvider = login.LoginProvider,
//                ProviderDisplayName = login.ProviderDisplayName
//            };
//        }

//        protected override UserRole CreateUserRole(User user, Role role)
//        {
//            return new UserRole
//            {
//                UserId = user.Id,
//                RoleId = role.Id
//            };
//        }

//        protected override UserToken CreateUserToken(User user, string loginProvider, string name, string value)
//        {
//            return new UserToken
//            {
//                UserId = user.Id,
//                LoginProvider = loginProvider,
//                Name = name,
//                Value = value
//            };
//        }
//    }
//}
