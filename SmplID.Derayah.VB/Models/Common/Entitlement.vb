Namespace Models.Common
    Public Class Entitlement
        Public Property EntitlementID As String
        Public Property EntitlementName As String
        Public Property Description As String
        Public Property ResourceType As String

        Public Function ExtractId() As Integer
            Return Convert.ToInt32(EntitlementID.Split("-"c)(0))
        End Function
    End Class

    Public Class EntitlementComparer
        Implements IEqualityComparer(Of Entitlement)

        Public Shadows Function Equals(ByVal x As Entitlement, ByVal y As Entitlement) As Boolean Implements IEqualityComparer(Of Entitlement).Equals
            Return x.ExtractId() = y.ExtractId()
        End Function

        Public Shadows Function GetHashCode(ByVal obj As Entitlement) As Integer Implements IEqualityComparer(Of Entitlement).GetHashCode
            Return obj.ExtractId().GetHashCode()
        End Function
    End Class
End Namespace
