using Refit;
using SmplID.Derayah.Models.Gitlab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmplID.Derayah.Data
{
    [Headers("Authorization: Bearer")]
    internal interface IGitLabData
    {
        [Get(path: "/groups")]
        Task<List<Group>> GetGroups();

        [Get(path: "/groups/{mainGroupId}/projects?include_subgroups=true")]
        Task<List<Project>> GetProjects(int mainGroupId);

        [Get(path: "/projects/{projectId}/members/all")]
        Task<List<Member>> GetProjectMemberss(int projectId);

        [Get(path: "/groups/{groupId}/members/all")]
        Task<List<Member>> GetGroupMemberss(int groupId);

        [Get(path: "/users/{userId}")]
        Task<Member> GetUser(int userId);

        [Post(path: "/groups/{groupId}/members")]
        Task PostGroupMember(string groupId, [Body] PostMember member);

        [Post(path: "/projects/{projectId}/members")]
        Task PostProjectMember(string projectId, [Body] PostMember member);

        [Put(path: "/groups/{groupId}/members/{userId}?access_level={accessLevel}")]
        Task EditGroupMember(string userId, string groupId, string accessLevel);

        [Put(path: "/projects/{projectId}/members/{userId}?access_level={accessLevel}")]
        Task EditProjectMember(string userId, string projectId, string accessLevel);

        [Delete(path: "/groups/{groupId}/members/{memberId}")]
        Task DeleteGroupMember(string groupId, string memberId);

        [Delete(path: "/projects/{projectId}/members/{memberId}")]
        Task DeleteProjectMember(string projectId, string memberId);
    }
}
