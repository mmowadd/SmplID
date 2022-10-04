Namespace Models.BOS
    Public Class User
        Public Property id As String
        Public Property email As String
        Public Property institutionId As Integer = 1
        Public Property language As String = "en"
        Public Property groups As List(Of Group)

    End Class

End Namespace
