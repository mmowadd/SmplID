Imports SmplID.Derayah.Models.Gitlab
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices

Namespace SmplID.Derayah.Models
    Friend Module Extensions
        <Extension()>
        Friend Function ToEntitlementList(ByVal groups As List(Of Models.Gitlab.Group)) As List(Of Models.Entitlement)
            Dim entitlements As List(Of SmplID.Derayah.Models.Entitlement) = New List(Of SmplID.Derayah.Models.Entitlement)()

            For Each group In groups
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-10",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Guest"
                })
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-20",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Reporter"
                })
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-30",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Developer"
                })
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-40",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Maintainer"
                })
            Next

            Return entitlements
        End Function

        <Extension()>
        Friend Function ToEntitlementList(ByVal projects As List(Of Models.Gitlab.Project)) As List(Of Models.Entitlement)
            Dim entitlements As List(Of SmplID.Derayah.Models.Entitlement) = New List(Of SmplID.Derayah.Models.Entitlement)()

            For Each project In projects
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-10",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Guest"
                })
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-20",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Reporter"
                })
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-30",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Developer"
                })
                entitlements.Add(New Models.Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-40",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Maintainer"
                })
            Next

            Return entitlements
        End Function

        <Extension()>
        Friend Function ToUser(ByVal member As Models.Gitlab.Member) As Models.Gitlab.User
            Return New Models.Gitlab.User With {
                .UserID = member.id,
                .UserName = member.username,
                .FirstName = member.name,
                .LastName = "",
                .DisplayName = member.name,
                .EmailID = member.public_email,
                .AccountStatus = member.state
            }
        End Function

        <Extension()>
        Friend Function ToUserEntitlmentList(ByVal members As List(Of Models.Gitlab.Member)) As List(Of Models.UserEntitlement)
            Return members.[Select](Function(x) New Models.UserEntitlement With {
                .EntitlementName = x.entitlementId,
                .UserName = x.username
            }).ToList()
        End Function
    End Module
End Namespace
