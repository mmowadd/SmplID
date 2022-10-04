using Refit;
using SmplID.Derayah.Models;
using SmplID.Derayah.Models.Gitlab;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmplID.Derayah.Data
{
    public class GitlabData
    {
        private readonly IGitLabData _gitlabData;
        public GitlabData(string baseUrl, string accessToken)
        {
            _gitlabData = RestService.For<IGitLabData>(baseUrl, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(accessToken)
            });
        }
        private async Task<List<Member>> GetMembers(int mainGroupId)
        {
            var groups = await _gitlabData.GetGroups();
            var projects = await _gitlabData.GetProjects(mainGroupId);
            var members = new List<Member>();

            foreach (var group in groups)
            {
                var groupMembers = await _gitlabData.GetGroupMemberss(group.id);
                groupMembers.ForEach(x => x.entitlementId = $"{group.id}-{x.access_level}");
                members.AddRange(groupMembers);
            }
            foreach (var project in projects)
            {
                var projectMemebrs = await _gitlabData.GetProjectMemberss(project.id);
                projectMemebrs.ForEach(x => x.entitlementId = $"{project.id}-{x.access_level}");
                members.AddRange(projectMemebrs);
            }
            return members;
        }
        public async Task GetEntitlments(int mainGroupId, string path)
        {
            var groups = await _gitlabData.GetGroups();
            var projects = await _gitlabData.GetProjects(mainGroupId);
            Common<Entitlement>.CreateCsvFile($@"{path}\entitlements.csv",
                groups.ToEntitlementList().Concat(projects.ToEntitlementList()).ToList());
        }
        public async Task GetUsers(int mainGroupId, string path)
        {
            var members = await GetMembers(mainGroupId);
            var users = new List<User>();

            foreach (var member in members.Distinct(new MembersComparer()).ToList())
            {
                var gitLabUser = await _gitlabData.GetUser(member.id);
                users.Add(gitLabUser.ToUser());
            }
            Common<User>.CreateCsvFile($@"{path}\users.csv", users);
        }
        public async Task GetUsersEntitlements(int mainGroupId, string path)
        {
            var members = await GetMembers(mainGroupId);
            Common<UserEntitlement>.CreateCsvFile($@"{path}\usersinentitlements.csv", members.ToUserEntitlmentList());
        }
        public async Task AssignUserEntitlement(string userId, string entitlementId, string entitlementType)
        {
            var resourceId = entitlementId.Split('-')[0];
            var accessLevel = entitlementId.Split('-')[1];

            if (entitlementType == "Group")
                await _gitlabData.PostGroupMember(resourceId, new PostMember { user_id = userId, access_level = accessLevel });
            else if (entitlementType == "Project")
                await _gitlabData.PostProjectMember(resourceId, new PostMember { user_id = userId, access_level = accessLevel });
        }
        public async Task RemoveUserEntitlement(string userId, string entitlementId, string entitlementType)
        {
            var resourceId = entitlementId.Split('-')[0];

            if (entitlementType == "Group")
                await _gitlabData.DeleteGroupMember(resourceId, userId);
            else if (entitlementType == "Project")
                await _gitlabData.DeleteProjectMember(resourceId, userId);
        }
        public async Task UpdateUserEntitlement(string userId, string entitlementId, string entitlementType)
        {
            var resourceId = entitlementId.Split('-')[0];
            var accessLevel = entitlementId.Split('-')[1];

            if (entitlementType == "Group")
                await _gitlabData.EditGroupMember(userId, resourceId, accessLevel);
            else if (entitlementType == "Project")
                await _gitlabData.EditProjectMember(userId, resourceId, accessLevel);
        }
    }
}
