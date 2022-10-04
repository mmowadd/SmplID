using SmplID.Derayah.Models.Gitlab;
using System.Collections.Generic;
using System.Linq;

namespace SmplID.Derayah.Models
{
    internal static class Extensions
    {
        internal static List<Entitlement> ToEntitlementList(this List<Group> groups)
        {
            List<Entitlement> entitlements = new List<Entitlement>();
            foreach (var group in groups)
            {
                entitlements.Add(new Entitlement { ResourceType = "Group", EntitlementID = $"{group.id}-10", Description = group.description, EntitlementName = $"{group.full_name}-Guest" });
                entitlements.Add(new Entitlement { ResourceType = "Group", EntitlementID = $"{group.id}-20", Description = group.description, EntitlementName = $"{group.full_name}-Reporter" });
                entitlements.Add(new Entitlement { ResourceType = "Group", EntitlementID = $"{group.id}-30", Description = group.description, EntitlementName = $"{group.full_name}-Developer" });
                entitlements.Add(new Entitlement { ResourceType = "Group", EntitlementID = $"{group.id}-40", Description = group.description, EntitlementName = $"{group.full_name}-Maintainer" });
            }
            return entitlements;
        }

        internal static List<Entitlement> ToEntitlementList(this List<Project> projects)
        {
            List<Entitlement> entitlements = new List<Entitlement>();
            foreach (var project in projects)
            {
                entitlements.Add(new Entitlement { ResourceType = "Project", EntitlementID = $"{project.id}-10", Description = project.description, EntitlementName = $"{project.name_with_namespace}-Guest" });
                entitlements.Add(new Entitlement { ResourceType = "Project", EntitlementID = $"{project.id}-20", Description = project.description, EntitlementName = $"{project.name_with_namespace}-Reporter" });
                entitlements.Add(new Entitlement { ResourceType = "Project", EntitlementID = $"{project.id}-30", Description = project.description, EntitlementName = $"{project.name_with_namespace}-Developer" });
                entitlements.Add(new Entitlement { ResourceType = "Project", EntitlementID = $"{project.id}-40", Description = project.description, EntitlementName = $"{project.name_with_namespace}-Maintainer" });
            }
            return entitlements;
        }

        internal static User ToUser(this Member member)
        {
            return new User
            {
                UserID = member.id,
                UserName = member.username,
                FirstName = member.name,
                LastName = "",
                DisplayName = member.name,
                EmailID = member.public_email,
                AccountStatus = member.state
            };
        }

        internal static List<UserEntitlement> ToUserEntitlmentList(this List<Member> members)
        {
            return members.Select(x => new UserEntitlement { EntitlementName = x.entitlementId, UserName = x.username }).ToList();
        }
    }

}
