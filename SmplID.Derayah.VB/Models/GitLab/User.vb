Namespace Models.Gitlab
    Public Class User
        Public Property UserID As Integer
        Public Property UserName As String
        Public Property FirstName As String
        Public Property LastName As String
        Public Property DisplayName As String
        Public Property EmailID As String
        Public Property AccountStatus As String
    End Class

    Public Class Member
        Public Property id As Integer
        Public Property name As String
        Public Property username As String
        Public Property state As String
        Public Property access_level As Integer
        Public Property public_email As String
        Public Property entitlementName As String
    End Class

    Public Class PostMember
        Public Property user_id As String
        Public Property access_level As String
    End Class

    Public Class MembersComparer
        Implements IEqualityComparer(Of Member)

        Public Shadows Function Equals(ByVal x As Member, ByVal y As Member) As Boolean Implements IEqualityComparer(Of Member).Equals
            Return x.id = y.id
        End Function

        Public Shadows Function GetHashCode(ByVal obj As Member) As Integer Implements IEqualityComparer(Of Member).GetHashCode
            Return obj.id.GetHashCode()
        End Function
    End Class
End Namespace
