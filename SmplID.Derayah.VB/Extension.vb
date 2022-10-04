Imports System.Runtime.CompilerServices
Imports SmplID.Derayah.VB.Models.Common
Imports SmplID.Derayah.VB.Models.BOS
Imports SmplID.Derayah.VB.Models.Gitlab
Imports SmplID.Derayah.VB.Models.HCM

Friend Module Extensions

    <Extension()>
    Friend Function ToEntitlementList(groups As List(Of Models.Gitlab.Group), unique As Boolean) As List(Of Entitlement)
        Dim entitlements As New List(Of Entitlement)()

        If Not unique Then
            For Each group In groups
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-10",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Guest"
                })
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-20",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Reporter"
                })
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-30",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Developer"
                })
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = $"{group.id}-40",
                    .Description = group.description,
                    .EntitlementName = $"{group.full_name}-Maintainer"
                })
                entitlements.Add(New Entitlement With {
              .ResourceType = "Group",
              .EntitlementID = $"{group.id}-50",
              .Description = group.description,
              .EntitlementName = $"{group.full_name}-Owner"
                 })
            Next
        Else
            For Each group In groups
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Group",
                    .EntitlementID = group.id,
                    .Description = group.description,
                    .EntitlementName = group.full_name
                })
            Next
        End If


        Return entitlements
    End Function

    <Extension()>
    Friend Function ToEntitlementList(projects As List(Of Project), unique As Boolean) As List(Of Entitlement)
        Dim entitlements As New List(Of Entitlement)()

        If Not unique Then
            For Each project In projects
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-10",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Guest"
                })
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-20",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Reporter"
                })
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-30",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Developer"
                })
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = $"{project.id}-40",
                    .Description = project.description,
                    .EntitlementName = $"{project.name_with_namespace}-Maintainer"
                })
                entitlements.Add(New Entitlement With {
                  .ResourceType = "Project",
                  .EntitlementID = $"{project.id}-50",
                  .Description = project.description,
                  .EntitlementName = $"{project.name_with_namespace}-Owner"
                })
            Next
        Else
            For Each project In projects
                entitlements.Add(New Entitlement With {
                    .ResourceType = "Project",
                    .EntitlementID = project.id,
                    .Description = project.description,
                    .EntitlementName = project.name_with_namespace
                })
            Next
        End If

        Return entitlements
    End Function

    <Extension()>
    Friend Function ToUser(member As Member) As Models.Gitlab.User
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
    Friend Function ToUserEntitlmentList(members As List(Of Member)) As List(Of UserEntitlement)
        Return members.[Select](Function(x) New UserEntitlement With {
            .EntitlementName = x.entitlementName,
            .UserName = x.username
        }).ToList()
    End Function

    <Extension()>
    Friend Function ToUsersInRolesList(roles As List(Of Role)) As List(Of UsersInRoles)
        Dim usersInRoles As New List(Of UsersInRoles)
        For Each role In roles
            If role.members IsNot Nothing Then
                For Each member In role.members
                    usersInRoles.Add(New UsersInRoles With {.roleId = role.id, .userId = member.value})
                Next
            End If
        Next
        Return usersInRoles
    End Function

    <Extension()>
    Friend Function ToEmployeesList(dtos As List(Of EmployeeDto)) As List(Of Employee)
        Dim employees As New List(Of Employee)
        For Each x In dtos
            Dim activeAssignment = x.assignments.FirstOrDefault(Function(t) t.AssignmentStatus = "ACTIVE")
            Dim emp As New Employee With {
            .Salutation = x.Salutation,
            .FirstName = x.FirstName,
            .MiddleName = x.MiddleName,
            .LastName = x.LastName,
            .DisplayName = x.DisplayName,
            .PersonNumber = x.PersonNumber,
            .WorkPhoneNumber = x.WorkPhoneNumber,
            .WorkMobilePhoneNumber = x.WorkMobilePhoneNumber,
            .HomePhoneNumber = x.HomePhoneNumber,
            .WorkEmail = x.WorkEmail,
            .City = x.City,
            .Country = x.City,
            .HireDate = x.HireDate,
            .TerminationDate = x.TerminationDate,
            .Gender = x.Gender,
            .PersonId = x.PersonId,
            .UserName = x.UserName,
            .WorkerType = x.WorkerType,
            .LocationId = activeAssignment?.LocationId,
            .PositionId = activeAssignment?.PositionId,
            .ManagerId = activeAssignment?.ManagerId,
            .DepartmentId = activeAssignment?.DepartmentId
            }
            employees.Add(emp)
        Next
        Return employees
    End Function
    <Extension()>
    Friend Function ToAccessLevelName(accessLevel As Integer) As String
        Dim accessLevelName As String
        Select Case accessLevel
            Case 10
                accessLevelName = "Guest"
            Case 20
                accessLevelName = "Reporter"
            Case 30
                accessLevelName = "Developer"
            Case 40
                accessLevelName = "Maintainer"
            Case 50
                accessLevelName = "Owner"
            Case Else
                accessLevelName = accessLevel
        End Select
        Return accessLevelName
    End Function

    <Extension()>
    Friend Function ReverseGetUntilOrEmpty(text As String, Optional stopAt As String = "/") As String
        If Not String.IsNullOrWhiteSpace(text) Then
            Dim charLocation As Integer = text.LastIndexOf(stopAt, StringComparison.Ordinal)

            If charLocation > 0 Then
                Return text.Substring(charLocation + 1)
            End If
        End If

        Return String.Empty
    End Function




    <Extension()>
    Friend Function ToEntitlementList(groups As List(Of Models.BOS.Group)) As List(Of Entitlement)
        Return groups.[Select](Function(x) New Entitlement With {
            .EntitlementName = x.name,
            .EntitlementID = x.id,
            .ResourceType = "Group"
        }).ToList()
    End Function

End Module
