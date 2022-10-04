Imports Refit
Imports SmplID.Derayah.Models
Imports SmplID.Derayah.Models.Gitlab
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks

Namespace SmplID.Derayah.Data
    Public Class GitlabData
        Private ReadOnly _gitlabData As Data.IGitLabData

        Public Sub New(ByVal accessToken As String, ByVal baseUrl As String)
            _gitlabData = RestService.[For](Of SmplID.Derayah.Data.IGitLabData)(baseUrl, New RefitSettings() With {
                .AuthorizationHeaderValueGetter = Function() Task.FromResult(accessToken)
            })
        End Sub

        Private Async Function GetMembers(ByVal mainGroupId As Integer) As Task(Of List(Of Derayah.Models.Gitlab.Member))
            Dim groups = Await _gitlabData.GetGroups()
            Dim projects = Await _gitlabData.GetProjects(mainGroupId)
            Dim members = New List(Of SmplID.Derayah.Models.Gitlab.Member)()

            For Each group In groups
                Dim groupMembers = Await _gitlabData.GetGroupMemberss(group.id)
                groupMembers.ForEach(Sub(x) x.entitlementId = $"{group.id}-{x.access_level}")
                members.AddRange(groupMembers)
            Next

            For Each project In projects
                Dim projectMemebrs = Await _gitlabData.GetProjectMemberss(project.id)
                projectMemebrs.ForEach(Sub(x) x.entitlementId = $"{project.id}-{x.access_level}")
                members.AddRange(projectMemebrs)
            Next

            Return members
        End Function

        Public Async Function GetEntitlments(ByVal mainGroupId As Integer, ByVal path As String) As Task
            Dim groups = Await _gitlabData.GetGroups()
            Dim projects = Await _gitlabData.GetProjects(mainGroupId)
            Call Derayah.Common(Of Derayah.Models.Entitlement).CreateCsvFile($"{path}\entitlements.csv", groups.ToEntitlementList().Concat(projects.ToEntitlementList()).ToList())
        End Function

        Public Async Function GetUsers(ByVal mainGroupId As Integer, ByVal path As String) As Task
            Dim members = Await GetMembers(mainGroupId)
            Dim users = New List(Of SmplID.Derayah.Models.Gitlab.User)()

            For Each member In members.Distinct(New Derayah.Models.Gitlab.MembersComparer()).ToList()
                Dim gitLabUser = Await _gitlabData.GetUser(member.id)
                users.Add(gitLabUser.ToUser())
            Next

            Derayah.Common(Of Derayah.Models.Gitlab.User).CreateCsvFile($"{path}\users.csv", users)
        End Function

        Public Async Function GetUsersEntitlements(ByVal mainGroupId As Integer, ByVal path As String) As Task
            Dim members = Await GetMembers(mainGroupId)
            Call Derayah.Common(Of Derayah.Models.UserEntitlement).CreateCsvFile($"{path}\usersinentitlements.csv", members.ToUserEntitlmentList())
        End Function

        Public Async Function AssignUserEntitlement(ByVal userId As String, ByVal entitlementId As String, ByVal entitlementType As String) As Task
            Dim resourceId = entitlementId.Split("-"c)(0)
            Dim accessLevel = entitlementId.Split("-"c)(1)

            If Equals(entitlementType, "Group") Then
                Await _gitlabData.PostGroupMember(resourceId, New Derayah.Models.Gitlab.PostMember With {
                    .user_id = userId,
                    .access_level = accessLevel
                })
            ElseIf Equals(entitlementType, "Project") Then
                Await _gitlabData.PostProjectMember(resourceId, New Derayah.Models.Gitlab.PostMember With {
                    .user_id = userId,
                    .access_level = accessLevel
                })
            End If
        End Function

        Public Async Function RemoveUserEntitlement(ByVal userId As String, ByVal entitlementId As String, ByVal entitlementType As String) As Task
            Dim resourceId = entitlementId.Split("-"c)(0)

            If Equals(entitlementType, "Group") Then
                Await _gitlabData.DeleteGroupMember(resourceId, userId)
            ElseIf Equals(entitlementType, "Project") Then
                Await _gitlabData.DeleteProjectMember(resourceId, userId)
            End If
        End Function

        Public Async Function UpdateUserEntitlement(ByVal userId As String, ByVal entitlementId As String, ByVal entitlementType As String) As Task
            Dim resourceId = entitlementId.Split("-"c)(0)
            Dim accessLevel = entitlementId.Split("-"c)(1)

            If Equals(entitlementType, "Group") Then
                Await _gitlabData.EditGroupMember(userId, resourceId, accessLevel)
            ElseIf Equals(entitlementType, "Project") Then
                Await _gitlabData.EditProjectMember(userId, resourceId, accessLevel)
            End If
        End Function
    End Class
End Namespace
